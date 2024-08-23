using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FMC.WebSite.FIS.Models
{
    public class ValidationAccess
    {
        public string Nome { get; set; }
        public string GoogleCaptchaToken { get; set; }
    }

    public class ValidationDate
    {
        public string Day { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string GoogleCaptchaToken { get; set; }

        public string Nome { get; set; }
    }
}
