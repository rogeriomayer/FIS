using FMC.FIS.API.Models.FIS;
using FMC.Generic;
using System.Linq;

namespace FMC.FIS.API.Code.Business.DAO
{
    public class AgreementDAO : AbstractRepositoryPersistence<Agreement>
    {
        public AgreementDAO() : base("CNN_FIS") { }


    }
}