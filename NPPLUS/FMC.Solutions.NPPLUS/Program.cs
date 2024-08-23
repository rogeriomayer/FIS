using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using FMC.Solutions.NPPLUS.Library.Class;
using System.Globalization;
using System.Threading;
using DevExpress.XtraEditors;
using System.Net;

namespace FMC.Solutions.NPPLUS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// 
        /// </summary>
        static ApplicationContext MainContext = new ApplicationContext();

        [STAThread]
        static void Main()
        {
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            //try
            //{
            CultureInfo.CurrentCulture =
            new System.Globalization.CultureInfo("pt-BR");

            //teste TLS
            if (ServicePointManager.SecurityProtocol.HasFlag(SecurityProtocolType.Tls12) == false)
            {
                ServicePointManager.SecurityProtocol = ServicePointManager.SecurityProtocol | SecurityProtocolType.Tls12;
            }
            //**Add these lines**

            

            BonusSkins.Register();
            SkinManager.EnableFormSkins();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainContext.MainForm = new Login();
            Application.Run(MainContext);
            //}
            //catch (Exception e)
            //{
            //    Exception exception = e;
            //    string error = exception.StackTrace + Environment.NewLine;
            //    error += exception.Message;

            //    while (exception.InnerException != null)
            //    {
            //        exception = exception.InnerException;
            //        error += exception.StackTrace + Environment.NewLine;
            //        error += exception.Message + " " + Environment.NewLine;
            //    }
            //    Log.SaveFile(error);

            //    if (SessionP2.Session.IsConnected)
            //        SessionP2.Session.Logout();
            //}
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            // Log the exception, display it, etc
            Exception exception = e.Exception;
            string error = exception.StackTrace + Environment.NewLine;
            error += exception.Message;

            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
                error += exception.StackTrace + Environment.NewLine;
                error += exception.Message + " " + Environment.NewLine;
            }
            Log.SaveFile(error);
            //if (SessionP2.Session.IsConnected)
            //    SessionP2.Session.Logout();

        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Log.SaveFile((e.ExceptionObject as Exception).Message);
            // Log the exception, display it, etc
            Exception exception = e.ExceptionObject as Exception;
            string error = exception.StackTrace + Environment.NewLine;
            error += exception.Message;

            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
                error += exception.StackTrace + Environment.NewLine;
                error += exception.Message + " " + Environment.NewLine;
            }
            Log.SaveFile(error);

            //if (SessionP2.Session.IsConnected)
            //    SessionP2.Session.Logout();

        }

        public static void SetMainForm(Form MainForm)
        {
            MainContext.MainForm = MainForm;
        }

        public static void ShowMainForm()
        {
            MainContext.MainForm.Show();
        }
    }
}