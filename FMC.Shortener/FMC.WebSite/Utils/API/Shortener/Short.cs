using System;
using System.Collections.Generic;

namespace FMC.Shortener.Utils.API.Shortener
{
    public class Short
    {
        public Int64 IdShortURL { get; set; }
        public int Access { get; set; }
        public string Code{ get; set; }
        public DateTime DtInsert { get; set; }
        public DateTime? DtLastAccess { get; set; }
        public string URL { get; set; }
    }
}
