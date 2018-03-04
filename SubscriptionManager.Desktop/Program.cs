using System;
using System.Windows.Forms;
using SubscriptionManager.CompositionRoot;

namespace SubscriptionManager.Desktop
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            Composition.Load(serviceProvider => Container.SetContainer(serviceProvider));
        }
    }
}