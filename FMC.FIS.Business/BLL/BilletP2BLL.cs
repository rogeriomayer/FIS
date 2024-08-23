using FMC.FIS.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMC.FIS.Model;
using FMC.FIS.Business.Models;
using FMC.Generic;
using FMC.FIS.Business.Models.Boleto;

namespace FMC.FIS.BLL
{
    public class BilletP2BLL : BLL<BilletP2, BilletP2DAO>
    {

        public BilletResponse GetBillet(BilletRequest billetRequest)
        {
            var url = "http://10.40.0.110/ibi/ura.svc";
            //var url = "http://localhost:34072/URA.svc";

            if (!billetRequest.Account.StartsWith("000"))
                billetRequest.Account = "000" + billetRequest.Account;

            BilletResponse billet = null;
            if (billetRequest.Account.Length == 19 && billetRequest.Account.EndsWith("000"))
                billet = RestApi.Post<BilletResponse, BilletRequest>(url, "GetBilletFISP2", billetRequest, "");

            if (billet != null)
            {
                try
                {
                    Add
                      (
                          new BilletP2()
                          {
                              CPF = billetRequest.CPF,
                              Account = billetRequest.Account,
                              Age = 0,
                              CodeBar = billet.CodeBar,
                              VlBillet = billetRequest.Value,
                              Email = null,
                              DtInsert = DateTime.Now
                          }
                      );
                }
                finally { }
            }

            return billet;
        }




    }
}


