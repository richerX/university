using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fractals
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bool continueOperation = true;
            while (continueOperation)
            {
                try
                {
                    Application.Run(new MainWindow());
                    continueOperation = false;
                }
                catch (Exception)
                {
                    Application.Exit();
                }
            }
        }
    }
}
