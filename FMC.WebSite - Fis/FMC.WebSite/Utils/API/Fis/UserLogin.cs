namespace FMC.Fis.Utils.API.Fis
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class UserLogin
    {
        public int IdUser { get; set; }
        public string User { get; set; }
        public string DsName { get; set; }
        public UserProfile Profile { get; set; }
        public DateTime? DtLastLogin { get; set; }
        public DateTime? DtAlterPass { get; set; }
        public OAuth OAuth { get; set; }
    }

    public class OAuth
    {
        public OAuth(string access_token, string expires_in)
        {
            this.access_token = access_token;
            this.expires_in = expires_in;
        }

        public string access_token { get; set; }
        public string token_type { get { return "Bearer"; } }
        public string expires_in { get; set; }
    }
}
