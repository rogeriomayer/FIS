namespace FMC.FIS.API.Models
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.DirectoryServices.Protocols;

    [Table("UserLogin")]
    public class UserLogin
    {
        [Key]
        [Column("IdUserLogin", TypeName = "int")]
        public int IdUserLogin { get; set; }

        [Column("DsUser", TypeName = "varchar")]
        public string DsUser { get; set; }

        [Column("DsName", TypeName = "varchar")]
        public string DsName { get; set; }

        [Column("Password", TypeName = "varchar")]
        public string Password { get; set; }

        [Column("IdUserProfile", TypeName = "int")]
        public int IdUserProfile { get; set; }

        [Column("DtInsert", TypeName = "datetime")]
        public DateTime DtInsert { get; set; }

        [Column("DtLastLogin", TypeName = "datetime")]
        public DateTime? DtLastLogin { get; set; }

        [Column("DtAlterPass", TypeName = "datetime")]
        public DateTime? DtAlterPass { get; set; }

        [Column("DtBlock", TypeName = "datetime")]
        public DateTime? DtBlock { get; set; }

        [Column("InvalidPass", TypeName = "int")]
        public int? InvalidPass { get; set; }

        [Column("IdManager", TypeName = "int")]
        public int? IdManager { get; set; }

        [Column("FlLoginDialer", TypeName = "bit")]
        public bool FlLoginDialer { get; set; }

        [ForeignKey("IdUserProfile")]
        public virtual UserProfile FisUserProfile { get; set; }

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

    public class UserAuthenticate
    {
        public int IdUser { get; set; }
        public string User { get; set; }
        public string DsName { get; set; }
        public bool FlLoginDialer { get; set; }
        public UserProfileAuthenticate Profile { get; set; }
        public DateTime? DtLastLogin { get; set; }
        public DateTime? DtAlterPass { get; set; }
        public OAuth OAuth { get; set; }
    }

    public class UserProfileAuthenticate
    {
        public UserProfileAuthenticate()
        {
            UserProfileConstantAccess = new HashSet<UserProfileConstantAccessAuth>();
        }

        public int IdUserProfile { get; set; }

        public string DsProfile { get; set; }

        public byte IdProductType { get; set; }

        public byte LevelProfile { get; set; }

        public DateTime DtInsert { get; set; }

        public virtual ICollection<UserProfileConstantAccessAuth> UserProfileConstantAccess { get; set; }
    }

    public class UserProfileConstantAccessAuth
    {

        public int IdUserProfileConstantAccess { get; set; }

        public int IdUserProfile { get; set; }

        public int IdConstantAccess { get; set; }

        public virtual ConstantAccessAuth ConstantAccess { get; set; }

    }

    public class ConstantAccessAuth
    {
        public int IdConstantAccess { get; set; }

        public string DsConstantAccess { get; set; }

    }



    public class LoginIDP
    {
        public string User { get; set; }
        public string DsName { get; set; }
        public ICollection<string> Profiles { get; set; }
        public byte idProductType { get; set; }
    }
}
