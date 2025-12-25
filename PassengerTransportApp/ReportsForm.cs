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
            dtpEnd.Value = DateTime.Now;
            dtpStart.Value = DateTime.Now.AddMonths(-1);
        }

        private void btnShowReport_Click(object sender, EventArgs e)
        {
            string sql = @"
                SELECT 
                    TO_CHAR(tr.departure_datetime, 'DD.MM.YYYY') AS ""Дата"",
                    TO_CHAR(tr.departure_datetime, 'HH24:MI') AS ""Время отпр."",
                    TO_CHAR(tr.arrival_datetime, 'HH24:MI') AS ""Время приб."",
                    r.route_number AS ""№ Маршрута"",
                    r.departure_point || ' - ' || r.destination_point AS ""Направление"",
                    b.model AS ""Автобус"",
                    b.license_plate AS ""Гос. номер"",
                    
                    (
                        SELECT STRING_AGG(d.last_name || ' ' || SUBSTR(d.first_name, 1, 1) || '.', ', ')
                        FROM Trips_Drivers td
                        JOIN Drivers d ON td.driver_id = d.driver_id
                        WHERE td.trip_id = tr.trip_id
                    ) AS ""Водители"",

                    COUNT(t.ticket_id) AS ""Продано"",
                    SUM(t.cost) AS ""Выручка (руб.)""
                FROM Tickets t
                JOIN Trips tr ON t.trip_id = tr.trip_id
                JOIN Routes r ON tr.route_id = r.route_id
                JOIN Buses b ON tr.bus_id = b.bus_id
                WHERE t.sale_date >= @start AND t.sale_date <= @end
                
                GROUP BY 
                    tr.trip_id, 
                    tr.departure_datetime, 
                    tr.arrival_datetime, 
                    r.route_number, 
                    r.departure_point, 
                    r.destination_point, 
                    b.model, 
                    b.license_plate
                
                ORDER BY tr.departure_datetime DESC";

            DataTable dt = Database.ExecuteQuery(sql,
                new NpgsqlParameter("@start", dtpStart.Value.Date),
                new NpgsqlParameter("@end", dtpEnd.Value.Date.AddDays(1).AddSeconds(-1)));

            dgvReport.DataSource = dt;

            if (dt != null && dt.Rows.Count == 0)
            {
                MessageBox.Show("За выбранный период продаж не найдено.", "Результат");
            }
        }
    }
}