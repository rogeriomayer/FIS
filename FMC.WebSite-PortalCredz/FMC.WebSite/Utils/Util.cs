using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FMC.WebSite.FIS.Utils
{
    public class Util
    {

        public static bool IsEmail(string email)
        {
            try
            {
                MailAddress mail = new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsNumeric(string s)
        {
            float output;
            return float.TryParse(s, out output);
        }

        public static bool IsDate(string s)
        {
            DateTime output;
            return DateTime.TryParse(s, out output);
        }

        public static DateTime GetDateString(string message)
        {
            try
            {
                Match match = Regex.Match(message, @"\d{2}\/\d{2}\/\d{4}");
                string date = match.Value;
                if (!string.IsNullOrEmpty(date))
                {
                    var dateTime = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.CurrentCulture);
                    return dateTime;
                }
                else
                    return DateTime.Today.AddDays(4);
            }
            catch
            {
                return DateTime.Today.AddDays(4);
            }
        }

        public static AgreementAdd CreateAgreementAdd(AcordoCredz acordoCredz)
        {
            var entrada = acordoCredz.parcelas_novas.Where(p => p.parcela == "1").FirstOrDefault();
            if (entrada == null)
                entrada = new ParcelasNovaCredz()
                {
                    parcela = "1",
                    vencimento = acordoCredz.data,
                    valor = acordoCredz.total
                };

            return new AgreementAdd()
            {
                VlEntrace = entrada.valor,
                DtEntrace = entrada.vencimento,
                PcDiscount = acordoCredz.desconto_principal + acordoCredz.desconto_honorario + acordoCredz.desconto_juros + acordoCredz.desconto_multa,
                QtParcel = acordoCredz.parcelas_novas.Count > 0 ? acordoCredz.parcelas_novas.Count - 1 : 0,
                VlParcel = acordoCredz.parcelas_novas.Where(p => p.parcela == "2").Count() > 0 ? acordoCredz.parcelas_novas.Where(p => p.parcela == "2").FirstOrDefault().valor : 0,
                VlAgreement = acordoCredz.total,
                IdAgreementStatus = acordoCredz.status_id,
                CdPaymentOption = "Boleto",
                CdParcelPlan = "",
                CdAgreement = acordoCredz.id.ToString(),
                DtInsert = DateTime.Now,
                AgreementParcel =
                acordoCredz.parcelas_novas.Count() > 0 ?
                acordoCredz.parcelas_novas.Select(pn => new AgreementParcelAdd()
                {
                    NrParcel = Convert.ToInt32(pn.parcela) - 1,
                    DtParcel = pn.vencimento,
                    VlParcel = pn.valor
                }).ToList()
                :
                new List<AgreementParcelAdd>()
                {
                    new AgreementParcelAdd()
                    {
                        NrParcel = 0,
                        DtParcel = acordoCredz.data,
                        VlParcel = acordoCredz.total
                    }
                }
            };
        }

    }
}
