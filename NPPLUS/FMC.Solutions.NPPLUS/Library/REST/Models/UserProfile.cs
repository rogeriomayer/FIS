using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class UserProfile
{
    public int IdUserProfile { get; set; }

    public string DsProfile { get; set; }

    public byte IdProductType { get; set; }

    public DateTime DtInsert { get; set; }

    public ICollection<UserProfileConstantAccess> UserProfileConstantAccess { get; set; }
}

public class UserProfileConstantAccess
{

    public int IdUserProfileConstantAccess { get; set; }

    public int IdUserProfile { get; set; }

    public int IdConstantAccess { get; set; }

    public virtual ConstantAccess ConstantAccess { get; set; }

}

public class ConstantAccess
{
    public int IdConstantAccess { get; set; }

    public string DsConstantAccess { get; set; }

}
