namespace FMC.WebSite.FIS.Utils.API.Ibi
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class BilletIBI
    {
        public long IdBilletIBI { get; set; }
        public long IdNavigation { get; set; }
        public decimal VlBillet { get; set; }
        public string NrAccount { get; set; }
        public string CodeBar { get; set; }
        public int Age { get; set; }
        public string Email{ get; set; }
        public DateTime DtInsert { get; set; }
    }
}
