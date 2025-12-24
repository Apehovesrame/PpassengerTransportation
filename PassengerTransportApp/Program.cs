using System;
using System.Threading; // Нужно для обработки потоков
using System.Windows.Forms;

namespace PassengerTransportApp
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // === ГЛОБАЛЬНЫЙ ПЕРЕХВАТ ОШИБОК (ОТКАЗОУСТОЙЧИВОСТЬ) ===

            // 1. Указываем, что ошибки нужно ловить, а не давать Windows закрыть программу
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // 2. Ловим ошибки в графическом интерфейсе (кнопки, формы)
            Application.ThreadException += new ThreadExceptionEventHandler(GlobalThreadExceptionHandler);

            // 3. Ловим ошибки в фоновых процессах (системные)
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(GlobalDomainExceptionHandler);

            // Запуск программы
            Application.Run(new LoginForm());
        }

        // Обработчик ошибок интерфейса
        static void GlobalThreadExceptionHandler(object sender, ThreadExceptionEventArgs e)
        {
            ShowError(e.Exception);
        }

        // Обработчик системных ошибок
        static void GlobalDomainExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            ShowError((Exception)e.ExceptionObject);
        }

        // Метод для красивого отображения ошибки
        static void ShowError(Exception ex)
        {
            MessageBox.Show(
                $"Произошла непредвиденная ошибка!\n\nДетали: {ex.Message}\n\nПрограмма продолжит работу.",
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}