using Npgsql;
using NpgsqlTypes; // Нужно для работы с Bytea
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PassengerTransportApp
{
    public partial class BusManagerForm : Form
    {
        public BusManagerForm()
        {
            InitializeComponent();
        }

        private void BusManagerForm_Load(object sender, EventArgs e)
        {
            LoadBuses();
        }

        private void LoadBuses()
        {
            // Загружаем список автобусов (без тяжелой картинки, чтобы не тормозило)
            string sql = "SELECT bus_id, license_plate, model, seat_capacity FROM Buses ORDER BY bus_id";
            dgvBuses.DataSource = Database.ExecuteQuery(sql);
        }

        private void dgvBuses_SelectionChanged(object sender, EventArgs e)
        {
            // Когда кликнули на строку - подгружаем картинку отдельно
            if (dgvBuses.SelectedRows.Count > 0)
            {
                int busId = Convert.ToInt32(dgvBuses.SelectedRows[0].Cells["bus_id"].Value);
                LoadBusImage(busId);
            }
        }

        private void LoadBusImage(int busId)
        {
            string sql = "SELECT bus_image FROM Buses WHERE bus_id = @id";
            DataTable dt = Database.ExecuteQuery(sql, new NpgsqlParameter("@id", busId));

            if (dt.Rows.Count > 0 && dt.Rows[0]["bus_image"] != DBNull.Value)
            {
                // Конвертируем байты из БД обратно в картинку
                byte[] imgData = (byte[])dt.Rows[0]["bus_image"];
                using (MemoryStream ms = new MemoryStream(imgData))
                {
                    picBus.Image = Image.FromStream(ms);
                }
            }
            else
            {
                picBus.Image = null; // Если картинки нет
            }
        }
        
        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (dgvBuses.SelectedRows.Count == 0) return;

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Изображения|*.jpg;*.jpeg;*.png;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    // 1. Читаем файл в массив байт
                    byte[] fileBytes = File.ReadAllBytes(ofd.FileName);

                    // 2. Получаем ID автобуса
                    int busId = Convert.ToInt32(dgvBuses.SelectedRows[0].Cells["bus_id"].Value);

                    // 3. Сохраняем в БД
                    string sql = "UPDATE Buses SET bus_image = @img WHERE bus_id = @id";

                    // Важно указать тип Bytea явно
                    NpgsqlParameter paramImg = new NpgsqlParameter("@img", NpgsqlDbType.Bytea);
                    paramImg.Value = fileBytes;

                    Database.ExecuteNonQuery(sql, paramImg, new NpgsqlParameter("@id", busId));

                    // 4. Обновляем картинку на экране
                    picBus.Image = Image.FromFile(ofd.FileName);
                    MessageBox.Show("Фото успешно загружено!");
                }
            }
        }
    }
}