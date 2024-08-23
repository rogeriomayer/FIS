using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMC.Fis.Controllers.Class
{
    public class VerifyOperation
    {

        public static bool ValidOperation()
        {
            //if (DateTime.Today.DayOfWeek == DayOfWeek.Saturday)
            //{
                if (DateTime.Now > Convert.ToDateTime("01:30") && DateTime.Now < Convert.ToDateTime("22:30"))
                {
                    return true;
                }
            //}
            //else if (DateTime.Today.DayOfWeek != DayOfWeek.Saturday && DateTime.Today.DayOfWeek != DayOfWeek.Sunday)
            //{
            //    if (DateTime.Now > Convert.ToDateTime("08:00") && DateTime.Now < Convert.ToDateTime("22:30"))
            //    {
            //        return true;
            //    }
            //}
                return false;
            //return RedirectToAction("Encerrado", "ConsultaCpfCnpj");
        }
    }
}
