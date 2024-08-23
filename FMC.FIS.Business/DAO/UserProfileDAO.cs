using FMC.FIS.Business.Models;
using FMC.Generic;
using System.Collections.Generic;
using System.Linq;

namespace FMC.FIS.Business.DAO
{
    public class UserProfileDAO : AbstractRepositoryPersistence<UserProfile>
    {
        public UserProfileDAO() : base("CNN_FIS") { }

        public ICollection<UserProfile> GetByName(IList<string> userProfile, byte idProductType)
        {
            return Context.Where(p => userProfile.Contains(p.DsProfile) && p.IdProductType == idProductType).ToList();
        }
    }
}
