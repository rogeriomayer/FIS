using System;
using System.Collections.Generic;

namespace FMC.FIS.Business.Models.Customer
{
    public class StatusLeadRequest
    {
        public ICollection<int> idUser { get; set; }
        public DateTime dtIni { get; set; }
        public DateTime dtFim { get; set; }
    }
}
