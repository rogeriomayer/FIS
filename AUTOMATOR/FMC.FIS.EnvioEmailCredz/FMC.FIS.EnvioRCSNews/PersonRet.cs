using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMC.FIS.EnvioRCSNews
{
    public class PersonRet
    {
        [Key]
        public long IdPerson { get; set; }
        public long idproduct { get; set; }
        public string DsName { get; set; }
        public int Age { get; set; }

        public long IdContract { get; set; }
        public string Store { get; set; }
        public string contato { get; set; }
    }
}
