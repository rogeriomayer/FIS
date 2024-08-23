
using FMC.Solutions.NPPLUS.Library.REST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMC.Solutions.NPPLUS.Library.Class.Model
{
    public class WorkedProduct
    {
        public WorkedProduct()
        {
            DetailPayment = new DetailPayment();
            StatusLead = new StatusLead();
        }
        public Produto Produto { get; set; }
        public Type? Tipo { get; set; }
        public DetailPayment DetailPayment { get; set; }
        public bool AccountCompleted { get; set; }
        public string CodFinalizacao { get; set; }
        public string IdStatusDialer { get; set; }
        public int IdStatus { get; set; }
        public string DsStatusResum { get; set; }
        public DateTime? DataRetorno { get; set; }
        public string TelefoneRetorno { get; set; }
        public string TipoPromessa { get; set; }
        public enum Type
        {
            Promessa = 0,
            Acordo = 1,
            Outros = 3
        }
        public StatusLead StatusLead { get; set; }
    }
    public class DetailPayment
    {
        public TypePayment TipoPagamento { get; set; }
        public DateTime? DateEntrance { get; set; }
        public decimal VlEntrance { get; set; }
        public int? QtdParcel { get; set; }
        public decimal? VlParcel { get; set; }

        public enum TypePayment
        {
            AVista = 0,
            Parcelado = 1,
            SemEntrada = 2
        }
    }

}
