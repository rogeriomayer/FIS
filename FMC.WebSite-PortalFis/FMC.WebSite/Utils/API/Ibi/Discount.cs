using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMC.WebSite.FIS.Utils.API.Ibi
{
    public class Discount
    {
        public DateTime? DtInactive{get;set;}

        public int IdDiscount{get;set;}

        public int MaxAge{get;set;}

        public decimal MaxDiscount{get;set;}

        public int MaxParcel{get;set;}

        public int MinAge{get;set;}

        public int MinParcel{get;set;}
    }
}
