using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace PassengerTransportApp
{
    public partial class UserEditForm : Form
    {
        private int _userId;
        private string _userLogin;

        public UserEditForm(int userId)
        {
            InitializeComponent();
            _userId = userId;
        }

        private void UserEditForm_Load(object sender, EventArgs e)
        {
            LoadRoles();
            LoadUserData();
        }

        private void LoadRoles()
        {
            DataTable dt = Database.ExecuteQuery("SELECT role_id, role_name FROM Roles");
            cmbRole.DataSource = dt;
            cmbRole.DisplayMember = "role_name";
            cmbRole.ValueMember = "role_id";
        }

        private void LoadUserData()
        {
            string sql = "SELECT login, last_name, first_name, middle_name, role_id FROM Users WHERE user_id = @id";
            DataTable dt = Database.ExecuteQuery(sql, new NpgsqlParameter("@id", _userId));

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                _userLogin = row["login"].ToString();
                txtLogin.Text = _userLogin;
                txtLast.Text = row["last_name"].ToString();
                txtFirst.Text = row["first_name"].ToString();
                txtMiddle.Text = row["middle_name"].ToString();
                cmbRole.SelectedValue = row["role_id"];
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLast.Text) || string.IsNullOrWhiteSpace(txtFirst.Text))
            {
                MessageBox.Show("Фамилия и Имя обязательны!");
                return;
            }

            try
            {
                // 1. Обновляем данные пользователя
                string sqlUpdateUser = @"UPDATE Users 
                                         SET last_name = @ln, first_name = @fn, middle_name = @mn, role_id = @rid 
                                         WHERE user_id = @uid";

                Database.ExecuteNonQuery(sqlUpdateUser,
                    new NpgsqlParameter("@ln", txtLast.Text.Trim()),
                    new NpgsqlParameter("@fn", txtFirst.Text.Trim()),
                    new NpgsqlParameter("@mn", txtMiddle.Text.Trim()),
                    new NpgsqlParameter("@rid", cmbRole.SelectedValue),
                    new NpgsqlParameter("@uid", _userId));

                // 2. Если введен пароль - меняем его
                if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    string newHash = HashHelper.ComputeSha256Hash(txtPassword.Text);
                    string sqlUpdatePass = "UPDATE Authorizations SET password_hash = @hash WHERE login = @log";
                    Database.ExecuteNonQuery(sqlUpdatePass,
                        new NpgsqlParameter("@hash", newHash),
                        new NpgsqlParameter("@log", _userLogin));
                }

                MessageBox.Show("Данные сохранены!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
    }
}