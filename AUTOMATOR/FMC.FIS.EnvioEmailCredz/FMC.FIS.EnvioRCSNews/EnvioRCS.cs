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
using System.Threading.Tasks;

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
                if (envioRCS.Atraso >= 78)
                {
                    string ret = "";
                    if (envioRCS.Total > 1000)
                        ret = SendRCSOtima();
                    else
                        ret = SendRCS();

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

        private string SendRCSOtima()
        {
            //var contrato = GetContratos();
            if (envioRCS.Phones.Count() > 0)
            {
                //string description = envioRCS.Atraso < 181 ? Get78_180(envioRCS.Nome, envioRCS.NumeroCartao, envioRCS.NomeCartao, contrato) : Get181_9999(envioRCS.Nome, envioRCS.NumeroCartao, envioRCS.NomeCartao, envioRCS.Desconto.ToString("N0"), contrato);
                //string description = Get181_9999(envioRCS.Nome, envioRCS.NumeroCartao, envioRCS.NomeCartao, envioRCS.Desconto.ToString("N0"), contrato);
                string description = GetBody(envioRCS.Nome, envioRCS.NomeCartao);
                var listSuggestions = new List<Suggestions>();
                listSuggestions.Add
                    (
                        new Suggestions()
                        {
                            Type = "OPEN_URL",
                            Text = "CLIQUE AQUI E RENEGOCIE",
                            Url = "https://fmc.digital/dm",
                            ReplyId = "CLICK_NEW"
                        }
                    );
                if (DateTime.Now.Year - envioRCS.DtNascimento.Year >= 15 || envioRCS.Atraso > 360)
                    listSuggestions.Add
                     (
                         new Suggestions()
                         {
                             Type = "OPEN_URL",
                             Text = "RENEGOCIAR PELO WHATSSAPP",
                             Url = "https://zaps.chat/r/dm",
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
                                            Text = "Ola," + envioRCS.Nome + "! Vamos facilitar a regularizacao do seu cartao DM referente a loja " + envioRCS.NomeCartao + " Whatsapp: https://zaps.chat/r/dm."
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

        private string SendRCS()
        {
            //var contrato = GetContratos();
            if (envioRCS.Phones.Count() > 0)
            {
                //string description = envioRCS.Atraso < 181 ? Get78_180(envioRCS.Nome, envioRCS.NumeroCartao, envioRCS.NomeCartao, contrato) : Get181_9999(envioRCS.Nome, envioRCS.NumeroCartao, envioRCS.NomeCartao, envioRCS.Desconto.ToString("N0"), contrato);
                //string description = Get181_9999(envioRCS.Nome, envioRCS.NumeroCartao, envioRCS.NomeCartao, envioRCS.Desconto.ToString("N0"), contrato);
                var description = GetBody(envioRCS.Nome, envioRCS.NomeCartao);
                var listSuggestions = new List<Suggestion>();
                listSuggestions.Add
                    (
                        new Suggestion()
                        {
                            type = "OPEN_URL",
                            text = "PORTAL",
                            url = "https://fmc.digital/dm",
                            postbackData = "CLICK_NEW"
                        }
                    );
                if (DateTime.Now.Year - envioRCS.DtNascimento.Year >= 55 || envioRCS.Atraso > 360)
                    listSuggestions.Add
                     (
                         new Suggestion()
                         {
                             type = "OPEN_URL",
                             text = "WHATSSAPP",
                             url = "https://zaps.chat/r/dm",
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
                                        text = "Ola," + envioRCS.Nome + "! Vamos facilitar a regularizacao do seu cartao DM referente a loja " + envioRCS.NomeCartao + " Whatsapp: https://zaps.chat/r/dm."
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

        private string GetBody(string nome, string store)
        {
            return string.Format("Olá, {0}!"
                                + Environment.NewLine + Environment.NewLine + "Temos condições que podem facilitar a regularização do seu cartão DM referente a loja {1}." 
                                + Environment.NewLine + Environment.NewLine + "Clique em uma das opções abaixo para acessar o Portal ou falar conosco pelo WhatsApp e verificar as alternativas disponíveis.", nome, store);
        }


        private string Get78_180(string nome, string cartao, string nomeCartao, Contrato contrato)
        {
            StringBuilder body = new StringBuilder();

            simulate = null;
            if (contrato != null)
            {
                decimal vlParcel = 70;
                var parcela = 24;
                for (int i = 24; i > 0; i--)
                {
                    parcela = i;
                    vlParcel = (contrato.parcelas.FirstOrDefault().valor - (contrato.parcelas.FirstOrDefault().valor * (envioRCS.Desconto / 100))) / i;
                    if (vlParcel > 70)
                    {
                        break;
                    }
                }
                simulate = GetValueAgreement(parcela, contrato);
            }

            //MUDANÇA EMAIL 2024-01-18
            if (simulate != null && simulate.ParcelResponse != null && simulate.ParcelResponse.Count() > 0)
            {
                var parcela = simulate.ParcelResponse.FirstOrDefault();
                body.Append("Olá ").Append(nome).Append("\r\n\r\n");
                body.Append("A Credz tem uma oferta especial para parcelamento do seu ");
                body.Append(cartao).Append("").Append(nomeCartao).Append("");
                body.Append(" pagando apenas uma entrada de R$").Append(parcela.ValueEntrace.ToString("N2"));
                body.Append(" e ").Append(parcela.NrParcel).Append(" parcelas de R$");
                body.Append(parcela.VlParcel).Append(" .").Append("\r\n\r\n");
                body.Append("Não perca essa oportunidade!").Append("\r\n\r\n");
                body.Append("Esta oferta é válida até ").Append(DateTime.Today.AddDays(2).ToString("dd/MM/yyyy")).Append(" para pagamento até ").Append(simulate.DateEntrace.ToString("dd/MM/yyyy")).Append(".");
                body.Append("\r\n\r\n");


                body.Append("Em caso de dúvidas, pode entrar em contato com nossa central de atendimento");
                body.Append(" nos telefones 4003 4031(Capitais e Regiões Metropolitanas) ou 0800 880 4031(Demais Regiões).");
                body.Append("\r\n\r\n");
                body.Append("Caso já tenha efetuado o pagamento favor desconsiderar esta mensagem.");
                body.Append("\r\n\r\n");
                body.Append("Digite PARAR para cancelar o recebimento.");

                return body.ToString();
            }
            else
                return null;
        }

        private string Get181_9999(string nome, string cartao, string nomeCartao, string desconto, Contrato contrato)
        {
            StringBuilder body = new StringBuilder();
            simulate = null;

            if (contrato != null)
                simulate = GetValueAgreement(0, contrato);

            if (simulate != null && simulate.ParcelResponse != null && simulate.ParcelResponse.Count() > 0)
            {
                var avista = simulate.ParcelResponse.OrderBy(p => p.NrParcel).FirstOrDefault();

                //if (avista.ValueEntrace > avista.VlDiscount && avista.ValueEntrace > 4500)
                //    throw new Exception("Maior que 300");

                //var body = new StringBuilder();
                body.Append("Olá ").Append(nome).Append(" 😊").Append("\r\n\r\n");

                /*
                if (avista.VlDiscount > 5)
                    body.Append("Aproveite essa oferta que a Credz lhe oferece apenas até " + DateTime.Today.AddDays(5).ToString("dd/MM/yyyy") + " e renegocie sua dívida com um super desconto de R$").Append((avista.VlDiscount - 1).ToString("N2")).Append("");
                else
                    body.Append("Aproveite essa oferta que a Credz lhe oferece apenas até " + DateTime.Today.AddDays(5).ToString("dd/MM/yyyy") + " e renegocie seu ").Append(cartao).Append(" ").Append(nomeCartao).Append("");
                body.Append(" por apenas R$").Append(avista.ValueEntrace.ToString("N2")).Append(" no pagamento a vista!");
                */
                body.Append("A Credz quer te ajudar a limpar seu nome com uma condição especial:\r\n\r\n");
                body.Append("✨ Sua dívida de R$ ").Append((avista.ValueEntrace + avista.VlDiscount).ToString("N2")).Append(" por apenas:\r\n\r\n");
                body.Append("💚 R$ ").Append(avista.ValueEntrace.ToString("N2")).Append(" à vista\r\n");
                body.Append("R$ ").Append(avista.VlDiscount.ToString("N2")).Append(" de desconto!\r\n\r\n");
                var parcelas = Convert.ToInt32(simulate.VlDue / 99);
                if (parcelas > 24) parcelas = 24;
                simulate = GetValueAgreement(parcelas, contrato);

                if (simulate != null)
                {
                    var parcelamento = simulate.ParcelResponse.OrderByDescending(p => p.NrParcel).FirstOrDefault();
                    body.Append("💳 Prefere parcelar?\r\n");
                    body.Append("➡ Entrada de R$").Append(parcelamento.ValueEntrace.ToString("N2")).Append("\r\n");
                    body.Append("➡ ").Append(parcelamento.NrParcel).Append("x de R$ ").Append(parcelamento.VlParcel.ToString("N2")).Append("\r\n\r\n");
                }
                body.Append("📱 Para aderir a oferta basta no botão 'CLIQUE AQUI E RENEGOCIE' abaixo!");
                body.Append("🗓 Oferta válida até ").Append(DateTime.Now.AddDays(2).ToString("dd/MM/yyyy")).Append("\r\n");

                body.Append("Qualquer dúvida, fale com a gente:\r\n");
                body.Append("📱 4003-4031 (capitais)\r\n");
                body.Append("📱 0800 880 4031 (demais regiões)\r\n\r\n\r\n");

                body.Append("Caso já tenha efetuado o pagamento favor desconsiderar esta mensagem.\r\n");

                body.Append("Para cancelar, responda PARAR.\r\n");

                /*
                if (avista.ValueEntrace > 140)
                {
                    decimal vlParcel = 70;
                    //var parcela = 24;
                    //for (int i = 24; i > 0; i--)
                    //{
                    //    parcela = i;
                    //    vlParcel = (simulate.VlFull - simulate.PctDiscount) / i;
                    //    if (vlParcel > 70)
                    //    {
                    //        break;
                    //    }
                    //}
                    simulate = GetValueAgreement(24, contrato);
                    if (simulate != null)
                    {
                        var parcelamento = simulate.ParcelResponse.OrderByDescending(p => p.NrParcel).FirstOrDefault();
                        //body.Append("\r\nTemos também opção de parcelamento com desconto de R$").Append(parcelamento.VlDiscount.ToString("N2"));
                        body.Append("\r\nTemos também opção de parcelamento você poderá renegociar ");
                        body.Append("pagando apenas uma entrada de R$").Append(parcelamento.ValueEntrace.ToString("N2"));
                        body.Append(" e ").Append(parcelamento.NrParcel).Append(" parcelas de R$");
                        body.Append(parcelamento.VlParcel).Append(".");
                        body.Append("\r\n\r\n");
                        body.Append("Não perca essa oportunidade!");
                    }
                    else
                    {
                        body.Append("\r\n\r\n");
                        body.Append("Não perca essa oportunidade!");
                        body.Append("\r\n");
                        body.Append("Temos também opções de parcelamento com um desconto que vale a pena conferir!");
                    }
                }
                else
                {
                    body.Append("\r\n\r\n");
                    body.Append("Não perca essa oportunidade!");
                    body.Append("\r\n");
                    body.Append("Temos também opções de parcelamento com um desconto que vale a pena conferir!");
                }
                body.Append("\r\n\r\n");
                body.Append("Esta oferta é válida até ").Append(DateTime.Today.AddDays(2).ToString("dd/MM/yyyy")).Append(" para pagamento até ").Append(avista.DtParcel.ToString("dd/MM/yyyy")).Append(".");
                body.Append("\r\n\r\n");
                body.Append("Em caso de dúvidas, pode entrar em contato com nossa central de atendimento");
                body.Append(" nos telefones 4003 4031(Capitais e Regiões Metropolitanas) ou 0800 880 4031(Demais Regiões).");
                body.Append("\r\n\r\n");

                body.Append("Caso já tenha efetuado o pagamento favor desconsiderar esta mensagem.");
                body.Append("\r\n\r\n");
                body.Append("Digite PARAR para cancelar o recebimento.");
                */
                return body.ToString();
            }
            else
                return null;
        }

        private string FailOver(string nome, string cartao, string nomeCartao, Contrato contrato)
        {

            StringBuilder message = new StringBuilder();
            /*AgreementSimulateResponse simulate = null;


            if (contrato != null)
            {
                decimal vlParcel = 50;
                var parcela = 24;
                for (int i = 24; i > 0; i--)
                {
                    parcela = i;
                    vlParcel = (contrato.parcelas.FirstOrDefault().valor - (contrato.parcelas.FirstOrDefault().valor * (envioRCS.Desconto / 100))) / i;
                    if (vlParcel > 70)
                    {
                        break;
                    }
                }
                simulate = GetValueAgreement(parcela, contrato);
            }*/

            if (simulate != null && simulate.ParcelResponse != null && simulate.ParcelResponse.Count > 0)
            {
                decimal vlPgtVista = simulate.ParcelResponse.FirstOrDefault().VlFull;
                message.Append(nome.Split(' ').FirstOrDefault());

                if (vlPgtVista < 300)
                {
                    message.Append(" quite o seu ");
                    message.Append(nomeCartao);
                    message.Append(" por apenas R$");
                    message.Append(vlPgtVista.ToString("N2"));
                    message.Append(" a vista ");
                    message.Append(" ou parcele");
                    message.Append(" em https://fmc.digital/credz ou Whatsapp https://zaps.chat/r/credz");
                    if (message.Length > 160)
                    {
                        message.Clear();
                        message.Append(nome.Split(' ').FirstOrDefault());
                        message.Append(" quite o seu ");
                        message.Append(nomeCartao.Replace("CREDZ", "").Replace("VISA", ""));
                        message.Append(" por apenas R$");
                        message.Append(vlPgtVista.ToString("N2"));
                        message.Append(" a vista, ou parcele");
                        message.Append(" em https://fmc.digital/credz ou Whatsapp https://zaps.chat/r/credz");
                    }
                }
                else
                {
                    var parcel = simulate.ParcelResponse.OrderByDescending(p => p.NrParcel).FirstOrDefault();
                    message.Append(" quite o seu ");
                    message.Append(nomeCartao);
                    message.Append(" com uma entrada R$").Append(parcel.ValueEntrace.ToString("N2"));
                    message.Append(" + ").Append(parcel.NrParcel).Append("x de R$");
                    message.Append(parcel.VlParcel.ToString("N2"));
                    message.Append(" em https://fmc.digital/credz ou Whatsapp https://zaps.chat/r/credz");
                    if (message.Length > 160)
                    {
                        message.Clear();
                        message.Append(" quite o seu ");
                        message.Append(nomeCartao.Replace("CREDZ", "").Replace("VISA", ""));
                        message.Append(" com uma entrada R$").Append(parcel.ValueEntrace.ToString("N2"));
                        message.Append(" + ").Append(parcel.NrParcel).Append("x de R$");
                        message.Append(parcel.VlParcel.ToString("N2"));
                        message.Append(" em https://fmc.digital/credz ou Whatsapp https://zaps.chat/r/credz");
                    }
                }

            }
            else
                return "";

            if (message.Length <= 160)
            {
                return message.ToString();
            }
            else
            {
                return "";
            }
        }

        private Contrato GetContratos()
        {

            var lead = envioRCS.Lead;

            var person = CobmaisAPI.GetPessoa(lead.Product.Person.NrCNPJCPF);

            var phones = new List<string>();

            var phoneUra = new GenericQueryBLL<PhoneUra>().GetCollection("select top 1 CONVERT(varchar(11),telefone) telefone, dtLigacao from CREDZ.dbo.RetornoUra where SUBSTRING(CONVERT(varchar(11), telefone), 3,1) > 6 and  cpf = '" + lead.Product.Person.NrCNPJCPF + "' order by dtLigacao desc");

            if (phoneUra.Count() > 0)
                phones = phoneUra.Select(p => p.telefone).ToList();

            if (phones == null || phones.Count <= 0)
                phones = person.telefones.Where(p => p.ativo && p.contato && Convert.ToInt32(p.numero.Substring(2, 1)) >= 6).Select(p => p.numero).ToList();



            if (phones == null || phones.Count <= 0)
            {
                var phone = person.telefones.Where(p => p.ativo && Convert.ToInt32(p.numero.Substring(2, 1)) >= 6).Select(p => p.numero).ToList().FirstOrDefault();
                if (phone == null)
                    phone = person.telefones.Where(p => Convert.ToInt32(p.numero.Substring(2, 1)) >= 6).Select(p => p.numero).ToList().FirstOrDefault();

                if (phone != null)
                    phones.Add(phone);
            }

            if (phones.Count > 0)
            {
                phones.ForEach(p => envioRCS.Phones.Add(p));


                var contracts = CobmaisAPI.GetContratos(lead.Product.Person.NrCNPJCPF, "0", "0");

                if (contracts != null)
                {
                    var contract = contracts.Where(p => p.numero_contrato == lead.Product.DsProduct).FirstOrDefault();

                    return contract;
                }
            }
            return null;
        }

        private AgreementSimulateResponse GetValueAgreement(int nrParcel, Contrato contract)
        {
            try
            {
                var lead = envioRCS.Lead;

                ICollection<ParcelaCredz> complementData = new HashSet<ParcelaCredz>();


                complementData = contract.parcelas.Select(p =>
                        new ParcelaCredz()
                        {
                            id_parcela_original = p.id,
                            negociacao_id = contract.negociacao_id,
                            numero_parcela_original = p.numero,
                            vencimento = p.vencimento,
                            valor = p.valor
                        }

                    ).ToList();

                return new AgreementBLL().GetOnlyOneSimulateCredz
                    (
                        new Business.Models.Customer.AgreementSimulateRequest()
                        {
                            Age = lead.Age,
                            CPF = lead.Product.Person.NrCNPJCPF,
                            DtEntrace = DateTime.Today.AddDays(7),
                            PctDiscount = 0,
                            NrParcel = nrParcel,
                            VlEntrace = 99,
                            Product = lead.Product.DsProduct,
                            CdSimulate = "",
                            ParcelaCredz = complementData,
                            FixedEntraceValue = false
                        }
                    );

            }
            catch (Exception ex)
            {
                if (nrParcel > 2)
                {
                    nrParcel = nrParcel - 2;
                    return GetValueAgreement(nrParcel, contract);
                }
                else
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

        public DateTime DtNascimento { get; set; }
        public string NumeroCartao { get; set; }
        public string NomeCartao { get; set; }
        public decimal Desconto { get; set; }
        public int Atraso { get; set; }

        public Lead Lead { get; set; }
        public ICollection<string> Phones { get; set; }

        public string UrlCartao { get; set; }

        public int Total { get; set; }
    }

    public class PhoneUra
    {
        [Key]
        public string telefone { get; set; }
    }
}
