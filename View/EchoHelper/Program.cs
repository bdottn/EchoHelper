using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace EchoHelper
{
    static class Program
    {
        static string assemblyGuid
        {
            get
            {
                object[] attributes =
                    Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(GuidAttribute), false);

                if (attributes.Length == 0)
                {
                    return string.Empty;
                }
                else
                {
                    return ((GuidAttribute)attributes[0]).Value;
                }
            }
        }

        [STAThread]
        static void Main()
        {
            using (Mutex mutex = new Mutex(false, @"Global\" + assemblyGuid))
            {
                if (mutex.WaitOne(0, false) == false)
                {
                    MessageBox.Show("EchoHelper is running !");
                }
                else
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new MainForm());
                }
            }
        }
    }
}