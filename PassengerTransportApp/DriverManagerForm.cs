using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace PassengerTransportApp
{
    public partial class DriverManagerForm : Form
    {
        public DriverManagerForm()
        {
            InitializeComponent();

            txtLast.MaxLength = 50;  
            txtFirst.MaxLength = 50;
            txtMiddle.MaxLength = 50;

            txtLast.KeyPress += new KeyPressEventHandler(txtName_KeyPress);
            txtFirst.KeyPress += new KeyPressEventHandler(txtName_KeyPress);
            txtMiddle.KeyPress += new KeyPressEventHandler(txtName_KeyPress);
        }

        private void DriverManagerForm_Load(object sender, EventArgs e)
        {
            LoadDrivers();
        }

        private void LoadDrivers()
        {
            string sql = "SELECT driver_id, last_name AS \"Фамилия\", first_name AS \"Имя\", middle_name AS \"Отчество\" FROM Drivers ORDER BY last_name";
            dgvDrivers.DataSource = Database.ExecuteQuery(sql);
            if (dgvDrivers.Columns["driver_id"] != null) dgvDrivers.Columns["driver_id"].Visible = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLast.Text) || string.IsNullOrWhiteSpace(txtFirst.Text))
            {
                MessageBox.Show("Введите хотя бы Фамилию и Имя!", "Внимание");
                return;
            }

            string pattern = @"^[а-яА-Яa-zA-Z]+(?:[- ][а-яА-Яa-zA-Z]+)*$";

            if (!Regex.IsMatch(txtLast.Text, pattern) ||
                !Regex.IsMatch(txtFirst.Text, pattern) ||
                (!string.IsNullOrEmpty(txtMiddle.Text) && !Regex.IsMatch(txtMiddle.Text, pattern)))
            {
                MessageBox.Show("ФИО содержит недопустимые символы (например, '---' или пробелы в начале/конце).\nРазрешены только буквы и одиночные дефисы.", "Ошибка формата");
                return;
            }

            try
            {
                string sql = "INSERT INTO Drivers (last_name, first_name, middle_name) VALUES (@l, @f, @m)";
                Database.ExecuteNonQuery(sql,
                    new NpgsqlParameter("@l", txtLast.Text.Trim()),
                    new NpgsqlParameter("@f", txtFirst.Text.Trim()),
                    new NpgsqlParameter("@m", txtMiddle.Text.Trim()));

                MessageBox.Show("Водитель добавлен!");
                LoadDrivers();
                txtLast.Clear(); txtFirst.Clear(); txtMiddle.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        // КНОПКА: Изменить
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvDrivers.SelectedRows.Count == 0) return;

            int id = Convert.ToInt32(dgvDrivers.SelectedRows[0].Cells["driver_id"].Value);
            DriverEditForm editForm = new DriverEditForm(id);

            if (editForm.ShowDialog() == DialogResult.OK)
            {
                LoadDrivers();
            }
        }

        // КНОПКА: Удалить
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDrivers.SelectedRows.Count == 0) return;

            int id = Convert.ToInt32(dgvDrivers.SelectedRows[0].Cells["driver_id"].Value);
            string name = dgvDrivers.SelectedRows[0].Cells["Фамилия"].Value.ToString();

            // Проверка: есть ли у водителя рейсы?
            string sqlCheck = $"SELECT COUNT(*) FROM Trips_Drivers WHERE driver_id = {id}";
            long count = (long)Database.ExecuteQuery(sqlCheck).Rows[0][0];

            if (count > 0)
            {
                MessageBox.Show($"Нельзя удалить водителя {name}, так как он назначен на {count} рейсов (включая архивные).\nСначала удалите его из истории рейсов.", "Ошибка удаления", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Удалить водителя {name}?", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Database.ExecuteNonQuery($"DELETE FROM Drivers WHERE driver_id = {id}");
                LoadDrivers();
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