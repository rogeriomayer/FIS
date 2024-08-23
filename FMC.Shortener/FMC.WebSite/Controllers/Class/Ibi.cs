using FMC.Fis.Models;
using FMC.Fis.Utils;
using FMC.Fis.Utils.API.Ibi;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FMC.Fis.Controllers.Class
{
    public class Ibi
    {


        //public void AccountIBI(Produto _produto, IbiService.UR84.UR84Detail ibiCard, IbiService.AgreementSimulate parcelamento, SimulaParcelamento simulaParcelamento, IList<IbiService.Logo> logomarca, IList<string> message, Cache cache)
        public void AccountIBI(Produto _produto, UR84.UR84Detail ibiCard, AgreementSimulate parcelamento, SimulaParcelamento simulaParcelamento, IList<Logo> logomarca, IList<string> message, CacheSession cache)
        {

            simulaParcelamento = cache.Get<SimulaParcelamento>("simulaParcelamento");
            if (Convert.ToInt32(ibiCard.DiasAtraso) > AppSettings.MaxPromisse) //>56
            {
                Acordo(ibiCard, parcelamento, simulaParcelamento, logomarca, message, cache);
            }
            else
            {
                Promessa(ibiCard, simulaParcelamento, message, cache);
            }
        }

        /*
         * ACORDO 
         */
        //public void Acordo(IbiService.UR84.UR84Detail ibiCard, IbiService.AgreementSimulate parcelamento, SimulaParcelamento simulaParcelamento, IList<IbiService.Logo> logomarca, IList<string> message, Cache cache)
        public void Acordo(UR84.UR84Detail ibiCard, AgreementSimulate parcelamento, SimulaParcelamento simulaParcelamento, IList<Logo> logomarca, IList<string> message, CacheSession cache)
        {
            //IbiService.URAClient uraClient = new IbiService.URAClient();
            //IbiService.Discount discount = new IbiService.Discount();
            //IbiService.UR86 ur86 = uraClient.GetUR86Async(ibiCard.NumeroCartao, "1").Result;


            Discount discount = new Discount();
            UR86 ur86 = HttpHelper.GET<UR86>(Utils.API.Ibi.Uri.GetUR86(ibiCard.NumeroCartao, "1"));

            string vlEntrada = (simulaParcelamento != null && simulaParcelamento.Entrada != null) ? simulaParcelamento.Entrada : "0";
            string dataEntrada = (simulaParcelamento != null && simulaParcelamento.DataEntrada != null) ? simulaParcelamento.DataEntrada : DateTime.Now.ToString("dd/MM/yyyy");

            vlEntrada = Regex.Replace(vlEntrada, @"\D", "");
            //double vlEntradaMinimo = Convert.ToDouble(ibiCard.SaldoDevedorAtraso) * (AppSettings.PercentageEntranceParcel * 0.01);
            double vlEntradaMinimo = AppSettings.ValueEntranceParcel;

            //if (Convert.ToDouble(vlEntrada) > 0 && Convert.ToDouble(vlEntrada) / 100 < vlEntradaMinimo)
            if (Convert.ToDouble(vlEntrada) > 0 && Convert.ToDouble(vlEntrada) / 100 < vlEntradaMinimo)
            {
                message.Add("O valor da entrada não pode ser inferior a R$ " + vlEntradaMinimo.ToString("N2") + " por este motivo ele foi ajustado para a entrada mínima.");
                vlEntrada = Regex.Replace(vlEntradaMinimo.ToString(), @"\D", "");
                simulaParcelamento.Entrada = vlEntradaMinimo.ToString("N2");
            }

            if (Convert.ToDouble(vlEntrada) > 0 && Convert.ToDouble(vlEntrada) / 100 > Convert.ToDouble(ibiCard.SaldoDevedorAtraso))
            {
                message.Add("O valor da entrada não pode ser superior a R$ " + ibiCard.SaldoDevedorAtraso.ToString("N2") + " por este motivo ele foi ajustado para a entrada mínima.");
                vlEntrada = Regex.Replace(vlEntradaMinimo.ToString(), @"\D", "");
                simulaParcelamento.Entrada = vlEntradaMinimo.ToString("N2");
            }

            DateTime dateEntrance = IbiAcordoValidaDataEntrada(dataEntrada, vlEntrada, DateTime.Now.Date, simulaParcelamento, message, cache);

            simulaParcelamento = cache.Get<SimulaParcelamento>("simulaParcelamento");

            if (simulaParcelamento != null)
                //    discount = uraClient.GetDiscountAsync(ibiCard.DiasAtraso, simulaParcelamento.Parcela.ToString()).Result;
                discount = HttpHelper.GET<Discount>(Utils.API.Ibi.Uri.GetDiscount(ibiCard.DiasAtraso, simulaParcelamento.Parcela.ToString()));


            if (discount == null)
                //    discount = new IbiService.Discount();
                discount = new Discount();

            //a vista e <=360 -> 2
            string codIdent = (Convert.ToInt32(ibiCard.DiasAtraso) <= 360 && (simulaParcelamento == null || simulaParcelamento.Parcela == 0)) ? AppSettings.JurosAvistaIBI.ToString() : "0";
            codIdent = "0";
            //parcelamento = uraClient.GetParcelamentoAsync(ur86.Detail.FirstOrDefault().NumeroConta, (Convert.ToDouble(vlEntrada) / 100).ToString(), discount.MaxDiscount.ToString(), dateEntrance.ToString("yyyy-MM-dd"), codIdent).Result;
            parcelamento = HttpHelper.GET<AgreementSimulate>(Utils.API.Ibi.Uri.GetParcelamento(ur86.Detail.FirstOrDefault().NumeroConta, (Convert.ToDouble(vlEntrada) / 100).ToString(), discount.MaxDiscount.ToString(), dateEntrance.ToString("yyyy-MM-dd"), codIdent));
            cache.AddCache("parcelamento", parcelamento);

            #region Inserir Log Simulate [Acordo]
            Produto _produto = cache.Get<Produto>("produto");
            //IList<IbiService.WebSiteProduct> listWsProduct = cache.Get<List<IbiService.WebSiteProduct>>("WebSiteProduct");
            //IbiService.WebSiteProduct wsProduct = listWsProduct.Where(p => p.DsProduct == _produto.CodProduto).FirstOrDefault();

            IList<WebSiteProduct> listWsProduct = cache.Get<List<WebSiteProduct>>("WebSiteProduct");
            WebSiteProduct wsProduct = listWsProduct.Where(p => p.DsProduct == _produto.CodProduto).FirstOrDefault();

            var parcelOption = parcelamento.ParcelOption.FirstOrDefault();
            decimal vlAVista = 0;
            DateTime dtFirstParcel = DateTime.Today.AddDays(15);
            if (parcelamento != null && parcelOption != null)
            {
                vlAVista = parcelOption.ValueParcel;
                dtFirstParcel = Convert.ToDateTime(parcelamento.DateFirstParcel);
            }
            decimal VlEntraceLog = (simulaParcelamento == null || Convert.ToDecimal(simulaParcelamento.Entrada) == 0) ? vlAVista : Convert.ToDecimal(simulaParcelamento.Entrada);

            //var wsSimulate = new IbiService.WebSiteSimulate()
            var wsSimulate = new WebSiteSimulate()
            {
                IdWebSiteProduct = wsProduct.IdWebSiteProduct,
                IdAccount = _produto.IdAccount,
                VlEntrace = VlEntraceLog,
                DtEntrace = Convert.ToDateTime(simulaParcelamento.DataEntrada),
                NrParcel = Convert.ToInt32(simulaParcelamento.Parcela),
                FlPromisse = false,
                VlDesconto = discount.MaxDiscount,
                DtFirstParcel = dtFirstParcel,
                DtInsert = DateTime.Now
            };
            //var webSiteSimulate = uraClient.SetWebSiteSimulateAsync(wsSimulate).Result;
            var webSiteSimulate = HttpHelper.POST<WebSiteSimulate>(Utils.API.Ibi.Uri.SetWebSiteSimulate(), wsSimulate);
            cache.AddCache("WebSiteSimulate", webSiteSimulate);
            #endregion Inserir Log Simulate [Acordo]
        }

        //public void Promessa(IbiService.UR84.UR84Detail ibiCard, SimulaParcelamento simulaParcelamento, IList<string> message, Cache cache)
        public void Promessa(UR84.UR84Detail ibiCard, SimulaParcelamento simulaParcelamento, IList<string> message, CacheSession cache)
        {
            //IbiService.URAClient uraClient = new IbiService.URAClient();
            if (simulaParcelamento == null) simulaParcelamento = new SimulaParcelamento();
            string vlEntrada = (simulaParcelamento != null && simulaParcelamento.Entrada != null) ? simulaParcelamento.Entrada : "0";
            string dataEntrada = (simulaParcelamento != null && simulaParcelamento.DataEntrada != null) ? simulaParcelamento.DataEntrada : DateTime.Now.ToString("dd/MM/yyyy");

            vlEntrada = Regex.Replace(vlEntrada, @"\D", "");
            double vlEntradaMinimo = Convert.ToDouble(ibiCard.PagamentoMinimo);
            double vlSaldoDevedor = Convert.ToDouble(ibiCard.SaldoDevedorAtraso);

            if (Convert.ToDouble(vlEntrada) > 0 && Convert.ToDouble(vlEntrada) / 100 < vlEntradaMinimo)
            {
                message.Add("O valor para pagamento não pode ser inferior a R$ " + vlEntradaMinimo.ToString("N2") + " por este motivo ele foi ajustado para o valor mínimo da fatura.");
                vlEntrada = Regex.Replace(vlEntradaMinimo.ToString(), @"\D", "");
                simulaParcelamento.Entrada = vlEntradaMinimo.ToString("N2");
            }

            if (Convert.ToDouble(vlEntrada) > 0 && Convert.ToDouble(vlEntrada) / 100 > vlSaldoDevedor)
            {
                message.Add("O valor para pagamento não pode ser superior a R$ " + vlSaldoDevedor.ToString("N2") + " por este motivo ele foi ajustado para o valor total da fatura.");
                vlEntrada = Regex.Replace(vlEntradaMinimo.ToString(), @"\D", "");
                simulaParcelamento.Entrada = vlSaldoDevedor.ToString("N2");
            }

            DateTime dateEntrance = DateTime.Now.Date;
            if (!DateTime.TryParse(dataEntrada, out dateEntrance))
            {
                dateEntrance = DateTime.Now.AddDays(7);
                message.Add("Data informada incorreta, por este motivo foi ajustada para a data máxima permitida para pagamento que é " + dateEntrance.ToString("dd/MM/yyyy") + ".");
            }

            if (dateEntrance.Date < DateTime.Now.Date || dateEntrance > DateTime.Now.AddDays(7))
            {
                dateEntrance = DateTime.Now.AddDays(7);
                message.Add("Data máxima para pagamento é " + dateEntrance.ToString("dd/MM/yyyy") + ", por este motivo corrigimos para o máximo permitido.");
            }
            simulaParcelamento.Parcela = 3;
            simulaParcelamento.DataEntrada = dateEntrance.ToString("dd/MM/yyyy");
            cache.AddCache("simulaParcelamento", simulaParcelamento);

            //Inserir Log Simulate [Promessa][Pag. Mínimo]
            Produto _produto = cache.Get<Produto>("produto");
            //IList<IbiService.WebSiteProduct> listWsProduct = cache.Get<List<IbiService.WebSiteProduct>>("WebSiteProduct");
            IList<WebSiteProduct> listWsProduct = cache.Get<List<WebSiteProduct>>("WebSiteProduct");
            //IbiService.WebSiteProduct wsProduct = listWsProduct.Where(p => p.DsProduct == _produto.CodProduto).FirstOrDefault();
            WebSiteProduct wsProduct = listWsProduct.Where(p => p.DsProduct == _produto.CodProduto).FirstOrDefault();

            //var wsSimulateMin = new IbiService.WebSiteSimulate()
            var wsSimulateMin = new WebSiteSimulate()
            {
                IdWebSiteProduct = wsProduct.IdWebSiteProduct,
                IdAccount = _produto.IdAccount,
                VlEntrace = ibiCard.PagamentoMinimo,
                DtEntrace = Convert.ToDateTime(simulaParcelamento.DataEntrada),
                NrParcel = 0,
                FlPromisse = true,
                VlDesconto = 0,
                DtInsert = DateTime.Now
            };
            //uraClient.SetWebSiteSimulateAsync(wsSimulateMin).GetAwaiter().GetResult();
            //End Inserir Log Simulate [Promessa][Pag. Mínimo]

            //Inserir Log Simulate [Promessa][Pag. Total]
            //var wsSimulateTotal = new IbiService.WebSiteSimulate()
            var wsSimulateTotal = new WebSiteSimulate()
            {
                IdWebSiteProduct = wsProduct.IdWebSiteProduct,
                IdAccount = _produto.IdAccount,
                VlEntrace = ibiCard.PagamentoMinimo,
                DtEntrace = Convert.ToDateTime(simulaParcelamento.DataEntrada),
                NrParcel = 0,
                FlPromisse = true,
                VlDesconto = 0,
                DtInsert = DateTime.Now
            };
            //uraClient.SetWebSiteSimulateAsync(wsSimulateTotal).GetAwaiter().GetResult();
            //End Inserir Log Simulate [Promessa][Pag. Total]

            //Inserir Log Simulate [Promessa][Pag. Personalizado]
            //var wsSimulatePerson = new IbiService.WebSiteSimulate()
            var wsSimulatePerson = new WebSiteSimulate()
            {
                IdWebSiteProduct = wsProduct.IdWebSiteProduct,
                IdAccount = _produto.IdAccount,
                VlEntrace = Convert.ToDecimal(simulaParcelamento.Entrada),
                DtEntrace = Convert.ToDateTime(simulaParcelamento.DataEntrada),
                NrParcel = 0,
                FlPromisse = true,
                VlDesconto = 0,
                DtInsert = DateTime.Now
            };
            //uraClient.SetWebSiteSimulateAsync(wsSimulatePerson).GetAwaiter().GetResult();
            //End Inserir Log Simulate [Promessa][Pag. Personalizado]

            Parallel.Invoke(
                //() => uraClient.SetWebSiteSimulateAsync(wsSimulateMin).GetAwaiter().GetResult(),
                //() => uraClient.SetWebSiteSimulateAsync(wsSimulateTotal).GetAwaiter().GetResult(),
                //() => uraClient.SetWebSiteSimulateAsync(wsSimulatePerson).GetAwaiter().GetResult()

                () => HttpHelper.POST<WebSiteSimulate>(Utils.API.Ibi.Uri.SetWebSiteSimulate(), wsSimulateMin),
                () => HttpHelper.POST<WebSiteSimulate>(Utils.API.Ibi.Uri.SetWebSiteSimulate(), wsSimulateTotal),
                () => HttpHelper.POST<WebSiteSimulate>(Utils.API.Ibi.Uri.SetWebSiteSimulate(), wsSimulatePerson)
                );
        }

        public DateTime IbiAcordoValidaDataEntrada(string dataEntrada, string vlEntrada, DateTime dateEntrance, SimulaParcelamento simulaParcelamento, IList<string> message, CacheSession cache)
        {
            if (dataEntrada != "")
            {
                if (simulaParcelamento == null) simulaParcelamento = new SimulaParcelamento();
                if (!DateTime.TryParse(dataEntrada, out dateEntrance))
                {
                    dateEntrance = DateTime.Now.AddDays(7);
                    simulaParcelamento.DataEntrada = dateEntrance.ToString("dd/MM/yyyy");
                    message.Add("Data informada incorreta, por este motivo foi ajustada para a data máxima " + simulaParcelamento.DataEntrada);

                }

                if (Convert.ToDouble(vlEntrada) > 0)
                {
                    if (dateEntrance.Date < DateTime.Now.Date || dateEntrance.Date > DateTime.Now.AddDays(7).Date)
                    {
                        message.Add("A data máxima para entrada é " + DateTime.Now.AddDays(7).ToString("dd/MM/yyyy") + ", por este motivo corrigimos para o máximo permitido.");
                        dateEntrance = DateTime.Now.AddDays(7);

                        simulaParcelamento.DataEntrada = dateEntrance.ToString("dd/MM/yyyy");
                    }
                    //simulaParcelamento.DataEntrada
                }
                else
                {
                    if (dateEntrance.Date < DateTime.Now.Date || dateEntrance > DateTime.Now.AddDays(25))
                    {
                        message.Add("Data máxima para entrada é " + DateTime.Now.AddDays(25).ToString("dd/MM/yyyy") + ", por este motivo corrigimos para o máximo permitido.");
                        dateEntrance = DateTime.Now.AddDays(25);
                        if (simulaParcelamento == null) simulaParcelamento = new SimulaParcelamento();
                        simulaParcelamento.DataEntrada = dateEntrance.ToString("dd/MM/yyyy");
                    }
                }
                simulaParcelamento.DataEntrada = dateEntrance.ToString("dd/MM/yyyy");
                cache.AddCache("simulaParcelamento", simulaParcelamento);
            }
            return dateEntrance;
        }


    }
}
