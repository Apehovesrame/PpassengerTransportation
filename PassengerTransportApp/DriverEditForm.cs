using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace PassengerTransportApp
{
    public partial class DriverEditForm : Form
    {
        private int _driverId;

        public DriverEditForm(int driverId)
        {
            InitializeComponent();
            _driverId = driverId;

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
            if (string.IsNullOrWhiteSpace(txtLast.Text) || string.IsNullOrWhiteSpace(txtFirst.Text))
            {
                MessageBox.Show("Фамилия и Имя обязательны!");
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
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar != '-' && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }
    }
}