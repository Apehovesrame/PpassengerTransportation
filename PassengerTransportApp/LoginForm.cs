using Npgsql; // Не забудь добавить этот using наверху!
using System;
using System.Data;
using System.Windows.Forms;

namespace PassengerTransportApp
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль!");
                return;
            }

            // 1. Добавляем u.middle_name в запрос
            string sql = @"
                SELECT u.user_id, u.last_name, u.first_name, u.middle_name, r.role_name
                FROM Users u
                JOIN Roles r ON u.role_id = r.role_id
                JOIN Authorizations a ON u.login = a.login
                WHERE u.login = @login AND a.password_hash = @pass";

            string passwordHash = HashHelper.ComputeSha256Hash(password);

            DataTable dt = Database.ExecuteQuery(sql,
                new NpgsqlParameter("@login", login),
                new NpgsqlParameter("@pass", passwordHash));

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                int userId = Convert.ToInt32(row["user_id"]);
                string role = row["role_name"].ToString();

                // 2. Формируем красивые инициалы (Фамилия И.О.)
                string lastName = row["last_name"].ToString();
                string firstName = row["first_name"].ToString();
                string middleName = row["middle_name"].ToString();

                string shortFio = lastName + " " + firstName.Substring(0, 1) + ".";

                if (!string.IsNullOrEmpty(middleName))
                {
                    shortFio += middleName.Substring(0, 1) + ".";
                }

                MessageBox.Show($"Добро пожаловать, {shortFio}!\nВаша роль: {role}", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MainForm mainForm = new MainForm(userId, role, shortFio);
                mainForm.Show();

                mainForm.FormClosed += (s, args) => this.Show();

                this.Hide();

                txtPassword.Clear();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}