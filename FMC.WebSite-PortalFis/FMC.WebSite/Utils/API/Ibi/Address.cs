namespace FMC.WebSite.FIS.Utils.API.Ibi
{
    using FMC.WebSite.FIS.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Address
    {
        public long IdAddress { get; set; }
        public bool FlUpdateAddress { get; set; }
        public bool FlContinue { get; set; }
        public DateTime DtInsert { get; set; }
        public long IdNavigation { get; set; }
        public Navigation Navigation { get; set; }
    }
}
