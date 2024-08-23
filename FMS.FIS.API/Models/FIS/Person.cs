using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMC.FIS.API.Models.FIS
{
    [Table("Person")]
    public class Person
    {
        public Person()
        {
            Product = new HashSet<Product>();
            Phone = new HashSet<Phone>();
            Email = new HashSet<Email>();
            Address = new HashSet<Address>();
        }

        [Key]
        [Column("IdPerson", TypeName = "bigint")]
        public Int64 IdPerson { get; set; }

        [StringLength(50)]
        [Column("DsName", TypeName = "varchar")]
        public string DsName { get; set; }

        [StringLength(18)]
        [Column("NrCNPJCPF", TypeName = "varchar")]
        public string NrCNPJCPF { get; set; }

        [StringLength(10)]
        [Column("NrRG", TypeName = "varchar")]
        public string NrRG { get; set; }

        [Column("DtBirth", TypeName = "datetime")]
        public Nullable<DateTime> DtBirth { get; set; }

        [StringLength(50)]
        [Column("MotherName", TypeName = "varchar")]
        public string MotherName { get; set; }

        [Required]
        [Column("DtInsert", TypeName = "datetime")]
        public DateTime DtInsert { get; set; }

        [Column("DtUpdate", TypeName = "datetime")]
        public DateTime? DtUpdate { get; set; }

        public virtual ICollection<Product> Product { get; set; }

        public virtual ICollection<Phone> Phone { get; set; }

        public virtual ICollection<Email> Email { get; set; }

        public virtual ICollection<Address> Address { get; set; }
    }
}
