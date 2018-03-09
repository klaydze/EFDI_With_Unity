using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EFDI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IUnityContainer container = new UnityContainer();
            var unitySection = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            container.LoadConfiguration(unitySection);
            container.RegisterInstance<IUnityContainer>(container);

            var frm1 = container.Resolve<Form1>();

            Application.Run(frm1);
        }
    }
}
