using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMC.Solutions.NPPLUS.Library.REST.Models
{
    public class Pause
    {
        public int IdPause { get; set; }

        public int IdAytyPause { get; set; }

        public string DsPause { get; set; }

        public DateTime DtInsert { get; set; }

        public Nullable<DateTime> DtInactive { get; set; }
    }
}
