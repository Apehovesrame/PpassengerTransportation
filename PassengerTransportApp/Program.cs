using System;
using System.Threading; 
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

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            Application.ThreadException += new ThreadExceptionEventHandler(GlobalThreadExceptionHandler);

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(GlobalDomainExceptionHandler);

            Application.Run(new LoginForm());
        }

        static void GlobalThreadExceptionHandler(object sender, ThreadExceptionEventArgs e)
        {
            ShowError(e.Exception);
        }

        static void GlobalDomainExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            ShowError((Exception)e.ExceptionObject);
        }

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