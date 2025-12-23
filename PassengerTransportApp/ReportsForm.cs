using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace PassengerTransportApp
{
    public partial class ReportsForm : Form
    {
        public ReportsForm()
        {
            InitializeComponent();
        }

        private void ReportsForm_Load(object sender, EventArgs e)
        {
            // Ставим дату окончания на сегодня
            dtpEnd.Value = DateTime.Now;
            // Ставим дату начала на месяц назад
            dtpStart.Value = DateTime.Now.AddMonths(-1);
        }

        private void btnShowReport_Click(object sender, EventArgs e)
        {
            // SQL запрос с аналитикой (GROUP BY)
            // Добавили TO_CHAR(tr.departure_datetime, 'DD.MM.YYYY') чтобы видеть дату
            string sql = @"
                SELECT 
                    TO_CHAR(tr.departure_datetime, 'DD.MM.YYYY') AS ""Дата рейса"",
                    r.route_number AS ""Номер маршрута"",
                    r.departure_point || ' - ' || r.destination_point AS ""Направление"",
                    COUNT(t.ticket_id) AS ""Продано билетов"",
                    SUM(t.cost) AS ""Выручка (руб.)""
                FROM Tickets t
                JOIN Trips tr ON t.trip_id = tr.trip_id
                JOIN Routes r ON tr.route_id = r.route_id
                WHERE t.sale_date >= @start AND t.sale_date <= @end
                
                -- Группируем по ДАТЕ (без времени), номеру и направлению
                GROUP BY DATE(tr.departure_datetime), TO_CHAR(tr.departure_datetime, 'DD.MM.YYYY'), r.route_number, r.departure_point, r.destination_point
                
                -- Сортируем: Сначала свежие даты, потом по номеру маршрута
                ORDER BY DATE(tr.departure_datetime) DESC, r.route_number";

            // Важно: в PostgreSQL для сравнения дат с TIMESTAMPTZ лучше передавать DateTime
            DataTable dt = Database.ExecuteQuery(sql,
                new NpgsqlParameter("@start", dtpStart.Value.Date), // Начало дня (00:00)
                new NpgsqlParameter("@end", dtpEnd.Value.Date.AddDays(1).AddSeconds(-1))); // Конец дня (23:59:59)

            dgvReport.DataSource = dt;

            if (dt != null && dt.Rows.Count == 0)
            {
                MessageBox.Show("За выбранный период продаж не найдено.", "Результат");
            }
        }
    }
}