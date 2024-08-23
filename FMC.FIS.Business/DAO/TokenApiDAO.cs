using FMC.FIS.Business.Models;
using FMC.FIS.Business.Models.FIS;
using FMC.Generic;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FMC.FIS.Business.DAO
{
    public class TokenApiDAO : AbstractRepositoryPersistence<TokenAPI>
    {
        public TokenApiDAO() : base("CNN_FIS") { }

        public override TokenAPI GetBykey(object key)
        {
            return Context.Where(p => p.API == key.ToString()).FirstOrDefault();
        }
    }
}
