using FMC.FIS.BLL;
using FMC.FIS.Business.BLL;
using FMC.FIS.Business.Code.Api.Cobmais;
using FMC.FIS.Business.Models.Cobmais;
using FMC.FIS.Business.Models.Customer;
using FMC.FIS.Business.Models.FIS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FMC.FIS.EnvioEmailCredz
{
    public class EnvioEmailThread
    {
        EnvioEmailSMS envioEmail;

        public EnvioEmailThread(EnvioEmailSMS envio)
        {
            envioEmail = envio;

        }

        internal void SendMail()
        {
            try
            {
                if (envioEmail.Email.Where(p => Util.IsEmail(p)).Count() > 0)
                {

                    if (envioEmail.Atraso >= 78)
                    {
                        string body = GetBody(envioEmail);

                        string email = "credz@fmccobranca.com.br";
                        string senha = "UR3d@$23cmF";
                        string smtp = envioEmail.SmtpServer.Key;
                        int porta = envioEmail.SmtpServer.Value;

                        /*
                        if (envioEmail.Email.Where(p => p.Contains("yahoo")).Count() > 0)
                        {
                            email = "credz@fmcatendimento.com.br";
                            smtp = "10.40.0.82";
                            porta = 25;
                        }
                        */

                        if (SendMail(null, envioEmail.NomeCartao + " INFORMA", body, envioEmail.NomeCartao, smtp, email, senha, porta, envioEmail.Email))
                        {
                            string emails = "";
                            envioEmail.Email.ToList().ForEach(p => emails += p + ";");

                            new SendEmailBLL().Add
                                (
                                    new Business.Models.CREDZ.SendEmail()
                                    {
                                        idPerson = envioEmail.IdPerson,
                                        IdProduct = envioEmail.IdProduct,
                                        age = envioEmail.Atraso,
                                        email = emails,
                                        dtInsert = DateTime.Now
                                    }
                                );
                        }
                        else
                        {
                            Util.SaveFile("Envio = false: Não foi possível o enviar e-mail para " + envioEmail.Email + " conta " + envioEmail.NumeroCartao);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message + " | " + ex.StackTrace + Environment.NewLine;
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    erro += ex.Message + " | " + ex.StackTrace + Environment.NewLine;
                }
                Util.SaveFile("SendMail: Erro ao enviar e-mail para " + envioEmail.Email + " conta " + envioEmail.NumeroCartao);
                Util.SaveFile(erro);
            }
            finally
            {

            }
        }

        private bool SendMail(byte[] billet, string subject, string body, string smtpName, string smtpServer, string userSMTP, string passSMTP, int portSMTP, IList<string> emails)
        {
            MailMessage mail = new MailMessage();

            userSMTP = smtpServer == "10.40.0.82" ? "credz@fmcatendimento.com.br" : "credz@fmccobranca.com.br";
            mail.From = new MailAddress(userSMTP, smtpName);

            if (emails.Count() > 1)
                foreach (var email in emails)
                    mail.Bcc.Add(email);
            else
                mail.To.Add(emails.FirstOrDefault());

            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            //mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;

            mail.Subject = subject;

            mail.Body = body;

            SmtpClient smtp = new SmtpClient(smtpServer);
            smtp.Port = portSMTP;
            smtp.EnableSsl = false;

            smtp.Credentials = new System.Net.NetworkCredential(userSMTP, passSMTP);

            try
            {

                if (billet != null)
                {
                    System.IO.Stream stream = new System.IO.MemoryStream(billet);
                    System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType();
                    ct.MediaType = System.Net.Mime.MediaTypeNames.Application.Pdf;
                    ct.Name = "boletoCredz.pdf";
                    mail.Attachments.Add(new Attachment(stream, ct));
                }

                smtp.Send(mail);
                smtp.Dispose();
                mail.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        private string GetBody(EnvioEmailSMS envioEmail)
        {
            StringBuilder body = new StringBuilder();
            body.Append("<html>");
            if (envioEmail.Lead.DebitBalance - (envioEmail.Lead.DebitBalance * (envioEmail.Desconto / 100)) < 300)
                body = GetBody181_9999(envioEmail.Nome, envioEmail.NomeCartao, envioEmail.NumeroCartao, envioEmail.Desconto.ToString("N0"));
            else if (envioEmail.Atraso <= 180)
                body = GetBody78_180(envioEmail.Nome, envioEmail.NomeCartao, envioEmail.NumeroCartao);
            /*else if (envioEmail.Atraso <= 89)
                body = GetBody78_89(envioEmail.Nome, envioEmail.NomeCartao, envioEmail.NumeroCartao);
            else if (envioEmail.Atraso <= 100)
                body = GetBody90_100(envioEmail.Nome, envioEmail.NomeCartao, envioEmail.NumeroCartao);
            else if (envioEmail.Atraso <= 119)
                body = GetBody101_181(envioEmail.Nome, envioEmail.NomeCartao, envioEmail.NumeroCartao); */
            else
                body = GetBody181_9999(envioEmail.Nome, envioEmail.NomeCartao, envioEmail.NumeroCartao, envioEmail.Desconto.ToString("N0"));

            if (body == null)
                throw new Exception(envioEmail.NumeroCartao + " :conta não disponvivel!");

            body.Append("<br>");
            body.Append("<br>");
            body.Append("<br>");
            body.Append("<p><b>Equipe Negociador Credz</b></p>");

            body.Append("<p><b>4003 4031(Capitais e Regiões Metropolitanas) ou 0800 880 4031(demais regiões)</b></p>");
            body.Append("<p>");
            body.Append("<a href='https://fmc.digital/ecredz'>");
            body.Append("<img alt=\"\" style=\"width:100px\" src=\"https://negociadorcredz.fmcbrasil.com.br/images/topo/credz-logo-new.png\">");
            body.Append("</a>");
            body.Append("</p>");

            if (!string.IsNullOrEmpty(envioEmail.UrlCartao))
            {
                body.Append("<p>");
                body.Append("<a href='https://fmc.digital/ecredz'>");
                body.Append("<img alt=\"\"style=\"width:150px\" src=\"").Append(envioEmail.UrlCartao).Append("\"> ");
                body.Append("</a>");
                body.Append("</p>");
            }

            body.Append("<br>");
            body.Append("<br>");
            body.Append("<a href=\"http://fmcbrasil.com.br/descadastrar\" target=\"_blank\" rel=\"noopener noreferrer\" data-auth=\"NotApplicable\" style=\"color:#e60014; text-decoration:none\" data-linkindex=\"2\">Descadastre-se! <em>(Unsubscribe)</em></a>");
            body.Append("<br>");
            body.Append("<p><b>Evite fraudes com pagamento online:</b></p>");
            body.Append("<p>1.Observe se os seus dados (nome,  CPF,  endereço) constantes no boleto estão corretos e se há algum erro de português ou formatação.</p>");
            body.Append("<p>2.Verifique se os últimos números do código de barras correspondem ao valor do documento. Se forem diferentes, há uma grande chance de se tratar de uma fraude.");
            body.Append("<p>3.Confira se os 3 primeiros números do código de barras correspondem ao banco cuja logomarca aparece no boleto.");
            body.Append("<p>4.Sempre opte por pagar o boleto utilizando o leitor de códigos de barras disponível no aplicativo do seu banco. Em regra, boletos falsos possuem códigos de barras incompatíveis com esses leitores e obrigam a vítima a digitar o código número por número, manualmente, para efetivar o golpe.");
            body.Append("<p>5.Ao fazer a leitura do código de barras, verifique se o nome o beneficiário é realmente da empresa/pessoa contratada.");
            body.Append("<p>6.Sempre que possível, faça o download do boleto diretamente no site da empresa credora, utilizando, para tanto, uma conexão segura. Evite Wi-fi público. Se houver alguma suspeita, sempre entre em contato com a empresa.");

            body.Append("<br>");
            body.Append("<br>");

            body.Append("<p>AVISO LEGAL ...Esta mensagem é destinada exclusivamente para a(s) pessoa(s) a quem é dirigida, podendo conter informação confidencial e/ou legalmente privilegiada.</p>");
            body.Append("<p>Se você não for destinatário desta mensagem, desde já fica notificado de abster-se a divulgar, copiar, distribuir, examinar ou, de qualquer forma, utilizar a informação contida nesta mensagem, por ser ilegal. Caso você tenha recebido esta mensagem por engano, pedimos que nos retorne este E-Mail, promovendo, desde logo, a eliminação do seu conteúdo em sua base de dados, registros ou sistema de controle.</p>");
            body.Append("<p>Fica desprovida de eficácia e validade a mensagem que contiver vínculos obrigacionais, expedida por quem não detenha poderes de representação. </p>");
            body.Append("</html>");
            return body.ToString();
        }


        private StringBuilder GetBody78_180(string nome, string cartao, string nomeCartao)
        {
            StringBuilder body = new StringBuilder();
            var contrato = GetContratos();

            AgreementSimulateResponse simulate = null;
            if (contrato != null)
            {
                decimal vlParcel = 50;
                var parcela = 24;
                for (int i = 24; i > 0; i--)
                {
                    parcela = i;
                    vlParcel = (contrato.parcelas.FirstOrDefault().valor - (contrato.parcelas.FirstOrDefault().valor * (envioEmail.Desconto / 100))) / i;
                    if (vlParcel > 70)
                    {
                        break;
                    }
                }
                simulate = GetValueAgreement(parcela, contrato);
            }

            //MUDANÇA EMAIL 2024-01-18
            if (simulate != null && simulate.ParcelResponse != null && simulate.ParcelResponse.Count() > 0 && simulate.ParcelResponse.FirstOrDefault().VlParcel > 5)
            {
                var parcela = simulate.ParcelResponse.FirstOrDefault();
                body.Append("<html>");
                body.Append("<p>Olá ").Append(nome).Append("</p>");
                body.Append("<p>A Credz tem uma oferta especial para parcelamento do seu ");
                body.Append("<b>").Append(cartao).Append(" ").Append(nomeCartao).Append("</b>");
                body.Append(" pagando apenas uma entrada de <b>R$").Append(parcela.ValueEntrace.ToString("N2"));
                body.Append("</b> e ").Append(parcela.NrParcel).Append(" parcelas de <b>R$");
                body.Append(parcela.VlParcel).Append(" </b>.");
                body.Append("<p>Não perca essa oportunidade, valida apenas em nosso portal!</p>");
                body.Append("<p>Esta oferta é válida até ").Append(DateTime.Today.AddDays(2).ToString("dd/MM/yyyy")).Append(" para pagamento até ").Append(simulate.DateEntrace.ToString("dd/MM/yyyy")).Append(".</p>");
                body.Append("<br>");
            }
            else
            {
                return null;
            }

            body.Append("<p>Para aproveitar esta oferta ou simular outras condições acesse: <a href='https://fmc.digital/ecredz'>www.negociadorcredz.fmcbrasil.com.br</a> </p>");
            body.Append("<p>Em caso de dúvidas, pode entrar em contato com nossa central de atendimento");
            body.Append(" nos telefones <b>4003 4031(Capitais e Regiões Metropolitanas) ou 0800 880 4031(Demais Regiões)</b>.</p>");
            body.Append("<br>");
            body.Append("<br>");
            body.Append("<p>Caso já tenha efetuado o pagamento favor desconsiderar este e-mail.</p>");

            return body;
        }


        private StringBuilder GetBody181_9999(string nome, string cartao, string nomeCartao, string desconto)
        {
            StringBuilder body = new StringBuilder();
            AgreementSimulateResponse simulate = null;

            var contrato = GetContratos();
            if (contrato != null)
                simulate = GetValueAgreement(0, contrato);


            if (simulate != null && simulate.ParcelResponse != null && simulate.ParcelResponse.Count() > 0 && simulate.ParcelResponse.FirstOrDefault().VlParcel > 5)
            {
                var avista = simulate.ParcelResponse.OrderBy(p => p.NrParcel).FirstOrDefault();
                //var body = new StringBuilder();
                body.Append("<html>");
                body.Append("<p>Olá ").Append(nome).Append("</p>");
                body.Append("<p>Aproveite essa oferta que a Credz lhe oferece apenas nesse mês e renegocie sua dívida com um super desconto de <b>R$").Append((avista.VlDiscount - 1).ToString("N2")).Append("</b>");
                body.Append(" para quitar seu <b>").Append(cartao).Append(" ").Append(nomeCartao).Append("</b>");


                if (avista.ValueEntrace <= 500)
                {
                    body.Append(" por apenas <b>R$").Append(avista.ValueEntrace.ToString("N2")).Append("</b> no pagamento a vista!</p>");
                    body.Append("<p>Não perca essa oportunidade, valida apenas em nosso portal!</p>");
                    body.Append("<br>");
                    body.Append("<p>Temos também opções de parcelamento com um desconto que vale a pena conferir!</p>");
                }
                else
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
                    body.Append(" por apenas <b>R$").Append(avista.ValueEntrace.ToString("N2")).Append("</b> no pagamento <b>a vista</b>!</p>");
                    body.Append("<p> Temos também opção de parcelamento com desconto de <b>R$").Append(parcelamento.VlDiscount.ToString("N2"));
                    body.Append("</b>, pagando uma entrada de <b>R$").Append(parcelamento.ValueEntrace.ToString("N2"));
                    body.Append("</b> e ").Append(parcelamento.NrParcel).Append(" parcelas de <b>R$");
                    body.Append(parcelamento.VlParcel).Append(" </b>.");
                    body.Append("</p><br>");
                    body.Append("<p>Não perca essa oportunidade, valida apenas em nosso portal!</p>");

                }

                body.Append("<br>");
                body.Append("<p>Esta oferta é válida até ").Append(DateTime.Today.AddDays(2).ToString("dd/MM/yyyy")).Append(" para pagamento até ").Append(avista.DtParcel.ToString("dd/MM/yyyy")).Append(".</p>");
                body.Append("<p>Para aproveitar esta oferta ou simular outras condições acesse: <a href='https://fmc.digital/ecredz'>www.negociadorcredz.fmcbrasil.com.br</a> </p>");
                body.Append("<p>Em caso de dúvidas, pode entrar em contato com nossa central de atendimento");
                body.Append(" nos telefones <b>4003 4031(Capitais e Regiões Metropolitanas) ou 0800 880 4031(Demais Regiões)</b>.</p>");
                body.Append("<br>");
                body.Append("<br>");
                body.Append("<p>Caso já tenha efetuado o pagamento favor desconsiderar este e-mail.</p>");
            }
            else
            {
                return null;
            }
            return body;
        }

        private Contrato GetContratos()
        {
            var lead = envioEmail.Lead;
            var contracts = CobmaisAPI.GetContratos(lead.Product.Person.NrCNPJCPF, "0", "0");


            if (contracts != null)
            {
                var contract = contracts.Where(p => p.numero_contrato == lead.Product.DsProduct).FirstOrDefault();

                return contract;
            }
            return null;
        }
        private AgreementSimulateResponse GetValueAgreement(int nrParcel, Contrato contract)
        {
            try
            {
                var lead = envioEmail.Lead;

                ICollection<ComplementData> complementData = new HashSet<ComplementData>();


                complementData.Add(new ComplementData() { Name = "negociacao_id", Value = contract.negociacao_id.ToString() });
                complementData.Add(new ComplementData() { Name = "id", Value = contract.parcelas.OrderBy(p => p.vencimento).FirstOrDefault().id.ToString() });
                complementData.Add(new ComplementData() { Name = "numero", Value = contract.parcelas.OrderBy(p => p.vencimento).FirstOrDefault().numero.ToString() });
                complementData.Add(new ComplementData() { Name = "vencimento", Value = contract.parcelas.OrderBy(p => p.vencimento).FirstOrDefault().vencimento.ToString("yyyy-MM-dd") });
                complementData.Add(new ComplementData() { Name = "valor", Value = contract.parcelas.OrderBy(p => p.vencimento).FirstOrDefault().valor.ToString("N2") });

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
                            ComplementData = complementData,
                            FixedEntraceValue = false
                        }
                    );

            }
            catch (Exception ex)
            {
                if (nrParcel > 1 && ex.ToString().Contains("Valor de Parcela abaixo do valor mínimo permitido"))
                {
                    nrParcel = nrParcel - 2;
                    return GetValueAgreement(nrParcel, contract);
                }
                else
                    return null;
            }
        }

    }

    public class EnvioEmailSMS
    {
        public long IdPerson { get; set; }
        public long IdProduct { get; set; }
        public string Nome { get; set; }
        public string NumeroCartao { get; set; }
        public string NomeCartao { get; set; }
        public decimal Desconto { get; set; }
        public int Atraso { get; set; }

        public Lead Lead { get; set; }
        public IList<string> Email { get; set; }
        public KeyValuePair<string, int> SmtpServer { get; set; }

        public string UrlCartao { get; set; }
    }
}
