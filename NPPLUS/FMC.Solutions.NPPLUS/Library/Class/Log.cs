using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

public static class Log
{

    public static string pathLog = AppDomain.CurrentDomain.BaseDirectory + "LOG";

    public static void SaveFile(Exception exception)
    {
        var message = "Erro: " + exception.Message;
        while (exception.InnerException != null)
        {
            exception = exception.InnerException;
            message += "--" + exception.Message;
        }
        //if (!string.IsNullOrEmpty(dir))
        //    pathLog = Path.Combine(pathLog, dir);
        string fileName = pathLog + "\\LOG_" + DateTime.Now.ToString("ddMMyyyyHH") + ".log";

        StreamWriter logExecution = null;
        try
        {
            if (!Directory.Exists(pathLog))
                Directory.CreateDirectory(pathLog);

            using (logExecution = new StreamWriter(fileName, true, ASCIIEncoding.Default))
            {
                logExecution.WriteLine(Environment.NewLine + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " => " + message);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static void SaveFile(string message, bool save = false)
    {
        //if (!string.IsNullOrEmpty(dir))
        //    pathLog = Path.Combine(pathLog, dir);
        string fileName = pathLog + "\\LOG_" + DateTime.Now.ToString("ddMMyyyyHH") + ".log";

        StreamWriter logExecution = null;
        try
        {
            if (!Directory.Exists(pathLog))
                Directory.CreateDirectory(pathLog);

            using (logExecution = new StreamWriter(fileName, true, ASCIIEncoding.Default))
            {
                logExecution.WriteLine(Environment.NewLine + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " => " + message);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

}
