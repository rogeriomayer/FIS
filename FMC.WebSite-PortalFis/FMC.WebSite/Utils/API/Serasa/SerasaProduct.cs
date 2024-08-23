namespace FMC.WebSite.FIS.Utils.API.Ibi
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;

    public class SerasaProduct
    {
        public long IdSerasaProduct { get; set; }
        public long IdSerasaNavigation { get; set; }
        public int IdSystem { get; set; }
        public string DsProduct { get; set; }
        public string Account { get; set; }
        public int Age { get; set; }
        public decimal VlFull { get; set; }
        public decimal VlMinimum { get; set; }
        public DateTime DtInsert { get; set; }
        public SerasaNavigation SerasaNavigation { get; set; }
        public FmcSystem FMCSystem { get; set; }
        public SerasaSimulate SerasaSimulate { get; set; }
        public SerasaAgreement SerasaAgreement { get; set; }
    }
}
