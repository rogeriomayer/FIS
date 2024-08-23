using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMC.WebSite.Afinz.Utils.API.Nacc
{
    public class Comentario
    {
        public ICollection<string> listIdConta { get; set; }
        public string comentario { get; set; }
        public string cdUserNacc { get; set; }
    }
}
