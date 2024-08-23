using FMC.FIS.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMC.FIS.Model;
using FMC.Generic;
using FMC.FIS.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using FMC.FIS.API.Code.Business.BLL;
using FMC.FIS.API.Models.Customer;

namespace FMC.FIS.BLL
{
    public class FisLoginBLL : BLL<UserLogin, FisLoginDAO>
    {
        public UserAuthenticate Login(LoginIDP user)
        {
            var idProfile = GetUserProvider(user.Profiles.ToList(), user.idProductType);
            if (idProfile > 0)
            {
                var userLogin = persistence.GetByUser(user.User);

                if (userLogin == null)
                {
                    var newUser = new UserLogin()
                    {
                        DsUser = user.User,
                        DsName = user.DsName,
                        Password = "xxx",
                        IdUserProfile = idProfile,
                        DtInsert = DateTime.Now,
                        DtLastLogin = DateTime.Now,
                        DtAlterPass = DateTime.Now,
                        DtBlock = null
                    };

                    userLogin = persistence.Add(newUser);
                }
                else
                {

                    if (userLogin.DtBlock.HasValue)
                        throw new Exception("USUÁRIO BLOQUEADO");

                    userLogin.IdUserProfile = idProfile;
                    userLogin.DtLastLogin = DateTime.Now;
                    userLogin.DtBlock = null;
                    Update(userLogin);
                }
                return Authenticate(userLogin.IdUserLogin);
            }
            else
                throw new Exception("USUÁRIO SEM PERFIL PARA ACESSO AO NPPLUS");
        }

        private int GetUserProvider(IList<string> userProfile, byte idProductType)
        {
            var profiles = new UserProfileBLL().GetByName(userProfile, idProductType);
            if (profiles != null && profiles.Count() > 0)
            {
                return profiles.OrderBy(p => p.LevelProfile).FirstOrDefault().IdUserProfile;
            }
            else
            {
                return 0;
            }
        }

        public UserAuthenticate Login(string user, string pass)
        {
            var userLogin = persistence.GetByUser(user);
            if (userLogin != null)
            {
                if (userLogin.DtBlock.HasValue)
                    throw new Exception("USUÁRIO BLOQUEADO");

                if (TextUtil.GenerateCriptografy(pass) == userLogin.Password)
                {
                    userLogin.DtLastLogin = DateTime.Now;
                    userLogin.DtBlock = null;
                    Update(userLogin);
                    return Authenticate(userLogin.IdUserLogin);
                }
                else
                {
                    if (userLogin.InvalidPass.HasValue)
                        userLogin.InvalidPass++;
                    else
                        userLogin.InvalidPass = 1;
                    if (userLogin.InvalidPass > 3)
                        userLogin.DtBlock = DateTime.Now;

                    Update(userLogin);

                    throw new Exception("SENHA INVÁLIDA");
                }
            }
            else
            {
                throw new Exception("USUÁRIO INVÁLIDO");
            }
        }


        public override UserLogin Add(UserLogin fisLogin)
        {
            if (fisLogin.DsName.Length > 3 && fisLogin.DsUser.Length > 6 && fisLogin.IdUserProfile > 0)
            {
                fisLogin.Password = TextUtil.GenerateCriptografy(fisLogin.Password);
                fisLogin.DtInsert = DateTime.Now;
                return persistence.Add(fisLogin);
            }
            else
                throw new Exception("Dados inválidos!");
        }

        public bool ChangePassword(string user, string pass)
        {
            var userLogin = persistence.GetByUser(user);
            if (userLogin != null)
            {
                userLogin.Password = TextUtil.GenerateCriptografy(pass);
                userLogin.DtAlterPass = DateTime.Now;
                Update(userLogin);
                return true;
            }
            else return false;
        }

        public override UserLogin UpdateNoContext(UserLogin model)
        {
            model.Password = TextUtil.GenerateCriptografy(model.Password);
            return base.UpdateNoContext(model);
        }

        private UserAuthenticate Authenticate(int idUserLogin)
        {
            UserLogin userLogin = new FisLoginDAO().GetBykey(idUserLogin);
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(Constants.Jwt);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Sid , userLogin.IdUserLogin.ToString()),
                    new Claim(ClaimTypes.Name, userLogin.DsUser.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(Constants.TokenExpires),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return new UserAuthenticate()
            {
                IdUser = userLogin.IdUserLogin,
                User = userLogin.DsUser,
                DsName = userLogin.DsName,
                FlLoginDialer = userLogin.FlLoginDialer,
                Profile = new UserProfileAuthenticate()
                {
                    IdUserProfile = userLogin.FisUserProfile.IdUserProfile,
                    DsProfile = userLogin.FisUserProfile.DsProfile,
                    IdProductType = userLogin.FisUserProfile.IdProductType,
                    LevelProfile = userLogin.FisUserProfile.LevelProfile,
                    DtInsert = userLogin.FisUserProfile.DtInsert,
                    UserProfileConstantAccess = userLogin.FisUserProfile.UserProfileConstantAccess
                    .Select
                    (
                        p => new UserProfileConstantAccessAuth()
                        {
                            IdUserProfile = p.IdUserProfile,
                            IdConstantAccess = p.IdConstantAccess,
                            IdUserProfileConstantAccess = p.IdUserProfileConstantAccess,
                            ConstantAccess = new ConstantAccessAuth()
                            {
                                IdConstantAccess = p.ConstantAccess.IdConstantAccess,
                                DsConstantAccess = p.ConstantAccess.DsConstantAccess
                            }
                        }
                    ).ToList()
                }
                ,
                DtAlterPass = userLogin.DtAlterPass,
                DtLastLogin = userLogin.DtLastLogin,
                OAuth = new OAuth(tokenHandler.WriteToken(token), Constants.TokenExpires.ToString())
            };
        }

        public ICollection<UserLoginResponse> GetByProductType(byte productType)
        {
            var listUser = persistence.GetByProductType(productType);
            return listUser.Select
                (
                    p => new UserLoginResponse()
                    {
                        IdUserLogin = p.IdUserLogin,
                        DsUser = p.DsUser,
                        DsName = p.DsName,
                        IdUserProfile = p.IdUserProfile,
                        DtInsert = p.DtInsert,
                        DtLastLogin = p.DtLastLogin,
                        DtAlterPass = p.DtAlterPass,
                        DtBlock = p.DtBlock,
                        InvalidPass = p.InvalidPass
                    }
                ).ToList();
        }

        public ICollection<UserLoginResponse> GetUserIdManager(int idManager)
        {
            var listUser = persistence.GetUserIdManager(idManager);
            return listUser.Select
                (
                    p => new UserLoginResponse()
                    {
                        IdUserLogin = p.IdUserLogin,
                        DsUser = p.DsUser,
                        DsName = p.DsName,
                        IdUserProfile = p.IdUserProfile,
                        DtInsert = p.DtInsert,
                        DtLastLogin = p.DtLastLogin,
                        DtAlterPass = p.DtAlterPass,
                        DtBlock = p.DtBlock,
                        InvalidPass = p.InvalidPass
                    }
                ).ToList();
        }
    }

}
