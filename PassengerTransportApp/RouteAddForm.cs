using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace PassengerTransportApp
{
    public partial class RouteAddForm : Form
    {
        private int? _routeId;

        public int CreatedRouteId { get; private set; }

        public RouteAddForm(int? routeId = null)
        {
            InitializeComponent();
            _routeId = routeId;

            if (_routeId.HasValue)
            {
                this.Text = "Редактирование маршрута";
                btnSave.Text = "Сохранить изменения";
                LoadRouteData();
            }
        }

        private void LoadRouteData()
        {
            // 1. Грузим данные из таблицы Routes
            string sql = $"SELECT route_number, departure_point, destination_point, duration_minutes FROM Routes WHERE route_id = {_routeId}";
            DataTable dt = Database.ExecuteQuery(sql);

            if (dt.Rows.Count > 0)
            {
                txtNumber.Text = dt.Rows[0]["route_number"].ToString();
                txtFrom.Text = dt.Rows[0]["departure_point"].ToString();
                txtTo.Text = dt.Rows[0]["destination_point"].ToString();
                numDuration.Value = Convert.ToDecimal(dt.Rows[0]["duration_minutes"]);
            }

            // 2. Узнаем текущую полную цену (цена последней остановки)
            string sqlPrice = $"SELECT MAX(price_from_start) FROM Routes_Stops WHERE route_id = {_routeId}";
            object priceObj = Database.ExecuteQuery(sqlPrice).Rows[0][0];

            if (priceObj != DBNull.Value)
            {
                numPrice.Value = Convert.ToDecimal(priceObj);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNumber.Text) ||
                string.IsNullOrWhiteSpace(txtFrom.Text) ||
                string.IsNullOrWhiteSpace(txtTo.Text))
            {
                MessageBox.Show("Заполните все текстовые поля!", "Внимание");
                return;
            }

            try
            {
                if (_routeId.HasValue)
                {
                    // === РЕЖИМ РЕДАКТИРОВАНИЯ (UPDATE) ===

                    // 1. Обновляем заголовок маршрута
                    string sqlUpdate = @"
                        UPDATE Routes 
                        SET route_number = @num, departure_point = @dep, destination_point = @dest, duration_minutes = @dur
                        WHERE route_id = @id";

                    Database.ExecuteNonQuery(sqlUpdate,
                        new NpgsqlParameter("@num", txtNumber.Text.Trim()),
                        new NpgsqlParameter("@dep", txtFrom.Text.Trim()),
                        new NpgsqlParameter("@dest", txtTo.Text.Trim()),
                        new NpgsqlParameter("@dur", (int)numDuration.Value),
                        new NpgsqlParameter("@id", _routeId));

                    string sqlUpdateLastStop = @"
                        UPDATE Routes_Stops 
                        SET price_from_start = @price, arrival_time_from_start = @time
                        WHERE route_id = @id 
                          AND stop_order = (SELECT MAX(stop_order) FROM Routes_Stops WHERE route_id = @id)";

                    Database.ExecuteNonQuery(sqlUpdateLastStop,
                        new NpgsqlParameter("@price", numPrice.Value),
                        new NpgsqlParameter("@time", (int)numDuration.Value),
                        new NpgsqlParameter("@id", _routeId));

                    MessageBox.Show("Маршрут обновлен!");
                }
                else
                {
                    string sqlRoute = @"INSERT INTO Routes (route_number, departure_point, destination_point, duration_minutes)
                                        VALUES (@num, @dep, @dest, @dur) RETURNING route_id";

                    int newRouteId = (int)Database.ExecuteQuery(sqlRoute,
                        new NpgsqlParameter("@num", txtNumber.Text.Trim()),
                        new NpgsqlParameter("@dep", txtFrom.Text.Trim()),
                        new NpgsqlParameter("@dest", txtTo.Text.Trim()),
                        new NpgsqlParameter("@dur", (int)numDuration.Value)).Rows[0][0];

                    int startStopId = GetOrCreateStop(txtFrom.Text.Trim());
                    int endStopId = GetOrCreateStop(txtTo.Text.Trim());

                    // Начало
                    Database.ExecuteNonQuery(
                        "INSERT INTO Routes_Stops (route_id, stop_id, stop_order, arrival_time_from_start, price_from_start) VALUES (@rid, @sid, 1, 0, 0)",
                        new NpgsqlParameter("@rid", newRouteId),
                        new NpgsqlParameter("@sid", startStopId));

                    // Конец
                    Database.ExecuteNonQuery(
                        "INSERT INTO Routes_Stops (route_id, stop_id, stop_order, arrival_time_from_start, price_from_start) VALUES (@rid, @sid, 2, @time, @price)",
                        new NpgsqlParameter("@rid", newRouteId),
                        new NpgsqlParameter("@sid", endStopId),
                        new NpgsqlParameter("@time", (int)numDuration.Value),
                        new NpgsqlParameter("@price", numPrice.Value));
                    CreatedRouteId = newRouteId;
                    MessageBox.Show("Маршрут создан!", "Успех");
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private int GetOrCreateStop(string name)
        {
            string sqlSearch = "SELECT stop_id FROM Stops WHERE name = @n";
            DataTable dt = Database.ExecuteQuery(sqlSearch, new NpgsqlParameter("@n", name));

            if (dt.Rows.Count > 0) return Convert.ToInt32(dt.Rows[0][0]);

            string sqlInsert = "INSERT INTO Stops (name) VALUES (@n) RETURNING stop_id";
            return (int)Database.ExecuteQuery(sqlInsert, new NpgsqlParameter("@n", name)).Rows[0][0];
        }
    }
}