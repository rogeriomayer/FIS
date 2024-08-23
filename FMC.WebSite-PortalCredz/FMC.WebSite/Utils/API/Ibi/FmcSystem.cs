namespace FMC.WebSite.FIS.Utils.API.Ibi
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class FmcSystem
    {
        public int IdSystem { get; set; }
        public string System { get; set; }
        public ICollection<Product> Product { get; set; }
    }
}
