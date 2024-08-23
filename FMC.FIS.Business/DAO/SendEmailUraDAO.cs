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
    public class SendEmailUraDAO : AbstractRepositoryPersistence<SendEmailUra>
    {
        public SendEmailUraDAO() : base("CNN_FIS") { }


    }
}
