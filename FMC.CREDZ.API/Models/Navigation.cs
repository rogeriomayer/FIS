namespace FMC.CREDZ.API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;


    [Table("Navigation")]
    public class Navigation
    {
        public Navigation()
        {
            //Product = new HashSet<Product>();
            //Address = new HashSet<Address>();
            //Billet = new HashSet<Billet>();
            //BilletIBI = new HashSet<BilletIBI>();
        }

        [Key]
        [Column("IdNavigation", TypeName = "bigint")]
        public long IdNavigation { get; set; }

        [Column("CPF", TypeName = "varchar")]
        public string Cpf { get; set; }

        [Column("FlNegotiateNow", TypeName = "bit")]
        public bool FlNegotiateNow { get; set; }

        [Column("DsOrigem", TypeName = "varchar")]
        public string DsOrigem { get; set; }

        [Column("DtInsert", TypeName = "datetime")]
        public DateTime DtInsert { get; set; }

        [Column("Redirect", TypeName = "char")]
        public string Redirect { get; set; }

        [Column("CdFrom", TypeName = "char")]
        public string CdFrom { get; set; }
        

        //public virtual ICollection<Product> Product { get; set; }

        //public virtual ICollection<Address> Address { get; set; }

        //public virtual ICollection<Billet> Billet { get; set; }

        //public virtual ICollection<BilletIBI> BilletIBI { get; set; }


    }
}
