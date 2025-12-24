using Npgsql;
using NpgsqlTypes;
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
            // Загружаем список (вместимость переименована для красоты)
            string sql = "SELECT bus_id, license_plate AS \"Гос. номер\", model AS \"Модель\", seat_capacity AS \"Мест\" FROM Buses ORDER BY bus_id";
            dgvBuses.DataSource = Database.ExecuteQuery(sql);
            if (dgvBuses.Columns["bus_id"] != null) dgvBuses.Columns["bus_id"].Visible = false;
        }

        private void dgvBuses_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBuses.SelectedRows.Count > 0)
            {
                // 1. Заполняем поля ввода при клике
                txtLicense.Text = dgvBuses.SelectedRows[0].Cells["Гос. номер"].Value.ToString();
                txtModel.Text = dgvBuses.SelectedRows[0].Cells["Модель"].Value.ToString();
                numSeats.Value = Convert.ToInt32(dgvBuses.SelectedRows[0].Cells["Мест"].Value);

                // 2. Грузим фото
                int busId = Convert.ToInt32(dgvBuses.SelectedRows[0].Cells["bus_id"].Value);
                LoadBusImage(busId);
            }
            else
            {
                // Очистка, если ничего не выбрано
                txtLicense.Clear();
                txtModel.Clear();
                numSeats.Value = 20;
                picBus.Image = null;
            }
        }

        private void LoadBusImage(int busId)
        {
            string sql = "SELECT bus_image FROM Buses WHERE bus_id = @id";
            DataTable dt = Database.ExecuteQuery(sql, new NpgsqlParameter("@id", busId));

            if (dt.Rows.Count > 0 && dt.Rows[0]["bus_image"] != DBNull.Value)
            {
                try
                {
                    // Пытаемся превратить байты в картинку
                    byte[] imgData = (byte[])dt.Rows[0]["bus_image"];
                    using (MemoryStream ms = new MemoryStream(imgData))
                    {
                        picBus.Image = Image.FromStream(ms);
                    }
                }
                catch (ArgumentException)
                {
                    // Если файл оказался "битым" или это переименованный текстовый файл
                    picBus.Image = null; // Просто не показываем картинку
                    // Можно вывести сообщение, но лучше просто не ломать программу
                    // MessageBox.Show("Файл изображения поврежден."); 
                }
            }
            else
            {
                picBus.Image = null;
            }
        }

        // === ДОБАВИТЬ ===
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLicense.Text) || string.IsNullOrWhiteSpace(txtModel.Text))
            {
                MessageBox.Show("Заполните номер и модель!", "Внимание");
                return;
            }

            try
            {
                string sql = "INSERT INTO Buses (license_plate, model, seat_capacity) VALUES (@lic, @mod, @seat)";
                Database.ExecuteNonQuery(sql,
                    new NpgsqlParameter("@lic", txtLicense.Text.Trim()),
                    new NpgsqlParameter("@mod", txtModel.Text.Trim()),
                    new NpgsqlParameter("@seat", (int)numSeats.Value));

                MessageBox.Show("Автобус успешно добавлен!", "Успех");
                LoadBuses();
            }
            catch (PostgresException ex) when (ex.SqlState == "23505")
            {
                // Ловим ошибку уникальности (код 23505)
                MessageBox.Show($"Автобус с гос. номером '{txtLicense.Text}' уже существует в базе данных!",
                    "Ошибка: Дубликат номера", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                // Ловим все остальные ошибки
                MessageBox.Show("Произошла ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // === ИЗМЕНИТЬ ===
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvBuses.SelectedRows.Count == 0) return;
            int busId = Convert.ToInt32(dgvBuses.SelectedRows[0].Cells["bus_id"].Value);

            try
            {
                string sql = "UPDATE Buses SET license_plate = @lic, model = @mod, seat_capacity = @seat WHERE bus_id = @id";
                Database.ExecuteNonQuery(sql,
                    new NpgsqlParameter("@lic", txtLicense.Text.Trim()),
                    new NpgsqlParameter("@mod", txtModel.Text.Trim()),
                    new NpgsqlParameter("@seat", (int)numSeats.Value),
                    new NpgsqlParameter("@id", busId));

                MessageBox.Show("Данные автобуса обновлены!", "Успех");
                LoadBuses();
            }
            catch (PostgresException ex) when (ex.SqlState == "23505")
            {
                // Та же проверка при редактировании (вдруг мы меняем номер на уже занятый)
                MessageBox.Show($"Автобус с гос. номером '{txtLicense.Text}' уже существует!",
                    "Ошибка: Дубликат номера", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // УДАЛИТЬ
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvBuses.SelectedRows.Count == 0) return;
            int busId = Convert.ToInt32(dgvBuses.SelectedRows[0].Cells["bus_id"].Value);
            string license = txtLicense.Text;

            // Проверка: используется ли автобус в рейсах?
            string sqlCheck = $"SELECT COUNT(*) FROM Trips WHERE bus_id = {busId}";
            long count = (long)Database.ExecuteQuery(sqlCheck).Rows[0][0];

            if (count > 0)
            {
                MessageBox.Show($"Нельзя удалить автобус {license}, так как он назначен на {count} рейсов (включая архивные).\nСначала удалите рейсы.", "Ошибка удаления", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Удалить автобус {license}?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Database.ExecuteNonQuery($"DELETE FROM Buses WHERE bus_id = {busId}");
                LoadBuses();
                picBus.Image = null;
            }
        }

        // ФОТО (оставили как было)
        private void btnUpload_Click(object sender, EventArgs e)
        {
            if (dgvBuses.SelectedRows.Count == 0) return;

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Изображения|*.jpg;*.jpeg;*.png;*.bmp";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (Image testImg = Image.FromFile(ofd.FileName))
                        {

                        }

                        byte[] fileBytes = File.ReadAllBytes(ofd.FileName);
                        int busId = Convert.ToInt32(dgvBuses.SelectedRows[0].Cells["bus_id"].Value);

                        string sql = "UPDATE Buses SET bus_image = @img WHERE bus_id = @id";
                        NpgsqlParameter paramImg = new NpgsqlParameter("@img", NpgsqlDbType.Bytea);
                        paramImg.Value = fileBytes;

                        Database.ExecuteNonQuery(sql, paramImg, new NpgsqlParameter("@id", busId));

                        picBus.Image = Image.FromFile(ofd.FileName);
                        MessageBox.Show("Фото успешно загружено!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (OutOfMemoryException)
                    {
                        MessageBox.Show("Выбранный файл поврежден или не является изображением!", "Ошибка формата", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при загрузке: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void btnClearPhoto_Click(object sender, EventArgs e)
        {
            if (dgvBuses.SelectedRows.Count == 0) return;

            if (picBus.Image == null)
            {
                MessageBox.Show("У этого автобуса и так нет фото.");
                return;
            }

            if (MessageBox.Show("Вы уверены, что хотите удалить фотографию этого автобуса?", "Удаление фото", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    int busId = Convert.ToInt32(dgvBuses.SelectedRows[0].Cells["bus_id"].Value);

                    string sql = "UPDATE Buses SET bus_image = NULL WHERE bus_id = @id";
                    Database.ExecuteNonQuery(sql, new NpgsqlParameter("@id", busId));

                    picBus.Image = null;

                    MessageBox.Show("Фотография удалена.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
    }
}