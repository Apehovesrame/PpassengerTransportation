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

            // Запрос к БД.
            // Мы соединяем таблицу Users, Roles и Authorizations, чтобы сразу узнать кто это.
            // ВНИМАНИЕ: Сейчас мы сравниваем пароль как обычный текст (a.password_hash = @pass),
            // так как в базу мы записали 'admin', '12345' и т.д. В реальном проекте тут нужно хеширование.
            string sql = @"
                SELECT u.user_id, u.last_name, u.first_name, r.role_name
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
                // Данные найдены - вход успешен
                DataRow row = dt.Rows[0];
                int userId = Convert.ToInt32(row["user_id"]);
                string role = row["role_name"].ToString();
                string fio = $"{row["last_name"]} {row["first_name"]}";

                MessageBox.Show($"Добро пожаловать, {fio}!\nВаша роль: {role}", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MainForm mainForm = new MainForm(userId, role, fio);
                // В следующем шаге мы научим MainForm принимать эти данные, пока просто открываем
                mainForm.Show();

                // Скрываем окно авторизации
                this.Hide();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Чтобы при закрытии главной формы закрывалось всё приложение:
        // (этот код пока не нужен, добавим в Main позже)
    }
}