namespace FMC.WebSite.FIS.Utils.API.Ibi
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class SerasaBillet
    {
        public long IdSerasaBillet { get; set; }
        public long IdSerasaNavigation { get; set; }
        public string IdAgrement { get; set; }
        public bool FlPrint { get; set; }
        public DateTime DtInsert { get; set; }
        public SerasaNavigation SerasaNavigation { get; set; }
    }
}
