using FMC.FIS.API.Code.Business.DAO;
using FMC.FIS.API.Models;
using FMC.FIS.API.Models.Customer;
using FMC.FIS.DAO;
using FMC.Generic;
using System.Collections.Generic;
using System.Linq;

namespace FMC.FIS.API.Code.Business.BLL
{
    public class UserProfileBLL : BLL<UserProfile, UserProfileDAO>
    {
        public ICollection<UserProfileResponse> GetByName(IList<string> userProfile, byte idProductType)
        {
            var listUserProfile = persistence.GetByName(userProfile, idProductType);
            return listUserProfile.Select
                (
                    p => new UserProfileResponse()
                    {
                        IdUserProfile = p.IdUserProfile,
                        DsProfile = p.DsProfile,
                        IdProductType = p.IdProductType,
                        DtInsert = p.DtInsert,
                        LevelProfile = p.LevelProfile,
                        UserProfileConstantAccess = p.UserProfileConstantAccess.Select
                        (
                            u => new UserProfileConstantAccessResponse()
                            {
                                IdConstantAccess = u.IdConstantAccess,
                                IdUserProfile = u.IdUserProfile,
                                IdUserProfileConstantAccess = u.IdUserProfileConstantAccess,
                                ConstantAccess = new ConstantAccessResponse()
                                {
                                    IdConstantAccess = u.ConstantAccess.IdConstantAccess,
                                    DsConstantAccess = u.ConstantAccess.DsConstantAccess
                                }
                            }
                        ).ToList()
                    }
                ).ToList();
        }
    }
}
