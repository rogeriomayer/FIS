using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System;
using FMC.Generic;
using FMC.FIS.Business.Models.FIS;
using FMC.FIS.Business.DAO;

namespace FMC.FIS.Business.BLL
{
    public class ShortURLBLL : BLL<ShortURL, ShortURLDAO>
    {
        public ShortURL GetByCode(string code, string ip)
        {
            var shortUrl = persistence.GetByCode(code);

            shortUrl.Access += 1;
            shortUrl.DtLastAccess = DateTime.Now;
            Update(shortUrl);

            try
            {
                new ShortAccessDAO().Add
                    (
                        new ShortAccess()
                        {
                            IdShortURL = shortUrl.IdShortURL,
                            IP = ip,
                            DtInsert = DateTime.Now
                        }
                    );
            }
            catch { }

            return shortUrl;
        }

    }
}
