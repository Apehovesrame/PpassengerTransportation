using Npgsql;
using System;
using System.Data;
using System.Text.RegularExpressions; // <-- ВАЖНО: Добавлено для проверок
using System.Windows.Forms;

namespace PassengerTransportApp
{
    public partial class PassengerEditForm : Form
    {
        private int _passengerId;

        public PassengerEditForm(int passengerId)
        {
            InitializeComponent();
            _passengerId = passengerId;

            txtLast.KeyPress += new KeyPressEventHandler(txtName_KeyPress);
            txtFirst.KeyPress += new KeyPressEventHandler(txtName_KeyPress);
            txtMiddle.KeyPress += new KeyPressEventHandler(txtName_KeyPress);

            txtPassport.MaxLength = 11; 
            txtPassport.KeyPress += new KeyPressEventHandler(txtPassport_KeyPress);
        }

        private void PassengerEditForm_Load(object sender, EventArgs e)
        {
            string sql = "SELECT last_name, first_name, middle_name, passport_number FROM Passengers WHERE passenger_id = @id";
            DataTable dt = Database.ExecuteQuery(sql, new NpgsqlParameter("@id", _passengerId));

            if (dt.Rows.Count > 0)
            {
                txtLast.Text = dt.Rows[0]["last_name"].ToString();
                txtFirst.Text = dt.Rows[0]["first_name"].ToString();
                txtMiddle.Text = dt.Rows[0]["middle_name"].ToString();
                txtPassport.Text = dt.Rows[0]["passport_number"].ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLast.Text) || string.IsNullOrWhiteSpace(txtPassport.Text))
            {
                MessageBox.Show("Фамилия и Паспорт обязательны!", "Внимание");
                return;
            }

            string namePattern = @"^[а-яА-Яa-zA-Z]+(?:[- ][а-яА-Яa-zA-Z]+)*$";
            if (!Regex.IsMatch(txtLast.Text, namePattern) ||
                !Regex.IsMatch(txtFirst.Text, namePattern) ||
                (!string.IsNullOrEmpty(txtMiddle.Text) && !Regex.IsMatch(txtMiddle.Text, namePattern)))
            {
                MessageBox.Show("Некорректный формат ФИО (проверьте лишние пробелы и тире).", "Ошибка формата");
                return;
            }

            string cleanPassport = txtPassport.Text.Replace(" ", "").Trim();
            if (!Regex.IsMatch(cleanPassport, @"^\d{10}$"))
            {
                MessageBox.Show($"Паспорт РФ должен содержать ровно 10 цифр.\nВы ввели: {cleanPassport.Length}", "Ошибка формата");
                return;
            }

            try
            {
                string sqlUpdate = @"
                    UPDATE Passengers 
                    SET last_name = @ln, first_name = @fn, middle_name = @mn, passport_number = @pass
                    WHERE passenger_id = @id";

                Database.ExecuteNonQuery(sqlUpdate,
                    new NpgsqlParameter("@ln", txtLast.Text.Trim()),
                    new NpgsqlParameter("@fn", txtFirst.Text.Trim()),
                    new NpgsqlParameter("@mn", txtMiddle.Text.Trim()),
                    new NpgsqlParameter("@pass", cleanPassport), 
                    new NpgsqlParameter("@id", _passengerId));

                MessageBox.Show("Данные пассажира обновлены!");
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

            if (e.KeyChar == (char)Keys.Back) return;

            if (txt.SelectionStart == 0 && (e.KeyChar == '-' || e.KeyChar == ' '))
            {
                e.Handled = true;
                return;
            }

            if (txt.SelectionStart > 0 && (e.KeyChar == '-' || e.KeyChar == ' '))
            {
                char lastChar = txt.Text[txt.SelectionStart - 1];
                if (lastChar == '-' || lastChar == ' ')
                {
                    e.Handled = true;
                    return;
                }
            }

            if (!char.IsLetter(e.KeyChar) && e.KeyChar != '-' && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }
        private void txtPassport_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }
    }
}