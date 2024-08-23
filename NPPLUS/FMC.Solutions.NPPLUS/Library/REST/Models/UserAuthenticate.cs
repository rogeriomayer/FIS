using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class UserAuthenticate
{
    public int IdUser { get; set; }
    public string User { get; set; }
    public string DsName { get; set; }
    public bool FlLoginDialer { get; set; }
    public UserProfile Profile { get; set; }
    public DateTime? DtLastLogin { get; set; }
    public DateTime? DtAlterPass { get; set; }
    public OAuth OAuth { get; set; }
}

public class OAuth
{
    public string access_token { get; set; }
    public string token_type { get; set; }
    public string expires_in { get; set; }
}
