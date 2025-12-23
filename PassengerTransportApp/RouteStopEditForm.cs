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
                int stopId;

                // Если пользователь выбрал из списка
                if (cmbStops.SelectedValue != null && cmbStops.SelectedItem is DataRowView)
                {
                    stopId = Convert.ToInt32(cmbStops.SelectedValue);
                }
                else
                {
                    // Если пользователь ввел НОВОЕ название руками
                    string newName = cmbStops.Text.Trim();
                    if (string.IsNullOrEmpty(newName)) { MessageBox.Show("Укажите название!"); return; }

                    // Создаем новую остановку в справочнике
                    string sqlNewStop = "INSERT INTO Stops (name) VALUES (@n) RETURNING stop_id";
                    stopId = (int)Database.ExecuteQuery(sqlNewStop, new NpgsqlParameter("@n", newName)).Rows[0][0];
                }

                // Добавляем связь в Routes_Stops
                string sqlLink = @"INSERT INTO Routes_Stops (route_id, stop_id, stop_order, arrival_time_from_start, price_from_start)
                                   VALUES (@rid, @sid, @ord, @time, @price)";

                Database.ExecuteNonQuery(sqlLink,
                    new NpgsqlParameter("@rid", _routeId),
                    new NpgsqlParameter("@sid", stopId),
                    new NpgsqlParameter("@ord", (int)numOrder.Value),
                    new NpgsqlParameter("@time", (int)numTime.Value),
                    new NpgsqlParameter("@price", numPrice.Value));

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