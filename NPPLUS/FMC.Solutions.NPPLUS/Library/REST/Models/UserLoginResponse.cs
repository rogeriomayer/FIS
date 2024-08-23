using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class UserLoginResponse
{
    public int IdUserLogin { get; set; }

    public string DsUser { get; set; }

    public string DsName { get; set; }

    public int IdUserProfile { get; set; }

    public DateTime DtInsert { get; set; }

    public DateTime? DtLastLogin { get; set; }

    public DateTime? DtAlterPass { get; set; }

    public DateTime? DtBlock { get; set; }

    public int? InvalidPass { get; set; }

}
