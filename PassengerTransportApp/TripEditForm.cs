using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace PassengerTransportApp
{
    public partial class TripEditForm : Form
    {
        private int _currentUserId;
        private int? _tripId; // Если null - создание, если число - редактирование

        // Конструктор: параметр tripId опциональный (по умолчанию null)
        public TripEditForm(int userId, int? tripId = null)
        {
            InitializeComponent();
            _currentUserId = userId;
            _tripId = tripId;

            // Меняем заголовок кнопки в зависимости от режима
            if (_tripId.HasValue)
            {
                this.Text = "Редактирование рейса";
                btnSave.Text = "СОХРАНИТЬ ИЗМЕНЕНИЯ";
            }
        }

        private void TripEditForm_Load(object sender, EventArgs e)
        {
            LoadDirectories(); // Сначала грузим списки

            if (_tripId.HasValue)
            {
                LoadTripData(); // Если редактируем, заполняем поля
            }
        }

        private void LoadDirectories()
        {
            // 1. Маршруты
            cmbRoutes.DataSource = Database.ExecuteQuery("SELECT route_id, route_number || ' (' || departure_point || '-' || destination_point || ')' as name FROM Routes");
            cmbRoutes.DisplayMember = "name";
            cmbRoutes.ValueMember = "route_id";

            // 2. Автобусы
            cmbBuses.DataSource = Database.ExecuteQuery("SELECT bus_id, model || ' (' || license_plate || ')' as name FROM Buses");
            cmbBuses.DisplayMember = "name";
            cmbBuses.ValueMember = "bus_id";

            // 3. Водители
            DataTable dtDrivers = Database.ExecuteQuery("SELECT driver_id, last_name || ' ' || first_name as name FROM Drivers ORDER BY last_name");
            clbDrivers.Items.Clear();
            foreach (DataRow row in dtDrivers.Rows)
            {
                clbDrivers.Items.Add(new DriverItem
                {
                    Id = (int)row["driver_id"],
                    Name = row["name"].ToString()
                });
            }
        }

        private void LoadTripData()
        {
            // Получаем данные о самом рейсе
            string sqlTrip = $"SELECT route_id, bus_id, departure_datetime FROM Trips WHERE trip_id = {_tripId}";
            DataTable dt = Database.ExecuteQuery(sqlTrip);

            if (dt.Rows.Count > 0)
            {
                // Устанавливаем значения в полях
                cmbRoutes.SelectedValue = dt.Rows[0]["route_id"];
                cmbBuses.SelectedValue = dt.Rows[0]["bus_id"];
                dtpDeparture.Value = (DateTime)dt.Rows[0]["departure_datetime"];
            }

            // Получаем назначенных водителей
            string sqlDrivers = $"SELECT driver_id FROM Trips_Drivers WHERE trip_id = {_tripId}";
            DataTable dtAssigned = Database.ExecuteQuery(sqlDrivers);

            // Проставляем галочки
            for (int i = 0; i < clbDrivers.Items.Count; i++)
            {
                DriverItem item = (DriverItem)clbDrivers.Items[i];
                foreach (DataRow row in dtAssigned.Rows)
                {
                    if (item.Id == (int)row["driver_id"])
                    {
                        clbDrivers.SetItemChecked(i, true);
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (clbDrivers.CheckedItems.Count == 0)
            {
                MessageBox.Show("Выберите хотя бы одного водителя!", "Внимание");
                return;
            }

            try
            {
                int routeId = (int)cmbRoutes.SelectedValue;
                int busId = (int)cmbBuses.SelectedValue;

                // Считаем время
                string sqlDuration = $"SELECT duration_minutes FROM Routes WHERE route_id = {routeId}";
                int duration = Convert.ToInt32(Database.ExecuteQuery(sqlDuration).Rows[0][0]);
                DateTime newStart = dtpDeparture.Value;
                DateTime newEnd = newStart.AddMinutes(duration);

                // --- ПРОВЕРКА НА ЗАНЯТОСТЬ АВТОБУСА ---
                // Важно: исключаем текущий рейс из проверки (trip_id != ...), 
                // иначе он будет конфликтовать сам с собой при редактировании
                string currentIdFilter = _tripId.HasValue ? $"AND trip_id != {_tripId}" : "";

                string sqlCheckBus = $@"
                    SELECT COUNT(*) FROM Trips 
                    WHERE bus_id = @bid
                      AND (departure_datetime < @newEnd AND arrival_datetime > @newStart)
                      {currentIdFilter}";

                long busConflicts = (long)Database.ExecuteQuery(sqlCheckBus,
                    new NpgsqlParameter("@bid", busId),
                    new NpgsqlParameter("@newStart", newStart),
                    new NpgsqlParameter("@newEnd", newEnd)).Rows[0][0];

                if (busConflicts > 0)
                {
                    MessageBox.Show("Этот автобус занят в выбранное время!", "Конфликт");
                    return;
                }

                // --- ПРОВЕРКА ВОДИТЕЛЕЙ ---
                foreach (DriverItem item in clbDrivers.CheckedItems)
                {
                    string sqlCheckDriver = $@"
                        SELECT COUNT(*) FROM Trips t
                        JOIN Trips_Drivers td ON t.trip_id = td.trip_id
                        WHERE td.driver_id = @did
                          AND (t.departure_datetime < @newEnd AND t.arrival_datetime > @newStart)
                          {currentIdFilter}"; // Та же фильтрация текущего рейса

                    long drvConflicts = (long)Database.ExecuteQuery(sqlCheckDriver,
                        new NpgsqlParameter("@did", item.Id),
                        new NpgsqlParameter("@newStart", newStart),
                        new NpgsqlParameter("@newEnd", newEnd)).Rows[0][0];

                    if (drvConflicts > 0)
                    {
                        MessageBox.Show($"Водитель {item.Name} занят в это время!", "Конфликт");
                        return;
                    }
                }

                // --- СОХРАНЕНИЕ ---

                if (_tripId.HasValue)
                {
                    // РЕЖИМ РЕДАКТИРОВАНИЯ (UPDATE)
                    string sqlUpdate = @"
                        UPDATE Trips 
                        SET route_id = @rid, bus_id = @bid, departure_datetime = @dep, arrival_datetime = @arr
                        WHERE trip_id = @tid";

                    Database.ExecuteNonQuery(sqlUpdate,
                        new NpgsqlParameter("@rid", routeId),
                        new NpgsqlParameter("@bid", busId),
                        new NpgsqlParameter("@dep", newStart),
                        new NpgsqlParameter("@arr", newEnd),
                        new NpgsqlParameter("@tid", _tripId));

                    // Обновляем водителей: удаляем старых, пишем новых
                    Database.ExecuteNonQuery($"DELETE FROM Trips_Drivers WHERE trip_id = {_tripId}");

                    foreach (DriverItem item in clbDrivers.CheckedItems)
                    {
                        Database.ExecuteNonQuery($"INSERT INTO Trips_Drivers (trip_id, driver_id) VALUES ({_tripId}, {item.Id})");
                    }

                    MessageBox.Show("Рейс обновлен!");
                }
                else
                {
                    // РЕЖИМ СОЗДАНИЯ (INSERT) - как и было раньше
                    string sqlInsert = @"
                        INSERT INTO Trips (route_id, bus_id, created_by_user_id, departure_datetime, arrival_datetime) 
                        VALUES (@rid, @bid, @uid, @dep, @arr) RETURNING trip_id";

                    DataTable dtRes = Database.ExecuteQuery(sqlInsert,
                        new NpgsqlParameter("@rid", routeId),
                        new NpgsqlParameter("@bid", busId),
                        new NpgsqlParameter("@uid", _currentUserId),
                        new NpgsqlParameter("@dep", newStart),
                        new NpgsqlParameter("@arr", newEnd));

                    int newId = (int)dtRes.Rows[0][0];

                    foreach (DriverItem item in clbDrivers.CheckedItems)
                    {
                        Database.ExecuteNonQuery($"INSERT INTO Trips_Drivers (trip_id, driver_id) VALUES ({newId}, {item.Id})");
                    }
                    MessageBox.Show("Рейс создан!");
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
    }

    public class DriverItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public override string ToString() => Name;
    }
}