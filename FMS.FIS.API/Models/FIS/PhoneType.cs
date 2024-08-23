using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMC.FIS.API.Models.FIS
{
    [Table("PhoneType")]
    public class PhoneType
    {
        public PhoneType()
        {
            Phone = new HashSet<Phone>();
        }

        [Key]
        public Int32 IdPhoneType { get; set; }

        [MaxLength(50)]
        public string DsPhoneType { get; set; }

        public virtual ICollection<Phone> Phone { get; set; }
    }
}
