using FMC.FIS.API.Models.FIS;
using FMC.Generic;
using System.Collections.Generic;
using System.Linq;

namespace FMC.FIS.API.Code.Business.DAO
{
    public class BilletDAO : AbstractRepositoryPersistence<Billet>
    {
        public BilletDAO() : base("CNN_FIS") { }

        public ICollection<Billet> GetByIdProduct(long idProduct)
        {
            var listBillet = Context.Where(p => p.IdProduct == idProduct).ToList();
            if (listBillet.Count == 0)
                listBillet = Context.Where(p => p.AgreementParcel.Agreement.StatusLead.Lead.IdProduct == idProduct || p.Promisse.StatusLead.Lead.IdProduct == idProduct).ToList();
            return listBillet;
        }

        public ICollection<Billet> GetByCdAgreement(string CdAgreement)
        {
            //return Context.Where(p => (p.AgreementParcel.Agreement.CdAgreement == CdAgreement) || p.Promisse.CdAgreement == CdAgreement).FirstOrDefault();
            return Context.Where(p => p.CdAgreement == CdAgreement).ToList();
        }
    }
}