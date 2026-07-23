using FMC.FIS.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMC.FIS.Model;
using FMC.Generic;
using FMC.FIS.Business.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using FMC.FIS.Business.Models.FIS;
using FMC.FIS.Business.DAO;
using FMC.FIS.Business.Models.CREDZ;

namespace FMC.FIS.BLL
{
    public class SendRcsBLL : BLL<SendRCS, SendRcsDAO>
    {
        public IList<SendRCS> GetSendToday()
        {
            return persistence.GetSendToday();
        }
    }
}
