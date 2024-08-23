using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMC.Solutions.NPPLUS.Library.REST.Models
{
    public class StatusResponse
    {
        public int IdStatus { get; set; }

        public string DsStatus { get; set; }

        public string CdStatus { get; set; }

        public bool FlEfective { get; set; }

        public bool FlShowUser { get; set; }

        public bool FlCallBack { get; set; }

        public string IdStatusDialer { get; set; }
    }

    public class StatusLeadRequest
    {
        public ICollection<int> idUser { get; set; }
        public DateTime dtIni { get; set; }
        public DateTime dtFim { get; set; }
    }
}
