using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace PassengerTransportApp
{
    public partial class TicketSaleForm : Form
    {
        private int _tripId;
        private int _cashierId;

        // Конструктор формы
        public TicketSaleForm(int tripId, int cashierId)
        {
            // 1. Создаем визуальные элементы (кнопки, поля)
            InitializeComponent();

            // 2. Сохраняем переданные данные
            _tripId = tripId;
            _cashierId = cashierId;

            // 3. Загружаем остановки (только после пункта 1!)
            LoadStops();
        }

        // Пустой метод, чтобы дизайнер Visual Studio не ругался
        private void TicketSaleForm_Load(object sender, EventArgs e)
        {
        }

        private void LoadStops()
        {
            // Запрос остановок для конкретного рейса
            string sql = @"
                SELECT s.stop_id, s.name 
                FROM Trips t
                JOIN Routes_Stops rs ON t.route_id = rs.route_id
                JOIN Stops s ON rs.stop_id = s.stop_id
                WHERE t.trip_id = @tripId
                ORDER BY rs.stop_order";

            // Выполняем запрос
            DataTable dt = Database.ExecuteQuery(sql, new NpgsqlParameter("@tripId", _tripId));

            // ПРОВЕРКА: Если база вернула ошибку (null), выходим, чтобы не было 'NullReferenceException'
            if (dt == null)
            {
                MessageBox.Show("Не удалось загрузить остановки. Проверьте подключение к БД.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Заполняем выпадающий список
            cmbStops.DataSource = dt;
            cmbStops.DisplayMember = "name";    // Что показывать пользователю
            cmbStops.ValueMember = "stop_id";   // Что хранить в программе (ID)
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Проверка: заполнил ли пользователь поля?
            if (string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtPassport.Text))
            {
                MessageBox.Show("Заполните ФИО и номер паспорта!", "Внимание");
                return;
            }

            try
            {
                // 1. Находим или создаем пассажира
                int passengerId = GetOrCreatePassenger();

                // Если пассажира не удалось создать (ошибка БД)
                if (passengerId == -1) return;

                // 2. Продаем билет
                string sqlTicket = @"
                    INSERT INTO Tickets (trip_id, passenger_id, destination_stop_id, sold_by_user_id, seat_number, cost)
                    VALUES (@trip, @pass, @stop, @user, @seat, @cost)";

                Database.ExecuteNonQuery(sqlTicket,
                    new NpgsqlParameter("@trip", _tripId),
                    new NpgsqlParameter("@pass", passengerId),
                    new NpgsqlParameter("@stop", cmbStops.SelectedValue),
                    new NpgsqlParameter("@user", _cashierId),
                    new NpgsqlParameter("@seat", (int)numSeat.Value),
                    new NpgsqlParameter("@cost", numPrice.Value));

                MessageBox.Show("Билет успешно продан!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK; // Говорим главной форме, что всё ок
                this.Close(); // Закрываем окно
            }
            catch (Exception ex)
            {
                // Сюда попадем, если сработает Триггер (нет мест) или другая ошибка
                MessageBox.Show("Ошибка продажи: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetOrCreatePassenger()
        {
            string passport = txtPassport.Text.Trim();

            // Шаг А: Ищем пассажира по паспорту
            string sqlCheck = "SELECT passenger_id FROM Passengers WHERE passport_number = @pass";
            DataTable dt = Database.ExecuteQuery(sqlCheck, new NpgsqlParameter("@pass", passport));

            if (dt == null) return -1; // Ошибка БД

            if (dt.Rows.Count > 0)
            {
                // Пассажир найден, возвращаем его ID
                return Convert.ToInt32(dt.Rows[0]["passenger_id"]);
            }
            else
            {
                // Шаг Б: Пассажира нет, создаем нового
                string sqlInsert = @"
                    INSERT INTO Passengers (last_name, first_name, middle_name, passport_number, birth_year)
                    VALUES (@ln, @fn, @mn, @pass, @year)
                    RETURNING passenger_id";

                DataTable dtNew = Database.ExecuteQuery(sqlInsert,
                    new NpgsqlParameter("@ln", txtLastName.Text.Trim()),
                    new NpgsqlParameter("@fn", txtFirstName.Text.Trim()),
                    new NpgsqlParameter("@mn", txtMiddleName.Text.Trim()),
                    new NpgsqlParameter("@pass", passport),
                    new NpgsqlParameter("@year", (int)numYear.Value));

                if (dtNew == null || dtNew.Rows.Count == 0) return -1;

                return Convert.ToInt32(dtNew.Rows[0][0]);
            }
        }
    }
}