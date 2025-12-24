using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace PassengerTransportApp
{
    public partial class DriverEditForm : Form
    {
        private int _driverId;

        public DriverEditForm(int driverId)
        {
            InitializeComponent();
            _driverId = driverId;

            txtLast.MaxLength = 50;
            txtFirst.MaxLength = 50;
            txtMiddle.MaxLength = 50;

            txtLast.KeyPress += new KeyPressEventHandler(txtName_KeyPress);
            txtFirst.KeyPress += new KeyPressEventHandler(txtName_KeyPress);
            txtMiddle.KeyPress += new KeyPressEventHandler(txtName_KeyPress);
        }

        private void DriverEditForm_Load(object sender, EventArgs e)
        {
            string sql = "SELECT last_name, first_name, middle_name FROM Drivers WHERE driver_id = @id";
            DataTable dt = Database.ExecuteQuery(sql, new NpgsqlParameter("@id", _driverId));

            if (dt.Rows.Count > 0)
            {
                txtLast.Text = dt.Rows[0]["last_name"].ToString();
                txtFirst.Text = dt.Rows[0]["first_name"].ToString();
                txtMiddle.Text = dt.Rows[0]["middle_name"].ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Старая проверка на пустоту...
            if (string.IsNullOrWhiteSpace(txtLast.Text) || string.IsNullOrWhiteSpace(txtFirst.Text))
            {
                MessageBox.Show("Фамилия и Имя обязательны!");
                return;
            }

            // НОВАЯ ПРОВЕРКА
            string pattern = @"^[а-яА-Яa-zA-Z]+(?:[- ][а-яА-Яa-zA-Z]+)*$";

            if (!Regex.IsMatch(txtLast.Text, pattern) ||
                !Regex.IsMatch(txtFirst.Text, pattern) ||
                (!string.IsNullOrEmpty(txtMiddle.Text) && !Regex.IsMatch(txtMiddle.Text, pattern)))
            {
                MessageBox.Show("Некорректный формат имени!\nПроверьте, не ввели ли вы лишние тире или пробелы.", "Ошибка");
                return;
            }

            try
            {
                string sql = @"UPDATE Drivers 
                               SET last_name = @l, first_name = @f, middle_name = @m 
                               WHERE driver_id = @id";

                Database.ExecuteNonQuery(sql,
                    new NpgsqlParameter("@l", txtLast.Text.Trim()),
                    new NpgsqlParameter("@f", txtFirst.Text.Trim()),
                    new NpgsqlParameter("@m", txtMiddle.Text.Trim()),
                    new NpgsqlParameter("@id", _driverId));

                MessageBox.Show("Данные обновлены!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
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
    }
}