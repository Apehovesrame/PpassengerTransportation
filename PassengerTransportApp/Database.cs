using System;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace PassengerTransportApp
{
    public static class Database
    {
        // ВАЖНО: В параметре Password укажи ТОТ пароль, который ты вводил
        // при установке самого PostgreSQL (суперпользователя postgres).
        // Это НЕ пароль 'admin' или '12345', которые мы писали в SQL-скрипте для пользователей системы!
        private static string connString = "Host=localhost;Username=postgres;Password=1234;Database=passenger_transport";

        // Метод для получения соединения (используется внутри других методов)
        public static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(connString);
        }

        // Универсальный метод для получения таблицы данных (SELECT)
        public static DataTable ExecuteQuery(string sql, params NpgsqlParameter[] parameters)
        {
            using (var conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        // Если есть параметры (защита от взлома), добавляем их
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        var dt = new DataTable();
                        using (var da = new NpgsqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                        return dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка подключения к БД:\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
        }

        // Универсальный метод для команд без возврата данных (INSERT, UPDATE, DELETE)
        public static void ExecuteNonQuery(string sql, params NpgsqlParameter[] parameters)
        {
            using (var conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка выполнения операции:\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw; // Пробрасываем ошибку дальше, чтобы форма знала, что операция не удалась
                }
            }
        }
    }
}