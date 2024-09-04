using FMC.FIS.Business.BLL;
using FMC.FIS.Business.Models.FIS;
using FMC_FIS_EnvioEmailCredz1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMC.FIS.GenerateScore
{
    internal class Generate : IDisposable
    {
        
        
        ~Generate()
        {
            Dispose();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public decimal GenerateScore(Lead lead)
        {
            try
            {

                if (lead != null && lead.Product.Person.Address != null && lead.Product.Person.Address.Count > 0)
                {
                    var address = lead.Product.Person.Address.OrderByDescending(p => p.DtInsert).FirstOrDefault();


                    Agreement lastAgreement = null;
                    if (lead.Product.Lead.Where(p => p.StatusLead.Count() > 0).Any())
                    {
                        foreach (var l in lead.Product.Lead.Where(p => p.StatusLead.Count > 0).ToList())
                        {
                            if (lastAgreement == null || lastAgreement.DtInsert < l.StatusLead.FirstOrDefault().DtInsert)
                                if (l.StatusLead.FirstOrDefault().Agreement != null)
                                    lastAgreement = l.StatusLead.FirstOrDefault().Agreement.FirstOrDefault();

                        }
                    }
                    return new ScoreAiBLL().GetSingleScore
                        (
                            new MLGeneric.ModelInput()
                            {
                                Col0 = Convert.ToInt16(DateTime.Today.Subtract(lead.Product.Person.DtBirth.Value).TotalDays / 365).ToString(),
                                Col1 = (float)(lead.Product.IdProductSpecification != null ? lead.Product.IdProductSpecification : 0),
                                Col2 = address.DsCity,
                                Col3 = address.DsUF,
                                Col4 = (float)Convert.ToInt64(address.DsCep.Replace("-", "").Replace(".", "").Replace(" ", "")),
                                Col5 = lead.Age,
                                Col6 = (float)lead.DebitBalance,
                                Col7 = lastAgreement != null ? lastAgreement.DtInsert.Day : 0,
                                Col8 = lastAgreement != null ? lastAgreement.DtEntrace.Day : 0
                            }
                        );

                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}