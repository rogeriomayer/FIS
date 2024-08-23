using FMC.FIS.API.Models;
using FMC.Generic;
using System.Collections.Generic;
using System.Linq;

namespace FMC.FIS.API.Code.Business.DAO
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
