using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace PassengerTransportApp
{
    public partial class PassengerListForm : Form
    {
        private int _tripId;

        public PassengerListForm(int tripId)
        {
            InitializeComponent();
            _tripId = tripId;
        }

        private void PassengerListForm_Load(object sender, EventArgs e)
        {
            LoadPassengers();
        }

        private void LoadPassengers()
        {
            // Загружаем данные + ID (которые скроем)
            string sql = @"
                SELECT 
                    t.ticket_id,        -- Скрытое поле
                    p.passenger_id,     -- Скрытое поле
                    t.seat_number AS ""Место"",
                    p.last_name || ' ' || p.first_name || ' ' || COALESCE(p.middle_name, '') AS ""ФИО Пассажира"",
                    p.passport_number AS ""Паспорт"",
                    s.name AS ""Едет до"",
                    t.cost AS ""Цена""
                FROM Tickets t
                JOIN Passengers p ON t.passenger_id = p.passenger_id
                JOIN Stops s ON t.destination_stop_id = s.stop_id
                WHERE t.trip_id = @id
                ORDER BY t.seat_number";

            DataTable dt = Database.ExecuteQuery(sql, new NpgsqlParameter("@id", _tripId));
            dgvPassengers.DataSource = dt;

            // Скрываем технические колонки (ID)
            if (dgvPassengers.Columns["ticket_id"] != null)
                dgvPassengers.Columns["ticket_id"].Visible = false;

            if (dgvPassengers.Columns["passenger_id"] != null)
                dgvPassengers.Columns["passenger_id"].Visible = false;
        }

        // === ВЕРНУТЬ БИЛЕТ (УДАЛИТЬ) ===
        private void btnRefundTicket_Click(object sender, EventArgs e)
        {
            if (dgvPassengers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите пассажира из списка!");
                return;
            }

            // Берем ID билета из скрытой колонки
            int ticketId = Convert.ToInt32(dgvPassengers.SelectedRows[0].Cells["ticket_id"].Value);
            string fio = dgvPassengers.SelectedRows[0].Cells["ФИО Пассажира"].Value.ToString();

            if (MessageBox.Show($"Оформить возврат билета для пассажира: {fio}?\nБилет будет аннулирован, место освободится.",
                "Возврат билета", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    Database.ExecuteNonQuery($"DELETE FROM Tickets WHERE ticket_id = {ticketId}");
                    MessageBox.Show("Билет возвращен успешно.");
                    LoadPassengers(); // Обновляем список
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }

        // === ИЗМЕНИТЬ ДАННЫЕ ПАССАЖИРА ===
        private void btnEditPassenger_Click(object sender, EventArgs e)
        {
            if (dgvPassengers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите пассажира из списка!");
                return;
            }

            // Берем ID пассажира из скрытой колонки
            int passId = Convert.ToInt32(dgvPassengers.SelectedRows[0].Cells["passenger_id"].Value);

            // Открываем форму редактирования
            PassengerEditForm editForm = new PassengerEditForm(passId);

            if (editForm.ShowDialog() == DialogResult.OK)
            {
                LoadPassengers(); // Обновляем список (ФИО или паспорт могли измениться)
            }
        }
    }
}