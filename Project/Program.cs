using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using DevExpress.LookAndFeel;

namespace DXApplicationImageMemory
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //注册捕捉异常事件处理非UI线程异常
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledExceptionHandler);
            //处理未捕获的异常
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //处理UI线程异常
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new XtraFormMain());
        }

        public static void CurrentDomain_UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception unhandledException = (Exception)args.ExceptionObject;
            //throw unhandledException;
            //Application.Restart();
        }
        public static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs args)
        {
            Exception threadException = (Exception)args.Exception;
            //throw threadException;
            ////Application.Restart();
        }
    }
}
