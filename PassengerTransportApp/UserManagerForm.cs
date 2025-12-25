using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace PassengerTransportApp
{
    public partial class UserManagerForm : Form
    {
        public UserManagerForm()
        {
            InitializeComponent();
        }

        private void UserManagerForm_Load(object sender, EventArgs e)
        {
            LoadUsers();
        }

        private void LoadUsers()
        {
            string sql = @"
                SELECT 
                    u.user_id, 
                    u.login AS ""Логин"",
                    u.last_name || ' ' || u.first_name || ' ' || COALESCE(u.middle_name, '') AS ""ФИО"",
                    r.role_name AS ""Роль""
                FROM Users u
                JOIN Roles r ON u.role_id = r.role_id
                ORDER BY u.last_name";

            dgvUsers.DataSource = Database.ExecuteQuery(sql);
            if (dgvUsers.Columns["user_id"] != null) dgvUsers.Columns["user_id"].Visible = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddUserForm form = new AddUserForm();
            if (form.ShowDialog() == DialogResult.OK) 
            {
                LoadUsers();
            }
            else
            {
                LoadUsers(); 
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0) return;
            int userId = Convert.ToInt32(dgvUsers.SelectedRows[0].Cells["user_id"].Value);

            UserEditForm form = new UserEditForm(userId);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadUsers();
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0) return;

            int userId = Convert.ToInt32(dgvUsers.SelectedRows[0].Cells["user_id"].Value);
            string login = dgvUsers.SelectedRows[0].Cells["Логин"].Value.ToString();

            if (MessageBox.Show($"Удалить сотрудника {login}?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    Database.ExecuteNonQuery($"DELETE FROM Users WHERE user_id = {userId}");

                    Database.ExecuteNonQuery("DELETE FROM Authorizations WHERE login = @log", new NpgsqlParameter("@log", login));

                    MessageBox.Show("Сотрудник удален.");
                    LoadUsers();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Нельзя удалить сотрудника, так как у него есть связанные данные (проданные билеты или созданные рейсы).\n\nОшибка: " + ex.Message);
                }
            }
        }
    }
}