using System;
using System.Collections.Generic;

namespace FMC.Fis.Utils.API.Fis
{
    public class UserProfile
    {
        public int IdUserProfile { get; set; }

        public string DsProfile { get; set; }

        public byte IdProductType { get; set; }

        public byte LevelProfile { get; set; }

        public DateTime DtInsert { get; set; }

    }
}
