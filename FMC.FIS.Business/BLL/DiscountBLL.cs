﻿using FMC.FIS.DAO;
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

namespace FMC.FIS.Business.BLL
{
    public class DiscountBLL : BLL<Discount, DiscountDAO>
    {
        public ICollection<Discount> GetByProductType(int idProdutctType)
        {
            return persistence.GetByProductType(idProdutctType);
        }

        public Discount GetDiscount(int idProdutctType, int age, int parcel)
        {
            return persistence.GetDiscount(idProdutctType, age, parcel);
        }

    }

}
