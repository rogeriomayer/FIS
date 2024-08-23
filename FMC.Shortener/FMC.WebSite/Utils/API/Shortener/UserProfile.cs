using System;
using System.Collections.Generic;

namespace FMC.Shortener.Utils.API.Shortener
{
    public class UserProfile
    {
        public int IdUserProfile { get; set; }
        public string DsProfile { get; set; }
        public DateTime DtInsert { get; set; }
        public virtual ICollection<UserLogin> FisLogin { get; set; }
    }
}
