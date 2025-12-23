using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace PassengerTransportApp
{
    public partial class RouteManagerForm : Form
    {
        public RouteManagerForm()
        {
            InitializeComponent();
        }

        private void RouteManagerForm_Load(object sender, EventArgs e)
        {
            LoadRoutes();
        }

        private void LoadRoutes()
        {
            string sql = @"
                SELECT 
                    route_id, 
                    route_number AS ""Номер маршрута"", 
                    departure_point || ' - ' || destination_point AS ""Направление"" 
                FROM Routes 
                ORDER BY route_number";

            dgvRoutes.DataSource = Database.ExecuteQuery(sql);

            // Скрываем ID
            if (dgvRoutes.Columns["route_id"] != null)
                dgvRoutes.Columns["route_id"].Visible = false;
        }

        private void dgvRoutes_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRoutes.SelectedRows.Count > 0)
            {
                int routeId = Convert.ToInt32(dgvRoutes.SelectedRows[0].Cells["route_id"].Value);
                LoadRouteStops(routeId);
            }
            else
            {
                dgvStops.DataSource = null;
            }
        }

        private void LoadRouteStops(int routeId)
        {
            // Показываем: Порядок, Название, Время, Цену
            string sql = @"
                SELECT 
                    rs.stop_id, -- скрыто
                    rs.stop_order AS ""№"", 
                    s.name AS ""Остановка"", 
                    rs.arrival_time_from_start AS ""Минут от старта"", 
                    rs.price_from_start AS ""Цена (руб)""
                FROM Routes_Stops rs
                JOIN Stops s ON rs.stop_id = s.stop_id
                WHERE rs.route_id = @rid
                ORDER BY rs.stop_order";

            DataTable dt = Database.ExecuteQuery(sql, new NpgsqlParameter("@rid", routeId));
            dgvStops.DataSource = dt;
            if (dgvStops.Columns["stop_id"] != null) dgvStops.Columns["stop_id"].Visible = false;
        }

        private void btnAddStop_Click(object sender, EventArgs e)
        {
            if (dgvRoutes.SelectedRows.Count == 0) return;
            int routeId = Convert.ToInt32(dgvRoutes.SelectedRows[0].Cells["route_id"].Value);

            RouteStopEditForm form = new RouteStopEditForm(routeId);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadRouteStops(routeId);
            }
        }

        private void btnDelStop_Click(object sender, EventArgs e)
        {
            // 1. Проверяем, выбраны ли строки в обеих таблицах
            if (dgvRoutes.SelectedRows.Count == 0 || dgvStops.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите маршрут и остановку, которую хотите удалить.");
                return;
            }

            // Получаем ID
            int routeId = Convert.ToInt32(dgvRoutes.SelectedRows[0].Cells["route_id"].Value);
            int stopId = Convert.ToInt32(dgvStops.SelectedRows[0].Cells["stop_id"].Value);
            string stopName = dgvStops.SelectedRows[0].Cells["Остановка"].Value.ToString();

            // 2. ВАЖНАЯ ПРОВЕРКА: Есть ли проданные билеты до этой остановки?
            // Мы ищем билеты (Tickets), которые куплены на рейсы (Trips) этого маршрута (@rid)
            // и у которых пункт назначения (@sid) — это удаляемая остановка.
            string sqlCheck = @"
                SELECT COUNT(*) 
                FROM Tickets t
                JOIN Trips tr ON t.trip_id = tr.trip_id
                WHERE tr.route_id = @rid AND t.destination_stop_id = @sid";

            long ticketsCount = (long)Database.ExecuteQuery(sqlCheck,
                new NpgsqlParameter("@rid", routeId),
                new NpgsqlParameter("@sid", stopId)).Rows[0][0];

            if (ticketsCount > 0)
            {
                MessageBox.Show($"Нельзя удалить остановку \"{stopName}\", так как до неё уже продано {ticketsCount} билетов!\n\nСначала нужно отменить соответствующие рейсы или вернуть билеты.", "Ошибка удаления", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3. Если билетов нет — удаляем
            if (MessageBox.Show($"Удалить остановку \"{stopName}\" из маршрута?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string sql = "DELETE FROM Routes_Stops WHERE route_id = @rid AND stop_id = @sid";
                    Database.ExecuteNonQuery(sql, new NpgsqlParameter("@rid", routeId), new NpgsqlParameter("@sid", stopId));

                    // Обновляем таблицу справа
                    LoadRouteStops(routeId);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }

        private void btnAddRoute_Click(object sender, EventArgs e)
        {
            RouteAddForm form = new RouteAddForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadRoutes(); // Обновляем список, чтобы увидеть новый маршрут
            }
        }

        private void btnDelRoute_Click(object sender, EventArgs e)
        {
            if (dgvRoutes.SelectedRows.Count == 0) return;

            int routeId = Convert.ToInt32(dgvRoutes.SelectedRows[0].Cells["route_id"].Value);
            string routeName = dgvRoutes.SelectedRows[0].Cells["Направление"].Value.ToString();

            // 1. Сначала проверяем АКТИВНЫЕ рейсы (как и раньше, это полезно)
            string sqlCheck = $"SELECT COUNT(*) FROM Trips WHERE route_id = {routeId} AND is_deleted = FALSE";
            long activeTrips = (long)Database.ExecuteQuery(sqlCheck).Rows[0][0];

            if (activeTrips > 0)
            {
                MessageBox.Show($"Нельзя удалить маршрут \"{routeName}\", так как по нему есть {activeTrips} запланированных рейсов в расписании.\nСначала отмените их в главном окне.",
                    "Ошибка: Есть активные рейсы", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show($"Удалить маршрут \"{routeName}\"?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    // Попытка удалить маршрут
                    Database.ExecuteNonQuery($"DELETE FROM Routes WHERE route_id = {routeId}");

                    // Если удаление прошло успешно (ошибки не было):
                    MessageBox.Show("Маршрут успешно удален.", "Успех");
                    LoadRoutes();
                    dgvStops.DataSource = null;
                }
                catch (PostgresException ex) when (ex.SqlState == "23503")
                {
                    // ПЕРЕХВАТ ОШИБКИ ВНЕШНЕГО КЛЮЧА
                    // Код 23503 означает "Foreign key violation" (есть связанные записи)

                    MessageBox.Show("Невозможно удалить маршрут, так как в Архиве (Корзине) остались старые рейсы, связанные с ним.\n\n" +
                                    "ЧТО НУЖНО СДЕЛАТЬ:\n" +
                                    "1. Закройте это окно.\n" +
                                    "2. В главном меню поставьте галочку 'Показать удаленные'.\n" +
                                    "3. Найдите старые рейсы этого маршрута и нажмите кнопку 'Удалить навсегда' (или очистите весь архив).\n\n" +
                                    "Только после полной очистки истории можно удалить сам маршрут.",
                                    "Ошибка: Связанные данные", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    // Любые другие ошибки
                    MessageBox.Show("Произошла ошибка: " + ex.Message);
                }
            }
        }
    }
}