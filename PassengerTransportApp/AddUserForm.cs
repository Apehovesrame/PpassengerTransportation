using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace PassengerTransportApp
{
    public partial class AddUserForm : Form
    {
        public AddUserForm()
        {
            InitializeComponent();
            LoadRoles();

            txtFIO.KeyPress += new KeyPressEventHandler(txtName_KeyPress);
        }

        private void LoadRoles()
        {
            DataTable dt = Database.ExecuteQuery("SELECT role_id, role_name FROM Roles");
            cmbRole.DataSource = dt;
            cmbRole.DisplayMember = "role_name";
            cmbRole.ValueMember = "role_id";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLogin.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Заполните Логин и Пароль!");
                return;
            }

            try
            {
                // 1. Хешируем пароль
                string passHash = HashHelper.ComputeSha256Hash(txtPassword.Text);

                // 2. Вставка в таблицу Авторизации
                string sqlAuth = "INSERT INTO Authorizations (login, password_hash) VALUES (@log, @hash)";
                Database.ExecuteNonQuery(sqlAuth,
                    new NpgsqlParameter("@log", txtLogin.Text.Trim()),
                    new NpgsqlParameter("@hash", passHash));

                // 3. Разделяем ФИО
                string[] fio = txtFIO.Text.Split(' ');
                string last = fio.Length > 0 ? fio[0] : "-";
                string first = fio.Length > 1 ? fio[1] : "-";
                string mid = fio.Length > 2 ? fio[2] : "";

                // 4. Вставка в таблицу Сотрудников
                string sqlUser = @"INSERT INTO Users (role_id, login, last_name, first_name, middle_name) 
                                   VALUES (@rid, @log, @ln, @fn, @mn)";

                Database.ExecuteNonQuery(sqlUser,
                    new NpgsqlParameter("@rid", cmbRole.SelectedValue),
                    new NpgsqlParameter("@log", txtLogin.Text.Trim()),
                    new NpgsqlParameter("@ln", last),
                    new NpgsqlParameter("@fn", first),
                    new NpgsqlParameter("@mn", mid));

                MessageBox.Show("Сотрудник добавлен успешно!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка (возможно логин занят): " + ex.Message);
            }
        }
        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar != '-' && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }
    }
}