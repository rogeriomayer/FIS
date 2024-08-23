using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FMC.Shortener.Utils
{
    public static class Console
    {
        public static IHttpContextAccessor _contextAccessor;
        public static string scriptTag = "<script type=\"\" language=\"\">{0}</script>";
    
        public static void Log(string message)
        {
            string function = "console.log('{0}');";
            string log = string.Format(GenerateCodeFromFunction(function), message);
            new HttpContextAccessor().HttpContext.Response.WriteAsync(log);
        }

        static string GenerateCodeFromFunction(string function)
        {
            return string.Format(scriptTag, function);
        }
    }
}
