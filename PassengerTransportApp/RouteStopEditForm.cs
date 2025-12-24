using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace PassengerTransportApp
{
    public partial class RouteStopEditForm : Form
    {
        private int _routeId;

        public RouteStopEditForm(int routeId)
        {
            InitializeComponent();
            _routeId = routeId;
        }

        private void RouteStopEditForm_Load(object sender, EventArgs e)
        {
            // Загружаем все возможные остановки
            DataTable dt = Database.ExecuteQuery("SELECT stop_id, name FROM Stops ORDER BY name");
            cmbStops.DataSource = dt;
            cmbStops.DisplayMember = "name";
            cmbStops.ValueMember = "stop_id";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int order = (int)numOrder.Value;
                int time = (int)numTime.Value;
                decimal price = numPrice.Value;

                // --- ПРОВЕРКА 1: Дублирование порядка ---
                string sqlCheckOrder = $"SELECT COUNT(*) FROM Routes_Stops WHERE route_id = {_routeId} AND stop_order = {order}";
                long countOrder = (long)Database.ExecuteQuery(sqlCheckOrder).Rows[0][0];

                if (countOrder > 0)
                {
                    MessageBox.Show($"Остановка с порядковым номером {order} уже существует!", "Ошибка порядка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Получаем параметры всего маршрута (Длительность и Макс. цену)
                // MAX(price_from_start) — это цена билета до конечной
                string sqlRouteInfo = $@"
                    SELECT duration_minutes, 
                           (SELECT MAX(price_from_start) FROM Routes_Stops WHERE route_id = {_routeId}) as max_price 
                    FROM Routes WHERE route_id = {_routeId}";

                DataTable dtInfo = Database.ExecuteQuery(sqlRouteInfo);
                int totalDuration = Convert.ToInt32(dtInfo.Rows[0]["duration_minutes"]);

                // Если маршрут пустой (маловероятно), считаем макс цену 0
                decimal maxPrice = dtInfo.Rows[0]["max_price"] != DBNull.Value
                                   ? Convert.ToDecimal(dtInfo.Rows[0]["max_price"])
                                   : 0;

                // --- ПРОВЕРКА 2: Время в пути ---
                if (time > totalDuration)
                {
                    MessageBox.Show($"Время прибытия ({time} мин) не может быть больше общей длительности маршрута ({totalDuration} мин)!", "Ошибка времени", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // --- ПРОВЕРКА 3: Стоимость (НОВОЕ) ---
                // Если это промежуточная остановка (время меньше общего), то и цена должна быть меньше полной
                if (price > maxPrice)
                {
                    MessageBox.Show($"Цена билета ({price} руб.) не может быть дороже, чем билет до конечной станции ({maxPrice} руб.)!", "Ошибка цены", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // --- СОХРАНЕНИЕ ---
                int stopId;

                if (cmbStops.SelectedValue != null && cmbStops.SelectedItem is DataRowView)
                {
                    stopId = Convert.ToInt32(cmbStops.SelectedValue);
                }
                else
                {
                    string newName = cmbStops.Text.Trim();
                    if (string.IsNullOrEmpty(newName)) { MessageBox.Show("Укажите название!"); return; }
                    string sqlNewStop = "INSERT INTO Stops (name) VALUES (@n) RETURNING stop_id";
                    stopId = (int)Database.ExecuteQuery(sqlNewStop, new NpgsqlParameter("@n", newName)).Rows[0][0];
                }

                string sqlLink = @"INSERT INTO Routes_Stops (route_id, stop_id, stop_order, arrival_time_from_start, price_from_start)
                                   VALUES (@rid, @sid, @ord, @time, @price)";

                Database.ExecuteNonQuery(sqlLink,
                    new NpgsqlParameter("@rid", _routeId),
                    new NpgsqlParameter("@sid", stopId),
                    new NpgsqlParameter("@ord", order),
                    new NpgsqlParameter("@time", time),
                    new NpgsqlParameter("@price", price));

                MessageBox.Show("Остановка добавлена!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
    }
}