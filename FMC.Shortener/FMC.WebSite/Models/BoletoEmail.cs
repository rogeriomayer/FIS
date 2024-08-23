using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FMC.Shortener.Models
{
    public class BoletoEmail
    {
        public string Email{ get; set; }
        public string EmailAgain{ get; set; }
        public string Card { get; set; }
    }
}
