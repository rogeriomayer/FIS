using FMC.FIS.Business.Models;
using FMC.FIS.Business.Models.CREDZ;
using FMC.FIS.Business.Models.FIS;
using FMC.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FMC.FIS.Business.DAO
{
    public class TelegramCodeDAO : AbstractRepositoryPersistence<TelegramCode>
    {
        public TelegramCodeDAO() : base("CNN_FIS") { }


    }
}
