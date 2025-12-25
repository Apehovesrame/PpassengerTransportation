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

        // Добавили параметр idToSelect (по умолчанию null)
        private void LoadRoutes(int? idToSelect = null)
        {
            string sql = @"
                SELECT 
                    route_id, 
                    route_number AS ""Номер маршрута"", 
                    departure_point || ' - ' || destination_point AS ""Направление"" 
                FROM Routes 
                ORDER BY route_number";

            dgvRoutes.DataSource = Database.ExecuteQuery(sql);

            if (dgvRoutes.Columns["route_id"] != null)
                dgvRoutes.Columns["route_id"].Visible = false;

            if (idToSelect.HasValue && dgvRoutes.Rows.Count > 0)
            {
                dgvRoutes.ClearSelection(); 

                foreach (DataGridViewRow row in dgvRoutes.Rows)
                {
                    if (Convert.ToInt32(row.Cells["route_id"].Value) == idToSelect.Value)
                    {
                        row.Selected = true; 
                        dgvRoutes.CurrentCell = row.Cells[1]; 
                        return; 
                    }
                }
            }
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
        private void UpdateRouteHeader(int routeId)
        {
            try
            {
                string sqlStart = @"
                    SELECT s.name 
                    FROM Routes_Stops rs 
                    JOIN Stops s ON rs.stop_id = s.stop_id 
                    WHERE rs.route_id = @id 
                    ORDER BY rs.stop_order ASC LIMIT 1";

                object startObj = Database.ExecuteQuery(sqlStart, new NpgsqlParameter("@id", routeId)).Rows.Count > 0
                                  ? Database.ExecuteQuery(sqlStart, new NpgsqlParameter("@id", routeId)).Rows[0][0]
                                  : null;

                string sqlEnd = @"
                    SELECT s.name, rs.arrival_time_from_start 
                    FROM Routes_Stops rs 
                    JOIN Stops s ON rs.stop_id = s.stop_id 
                    WHERE rs.route_id = @id 
                    ORDER BY rs.stop_order DESC LIMIT 1";

                DataTable dtEnd = Database.ExecuteQuery(sqlEnd, new NpgsqlParameter("@id", routeId));

                if (startObj != null && dtEnd.Rows.Count > 0)
                {
                    string startName = startObj.ToString();
                    string endName = dtEnd.Rows[0]["name"].ToString();
                    int maxTime = Convert.ToInt32(dtEnd.Rows[0]["arrival_time_from_start"]);

                    string sqlUpdate = @"
                        UPDATE Routes 
                        SET departure_point = @dep, 
                            destination_point = @dest,
                            duration_minutes = @dur
                        WHERE route_id = @id";

                    Database.ExecuteNonQuery(sqlUpdate,
                        new NpgsqlParameter("@dep", startName),
                        new NpgsqlParameter("@dest", endName),
                        new NpgsqlParameter("@dur", maxTime),
                        new NpgsqlParameter("@id", routeId));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка обновления заголовка маршрута: " + ex.Message);
            }
        }
        private void btnAddStop_Click(object sender, EventArgs e)
        {
            if (dgvRoutes.SelectedRows.Count == 0) return;
            int routeId = Convert.ToInt32(dgvRoutes.SelectedRows[0].Cells["route_id"].Value);

            RouteStopEditForm form = new RouteStopEditForm(routeId);
            if (form.ShowDialog() == DialogResult.OK)
            {
                UpdateRouteHeader(routeId); 
                LoadRouteStops(routeId);
                LoadRoutes(routeId); 
            }
        }

        private void btnDelStop_Click(object sender, EventArgs e)
        {
            if (dgvRoutes.SelectedRows.Count == 0 || dgvStops.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите маршрут и остановку для удаления.");
                return;
            }

            int routeId = Convert.ToInt32(dgvRoutes.SelectedRows[0].Cells["route_id"].Value);
            int stopId = Convert.ToInt32(dgvStops.SelectedRows[0].Cells["stop_id"].Value);
            string stopName = dgvStops.SelectedRows[0].Cells["Остановка"].Value.ToString();

            int deletedOrder = Convert.ToInt32(dgvStops.SelectedRows[0].Cells["№"].Value);

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
                MessageBox.Show($"Нельзя удалить остановку \"{stopName}\", так как до неё уже продано {ticketsCount} билетов!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show($"Удалить остановку \"{stopName}\" (№{deletedOrder})?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string sqlDel = "DELETE FROM Routes_Stops WHERE route_id = @rid AND stop_id = @sid";
                    Database.ExecuteNonQuery(sqlDel, new NpgsqlParameter("@rid", routeId), new NpgsqlParameter("@sid", stopId));
                    string sqlShift = "UPDATE Routes_Stops SET stop_order = stop_order - 1 WHERE route_id = @rid AND stop_order > @ord";
                    Database.ExecuteNonQuery(sqlShift, new NpgsqlParameter("@rid", routeId), new NpgsqlParameter("@ord", deletedOrder));

                    string sqlResetStart = "UPDATE Routes_Stops SET arrival_time_from_start = 0, price_from_start = 0 WHERE route_id = @rid AND stop_order = 1";
                    Database.ExecuteNonQuery(sqlResetStart, new NpgsqlParameter("@rid", routeId));

                    UpdateRouteHeader(routeId);

                    LoadRoutes(routeId);
                    LoadRouteStops(routeId);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
        private void btnEditRoute_Click(object sender, EventArgs e)
        {
            if (dgvRoutes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите маршрут для редактирования.");
                return;
            }

            int routeId = Convert.ToInt32(dgvRoutes.SelectedRows[0].Cells["route_id"].Value);

            RouteAddForm form = new RouteAddForm(routeId);

            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadRoutes(routeId); 
                LoadRouteStops(routeId); 
            }
        }
        private void btnAddRoute_Click(object sender, EventArgs e)
        {
            RouteAddForm form = new RouteAddForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadRoutes(form.CreatedRouteId);
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
                    Database.ExecuteNonQuery($"DELETE FROM Routes WHERE route_id = {routeId}");

                    MessageBox.Show("Маршрут успешно удален.", "Успех");
                    LoadRoutes();
                    dgvStops.DataSource = null;
                }
                catch (PostgresException ex) when (ex.SqlState == "23503")
                {
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
                    MessageBox.Show("Произошла ошибка: " + ex.Message);
                }
            }
        }
    }
}