using Npgsql;
using System;
using System.Data;
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
        }

        private void PassengerEditForm_Load(object sender, EventArgs e)
        {
            // Загружаем текущие данные пассажира
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
                MessageBox.Show("Фамилия и Паспорт обязательны!");
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
                    new NpgsqlParameter("@pass", txtPassport.Text.Trim()),
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
    }
}