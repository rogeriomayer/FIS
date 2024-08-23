using FMC.FIS.API.Models;
using FMC.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMC.FIS.DAO
{
    public class FisLoginDAO : AbstractRepositoryPersistence<UserLogin>
    {
        public FisLoginDAO() : base("CNN_FIS") { }
        public UserLogin GetByUser(string userLogin)
        {
            return Context.Where(p => p.DsUser == userLogin).FirstOrDefault();
        }

        public ICollection<UserLogin> GetByProductType(byte productType)
        {
            return Context.Where(p => p.FisUserProfile.IdProductType == productType).ToList();
        }

        public ICollection<UserLogin> GetUserIdManager(int idManager)
        {
            return Context.Where(p => p.IdManager == idManager).ToList();
        }
    }
}
