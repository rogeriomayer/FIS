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
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FMC.FIS.EnvioEmailCredz
{
    public class EnvioRCS
    {
        RCS envioRCS;

        public EnvioRCS(RCS envio)
        {
            envioRCS = envio;

        }


        public string Send()
        {
            string phones = "";

            try
            {
                if (envioRCS.Atraso >= 78)
                {
                    var ret = SendRCS();

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

        private string SendRCS()
        {
            var contrato = GetContratos();
            if (envioRCS.Phones.Count() > 0)
            {
                string description = envioRCS.Atraso < 181 ? Get78_180(envioRCS.Nome, envioRCS.NumeroCartao, envioRCS.NomeCartao, contrato) : Get181_9999(envioRCS.Nome, envioRCS.NumeroCartao, envioRCS.NomeCartao, envioRCS.Desconto.ToString("N0"), contrato);

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
                                    title = "PORTAL NEGOCIAÇÃO " + envioRCS.NomeCartao,
                                    description = description,
                                    media = new Media()
                                    {
                                        file = new File() { url = envioRCS.UrlCartao },
                                        thumbnail = new Thumbnail() { url = "https://negociadorcredz.fmcbrasil.com.br/images/topo/credz-logo-new.png" },
                                        height = "TALL"
                                    },
                                    suggestions = new List<Suggestion>()
                                    {
                                    new Suggestion()
                                    {
                                        type = "OPEN_URL",
                                        text = "CLIQUE AQUI E RENEGOCIE",
                                        url = "https://fmc.digital/rcs",
                                        postbackData = "CLICK_NEW"
                                    }
                                    }
                                }

                            },
                            new Options()
                            {
                                smsFailover = new SmsFailover()
                                {
                                    sender = "fmcbrasil",
                                    text = FailOver(envioRCS.Nome, envioRCS.NumeroCartao, envioRCS.NomeCartao, contrato)
                                }
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
                return String.Empty;
        }


        private string Get78_180(string nome, string cartao, string nomeCartao, Contrato contrato)
        {
            StringBuilder body = new StringBuilder();

            AgreementSimulateResponse simulate = null;
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
            AgreementSimulateResponse simulate = null;

            if (contrato != null)
                simulate = GetValueAgreement(0, contrato);

            if (simulate != null && simulate.ParcelResponse != null && simulate.ParcelResponse.Count() > 0)
            {
                var avista = simulate.ParcelResponse.OrderBy(p => p.NrParcel).FirstOrDefault();
                //var body = new StringBuilder();
                body.Append("Olá ").Append(nome).Append("\r\n\r\n");
                body.Append("Aproveite essa oferta que a Credz lhe oferece apenas nesse mês e renegocie sua dívida com um super desconto de R$").Append((avista.VlDiscount - 1).ToString("N2")).Append("");
                body.Append(" para quitar seu ").Append(cartao).Append(" ").Append(nomeCartao).Append("");
                body.Append(" por apenas R$").Append(avista.ValueEntrace.ToString("N2")).Append(" no pagamento a vista!");
                if (avista.ValueEntrace > 400)
                {
                    decimal vlParcel = 50;
                    var parcela = 24;
                    for (int i = 24; i > 0; i--)
                    {
                        parcela = i;
                        vlParcel = (simulate.VlFull - simulate.PctDiscount) / i;
                        if (vlParcel > 50)
                        {
                            break;
                        }
                    }
                    simulate = GetValueAgreement(parcela, contrato);
                    var parcelamento = simulate.ParcelResponse.OrderByDescending(p => p.NrParcel).FirstOrDefault();
                    body.Append("\r\nTemos também opção de parcelamento com desconto de R$").Append(parcelamento.VlDiscount.ToString("N2"));
                    body.Append(", pagando uma entrada de R$").Append(parcelamento.ValueEntrace.ToString("N2"));
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
                body.Append("\r\n\r\n");
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

        private string FailOver(string nome, string cartao, string nomeCartao, Contrato contrato)
        {
            StringBuilder message = new StringBuilder();
            AgreementSimulateResponse simulate = null;


            if (contrato != null)
                simulate = GetValueAgreement(0, contrato);

            if (simulate != null && simulate.ParcelResponse != null && simulate.ParcelResponse.Count > 0)
            {
                decimal vlPgt = simulate.ParcelResponse.Where(p => p.NrParcel == 0).FirstOrDefault().VlParcel;

                message.Append(nome.Split(' ').FirstOrDefault());
                message.Append(" quite o seu ");
                message.Append(nomeCartao);
                message.Append(" por apenas R$");
                message.Append((simulate.ParcelResponse.FirstOrDefault().VlFull).ToString("N2"));
                message.Append(" a vista ");
                message.Append(" ou parcele");
                message.Append(" em http://fmc.digital/credz ou ligue 40034031");
                if (message.Length > 160)
                {
                    message.Clear();
                    message.Append(nome.Split(' ').FirstOrDefault());
                    message.Append(" quite o seu ");
                    message.Append(nomeCartao.Replace("CREDZ", "").Replace("VISA", ""));
                    message.Append(" por apenas R$");
                    message.Append((simulate.ParcelResponse.FirstOrDefault().VlFull).ToString("N2"));
                    message.Append(" a vista, ou parcele");
                    message.Append(" em http://fmc.digital/credz ou ligue 40034031");
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
            phones = person.telefones.Where(p => p.ativo && p.contato && Convert.ToInt32(p.numero.Substring(2, 1)) >= 6).Select(p => p.numero).ToList();
            if (phones == null || phones.Count <= 0)
            {
                var phone = person.telefones.Where(p => p.ativo && Convert.ToInt32(p.numero.Substring(2, 1)) >= 6).Select(p => p.numero).ToList().FirstOrDefault();
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
                            VlEntrace = 0,
                            Product = lead.Product.DsProduct,
                            CdSimulate = "",
                            ParcelaCredz = complementData,
                            FixedEntraceValue = false
                        }
                    );

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
        public string NumeroCartao { get; set; }
        public string NomeCartao { get; set; }
        public decimal Desconto { get; set; }
        public int Atraso { get; set; }

        public Lead Lead { get; set; }
        public ICollection<string> Phones { get; set; }

        public string UrlCartao { get; set; }
    }
}
