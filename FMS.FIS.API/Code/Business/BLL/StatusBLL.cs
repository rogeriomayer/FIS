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
using FMC.FIS.API.Models.FIS;
using FMC.FIS.API.Code.Business.DAO;

namespace FMC.FIS.BLL
{
    public class StatusBLL : BLL<Status, StatusDAO>
    {

        public ICollection<StatusResponse> GetByProductType(int idProdutctType)
        {
            var listStatus = persistence.GetByProductType(idProdutctType);

            return listStatus.Select(p => new StatusResponse()
            {
                IdStatus = p.IdStatus,
                DsStatus = p.DsStatus,
                CdStatus = p.CdStatus,
                FlEfective = p.FlEfective,
                FlShowUser = p.FlShowUser,
                FlCallBack = p.FlCallBack,
                IdStatusDialer = p.IdStatusDialer
            }
            ).ToList();
        }

    }

}
