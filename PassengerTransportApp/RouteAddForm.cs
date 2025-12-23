using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace PassengerTransportApp
{
    public partial class RouteAddForm : Form
    {
        public RouteAddForm()
        {
            InitializeComponent();
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
                // 1. Создаем маршрут (Заголовок)
                string sqlRoute = @"INSERT INTO Routes (route_number, departure_point, destination_point, duration_minutes)
                                    VALUES (@num, @dep, @dest, @dur) RETURNING route_id";

                int newRouteId = (int)Database.ExecuteQuery(sqlRoute,
                    new NpgsqlParameter("@num", txtNumber.Text.Trim()),
                    new NpgsqlParameter("@dep", txtFrom.Text.Trim()),
                    new NpgsqlParameter("@dest", txtTo.Text.Trim()),
                    new NpgsqlParameter("@dur", (int)numDuration.Value)).Rows[0][0];

                // 2. Получаем (или создаем) ID для начальной и конечной остановки
                int startStopId = GetOrCreateStop(txtFrom.Text.Trim());
                int endStopId = GetOrCreateStop(txtTo.Text.Trim());

                // 3. Добавляем НАЧАЛЬНУЮ остановку (Порядок 1, Цена 0, Время 0)
                Database.ExecuteNonQuery(
                    "INSERT INTO Routes_Stops (route_id, stop_id, stop_order, arrival_time_from_start, price_from_start) VALUES (@rid, @sid, 1, 0, 0)",
                    new NpgsqlParameter("@rid", newRouteId),
                    new NpgsqlParameter("@sid", startStopId));

                // 4. Добавляем КОНЕЧНУЮ остановку (Порядок 2, Цена = полная, Время = полное)
                Database.ExecuteNonQuery(
                    "INSERT INTO Routes_Stops (route_id, stop_id, stop_order, arrival_time_from_start, price_from_start) VALUES (@rid, @sid, 2, @time, @price)",
                    new NpgsqlParameter("@rid", newRouteId),
                    new NpgsqlParameter("@sid", endStopId),
                    new NpgsqlParameter("@time", (int)numDuration.Value),
                    new NpgsqlParameter("@price", numPrice.Value));

                MessageBox.Show("Маршрут успешно создан!\nАвтоматически добавлены начальная и конечная остановки.", "Успех");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        // Вспомогательный метод: Найти ID остановки по имени, или создать новую
        private int GetOrCreateStop(string name)
        {
            // 1. Ищем
            string sqlSearch = "SELECT stop_id FROM Stops WHERE name = @n";
            DataTable dt = Database.ExecuteQuery(sqlSearch, new NpgsqlParameter("@n", name));

            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            else
            {
                // 2. Создаем
                string sqlInsert = "INSERT INTO Stops (name) VALUES (@n) RETURNING stop_id";
                return (int)Database.ExecuteQuery(sqlInsert, new NpgsqlParameter("@n", name)).Rows[0][0];
            }
        }
    }
}