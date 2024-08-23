using FMC.FIS.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMC.FIS.Model;
using FMC.FIS.API.Models;
using FMC.Generic;


namespace FMC.FIS.BLL
{
    public class BilletP1BLL : BLL<BilletP1, BilletP1DAO>
    {

        public BilletResponse GetBillet(BilletRequest billetRequest)
        {
            var url = "http://10.40.0.109/ibi/ura.svc";
            //var url = "http://localhost:34072/URA.svc";
            var billet = RestApi.Post<BilletResponse, BilletRequest>(url, "GetBilletFISP1", billetRequest, "");

            if (billet != null)
            {
                try
                {
                    Add
                      (
                          new BilletP1()
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
