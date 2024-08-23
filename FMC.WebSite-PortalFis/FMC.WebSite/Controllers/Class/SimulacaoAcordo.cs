using FMC.WebSite.FIS.Models;
using FMC.WebSite.FIS.Utils;
using FMC.WebSite.FIS.Utils.API.Ibi;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FMC.WebSite.FIS.Controllers.Class
{
    public class SimulacaoAcordo
    {
        public bool AccountIBI(Produto _produto, CardResponse cardResponse, AgreementSimulateRequest agreementSimulateRequest, SimulaParcelamento simulaParcelamento, IList<string> message, CacheSession cache)
        {
            try
            {
                if (Convert.ToInt32(cardResponse.Age) > AppSettings.MaxPromisse)
                {
                    return Acordo(cardResponse, agreementSimulateRequest, simulaParcelamento, message, cache);
                }
                else
                {
                    return Promessa(cardResponse, agreementSimulateRequest, simulaParcelamento, message, cache);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Acordo(CardResponse cardResponse, AgreementSimulateRequest agreementSimulateRequest, SimulaParcelamento simulaParcelamento, IList<string> message, CacheSession cache)
        {
            try
            {
                //Discount discount = new Discount();

                decimal vlEntrada = simulaParcelamento.Entrada.Contains(".") || simulaParcelamento.Entrada.Contains(",") ? Convert.ToDecimal(simulaParcelamento.Entrada.Replace(",", "").Replace(".", "")) / 100 : Convert.ToDecimal(simulaParcelamento.Entrada);
                DateTime dataEntrada = (simulaParcelamento != null && simulaParcelamento.DataEntrada != null) ? Convert.ToDateTime(simulaParcelamento.DataEntrada) : DateTime.Today;

                if (vlEntrada > 0 && vlEntrada < AppSettings.ValueEntranceParcel)
                {
                    message.Add("O valor da entrada não pode ser inferior a R$ " + AppSettings.ValueEntranceParcel.ToString("N2") + " por este motivo ele foi ajustado para a entrada mínima.");
                    vlEntrada = AppSettings.ValueEntranceParcel;
                    simulaParcelamento.Entrada = AppSettings.ValueEntranceParcel.ToString("N2");
                }

                if (vlEntrada > 0 && vlEntrada > cardResponse.VlFull)
                {
                    message.Add("O valor da entrada não pode ser superior a R$ " + cardResponse.VlFull.ToString("N2") + " por este motivo ele foi ajustado para a entrada mínima.");
                    vlEntrada = AppSettings.ValueEntranceParcel;
                    simulaParcelamento.Entrada = AppSettings.ValueEntranceParcel.ToString("N2");
                }

                //DateTime dateEntrance = IbiAcordoValidaDataEntrada(dataEntrada, vlEntrada, DateTime.Now.Date, simulaParcelamento, message, cache);

                //simulaParcelamento = cache.Get<SimulaParcelamento>("simulaParcelamento");
                DateTime dateEntrance = Convert.ToDateTime(simulaParcelamento.DataEntrada);

                agreementSimulateRequest.DtEntrace = dateEntrance;
                agreementSimulateRequest.VlEntrace = vlEntrada;

                AgreementSimulateResponse agreementSimulate = null;
                try
                {
                    agreementSimulate = FisAPI.AgreementSimulate(agreementSimulateRequest, 0);
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("DATA DE PAGTO ESTA FECHADA P/PROCESSAMENTO. SELECIONEOUTRA DATA"))
                    {
                        simulaParcelamento.DataEntrada = DateTime.Today.ToString("dd/MM/yyyy");
                        agreementSimulateRequest.DtEntrace = DateTime.Today;
                        agreementSimulate = FisAPI.AgreementSimulate(agreementSimulateRequest, 0);
                    }
                    else
                        throw ex;
                }
                cache.AddCache("parcelamento", agreementSimulate);

                #region Inserir Log Simulate [Acordo]
                Produto _produto = cache.Get<Produto>("produto");

                IList<Product> listWsProduct = cache.Get<List<Product>>("Product");
                Product wsProduct = listWsProduct.Where(p => p.Account == _produto.CodProduto).FirstOrDefault();

                var parcelOption = agreementSimulate.ParcelResponse.FirstOrDefault();
                decimal vlAVista = 0;
                DateTime dtFirstParcel = DateTime.Today.AddDays(15);
                if (agreementSimulate != null && parcelOption != null)
                {
                    vlAVista = parcelOption.ValueEntrace;
                    dtFirstParcel = Convert.ToDateTime(agreementSimulate.DateFirstParcel);
                }
                decimal VlEntraceLog = (simulaParcelamento == null || Convert.ToDecimal(simulaParcelamento.Entrada) == 0) ? vlAVista : Convert.ToDecimal(simulaParcelamento.Entrada);

                var simulate = new Simulate()
                {
                    IdProduct = wsProduct.IdProduct,
                    IdAccount = _produto.IdAccount,
                    VlEntrace = VlEntraceLog,
                    DtEntrace = Convert.ToDateTime(simulaParcelamento.DataEntrada),
                    NrParcel = agreementSimulate.ParcelResponse != null ? agreementSimulate.ParcelResponse.Count : 0,//Convert.ToInt32(simulaParcelamento.Parcela),
                    FlPromisse = false,
                    VlDiscount = AppSettings.Desconto,
                    DtFirstParcel = dtFirstParcel,
                    DtInsert = DateTime.Now
                };
                var webSiteSimulate = AfinzAPI.SetSimulate(simulate);
                cache.AddCache("WebSiteSimulate", webSiteSimulate);
                #endregion Inserir Log Simulate [Acordo]
                return true;
            }
            catch (Exception ex)
            {
                string erro = ex.Message + " - ";
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    erro += ex.Message + " - ";
                }

                if (erro.Contains("Data máxima permitida para pagamento"))
                {
                    DateTime newDate = Util.GetDateString(erro);
                    simulaParcelamento.DataEntrada = newDate.ToString("dd/MM/yyyy");
                    Acordo(cardResponse, agreementSimulateRequest, simulaParcelamento, message, cache);
                }

                IList<string> messages = erro.Split('"').ToList();
                for (int i = 1; i < messages.Count; i++)
                    if (messages[i].Length > 20)
                        message.Add(messages[i]);

                return false;
            }
        }

        public bool Promessa(CardResponse conta, AgreementSimulateRequest agreementSimulateRequest, SimulaParcelamento simulaParcelamento, IList<string> message, CacheSession cache)
        {
            try
            {
                cache.Remove("simulaParcelamentoFatura");

                decimal vlEntradaMinimo = conta.VlMinimum / 3;
                decimal vlSaldoDevedor = conta.VlFull;
                DateTime dtEntrada = DateTime.Today.DayOfWeek == DayOfWeek.Sunday ? DateTime.Today.AddDays(3) : DateTime.Today.AddDays(4);

                if (simulaParcelamento == null)
                {
                    simulaParcelamento = new SimulaParcelamento();
                    simulaParcelamento.DataEntrada = dtEntrada.ToString("yyyy-MM-dd");
                    simulaParcelamento.Entrada = vlEntradaMinimo.ToString("N2");
                    //simulaParcelamento.Parcela = "0";

                    cache.AddCache("simulaParcelamento", simulaParcelamento);
                }


                //Inserir Log Simulate [Promessa][Pag. Mínimo]
                Produto _produto = cache.Get<Produto>("produto");
                IList<Product> listWsProduct = cache.Get<List<Product>>("Product");
                Product product = listWsProduct.Where(p => p.Account == _produto.CodProduto).FirstOrDefault();

                var simulaParcelamentoFatura = FisAPI.GetParcelamentoFatura(1, agreementSimulateRequest.CPF, Convert.ToDecimal(simulaParcelamento.Entrada), conta.VlFull, conta.VlMinimum, Convert.ToDateTime(simulaParcelamento.DataEntrada), conta.Age, "");
                cache.AddCache("simulaParcelamentoFatura", simulaParcelamentoFatura);

                var wsSimulateMin = new Simulate()
                {
                    IdProduct = product.IdProduct,
                    IdAccount = _produto.IdAccount,
                    VlEntrace = vlEntradaMinimo,
                    DtEntrace = Convert.ToDateTime(simulaParcelamento.DataEntrada),
                    NrParcel = 0,
                    FlPromisse = true,
                    VlDiscount = 0,
                    DtInsert = DateTime.Now
                };
                //End Inserir Log Simulate [Promessa][Pag. Mínimo]

                //Inserir Log Simulate [Promessa][Pag. Total]
                var wsSimulateTot = new Simulate()
                {
                    IdProduct = product.IdProduct,
                    IdAccount = _produto.IdAccount,
                    VlEntrace = vlSaldoDevedor,
                    DtEntrace = Convert.ToDateTime(simulaParcelamento.DataEntrada),
                    NrParcel = 0,
                    FlPromisse = true,
                    VlDiscount = 0,
                    DtInsert = DateTime.Now
                };


                Parallel.Invoke(
                    () => AfinzAPI.SetSimulate(wsSimulateMin),
                    () => AfinzAPI.SetSimulate(wsSimulateTot)
                    );

                return true;
            }
            catch (Exception ex)
            {
                string erro = ex.Message + " - ";
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    erro += ex.Message + " - ";
                }

                IList<string> msgs = erro.Split('"').ToList();
                for (int i = msgs.Count; i > 0; i--)
                    if (msgs[i].Length > 35 && msgs.Count <= 2)
                        message.Add(msgs[i]);

                return false;
            }
        }

        public DateTime IbiAcordoValidaDataEntrada(DateTime dataEntrada, decimal vlEntrada, DateTime dateEntrance, SimulaParcelamento simulaParcelamento, IList<string> message, CacheSession cache)
        {
            if (dataEntrada != null)
            {
                if (simulaParcelamento == null) simulaParcelamento = new SimulaParcelamento();

                if (Convert.ToDouble(vlEntrada) > 0)
                {
                    if (dateEntrance.Date < DateTime.Now.Date || dateEntrance.Date > DateTime.Now.AddDays(7).Date)
                    {
                        message.Add("A data máxima para entrada é " + DateTime.Now.AddDays(5).ToString("dd/MM/yyyy") + ", por este motivo corrigimos para o máximo permitido.");
                        dateEntrance = DateTime.Now.AddDays(5);

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

