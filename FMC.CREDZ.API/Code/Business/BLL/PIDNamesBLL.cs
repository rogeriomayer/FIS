using FMC.CREDZ.API.Models;
using FMC.CREDZ.DAO.Persistence;
using FMC.Generic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FMC.CREDZ.API.Code.Business.BLL
{
    public class PIDNamesBLL : BLL<PIDNames, PIDNamesDAO>
    {
        public IList<string> GetNames(string name, int count)
        {
            var rnd = new Random();
            return persistence.GetNames(name, count).OrderBy(p => rnd.Next()).Take(count).ToList();
        }
    }
}
