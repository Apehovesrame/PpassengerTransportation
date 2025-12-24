using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PassengerTransportApp
{
    public partial class TicketSaleForm : Form
    {
        private int _tripId;
        private int _cashierId;
        private const string PASSPORT_PLACEHOLDER = "Серия и номер";

        public TicketSaleForm(int tripId, int cashierId)
        {
            InitializeComponent();
            _tripId = tripId;
            _cashierId = cashierId;

            txtLastName.KeyPress += new KeyPressEventHandler(txtName_KeyPress);
            txtFirstName.KeyPress += new KeyPressEventHandler(txtName_KeyPress);
            txtMiddleName.KeyPress += new KeyPressEventHandler(txtName_KeyPress);

            txtLastName.MaxLength = 20;
            txtFirstName.MaxLength = 20;
            txtMiddleName.MaxLength = 20;

            txtPassport.MaxLength = 11;

            txtPassport.Text = PASSPORT_PLACEHOLDER;
            txtPassport.ForeColor = Color.Gray;

            txtPassport.KeyPress += new KeyPressEventHandler(txtPassport_KeyPress);
            cmbStops.SelectedIndexChanged += CmbStops_SelectedIndexChanged;

            LoadStops();
            LoadFreeSeats();
        }

        private void TicketSaleForm_Load(object sender, EventArgs e) { }

        // === ОБНОВЛЕННАЯ ЗАГРУЗКА ОСТАНОВОК И ЦЕН ===
        private void LoadStops()
        {
            // Берем ID, Название и ЦЕНУ
            string sql = @"
                SELECT s.stop_id, s.name, rs.price_from_start
                FROM Trips t
                JOIN Routes_Stops rs ON t.route_id = rs.route_id
                JOIN Stops s ON rs.stop_id = s.stop_id
                WHERE t.trip_id = @tripId
                ORDER BY rs.stop_order";

            DataTable dt = Database.ExecuteQuery(sql, new NpgsqlParameter("@tripId", _tripId));
            if (dt == null) return;

            cmbStops.Items.Clear();

            foreach (DataRow row in dt.Rows)
            {
                // Пропускаем начальную остановку (где цена 0), так как туда билет купить нельзя (мы уже там)
                decimal price = Convert.ToDecimal(row["price_from_start"]);
                if (price > 0)
                {
                    cmbStops.Items.Add(new StopItem
                    {
                        Id = Convert.ToInt32(row["stop_id"]),
                        Name = row["name"].ToString(),
                        Price = price
                    });
                }
            }

            if (cmbStops.Items.Count > 0)
                cmbStops.SelectedIndex = 0; // Выбираем первую по умолчанию
        }

        // === АВТОМАТИЧЕСКАЯ СМЕНА ЦЕНЫ ===
        private void CmbStops_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbStops.SelectedItem is StopItem selectedStop)
            {
                numPrice.Value = selectedStop.Price; // Подставляем цену в поле
            }
        }

        private void txtPassport_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ' '))
                e.Handled = true;
        }
        // Универсальный метод: разрешает только Буквы, Пробел, Дефис и Backspace
        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = sender as TextBox;

            // 1. Разрешаем Backspace
            if (e.KeyChar == (char)Keys.Back) return;

            // 2. Запрещаем пробел или дефис, если это ПЕРВЫЙ символ
            if (txt.SelectionStart == 0 && (e.KeyChar == '-' || e.KeyChar == ' '))
            {
                e.Handled = true;
                return;
            }

            // 3. Запрещаем двойное тире (если предыдущий символ уже тире)
            if (txt.SelectionStart > 0 && (e.KeyChar == '-' || e.KeyChar == ' '))
            {
                char lastChar = txt.Text[txt.SelectionStart - 1];
                if (lastChar == '-' || lastChar == ' ')
                {
                    e.Handled = true; // Блокируем повтор
                    return;
                }
            }

            // 4. Разрешаем только буквы, пробел и дефис
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != '-' && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void LoadFreeSeats()
        {
            string sqlCapacity = @"SELECT b.seat_capacity FROM Trips t JOIN Buses b ON t.bus_id = b.bus_id WHERE t.trip_id = @id";
            int capacity = (int)Database.ExecuteQuery(sqlCapacity, new NpgsqlParameter("@id", _tripId)).Rows[0][0];

            string sqlTaken = @"SELECT seat_number FROM Tickets WHERE trip_id = @id";
            DataTable dtTaken = Database.ExecuteQuery(sqlTaken, new NpgsqlParameter("@id", _tripId));

            HashSet<int> takenSeats = new HashSet<int>();
            foreach (DataRow row in dtTaken.Rows)
            {
                takenSeats.Add(Convert.ToInt32(row["seat_number"]));
            }

            cmbSeat.Items.Clear();
            for (int i = 1; i <= capacity; i++)
            {
                if (!takenSeats.Contains(i))
                {
                    cmbSeat.Items.Add(i);
                }
            }

            if (cmbSeat.Items.Count > 0)
                cmbSeat.SelectedIndex = 0;
            else
                MessageBox.Show("На этот рейс мест нет!", "Внимание");
        }

        private void txtPassport_Enter(object sender, EventArgs e)
        {
            if (txtPassport.Text == PASSPORT_PLACEHOLDER)
            {
                txtPassport.Text = "";
                txtPassport.ForeColor = Color.Black;
            }
        }

        private void txtPassport_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassport.Text))
            {
                txtPassport.Text = PASSPORT_PLACEHOLDER;
                txtPassport.ForeColor = Color.Gray;
            }
        }

        private bool ValidateFields()
        {
            bool isValid = true;
            Color errorColor = Color.MistyRose;
            Color normalColor = SystemColors.Window;

            // Сброс цветов
            txtLastName.BackColor = normalColor;
            txtFirstName.BackColor = normalColor;
            txtMiddleName.BackColor = normalColor;
            txtPassport.BackColor = normalColor;
            cmbSeat.BackColor = normalColor;

            string namePattern = @"^[а-яА-Яa-zA-Z]+(?:[- ][а-яА-Яa-zA-Z]+)*$";

            // Проверка Фамилии
            if (string.IsNullOrWhiteSpace(txtLastName.Text) || !Regex.IsMatch(txtLastName.Text, namePattern))
            {
                txtLastName.BackColor = errorColor;
                MessageBox.Show("Некорректная Фамилия (проверьте тире и пробелы)", "Ошибка");
                isValid = false;
            }

            // Проверка Имени
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) || !Regex.IsMatch(txtFirstName.Text, namePattern))
            {
                txtFirstName.BackColor = errorColor;
                isValid = false;
            }

            // 1. Проверка на пустоту
            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                txtLastName.BackColor = errorColor;
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                txtFirstName.BackColor = errorColor;
                isValid = false;
            }
            if (cmbSeat.SelectedItem == null)
            {
                cmbSeat.BackColor = errorColor;
                isValid = false;
            }

            // 2. НОВАЯ ПРОВЕРКА: Длина текста
            if (txtLastName.Text.Length > 20)
            {
                MessageBox.Show("Фамилия слишком длинная! Максимум 20 букв.", "Ошибка формата");
                txtLastName.BackColor = errorColor;
                return false;
            }
            if (txtFirstName.Text.Length > 20)
            {
                MessageBox.Show("Имя слишком длинное! Максимум 20 букв.", "Ошибка формата");
                txtFirstName.BackColor = errorColor;
                return false;
            }
            if (txtMiddleName.Text.Length > 20)
            {
                MessageBox.Show("Отчество слишком длинное! Максимум 20 букв.", "Ошибка формата");
                txtMiddleName.BackColor = errorColor;
                return false;
            }

            string cleanPassport = txtPassport.Text.Replace(" ", "").Trim();
            if (txtPassport.Text == PASSPORT_PLACEHOLDER ||
                string.IsNullOrWhiteSpace(cleanPassport) ||
                !Regex.IsMatch(cleanPassport, @"^\d{10}$"))
            {
                txtPassport.BackColor = errorColor;
                if (txtPassport.Text != PASSPORT_PLACEHOLDER && !string.IsNullOrWhiteSpace(cleanPassport))
                {
                    MessageBox.Show($"Паспорт РФ должен содержать ровно 10 цифр.\nВы ввели: {cleanPassport.Length}", "Ошибка формата");
                }
                isValid = false;
            }

            return isValid;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string sqlDate = $"SELECT departure_datetime FROM Trips WHERE trip_id = {_tripId}";
            DateTime departure = (DateTime)Database.ExecuteQuery(sqlDate).Rows[0][0];

            if (departure < DateTime.Now)
            {
                MessageBox.Show("Нельзя продать билет на этот рейс, так как автобус уже уехал!", "Ошибка времени", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidateFields())
            {
                //MessageBox.Show("Пожалуйста, исправьте ошибки!", "Ошибка ввода");
                return;
            }

            try
            {
                int passengerId = GetOrCreatePassenger();
                string sqlCheckPass = $"SELECT COUNT(*) FROM Tickets WHERE trip_id = {_tripId} AND passenger_id = {passengerId}";
                long count = (long)Database.ExecuteQuery(sqlCheckPass).Rows[0][0];

                if (count > 0)
                {
                    MessageBox.Show("Этот пассажир уже имеет билет на данный рейс!", "Дубликат пассажира", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (passengerId == -1) return;

                // Получаем ID остановки из выбранного объекта StopItem
                int destinationStopId = ((StopItem)cmbStops.SelectedItem).Id;

                string sqlTicket = @"
                    INSERT INTO Tickets (trip_id, passenger_id, destination_stop_id, sold_by_user_id, seat_number, cost)
                    VALUES (@trip, @pass, @stop, @user, @seat, @cost)";

                Database.ExecuteNonQuery(sqlTicket,
                    new NpgsqlParameter("@trip", _tripId),
                    new NpgsqlParameter("@pass", passengerId),
                    new NpgsqlParameter("@stop", destinationStopId),
                    new NpgsqlParameter("@user", _cashierId),
                    new NpgsqlParameter("@seat", Convert.ToInt32(cmbSeat.SelectedItem)),
                    new NpgsqlParameter("@cost", numPrice.Value));

                MessageBox.Show("Билет успешно продан!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка продажи: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetOrCreatePassenger()
        {
            string passport = txtPassport.Text.Replace(" ", "").Trim();
            string sqlCheck = "SELECT passenger_id FROM Passengers WHERE passport_number = @pass";
            DataTable dt = Database.ExecuteQuery(sqlCheck, new NpgsqlParameter("@pass", passport));

            if (dt != null && dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0]["passenger_id"]);
            }
            else
            {
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

    // Вспомогательный класс для выпадающего списка
    public class StopItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"{Name} — {Price:N0} руб.";
        }
    }
}