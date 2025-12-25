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

            numOrder.Minimum = 1;

            numOrder.ValueChanged += NumOrder_ValueChanged;
        }

        private void RouteStopEditForm_Load(object sender, EventArgs e)
        {
            DataTable dt = Database.ExecuteQuery("SELECT stop_id, name FROM Stops ORDER BY name");
            cmbStops.DataSource = dt;
            cmbStops.DisplayMember = "name";
            cmbStops.ValueMember = "stop_id";

            CheckStartStopConstraint();
        }

        private void NumOrder_ValueChanged(object sender, EventArgs e)
        {
            CheckStartStopConstraint();
        }

        private void CheckStartStopConstraint()
        {
            if (numOrder.Value == 1)
            {
                numTime.Value = 0;
                numPrice.Value = 0;
                numTime.Enabled = false;
                numPrice.Enabled = false;
            }
            else
            {
                numTime.Enabled = true;
                numPrice.Enabled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int newOrder = (int)numOrder.Value;
                int time = (int)numTime.Value;
                decimal price = numPrice.Value;

                if (newOrder < 1)
                {
                    MessageBox.Show("Порядковый номер не может быть меньше 1!", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (newOrder == 1)
                {
                    time = 0;
                    price = 0;
                }

                string sqlRouteInfo = $@"
                    SELECT 
                        duration_minutes,
                        (SELECT MAX(price_from_start) FROM Routes_Stops WHERE route_id = {_routeId}) as max_price,
                        (SELECT MAX(stop_order) FROM Routes_Stops WHERE route_id = {_routeId}) as max_order
                    FROM Routes WHERE route_id = {_routeId}";

                DataTable dtInfo = Database.ExecuteQuery(sqlRouteInfo);

                int totalDuration = Convert.ToInt32(dtInfo.Rows[0]["duration_minutes"]);
                int currentMaxOrder = dtInfo.Rows[0]["max_order"] != DBNull.Value ? Convert.ToInt32(dtInfo.Rows[0]["max_order"]) : 0;
                decimal maxPrice = dtInfo.Rows[0]["max_price"] != DBNull.Value ? Convert.ToDecimal(dtInfo.Rows[0]["max_price"]) : 0;

                if (newOrder > currentMaxOrder + 1)
                {
                    MessageBox.Show($"Максимально возможный следующий номер: {currentMaxOrder + 1}.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (newOrder > 1 && time <= 0)
                {
                    MessageBox.Show("Время от старта должно быть больше 0!", "Ошибка времени", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (newOrder > 1 && time > totalDuration)
                {
                    MessageBox.Show($"Время ({time} мин) больше общей длительности ({totalDuration} мин)!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (newOrder > 1 && price <= 0)
                {
                    MessageBox.Show("Цена билета должна быть больше 0!", "Ошибка цены", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (newOrder <= currentMaxOrder && price > maxPrice && newOrder > 1)
                {
                    MessageBox.Show($"Цена промежуточной остановки не может быть выше конечной!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int stopId;
                if (cmbStops.SelectedValue != null && cmbStops.SelectedItem is DataRowView)
                {
                    stopId = Convert.ToInt32(cmbStops.SelectedValue);
                }
                else
                {
                    string newName = cmbStops.Text.Trim();
                    if (string.IsNullOrEmpty(newName)) { MessageBox.Show("Укажите название!"); return; }

                    string checkName = $"SELECT stop_id FROM Stops WHERE name = '{newName}'";
                    DataTable dtName = Database.ExecuteQuery(checkName);
                    if (dtName.Rows.Count > 0)
                        stopId = (int)dtName.Rows[0][0];
                    else
                        stopId = (int)Database.ExecuteQuery($"INSERT INTO Stops (name) VALUES ('{newName}') RETURNING stop_id").Rows[0][0];
                }

                string sqlCheckDuplicate = $"SELECT COUNT(*) FROM Routes_Stops WHERE route_id = {_routeId} AND stop_id = {stopId}";
                if ((long)Database.ExecuteQuery(sqlCheckDuplicate).Rows[0][0] > 0)
                {
                    MessageBox.Show("Эта остановка уже есть в маршруте!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (newOrder == 1)
                {
                    if (MessageBox.Show("Вы добавляете новое НАЧАЛО маршрута.\nСтарая начальная остановка будет удалена.\nПродолжить?",
                        "Замена начальной станции", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Database.ExecuteNonQuery($"DELETE FROM Routes_Stops WHERE route_id = {_routeId} AND stop_order = 1");
                        time = 0;
                        price = 0;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    string sqlCheckBusy = $"SELECT COUNT(*) FROM Routes_Stops WHERE route_id = {_routeId} AND stop_order = {newOrder}";
                    if ((long)Database.ExecuteQuery(sqlCheckBusy).Rows[0][0] > 0)
                    {
                        Database.ExecuteNonQuery($"UPDATE Routes_Stops SET stop_order = stop_order + 1 WHERE route_id = {_routeId} AND stop_order >= {newOrder}");
                    }
                }

                string sqlInsert = @"INSERT INTO Routes_Stops (route_id, stop_id, stop_order, arrival_time_from_start, price_from_start)
                                     VALUES (@rid, @sid, @ord, @time, @price)";

                Database.ExecuteNonQuery(sqlInsert,
                    new NpgsqlParameter("@rid", _routeId),
                    new NpgsqlParameter("@sid", stopId),
                    new NpgsqlParameter("@ord", newOrder),
                    new NpgsqlParameter("@time", time),
                    new NpgsqlParameter("@price", price));

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