using FMC.FIS.API.Models.FIS;
using FMC.Generic;
using System.Linq;

namespace FMC.FIS.API.Code.Business.DAO
{
    public class LeadDAO : AbstractRepositoryPersistence<Lead>
    {
        public LeadDAO() : base("CNN_FIS") { }

        public Lead GetByCPF(string cpf, byte productType)
        {
            return Context.Where(p => p.Product.Person.NrCNPJCPF == cpf && p.Product.IdProductType == productType).OrderByDescending(p => p.DtInsert).FirstOrDefault();
        }
    }
}