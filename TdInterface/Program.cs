using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TdInterface
{
    internal static class Program
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
            
            var services = new ServiceCollection();
            ConfigureServices(services);

            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                //var mainForm = serviceProvider.GetRequiredService<MainForm>();
                //Application.Run(mainForm);
                var masterForm = serviceProvider.GetRequiredService<MasterForm>();
                Application.Run(masterForm);

            }
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<MasterForm>();
        }
    }
}
