namespace FMC.Shortener.Utils.API.Shortener
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class UserLogin
    {
        public int IdUserLogin { get; set; }

        public string DsUser { get; set; }

        public string DsName { get; set; }

        public string Password { get; set; }

        public int IdUserProfile { get; set; }

        public DateTime DtInsert { get; set; }

        public DateTime? DtLastLogin { get; set; }
        public DateTime? DtAlterPass{ get; set; }

        public virtual UserProfile FisUserProfile { get; set; }
    }
}
