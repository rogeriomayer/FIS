using System;
using System.Collections.Generic;
using System.Linq;
using FMC.FIS.Business.Models.FIS;
using FMC.Generic;

namespace FMC.FIS.Business.DAO
{
    public class ShortAccessDAO : AbstractRepositoryPersistence<ShortAccess>
    {
        public ShortAccessDAO() : base("CNN_FIS") { }

    }
}
