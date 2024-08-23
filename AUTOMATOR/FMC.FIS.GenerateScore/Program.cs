using FMC.FIS.Business.BLL;
using FMC.FIS.Business.Models.FIS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace FMC.FIS.GenerateScore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var startTime = DateTime.Now;


            var listProduct = new ProductBLL().GetProductByProductType(3);
            var listPredict = new List<ScoreAI>();
            Lead lead = null;
            foreach (var product in listProduct)
            {
                if (DateTime.Now.Subtract(startTime).Hours > 2)
                    break;
                try
                {
                    Console.Clear();
                    Console.WriteLine(product.DsProduct);
                    Console.WriteLine(listPredict.Count());
                    lead = product.Lead.OrderByDescending(p => p.IdLead).FirstOrDefault();
                    if (lead != null)
                    {


                        listPredict.Add(
                             new Business.Models.FIS.ScoreAI()
                             {
                                 IdProduct = product.IdProduct,
                                 Score = GenerateScore(lead),
                                 DtInsert = DateTime.Now,
                                 DtUpdate = DateTime.Now
                             }
                         );

                        if (listPredict.Count() == 500)
                        {
                            Console.WriteLine("Descarregando!!!");
                            var scoreAiBll = new ScoreAiBLL();
                            scoreAiBll.AddRangeNormal(listPredict);

                            GC.SuppressFinalize(scoreAiBll);
                            GC.SuppressFinalize(listPredict);
                            listPredict.Clear();
                            GC.Collect();
                            listPredict = new List<ScoreAI>();
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            new ScoreAiBLL().AddRangeNormal(listPredict);

        }

        public static decimal GenerateScore(Lead lead)
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
                            new FMC_FIS_ML_Score.MLGeneric.ModelInput()
                            {
                                Col0 = Convert.ToInt16(DateTime.Today.Subtract(lead.Product.Person.DtBirth.Value).TotalDays / 365),
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

