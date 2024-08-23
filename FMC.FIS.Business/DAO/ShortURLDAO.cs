using System;
using System.Collections.Generic;
using System.Linq;
using FMC.FIS.Business.Models.FIS;
using FMC.Generic;

namespace FMC.FIS.Business.DAO
{
    public class ShortURLDAO : AbstractRepositoryPersistence<ShortURL>
    {
        public ShortURLDAO() : base("CNN_FIS") { }

        public ShortURL GetByCode(string code)
        {
            return Context.Where(p => p.Code == code).OrderByDescending(p => p.IdShortURL).FirstOrDefault();
        }

        public ShortURL GetByURL(string url)
        {
            return Context.Where(p => p.URL == url).OrderByDescending(p => p.IdShortURL).FirstOrDefault();
        }

    }
}
