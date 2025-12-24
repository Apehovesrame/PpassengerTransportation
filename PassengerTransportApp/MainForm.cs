using Npgsql;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PassengerTransportApp
{
    public partial class MainForm : Form
    {
        private int _currentUserId;
        private string _userRole;

        private bool _isLogout = false;

        public MainForm(int userId, string role, string fio)
        {
            InitializeComponent();
            _currentUserId = userId;
            _userRole = role;

            statusLabelUser.Text = $"Текущий пользователь: {fio} ({role})";

            ApplyRolePermissions();
            LoadTrips();
        }

        public MainForm() : this(0, "Гость", "Неизвестно") { }

        // ОБРАБОТКА КНОПКИ ВЫХОД
        private void btnLogout_Click(object sender, EventArgs e)
        {
            _isLogout = true; 
            this.Close();  
        }

        // ОБРАБОТКА ЗАКРЫТИЯ ФОРМЫ
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_isLogout)
            {

            }
            else
            {
                Application.Exit();
            }
        }

        private void ApplyRolePermissions()
        {
            // Скрываем все административные кнопки
            btnBuses.Visible = false;
            btnReports.Visible = false;
            btnManageDrivers.Visible = false;
            btnAddTrip.Visible = false;
            btnSellTicket.Visible = false;
            btnEditTrip.Visible = false;
            btnDeleteTrip.Visible = false;
            btnShowPassengers.Visible = true;
            btnRouteManager.Visible = false;
            btnAddUser.Visible = false;

            // Скрываем элементы "Мягкого удаления"
            chkShowDeleted.Visible = false;
            btnRestoreTrip.Visible = false;
            btnHardDelete.Visible = false;
            btnClearArchive.Visible = false;

            // Включаем в зависимости от роли
            switch (_userRole)
            {
                case "Администратор":
                    btnBuses.Visible = true;
                    btnReports.Visible = true;
                    btnManageDrivers.Visible = true;
                    btnAddTrip.Visible = true;
                    btnSellTicket.Visible = true;
                    btnEditTrip.Visible = true;
                    btnDeleteTrip.Visible = true;
                    chkShowDeleted.Visible = true;
                    btnShowPassengers.Visible = true;
                    btnRouteManager.Visible = true;
                    btnAddUser.Visible = true;
                    break;

                case "Кассир":
                    btnSellTicket.Visible = true;
                    break;

                case "Диспетчер":
                    btnBuses.Visible = true;
                    btnManageDrivers.Visible = true;
                    btnAddTrip.Visible = true;
                    btnEditTrip.Visible = true;
                    btnDeleteTrip.Visible = true;
                    chkShowDeleted.Visible = true;
                    btnShowPassengers.Visible = true;
                    btnRouteManager.Visible = true;
                    break;
            }
        }

        private string GetDeclension(long number, string nominativ, string genetiv, string plural)
        {
            number = Math.Abs(number) % 100;
            var n1 = number % 10;
            if (number > 10 && number < 20) return plural;
            if (n1 > 1 && n1 < 5) return genetiv;
            if (n1 == 1) return nominativ;
            return plural;
        }

        private void LoadTrips()
        {
            // Фильтр: показываем активные (false) или удаленные (true)
            string deletedFilter = chkShowDeleted.Checked ? "TRUE" : "FALSE";

            string sql = $@" 
                SELECT 
                    t.trip_id,
                    r.route_number AS ""Номер маршрута"",
                    r.departure_point || ' - ' || r.destination_point AS ""Маршрут"",
                    
                    -- ОТПРАВЛЕНИЕ
                    TO_CHAR(t.departure_datetime, 'DD.MM.YYYY HH24:MI') AS ""Отправление"",
                    
                    -- ПРИБЫТИЕ (Добавлено)
                    TO_CHAR(t.arrival_datetime, 'DD.MM.YYYY HH24:MI') AS ""Прибытие"",

                    b.model AS ""Автобус"",
                    b.license_plate AS ""Гос. номер"",
                    
                    -- ВОДИТЕЛИ (С исправленными инициалами И.О.)
                    (
                        SELECT STRING_AGG(
                            d.last_name || ' ' || 
                            SUBSTR(d.first_name, 1, 1) || '.' || 
                            CASE WHEN d.middle_name IS NOT NULL AND d.middle_name != '' 
                                 THEN ' ' || SUBSTR(d.middle_name, 1, 1) || '.' 
                                 ELSE '' 
                            END, 
                            ', '
                        )
                        FROM Trips_Drivers td
                        JOIN Drivers d ON td.driver_id = d.driver_id
                        WHERE td.trip_id = t.trip_id
                    ) AS ""Водители"",

                    b.seat_capacity AS ""Всего мест"",
                    (b.seat_capacity - (SELECT COUNT(*) FROM Tickets WHERE trip_id = t.trip_id)) AS ""Свободно""
                FROM Trips t
                JOIN Routes r ON t.route_id = r.route_id
                JOIN Buses b ON t.bus_id = b.bus_id
                WHERE t.is_deleted = {deletedFilter}
                ORDER BY t.departure_datetime";

            DataTable dt = Database.ExecuteQuery(sql);
            dgvTrips.DataSource = dt;

            if (dgvTrips.Columns["trip_id"] != null)
                dgvTrips.Columns["trip_id"].Visible = false;

            if (chkShowDeleted.Checked)
            {
                dgvTrips.BackgroundColor = Color.LightGray;
                btnRestoreTrip.Visible = true;

                bool canManage = (_userRole == "Администратор" || _userRole == "Диспетчер");
                btnHardDelete.Visible = canManage;
                btnClearArchive.Visible = canManage;

                btnDeleteTrip.Visible = false;
                btnEditTrip.Visible = false;
                btnSellTicket.Visible = false;
            }
            else
            {
                dgvTrips.BackgroundColor = Color.White;
                btnRestoreTrip.Visible = false;
                btnHardDelete.Visible = false;
                btnClearArchive.Visible = false;

                bool canManage = (_userRole == "Администратор" || _userRole == "Диспетчер");
                btnDeleteTrip.Visible = canManage;
                btnEditTrip.Visible = canManage;
                btnSellTicket.Visible = true;
            }
        }
        private void btnDeleteTrip_Click(object sender, EventArgs e)
        {
            if (dgvTrips.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите рейс!", "Внимание");
                return;
            }

            int tripId = Convert.ToInt32(dgvTrips.SelectedRows[0].Cells["trip_id"].Value);

            string sqlCheck = $"SELECT COUNT(*) FROM Tickets WHERE trip_id = {tripId}";
            long soldCount = (long)Database.ExecuteQuery(sqlCheck).Rows[0][0];

            if (soldCount > 0)
            {
                string word = GetDeclension(soldCount, "билет", "билета", "билетов");

                string message = $"На этот рейс уже продано {soldCount} {word}!\n\n" +
                                 "Вы хотите оформить ПОЛНЫЙ ВОЗВРАТ средств пассажирам и отменить рейс?\n" +
                                 "(Билеты будут удалены из базы, выручка аннулирована)";

                if (MessageBox.Show(message, "Массовый возврат", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        Database.ExecuteNonQuery($"DELETE FROM Tickets WHERE trip_id = {tripId}");
                        Database.ExecuteNonQuery($"UPDATE Trips SET is_deleted = TRUE WHERE trip_id = {tripId}");
                        MessageBox.Show($"Успешно возвращено {soldCount} билетов. Рейс отменен.", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadTrips();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при возврате: " + ex.Message);
                    }
                }
            }
            else
            {
                if (MessageBox.Show("Переместить рейс в архив (отменить)?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Database.ExecuteNonQuery($"UPDATE Trips SET is_deleted = TRUE WHERE trip_id = {tripId}");
                    LoadTrips();
                }
            }
        }

        private void btnRestoreTrip_Click(object sender, EventArgs e)
        {
            if (dgvTrips.SelectedRows.Count == 0) return;

            int tripId = Convert.ToInt32(dgvTrips.SelectedRows[0].Cells["trip_id"].Value);

            if (MessageBox.Show("Восстановить этот рейс в расписание?", "Восстановление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Database.ExecuteNonQuery($"UPDATE Trips SET is_deleted = FALSE WHERE trip_id = {tripId}");
                chkShowDeleted.Checked = false;
                LoadTrips();
            }
        }

        private void chkShowDeleted_CheckedChanged(object sender, EventArgs e)
        {
            LoadTrips();
        }

        private void btnEditTrip_Click(object sender, EventArgs e)
        {
            if (dgvTrips.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите рейс для редактирования!", "Внимание");
                return;
            }

            int tripId = Convert.ToInt32(dgvTrips.SelectedRows[0].Cells["trip_id"].Value);
            TripEditForm editForm = new TripEditForm(_currentUserId, tripId);

            if (editForm.ShowDialog() == DialogResult.OK)
            {
                LoadTrips();
            }
        }
        private void btnShowPassengers_Click(object sender, EventArgs e)
        {
            if (dgvTrips.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите рейс!", "Внимание");
                return;
            }

            int tripId = Convert.ToInt32(dgvTrips.SelectedRows[0].Cells["trip_id"].Value);
            PassengerListForm form = new PassengerListForm(tripId);
            form.ShowDialog();
        }
        private void btnRouteManager_Click(object sender, EventArgs e)
        {
            RouteManagerForm form = new RouteManagerForm();
            form.ShowDialog();
        }
        private void btnAddTrip_Click(object sender, EventArgs e)
        {
            TripEditForm form = new TripEditForm(_currentUserId);
            if (form.ShowDialog() == DialogResult.OK) LoadTrips();
        }

        private void btnSellTicket_Click(object sender, EventArgs e)
        {
            if (dgvTrips.SelectedRows.Count > 0)
            {
                int selectedTripId = Convert.ToInt32(dgvTrips.SelectedRows[0].Cells["trip_id"].Value);
                TicketSaleForm saleForm = new TicketSaleForm(selectedTripId, _currentUserId);
                if (saleForm.ShowDialog() == DialogResult.OK) LoadTrips();
            }
            else
            {
                MessageBox.Show("Выберите рейс из списка!", "Внимание");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e) => LoadTrips();

        private void btnReports_Click(object sender, EventArgs e)
        {
            ReportsForm reportForm = new ReportsForm();
            reportForm.ShowDialog();
        }

        private void btnBuses_Click(object sender, EventArgs e)
        {
            BusManagerForm busForm = new BusManagerForm();
            busForm.ShowDialog();
        }

        private void btnManageDrivers_Click(object sender, EventArgs e)
        {
            DriverManagerForm form = new DriverManagerForm();
            form.ShowDialog();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            UserManagerForm form = new UserManagerForm();
            form.ShowDialog();
        }

        private void btnHardDelete_Click(object sender, EventArgs e)
        {
            if (dgvTrips.SelectedRows.Count == 0) return;

            int tripId = Convert.ToInt32(dgvTrips.SelectedRows[0].Cells["trip_id"].Value);

            if (MessageBox.Show("Вы уверены, что хотите УДАЛИТЬ НАВСЕГДА эту запись?\nВосстановление будет невозможно!",
                "Полное удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
            {
                try
                {
                    Database.ExecuteNonQuery($"DELETE FROM Tickets WHERE trip_id = {tripId}");
                    Database.ExecuteNonQuery($"DELETE FROM Trips_Drivers WHERE trip_id = {tripId}");
                    Database.ExecuteNonQuery($"DELETE FROM Trips WHERE trip_id = {tripId}");
                    LoadTrips();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка удаления: " + ex.Message);
                }
            }
        }

        private void btnClearArchive_Click(object sender, EventArgs e)
        {
            if (dgvTrips.Rows.Count == 0)
            {
                MessageBox.Show("Архив уже пуст.");
                return;
            }

            if (MessageBox.Show("ВНИМАНИЕ! Вы собираетесь полностью очистить архив.\nВсе удаленные рейсы будут уничтожены безвозвратно.\nПродолжить?",
                "Очистка архива", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
            {
                try
                {
                    Database.ExecuteNonQuery(@"DELETE FROM Tickets WHERE trip_id IN (SELECT trip_id FROM Trips WHERE is_deleted = TRUE)");
                    Database.ExecuteNonQuery(@"DELETE FROM Trips_Drivers WHERE trip_id IN (SELECT trip_id FROM Trips WHERE is_deleted = TRUE)");
                    Database.ExecuteNonQuery("DELETE FROM Trips WHERE is_deleted = TRUE");

                    MessageBox.Show("Архив успешно очищен.", "Готово");
                    LoadTrips();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка очистки: " + ex.Message);
                }
            }
        }

        private void dgvTrips_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}