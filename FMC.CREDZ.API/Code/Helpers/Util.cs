using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Util
{
    public static bool FileNameValid(string fileName)
    {
        IList<string> splitName = fileName.Split('_');
        if (splitName.Count < 2)
        {
            return false;
        }
        else if (splitName[1] != "E")
        {
            return false;
        }
        else
        {
            string data = splitName.LastOrDefault();
            string day = data.Substring(0, 2);
            string month = data.Substring(2, 2);
            string year = data.Substring(4, 4);

            return TextUtil.IsDate(year + "-" + month + "-" + day);
        }

        return false;

    }

    public static IList<byte> ReadFile(string path)
    {
        if (File.Exists(path))
        {
            MemoryStream ms;
            using (FileStream stream = File.Open(path, FileMode.Open))
            {

                byte[] buffer = new byte[stream.Length];

                using (ms = new MemoryStream())
                {
                    int read;
                    while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }

                }
            }
            return ms.ToArray();
        }
        else
            return null;
    }

}

public class UtilException : ControllerBase
{
    public IActionResult Error(Exception ex)
    {
        string error = ex.Message + Environment.NewLine + ex.StackTrace;
        while (ex.InnerException != null)
        {
            ex = ex.InnerException;
            error = ex.Message + Environment.NewLine + ex.StackTrace;
        }
        return BadRequest(ex.ToString());
    }
}

