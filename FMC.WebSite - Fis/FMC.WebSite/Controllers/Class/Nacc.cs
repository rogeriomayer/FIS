using FMC.Fis.Models;
using FMC.Fis.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Custom;
using FMC.Fis.Utils.API.Nacc;
using FMC.Fis.Utils.API.Ibi;

namespace FMC.Fis.Controllers.Class
{
    public class Nacc
    {
        //public void GetParcelamento(NaccService.IDebtorService debtorService, NaccService.Pessoa naccPessoa, NaccService.Debtor naccCard, NaccService.Conta naccConta, SimulaParcelamento simulaParcelamento, IList<string> message, Cache cache)
        public void GetParcelamento(Utils.API.Nacc.Pessoa naccPessoa, Utils.API.Nacc.Debtor naccCard, Utils.API.Nacc.Conta naccConta, SimulaParcelamento simulaParcelamento, IList<string> message, CacheSession cache)
        {
            //JOIN IDDEBTOR
            IList<string> listIdConta = new List<string>();
            decimal valorSaldo = 0;
            decimal valorDescontoParcelado = 0;
            decimal valorDescontoAVista = 0;

            if (simulaParcelamento == null) simulaParcelamento = new SimulaParcelamento();

            if (naccCard.Carteira == "ENO")
            {

                foreach (var a in naccPessoa.Debtor.Where(p => p.Carteira == "ENO"))
                {
                    //NaccService.Contratos contrato = naccConta.Contratos.Where(p => p.IdConta == a.IdDebtor).FirstOrDefault();
                    Utils.API.Nacc.Contratos contrato = naccConta.Contratos.Where(p => p.IdConta == a.IdDebtor).FirstOrDefault();
                    if (contrato != null)
                    {
                        valorDescontoParcelado += contrato.VlMaxDescontoParcelado;
                        valorDescontoAVista += contrato.VlMaxDescontoVista;
                        valorSaldo += contrato.VlAcordoVista;
                    }
                    listIdConta.Add(a.IdDebtor.ToString());
                }
            }
            else if (naccCard.Carteira == "CDJ")
            {

                foreach (var a in naccPessoa.Debtor.Where(p => p.Carteira == "CDJ"))
                {
                    //NaccService.Contratos contrato = naccConta.Contratos.Where(p => p.IdConta == a.IdDebtor).FirstOrDefault();
                    Utils.API.Nacc.Contratos contrato = naccConta.Contratos.Where(p => p.IdConta == a.IdDebtor).FirstOrDefault();
                    if (contrato != null)
                    {
                        valorDescontoParcelado += contrato.VlMaxDescontoParcelado;
                        valorDescontoAVista += contrato.VlMaxDescontoVista;
                        valorSaldo += contrato.VlAcordoVista;
                    }
                    listIdConta.Add(a.IdDebtor.ToString());
                }
            }
            else if (naccCard.Carteira == "MMN")
            {

                foreach (var a in naccPessoa.Debtor.Where(p => p.Carteira == "MMN"))
                {
                    //NaccService.Contratos contrato = naccConta.Contratos.Where(p => p.IdConta == a.IdDebtor).FirstOrDefault();
                    Utils.API.Nacc.Contratos contrato = naccConta.Contratos.Where(p => p.IdConta == a.IdDebtor).FirstOrDefault();
                    if (contrato != null)
                    {
                        valorDescontoParcelado += contrato.VlMaxDescontoParcelado;
                        valorDescontoAVista += contrato.VlMaxDescontoVista;
                        valorSaldo += contrato.VlAcordoVista;
                    }
                    listIdConta.Add(a.IdDebtor.ToString());
                }
            }
            else
            {
                //NaccService.Contratos contrato = naccConta.Contratos.Where(p => p.IdConta == naccCard.IdDebtor).FirstOrDefault();
                Utils.API.Nacc.Contratos contrato = naccConta.Contratos.Where(p => p.IdConta == naccCard.IdDebtor).FirstOrDefault();
                listIdConta.Add(naccCard.IdDebtor.ToString());
                valorSaldo = contrato.VlAcordoVista;
                valorDescontoParcelado = contrato.VlMaxDescontoParcelado;
            }

            string vlEntrada = (simulaParcelamento != null && simulaParcelamento.Entrada != null) ? simulaParcelamento.Entrada : "0";
            string dataEntrada = (simulaParcelamento != null && simulaParcelamento.DataEntrada != null) ? simulaParcelamento.DataEntrada : DateTime.Now.ToString("yyyy-MM-dd");

            decimal vlEntradaMinimo = 0;
            if (naccCard.Carteira == "PEP") //ENTRADA TEM QUE SER >= 20%
                vlEntradaMinimo = valorSaldo * Convert.ToDecimal(AppSettings.PercentageEntranceNaccPEP * 0.01);
            else
                vlEntradaMinimo = valorSaldo * Convert.ToDecimal(AppSettings.PercentageEntranceNacc * 0.01);

            decimal vlDesconto, vlDescontoAVista;
            if (naccCard.Carteira == "ENO")
            {
                vlDesconto = valorDescontoParcelado * Convert.ToDecimal(AppSettings.PercentageDiscountNaccParceledENO * 0.01);
                vlDescontoAVista = valorDescontoAVista * Convert.ToDecimal(AppSettings.PercentageDiscountNaccCashENO * 0.01);
            }
            else if (naccCard.Carteira == "CDJ")
            {
                vlDesconto = valorDescontoParcelado * Convert.ToDecimal(AppSettings.PercentageDiscountNaccParceled * 0.01);
                vlDescontoAVista = valorDescontoAVista * Convert.ToDecimal(AppSettings.PercentageDiscountNaccCash * 0.01);
            }
            else if (naccCard.Carteira == "MMN")
            {
                vlDesconto = valorDescontoParcelado * Convert.ToDecimal(AppSettings.PercentageDiscountNaccParceledMMN * 0.01);
                vlDescontoAVista = valorDescontoAVista * Convert.ToDecimal(AppSettings.PercentageDiscountNaccCashMMN * 0.01);
            }
            else
            {
                vlDesconto = valorDescontoParcelado * Convert.ToDecimal(AppSettings.PercentageDiscountNaccParceled * 0.01);
                vlDescontoAVista = valorDescontoAVista * Convert.ToDecimal(AppSettings.PercentageDiscountNaccCash * 0.01);
            }

            if (((Convert.ToDecimal(vlEntrada) * Convert.ToDecimal(0.01)) > 0 && Convert.ToDecimal(vlEntrada) < vlEntradaMinimo) || (Convert.ToDecimal(vlEntrada)) == 0)
            {
                message.Add("O valor da entrada não pode ser inferior a R$ " + vlEntradaMinimo.ToString("N2") + " por este motivo ele foi ajustado para a entrada mínima.");
                vlEntrada = Convert.ToDecimal(vlEntradaMinimo).ToString("N2");
                simulaParcelamento.Entrada = vlEntradaMinimo.ToString("N2");
            }

            if (Convert.ToDecimal(vlEntrada) > 0 && Convert.ToDecimal(vlEntrada) > valorSaldo)
            {
                message.Add("O valor da entrada não pode ser superior a R$ " + valorSaldo.ToString("N2") + " por este motivo ele foi ajustado para a entrada mínima.");
                vlEntrada = Convert.ToDecimal(vlEntradaMinimo).ToString("N2");
                simulaParcelamento.Entrada = vlEntradaMinimo.ToString("N2");
            }

            DateTime dtEntrada = ValidaDataEntrada(naccCard, dataEntrada, simulaParcelamento, message, cache);
            //IList<NaccService.Parcela> naccParcelamento = new List<NaccService.Parcela>();
            IList<Utils.API.Nacc.Parcela> naccParcelamento = new List<Utils.API.Nacc.Parcela>();
            if (naccCard.Carteira != "PEP" && naccCard.Carteira != "MMN")
            {
                //naccParcelamento =  debtorService.GetParcelamentoAsync(string.Join(",", listIdConta.ToArray()), Convert.ToDecimal(vlEntrada).ToString("N2"), vlDesconto.ToString("N2"), dtEntrada.ToString("yyyy-MM-dd"), naccCard.Carteira).Result;

                naccParcelamento = HttpHelper.GET<List<Utils.API.Nacc.Parcela>>(Utils.API.Nacc.Uri.GetParcelamento(string.Join(",", listIdConta.ToArray()), Convert.ToDecimal(vlEntrada).ToString("N2"), vlDesconto.ToString("N2"), dtEntrada.ToString("yyyy-MM-dd"), naccCard.Carteira));
                cache.AddCache("naccParcelado", naccParcelamento);
            }
            else
            {
                //cache.AddCache("naccParcelado", new List<NaccService.Parcela>());
                cache.AddCache("naccParcelado", new List<Utils.API.Nacc.Parcela>());
            }
            #region Inserir Log Simulate NACC

            //IbiService.URAClient uraClient = new IbiService.URAClient();
            Produto _produto = cache.Get<Produto>("produto");
            //IList<IbiService.WebSiteProduct> listWsProduct = cache.Get<List<IbiService.WebSiteProduct>>("WebSiteProduct");
            //IbiService.WebSiteProduct wsProduct;

            IList<WebSiteProduct> listWsProduct = cache.Get<List<WebSiteProduct>>("WebSiteProduct");
            WebSiteProduct wsProduct;
            if (naccCard.Carteira == "ENO")
            {
                List<string> contract = naccCard.Contrato.Split('/').ToList();
                wsProduct = listWsProduct.Where(p => p.DsProduct == contract[0]).FirstOrDefault();
            }
            else if (naccCard.Carteira == "CDJ")
            {
                List<string> contract = naccCard.Contrato.Split('/').ToList();
                wsProduct = listWsProduct.Where(p => p.DsProduct == contract[0]).FirstOrDefault();
            }
            else if (naccCard.Carteira == "MMN")
            {
                List<string> contract = naccCard.Contrato.Split('-').ToList();
                wsProduct = listWsProduct.Where(p => p.DsProduct == contract[0]).FirstOrDefault();
            }
            else
            {
                wsProduct = listWsProduct.Where(p => p.DsProduct == _produto.CodProduto).FirstOrDefault();
            }

            //var wsSimulate = new IbiService.WebSiteSimulate()
            var wsSimulate = new WebSiteSimulate()
            {
                IdWebSiteProduct = wsProduct.IdWebSiteProduct,
                IdAccount = naccCard.IdDebtor,
                VlEntrace = Convert.ToDecimal(simulaParcelamento.Entrada),
                DtEntrace = Convert.ToDateTime(simulaParcelamento.DataEntrada),
                NrParcel = 0,
                FlPromisse = false,
                DtInsert = DateTime.Now
            };
            //var result = uraClient.SetWebSiteSimulateAsync(wsSimulate).Result;
            var result = HttpHelper.POST<WebSiteSimulate>(Utils.API.Ibi.Uri.SetWebSiteSimulate(), wsSimulate);

            #endregion Inserir Log Simulate NACC

            //INSERIR COMMENTS NACC
            string listAccount = string.Join(",", listIdConta.ToArray());
            string comentario = "<div style='float:left;text-align:left;margin-left:20px;font-size:10px'>" +
                "<b><u style='font-size:11px;color:red;margin-bottom:5px;float:left;width:100%'>SIMULAÇÃO SITE</u></b>" +
                "<br /><b>Entrada</b>: R$ " + simulaParcelamento.Entrada +
                "<br /><b>Data da Entrada</b>: " + dtEntrada.ToString("dd/MM/yyyy") +
                "<br /><b>Desconto Ofertado - <span style='color:green'>A Vista</span></b>: R$ " + vlDescontoAVista.ToString("N2") +
                "<br /><b>Desconto Ofertado - <span style='color:orangered'>Parcelado</span></b>: R$ " + vlDesconto.ToString("N2") +
                "<br /><b>Máximo de Parcelas</b>: " + naccParcelamento.Count() + "</div>";
            //debtorService.SetComentariosNaccAsync(listAccount, comentario, AppSettings.CodUserNacc);
            var ret = HttpHelper.POST<bool>(Utils.API.Nacc.Uri.SetComentarios(), new Utils.API.Nacc.Comentario { listIdConta = listIdConta, comentario = comentario, cdUserNacc = AppSettings.CodUserNacc });
        }

        //public DateTime ValidaDataEntrada(NaccService.Debtor naccCard, string dataEntrada, SimulaParcelamento simulaParcelamento, IList<string> message, Cache cache)
        public DateTime ValidaDataEntrada(Utils.API.Nacc.Debtor naccCard, string dataEntrada, SimulaParcelamento simulaParcelamento, IList<string> message, CacheSession cache)
        {
            DateTime dateEntrance = DateTime.Today.AddDays(1);
            DateTime validDate = GetDate(naccCard);
            if (dataEntrada != "")
            {
                if (!DateTime.TryParse(dataEntrada, out dateEntrance))
                {
                    dateEntrance = DateTime.Today.AddDays(1);
                    simulaParcelamento.DataEntrada = dateEntrance.ToString("dd/MM/yyyy");
                    message.Add("Data informada incorreta, por este motivo foi ajustada para o próximo dia útil" + simulaParcelamento.DataEntrada);
                }
                if (dateEntrance.Date == DateTime.Today)
                {
                    dateEntrance = DateTime.Today.AddDays(1);
                    simulaParcelamento.DataEntrada = dateEntrance.ToString("dd/MM/yyyy");
                }

                if (dateEntrance.DayOfWeek == DayOfWeek.Friday)
                {
                    dateEntrance = dateEntrance.AddDays(3);
                    simulaParcelamento.DataEntrada = dateEntrance.ToString("dd/MM/yyyy");
                    message.Add("Data de vencimento ajustada para o próximo dia útil, " + dateEntrance.ToString("dd/MM/yyyy") + ".");
                }
                else if (dateEntrance.DayOfWeek == DayOfWeek.Saturday)
                {
                    dateEntrance = dateEntrance.AddDays(2);
                    simulaParcelamento.DataEntrada = dateEntrance.ToString("dd/MM/yyyy");
                    message.Add("Data de vencimento ajustada para o próximo dia útil, " + dateEntrance.ToString("dd/MM/yyyy") + ".");
                }
                else if (dateEntrance.DayOfWeek == DayOfWeek.Sunday)
                {
                    dateEntrance = dateEntrance.AddDays(1);
                    simulaParcelamento.DataEntrada = dateEntrance.ToString("dd/MM/yyyy");
                    message.Add("Data de vencimento ajustada para o próximo dia útil, " + dateEntrance.ToString("dd/MM/yyyy") + ".");
                }

                if (dateEntrance.Date < DateTime.Now.Date || dateEntrance.Date > validDate.Date)
                {
                    dateEntrance = validDate;
                    simulaParcelamento.DataEntrada = dateEntrance.ToString("dd/MM/yyyy");
                    message.Add("Data máxima para entrada é " + DateTime.Now.AddDays(5).ToString("dd/MM/yyyy") + ", por este motivo corrigimos para o máximo permitido.");
                }
                else
                {
                    simulaParcelamento.DataEntrada = dateEntrance.ToString("dd/MM/yyyy");
                }
            }
            else
            {
                if (dateEntrance.Date == DateTime.Today)
                    dateEntrance = DateTime.Today.AddDays(1);

                if (dateEntrance.DayOfWeek == DayOfWeek.Friday)
                {
                    dateEntrance = dateEntrance.AddDays(3);
                    simulaParcelamento.DataEntrada = dateEntrance.ToString("dd/MM/yyyy");
                    message.Add("Data para entrada não pode ser vazia, por este motivo corrigimos para o próximo dia útil, " + dateEntrance.ToString("dd/MM/yyyy") + ".");
                }
                else if (dateEntrance.DayOfWeek == DayOfWeek.Saturday)
                {
                    dateEntrance = dateEntrance.AddDays(2);
                    simulaParcelamento.DataEntrada = dateEntrance.ToString("dd/MM/yyyy");
                    message.Add("Data para entrada não pode ser vazia, por este motivo corrigimos para o próximo dia útil, " + dateEntrance.ToString("dd/MM/yyyy") + ".");
                }
                else if (dateEntrance.DayOfWeek == DayOfWeek.Sunday)
                {
                    dateEntrance = dateEntrance.AddDays(1);
                    simulaParcelamento.DataEntrada = dateEntrance.ToString("dd/MM/yyyy");
                    message.Add("Data para entrada não pode ser vazia, por este motivo corrigimos para o próximo dia útil, " + dateEntrance.ToString("dd/MM/yyyy") + ".");
                }
                else
                {
                    message.Add("Data para entrada não pode ser vazia, por este motivo corrigimos para o próximo dia útil, " + dateEntrance.ToString("dd/MM/yyyy") + ".");
                    simulaParcelamento.DataEntrada = dateEntrance.ToString("dd/MM/yyyy");
                }


            }
            cache.AddCache("simulaParcelamento", simulaParcelamento);
            return dateEntrance;
        }

        //public DateTime GetDate(NaccService.Debtor naccCard)
        public DateTime GetDate(Utils.API.Nacc.Debtor naccCard)
        {

            return DateTime.Now.AddDays(5);
            //if (naccCard.Carteira == "PEP")
            //{
            //    return DateTime.Now.AddDays(30);
            //}
            //else
            //{
            //    return new DateTime(DateTime.Now.Year, DateTime.Now.Month,
            //                DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
            //}
        }

        public void RegisterAgreement(CacheSession cache)
        {
            try
            {
                //NACC
                //IbiService.WebSiteAgreement agreement = cache.Get<IbiService.WebSiteAgreement>("WebSiteAgreement");
                WebSiteAgreement agreement = cache.Get<WebSiteAgreement>("WebSiteAgreement");

                //NaccService.IDebtorService debtorService = new NaccService.DebtorServiceClient();
                //NaccService.Pessoa naccPessoa = cache.Get<NaccService.Pessoa>("naccPessoa");
                //NaccService.Conta naccConta = cache.Get<NaccService.Conta>("naccConta");

                Utils.API.Nacc.Pessoa naccPessoa = cache.Get<Utils.API.Nacc.Pessoa>("naccPessoa");
                Utils.API.Nacc.Conta naccConta = cache.Get<Utils.API.Nacc.Conta>("naccConta");

                Produto _produto = cache.Get<Produto>("produto");
                //NaccService.Debtor naccCard = naccPessoa.Debtor.Where(p => p.IdDebtor.ToString() == _produto.CodProduto).FirstOrDefault();
                //NaccService.Billet billet = cache.Get<NaccService.Billet>("billet");
                Utils.API.Nacc.Debtor naccCard = naccPessoa.Debtor.Where(p => p.IdDebtor.ToString() == _produto.CodProduto).FirstOrDefault();
                Utils.API.Nacc.Billet billet = cache.Get<Utils.API.Nacc.Billet>("billet");

                List<string> listIdConta = new List<string>();
                decimal valorSaldo = 0;
                decimal valorDescontoParcelado = 0;
                decimal valorDescontoAVista = 0;
                string valorEntrada = "",
                    valorParcela = "",
                    valorDesconto = "",
                    numeroParcelas = "",
                    dtEntrada = "";

                if (naccCard.Carteira == "ENO")
                {
                    foreach (var a in naccPessoa.Debtor.Where(p => p.Carteira == "ENO"))
                    {
                        //   NaccService.Contratos contrato = naccConta.Contratos.Where(p => p.IdConta == a.IdDebtor).FirstOrDefault();
                        Utils.API.Nacc.Contratos contrato = naccConta.Contratos.Where(p => p.IdConta == a.IdDebtor).FirstOrDefault();
                        if (contrato != null)
                        {
                            valorDescontoParcelado += contrato.VlMaxDescontoParcelado;
                            valorDescontoAVista += contrato.VlMaxDescontoVista;
                            valorSaldo += contrato.VlAcordoVista;
                        }
                        listIdConta.Add(a.IdDebtor.ToString());
                    }
                }
                else if (naccCard.Carteira == "CDJ")
                {
                    foreach (var a in naccPessoa.Debtor.Where(p => p.Carteira == "CDJ"))
                    {
                        //NaccService.Contratos contrato = naccConta.Contratos.Where(p => p.IdConta == a.IdDebtor).FirstOrDefault();
                        Utils.API.Nacc.Contratos contrato = naccConta.Contratos.Where(p => p.IdConta == a.IdDebtor).FirstOrDefault();
                        if (contrato != null)
                        {
                            valorDescontoParcelado += contrato.VlMaxDescontoParcelado;
                            valorDescontoAVista += contrato.VlMaxDescontoVista;
                            valorSaldo += contrato.VlAcordoVista;
                        }
                        listIdConta.Add(a.IdDebtor.ToString());
                    }
                }
                else if (naccCard.Carteira == "MMN")
                {
                    foreach (var a in naccPessoa.Debtor.Where(p => p.Carteira == "MMN"))
                    {
                        //NaccService.Contratos contrato = naccConta.Contratos.Where(p => p.IdConta == a.IdDebtor).FirstOrDefault();
                        Utils.API.Nacc.Contratos contrato = naccConta.Contratos.Where(p => p.IdConta == a.IdDebtor).FirstOrDefault();
                        if (contrato != null)
                        {
                            valorDescontoParcelado += contrato.VlMaxDescontoParcelado;
                            valorDescontoAVista += contrato.VlMaxDescontoVista;
                            valorSaldo += contrato.VlAcordoVista;
                        }
                        listIdConta.Add(a.IdDebtor.ToString());
                    }
                }
                else
                {
                    //NaccService.Contratos contrato = naccConta.Contratos.Where(p => p.IdConta == naccCard.IdDebtor).FirstOrDefault();
                    Utils.API.Nacc.Contratos contrato = naccConta.Contratos.Where(p => p.IdConta == naccCard.IdDebtor).FirstOrDefault();
                    listIdConta.Add(naccCard.IdDebtor.ToString());
                    valorDescontoParcelado = contrato.VlMaxDescontoParcelado;
                    valorDescontoAVista = contrato.VlMaxDescontoVista;
                    valorSaldo = contrato.VlAcordoVista;
                }
                if (agreement.NrParcel == 0)
                {
                    if (naccCard.Carteira == "ENO")
                        valorDesconto = (valorDescontoAVista * Convert.ToDecimal(AppSettings.PercentageDiscountNaccCashENO * 0.01)).ToString("N2");
                    else if (naccCard.Carteira == "CDJ")
                        valorDesconto = (valorDescontoAVista * Convert.ToDecimal(AppSettings.PercentageDiscountNaccCash * 0.01)).ToString("N2");
                    else if (naccCard.Carteira == "MMN")
                        valorDesconto = (valorDescontoAVista * Convert.ToDecimal(AppSettings.PercentageDiscountNaccCashMMN * 0.01)).ToString("N2");
                    else
                        valorDesconto = (valorDescontoAVista * Convert.ToDecimal(AppSettings.PercentageDiscountNaccCash * 0.01)).ToString("N2");

                    valorEntrada = (valorSaldo - (Convert.ToDecimal(valorDesconto))).ToString("N2");
                    dtEntrada = agreement.DtEntrace.ToString("yyyy-MM-dd");
                    valorParcela = "0";
                    numeroParcelas = "0";
                }
                else
                {
                    valorEntrada = agreement.VlEntrace.ToString("N2");

                    if (naccCard.Carteira == "ENO")
                        valorDesconto = (valorDescontoParcelado * Convert.ToDecimal(AppSettings.PercentageDiscountNaccParceledENO * 0.01)).ToString("N2");
                    else if (naccCard.Carteira == "CDJ")
                        valorDesconto = (valorDescontoParcelado * Convert.ToDecimal(AppSettings.PercentageDiscountNaccParceled * 0.01)).ToString("N2");
                    else if (naccCard.Carteira == "MMN")
                        valorDesconto = (valorDescontoParcelado * Convert.ToDecimal(AppSettings.PercentageDiscountNaccParceledMMN * 0.01)).ToString("N2");
                    else
                        valorDesconto = (valorDescontoParcelado * Convert.ToDecimal(AppSettings.PercentageDiscountNaccParceled * 0.01)).ToString("N2");

                    valorParcela = agreement.VlParcel.ToString("N2");
                    numeroParcelas = agreement.NrParcel.ToString();
                    dtEntrada = agreement.DtEntrace.ToString("yyyy-MM-dd");
                }
                int idAcordo = cache.Get<int>("idAcordo");

                if (idAcordo == 0)
                {
                    //idAcordo = debtorService.SetAcordoAsync(string.Join(",", listIdConta), valorEntrada, valorParcela, valorDesconto, numeroParcelas, dtEntrada, AppSettings.CodUserNacc, AppSettings.IdUserNacc, naccCard.Carteira).Result;
                    idAcordo = HttpHelper.GET<int>(Utils.API.Nacc.Uri.SetAcordo(string.Join(",", listIdConta), valorEntrada, valorParcela, valorDesconto, numeroParcelas, dtEntrada, AppSettings.CodUserNacc, AppSettings.IdUserNacc, naccCard.Carteira));

                    if (naccCard.Carteira != "MMN")
                        //billet = debtorService.GetBoletoAsync(naccCard.IdDebtor.ToString(), idAcordo.ToString(), "1", "true", naccCard.Carteira).Result;
                        billet = HttpHelper.GET<Utils.API.Nacc.Billet>(Utils.API.Nacc.Uri.GetBoleto(naccCard.IdDebtor.ToString(), idAcordo.ToString(), "1", "true", naccCard.Carteira));
                    cache.AddCache("idAcordo", idAcordo);
                    cache.AddCache("billet", billet);

                    if (naccCard.Carteira != "MMN")
                        foreach (var email in naccPessoa.Email)
                        {
                            try
                            {
                                //debtorService.EnviarBoletoEmailAsync(billet.IdBoleto.ToString(), email, AppSettings.CodUserNacc);
                                HttpHelper.GET<bool>(Utils.API.Nacc.Uri.EnviarBoletoEmail(billet.IdBoleto.ToString(), email, AppSettings.CodUserNacc));
                            }
                            catch { }
                        }
                }

            }
            catch (Exception e)
            {

            }
        }

    }
}