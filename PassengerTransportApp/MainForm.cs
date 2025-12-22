using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace PassengerTransportApp
{
    public partial class MainForm : Form
    {
        // Храним данные о том, кто вошел
        private int _currentUserId;
        private string _userRole;

        // Конструктор теперь принимает данные пользователя
        public MainForm(int userId, string role, string fio)
        {
            InitializeComponent();
            _currentUserId = userId;
            _userRole = role;

            lblUserInfo.Text = $"Пользователь: {fio} ({role})";

            // Загружаем данные при запуске
            LoadTrips();
        }

        // Пустой конструктор (на всякий случай, чтобы дизайнер не ругался)
        public MainForm() : this(0, "Гость", "Неизвестно") { }

        private void LoadTrips()
        {
            // Сложный SQL запрос, который собирает данные из 4 таблиц
            // и считает свободные места (Вместимость - Проданные билеты)
            string sql = @"
                SELECT 
                    t.trip_id,
                    r.route_number AS ""Номер маршрута"",
                    r.departure_point || ' - ' || r.destination_point AS ""Маршрут"",
                    TO_CHAR(t.departure_datetime, 'DD.MM.YYYY HH24:MI') AS ""Отправление"",
                    b.model AS ""Автобус"",
                    b.license_plate AS ""Гос. номер"",
                    b.seat_capacity AS ""Всего мест"",
                    (b.seat_capacity - (SELECT COUNT(*) FROM Tickets WHERE trip_id = t.trip_id)) AS ""Свободно""
                FROM Trips t
                JOIN Routes r ON t.route_id = r.route_id
                JOIN Buses b ON t.bus_id = b.bus_id
                ORDER BY t.departure_datetime";

            DataTable dt = Database.ExecuteQuery(sql);
            dgvTrips.DataSource = dt;

            // Скрываем колонку ID, она нужна только программе
            if (dgvTrips.Columns["trip_id"] != null)
                dgvTrips.Columns["trip_id"].Visible = false;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadTrips();
        }
        private void btnReports_Click(object sender, EventArgs e)
        {
            // Создаем и показываем форму отчетов
            ReportsForm reportForm = new ReportsForm();
            reportForm.ShowDialog();
        }
        private void btnBuses_Click(object sender, EventArgs e)
        {
            BusManagerForm busForm = new BusManagerForm();
            busForm.ShowDialog();
        }
        private void btnSellTicket_Click(object sender, EventArgs e)
        {
            if (dgvTrips.SelectedRows.Count > 0)
            {
                int selectedTripId = Convert.ToInt32(dgvTrips.SelectedRows[0].Cells["trip_id"].Value);

                // Открываем форму продажи
                TicketSaleForm saleForm = new TicketSaleForm(selectedTripId, _currentUserId);

                // Если продажа прошла успешно (DialogResult.OK), обновляем таблицу
                if (saleForm.ShowDialog() == DialogResult.OK)
                {
                    LoadTrips(); // Перезагрузить список, чтобы обновилось кол-во свободных мест
                }
            }
            else
            {
                MessageBox.Show("Выберите рейс из списка!", "Внимание");
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // При закрытии главного окна закрываем всё приложение целиком
            Application.Exit();
        }
    }
}