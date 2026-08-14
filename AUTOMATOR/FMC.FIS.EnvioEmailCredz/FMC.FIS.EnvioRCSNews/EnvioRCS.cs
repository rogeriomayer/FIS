using FMC.FIS.BLL;
using FMC.FIS.Business.BLL;
using FMC.FIS.Business.Code.Api.Cobmais;
using FMC.FIS.Business.Code.Api.RCS;
using FMC.FIS.Business.Models.Cobmais;
using FMC.FIS.Business.Models.Customer;
using FMC.FIS.Business.Models.FIS;
using FMC.FIS.Business.Models.RCS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;

namespace FMC.FIS.EnvioEmailCredz
{
    public class EnvioRCS
    {
        RCS envioRCS;
        AgreementSimulateResponse simulate = null;

        public EnvioRCS(RCS envio)
        {
            envioRCS = envio;
            simulate = null;

        }


        public string Send()
        {
            string phones = "";

            try
            {
                if (envioRCS.Atraso >= 91 && envioRCS.Phones != null && envioRCS.Phones.Where(p => Convert.ToInt32(p[2]) > 6).Any())
                {
                    var agreementPlans = PostAgreementPlansAsync(envioRCS.CustomerId, envioRCS.ContractId, envioRCS.IdContract).GetAwaiter().GetResult();

                    if (agreementPlans != null)
                    {
                        var value = agreementPlans.RootElement[0]
                                    .GetProperty("installments")[0]
                                    .GetProperty("value")
                                    .GetDecimal();
                        if (value < 300)
                        {

                        }

                        string ret = "";
                        if (envioRCS.Total > 1500)
                            ret = SendRCSOtima(value);
                        else
                            ret = SendRCS(value);

                        if (!string.IsNullOrEmpty(ret))
                        {
                            envioRCS.Phones.ToList().ForEach(p => phones += p + ";");

                            new SendRcsBLL().Add
                                (
                                    new Business.Models.CREDZ.SendRCS()
                                    {
                                        IdPerson = envioRCS.IdPerson,
                                        IdProduct = envioRCS.IdProduct,
                                        Age = envioRCS.Atraso,
                                        Phone = phones,
                                        IdRCS = ret,
                                        DtInsert = DateTime.Now
                                    }
                                );

                            return ret;
                        }
                        else
                        {
                            Util.SaveFile("Send = false: Não foi possível enviar RCS para conta " + envioRCS.NumeroCartao);
                            return string.Empty;
                        }
                    }
                    else
                        Thread.Sleep(5000);
                }
                return string.Empty;

            }
            catch (Exception ex)
            {
                string erro = ex.Message + " | " + ex.StackTrace + Environment.NewLine;
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    erro += ex.Message + " | " + ex.StackTrace + Environment.NewLine;
                }
                Util.SaveFile("EnvioRCS: Erro ao enviar RCS para " + phones + " conta " + envioRCS.NumeroCartao);
                Util.SaveFile(erro);

                return string.Empty;
            }
            finally
            {

            }
        }

        private string SendRCSOtima(decimal value)
        {
            //var contrato = GetContratos();
            if (envioRCS.Phones.Count() > 0)
            {
                //string description = envioRCS.Atraso < 181 ? Get78_180(envioRCS.Nome, envioRCS.NumeroCartao, envioRCS.NomeCartao, contrato) : Get181_9999(envioRCS.Nome, envioRCS.NumeroCartao, envioRCS.NomeCartao, envioRCS.Desconto.ToString("N0"), contrato);
                //string description = Get181_9999(envioRCS.Nome, envioRCS.NumeroCartao, envioRCS.NomeCartao, envioRCS.Desconto.ToString("N0"), contrato);
                string description = GetBody(envioRCS.Nome, envioRCS.NomeCartao, value);
                var listSuggestions = new List<Suggestions>();

                listSuggestions.Add
                    (
                        new Suggestions()
                        {
                            Type = "OPEN_URL",
                            Text = "BOLETO A VISTA",
                            Url = $"https://negociadordm.fmcbrasil.com.br/BilletPdf" +
                                     $"?customerId={envioRCS.CustomerId}" +
                                     $"&contractId={envioRCS.ContractId}" +
                                     $"&downPaymentDate={DateTime.Today.AddDays(7).ToString("yyyy-MM-dd")}" +
                                     $"&installments=1",
                            ReplyId = "BOLETO_AVISTA"
                        }
                    );

                listSuggestions.Add
                    (
                        new Suggestions()
                        {
                            Type = "OPEN_URL",
                            Text = "CLIQUE AQUI E RENEGOCIE",
                            //Url = "https://fmc.digital/dm",
                            Url = "https://negociadordm.fmcbrasil.com.br?d=rcs&id=" + envioRCS.IdContract,
                            ReplyId = "CLICK_NEW"
                        }
                    );

                listSuggestions.Add
                     (
                         new Suggestions()
                         {
                             Type = "OPEN_URL",
                             Text = "RENEGOCIAR PELO WHATSSAPP",
                             Url = "https://fmc.digital/wdm",
                             ReplyId = "CLICK_WHATSAPP"
                         }
                     );


                if (!string.IsNullOrEmpty(description))
                {
                    try
                    {

                        var listMessage = new List<MessageItem>();

                        foreach (var phone in envioRCS.Phones)
                        {
                            listMessage.Add
                                (
                                    new MessageItem()
                                    {
                                        Phone = phone,
                                        Fallback = new Fallback()
                                        {
                                            AuthToken = "dWRpMmZtY2JyYXNpbDpmOVdyV0ZtWE54",
                                            BrokerCode = "500",
                                            CustomerCode = "190001774623071",
                                            Solution = "SMS",
                                            //Text = FailOver(envioRCS.Nome, envioRCS.NumeroCartao, envioRCS.NomeCartao, contrato),
                                            Text = GetFailOver(value)
                                        },
                                        Content = new MessageContent()
                                        {
                                            Type = "RICHCARD",
                                            Title = "NEGOCIADOR DM",// "PORTAL NEGOCIAÇÃO " + envioRCS.NomeCartao,
                                            Description = description,
                                            ImageUrl = "https://negociadordm.fmcbrasil.com.br/images/richcard_vertical_tall_2x1.jpg",
                                            CardOrientation = "VERTICAL",
                                            Size = "TALL",
                                            Suggestions = listSuggestions
                                        }

                                    }
                                /*
                                new Webhooks()
                                {
                                    callbackData = "CREDZ",
                                    delivery = new Delivery()
                                    {
                                        url = "http://fmcbrasil.com.br/infobip/rcs/webhook/delivery",
                                        intermediateReport = true,
                                        notify = true,
                                        receiveTriggeredFailoverReports = true
                                    },
                                    seen = new Seen() { url = "http://fmcbrasil.com.br/infobip/rcs/webhook/seen" }
                                }
                                */

                                );
                        }
                        var ret = OtimaRcsAPI.SendSingle(listMessage);

                        return ret.FirstOrDefault().MessageId;
                    }
                    catch (Exception ex)
                    {
                        return string.Empty;
                    }
                }
                else
                    return string.Empty;
            }
            else
                return String.Empty;
        }

        private string SendRCS(decimal value)
        {
            //var contrato = GetContratos();
            if (envioRCS.Phones.Count() > 0)
            {
                //string description = envioRCS.Atraso < 181 ? Get78_180(envioRCS.Nome, envioRCS.NumeroCartao, envioRCS.NomeCartao, contrato) : Get181_9999(envioRCS.Nome, envioRCS.NumeroCartao, envioRCS.NomeCartao, envioRCS.Desconto.ToString("N0"), contrato);
                //string description = Get181_9999(envioRCS.Nome, envioRCS.NumeroCartao, envioRCS.NomeCartao, envioRCS.Desconto.ToString("N0"), contrato);
                var description = GetBody(envioRCS.Nome, envioRCS.NomeCartao, value);
                var listSuggestions = new List<Suggestion>();

                listSuggestions.Add
                     (
                         new Suggestion()
                         {
                             type = "OPEN_URL",
                             text = "BOLETO A VISTA",
                             url = $"https://negociadordm.fmcbrasil.com.br/BilletPdf" +
                                      $"?customerId={envioRCS.CustomerId}" +
                                      $"&contractId={envioRCS.ContractId}" +
                                      $"&downPaymentDate={DateTime.Today.AddDays(7).ToString("yyyy-MM-dd")}" +
                                      $"&installments=1",
                             postbackData = "BOLETO_AVISTA"
                         }
                     );

                listSuggestions.Add
                    (
                        new Suggestion()
                        {
                            type = "OPEN_URL",
                            text = "CLIQUE AQUI E RENEGOCIE",
                            //url = "https://fmc.digital/dm",
                            url = "https://negociadordm.fmcbrasil.com.br?d=rcs&id=" + envioRCS.IdContract,
                            postbackData = "CLICK_NEW"
                        }
                    );

                listSuggestions.Add
                     (
                         new Suggestion()
                         {
                             type = "OPEN_URL",
                             text = "RENEGOCIAR PELO WHATSSAPP",
                             url = "https://fmc.digital/wdm",
                             postbackData = "CLICK_WHATSAPP"
                         }
                     );



                if (!string.IsNullOrEmpty(description))
                {
                    try
                    {
                        var ret = InfobipRcsAPI.SendSingle
                            (
                                envioRCS.Phones.ToList(),
                                new ContentRoot()
                                {
                                    alignment = "LEFT",
                                    orientation = "VERTICAL",
                                    type = "CARD",
                                    content = new ContentChild()
                                    {
                                        title = "NEGOCIADOR DM", //+ envioRCS.NomeCartao,
                                        description = description,
                                        media = new Media()
                                        {
                                            file = new File() { url = "https://negociadordm.fmcbrasil.com.br/images/richcard_vertical_tall_2x1.jpg" },
                                            thumbnail = new Thumbnail() { url = "https://negociadorcredz.fmcbrasil.com.br/images/topo/credz-logo-new.png" },
                                            height = "TALL"
                                        },
                                        suggestions = listSuggestions
                                    }

                                },
                                new Options()
                                {
                                    smsFailover = new SmsFailover()
                                    {
                                        sender = "fmcbrasil",
                                        //text = FailOver(envioRCS.Nome, envioRCS.NumeroCartao, envioRCS.NomeCartao, contrato)
                                        text = GetFailOver(value)
                                    }
                                },
                                new Webhooks()
                                {
                                    callbackData = "CREDZ",
                                    delivery = new Delivery()
                                    {
                                        url = "http://fmcbrasil.com.br/infobip/rcs/webhook/delivery",
                                        intermediateReport = true,
                                        notify = true,
                                        receiveTriggeredFailoverReports = true
                                    },
                                    seen = new Seen() { url = "http://fmcbrasil.com.br/infobip/rcs/webhook/seen" }
                                }
                            );
                        return ret.messages.FirstOrDefault().messageId;
                    }
                    catch (Exception ex)
                    {
                        return string.Empty;
                    }
                }
                else
                    return string.Empty;
            }
            else
                return String.Empty;
        }

        private string GetBody(string nome, string store, decimal intallmentValue)
        {
            if (store.ToUpper().Contains("EMPRESTIMO") || store.ToUpper().Contains("EMPRÉSTIMO"))
                return string.Format(
                $@"Olá, {nome}! 😊

Temos uma condição especial que pode facilitar a regularização do seu contrato DM, referente ao {store}.

💰 Apenas R$ {intallmentValue.ToString("N2")} no pagamento à vista!

💰 Ou parcele em até 35 vezes com entrada a partir de apenas R$ 99,00!

⏳ Essa oportunidade é por tempo limitado.

Clique em uma das opções abaixo para acessar o Portal ou falar conosco pelo WhatsApp e consultar as alternativas disponíveis para você.

Estamos à disposição para ajudar! 🤝", nome, store);
            else
                return string.Format(
                    $@"Olá, {nome}! 😊

Temos uma condição especial que pode facilitar a regularização do seu cartão DM, referente à loja {store}.

💰 Apenas R$ {intallmentValue.ToString("N2")} no pagamento à vista!

💰 Ou parcele com entrada a partir de apenas R$ 99,00!

⏳ Essa oportunidade é por tempo limitado.

Clique em uma das opções abaixo para acessar o Portal ou falar conosco pelo WhatsApp e consultar as alternativas disponíveis para você.

Estamos à disposição para ajudar! 🤝", nome, store, intallmentValue);
        }

        public string GetFailOver(decimal valor)
        {
            string nome = envioRCS.Nome.Split(' ').FirstOrDefault();
            string message = nome + "! Quite seu Cartão DM - " + envioRCS.NomeCartao + " por R$" + valor.ToString("N2") + " ou parcele pelo WhatsApp: https://fmc.digital/dm ou 3433014040";

            if (message.Length > 160)
            {
                message = nome + "!Quite seu Cartão DM " + envioRCS.NomeCartao + " por R$" + valor.ToString("N2") + " ou parcele pelo WhatsApp: https://fmc.digital/dm";
            }
            if (message.Length > 160)
            {
                message = "Quite seu Cartão DM " + envioRCS.NomeCartao + " por R$" + valor.ToString("N2") + " ou parcele pelo WhatsApp: https://fmc.digital/dm";
            }
            return message;
        }

        private static async Task<JsonDocument> PostAgreementPlansAsync(long customerId, long contractId, long idContract)
        {
            try
            {
                using var httpClient = new HttpClient
                {
                    BaseAddress = new Uri("http://10.40.0.52/digicob/")
                };

                var payload = new
                {
                    down_payment_date = DateTime.Today.AddDays(7).ToString("yyyy-MM-dd"),
                    channel = "Massivo",
                    installment_count = new[] { 1 },
                    IdPortalAccess = 0,
                    contracts = new[]
                    {
                        new
                        {
                            contract_id = contractId,
                            IdContract = idContract,
                            collection_ids = Array.Empty<int>()
                        }
                    }
                };

                var response = await httpClient.PostAsJsonAsync($"api/agreement/plans/{customerId}", payload);

                if (!response.IsSuccessStatusCode)
                    return null;

                var json = await response.Content.ReadAsStringAsync();

                return JsonDocument.Parse(json);
                /*
                var value = document.RootElement[0]
                    .GetProperty("installments")[0]
                    .GetProperty("value")
                    .GetDecimal();
                */


            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }

    public class RCS
    {
        public RCS()
        {
            Phones = new HashSet<string>();
        }
        public long IdPerson { get; set; }
        public long IdProduct { get; set; }
        public string Nome { get; set; }
        public long IdContract { get; set; }
        public long CustomerId { get; set; }
        public long ContractId { get; set; }
        public DateTime DtNascimento { get; set; }
        public string NumeroCartao { get; set; }
        public string NomeCartao { get; set; }
        public int Atraso { get; set; }

        public int Total { get; set; }

        public ICollection<string> Phones { get; set; }



    }
}
