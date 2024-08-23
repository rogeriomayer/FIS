using System;
using System.Collections.Generic;

namespace FMC.FIS.API.Models.Customer
{
    public class UserProfileResponse
    {
        public int IdUserProfile { get; set; }

        public string DsProfile { get; set; }

        public byte LevelProfile { get; set; }
        public byte IdProductType { get; set; }

        public DateTime DtInsert { get; set; }

        public ICollection<UserProfileConstantAccessResponse> UserProfileConstantAccess { get; set; }
    }

    public class UserProfileConstantAccessResponse
    {

        public int IdUserProfileConstantAccess { get; set; }

        public int IdUserProfile { get; set; }

        public int IdConstantAccess { get; set; }

        public virtual ConstantAccessResponse ConstantAccess { get; set; }

    }

    public class ConstantAccessResponse
    {
        public int IdConstantAccess { get; set; }

        public string DsConstantAccess { get; set; }

    }
}
