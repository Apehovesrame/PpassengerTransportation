using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace PassengerTransportApp
{
    public partial class DriverManagerForm : Form
    {
        public DriverManagerForm()
        {
            InitializeComponent();
        }

        // Вот этот метод отсутствовал, из-за чего была ошибка
        private void DriverManagerForm_Load(object sender, EventArgs e)
        {
            LoadDrivers();
        }

        private void LoadDrivers()
        {
            // Загружаем список водителей из БД
            string sql = "SELECT driver_id, last_name AS \"Фамилия\", first_name AS \"Имя\", middle_name AS \"Отчество\" FROM Drivers ORDER BY last_name";
            dgvDrivers.DataSource = Database.ExecuteQuery(sql);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Проверка ввода
            if (string.IsNullOrWhiteSpace(txtLast.Text) || string.IsNullOrWhiteSpace(txtFirst.Text))
            {
                MessageBox.Show("Введите хотя бы Фамилию и Имя!", "Внимание");
                return;
            }

            try
            {
                // Вставка нового водителя
                string sql = "INSERT INTO Drivers (last_name, first_name, middle_name) VALUES (@l, @f, @m)";
                Database.ExecuteNonQuery(sql,
                    new NpgsqlParameter("@l", txtLast.Text.Trim()),
                    new NpgsqlParameter("@f", txtFirst.Text.Trim()),
                    new NpgsqlParameter("@m", txtMiddle.Text.Trim()));

                MessageBox.Show("Водитель добавлен!");

                // Обновляем таблицу и очищаем поля
                LoadDrivers();
                txtLast.Clear();
                txtFirst.Clear();
                txtMiddle.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
    }
}