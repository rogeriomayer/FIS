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

                    if (envioEmail.Atraso >= 84)
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

                        string title = "Oferta especial para seu cartão " + envioEmail.NomeCartao.Replace("CREDZ", "") + " – pagamento facilitado";
                        if (SendMail(null, title, body, envioEmail.NomeCartao, smtp, email, senha, porta, envioEmail.Email))
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

            mail.From = new MailAddress(userSMTP, smtpName);

            if (emails.Count() > 1)
                foreach (var email in emails)
                    mail.Bcc.Add(email);
            else
                mail.To.Add(emails.FirstOrDefault());

            mail.IsBodyHtml = true;



            mail.Subject = subject;

            mail.Body = body;
            // smtpServers.Add(new KeyValuePair<string, int>("10.40.0.21", 25));
            SmtpClient smtp = new SmtpClient("10.40.0.21");
            smtp.Port = 25;

            smtp.EnableSsl = false;

            smtp.Credentials = new System.Net.NetworkCredential();

            try
            {

                if (billet != null)
                {
                    System.IO.Stream stream = new System.IO.MemoryStream(billet);
                    System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType();
                    ct.MediaType = System.Net.Mime.MediaTypeNames.Application.Pdf;
                    ct.Name = "boletoCrez.pdf";
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

            if (envioEmail.Lead.DebitBalance -
                (envioEmail.Lead.DebitBalance * (envioEmail.Desconto / 100)) < 300)
            {
                body = GetBody181_9999(
                    envioEmail.Nome,
                    envioEmail.NomeCartao,
                    envioEmail.NumeroCartao,
                    envioEmail.Desconto.ToString("N0"));
            }
            else if (envioEmail.Atraso <= 180)
            {
                body = GetBody78_180(
                    envioEmail.Nome,
                    envioEmail.NomeCartao,
                    envioEmail.NumeroCartao);
            }
            else
            {
                body = GetBody181_9999(
                    envioEmail.Nome,
                    envioEmail.NomeCartao,
                    envioEmail.NumeroCartao,
                    envioEmail.Desconto.ToString("N0"));
            }

            if (body == null)
                throw new Exception(envioEmail.NumeroCartao + " : conta não disponível.");
            // Observação se já pagou
            body.Append("<p>Se já pagou, ignore este e-mail.</p>");


            // Assinatura
            body.Append("<hr>");
            body.Append("<p>Atenciosamente,<br><b>Equipe Negociador DM</b></p>");
            body.Append("<p>Central de atendimento:<br>4003 4031 (Capitais e Regiões Metropolitanas)<br>0800 880 4031 (Demais Regiões)</p>");

            // Logo
            body.Append("<p><a href='https://fmc.digital/ecredz'><img alt='DM' style='width:60px' src='https://www.dmcardweb.com.br/src/public/images/brand/logo-dm.svg'></a></p>");
            if (!string.IsNullOrEmpty(envioEmail.UrlCartao))
            {
                body.Append("<p><a href='https://fmc.digital/ecredz'><img alt='Cartão' style='width:150px' src='")
                    .Append(envioEmail.UrlCartao).Append("'></a></p>");
            }

            // Rodapé legal
            body.Append("<hr>");
            body.Append("<p style='font-size:12px'>Você está recebendo este e-mail em razão de um relacionamento ativo ou recente com a DM / Credz. Caso não deseje mais receber comunicações eletrônicas, <a href='http://fmcbrasil.com.br/descadastrar'>clique aqui para cancelar o recebimento</a>.</p>");
            body.Append("<p style='font-size:12px'>Recomendamos que pagamentos sejam realizados exclusivamente pelos canais oficiais. Antes de concluir qualquer pagamento, verifique os dados do beneficiário.</p>");
            body.Append("<p style='font-size:11px; color:#666'>Esta mensagem é destinada exclusivamente ao destinatário indicado e pode conter informações confidenciais. Caso tenha recebido este e-mail por engano, por favor, desconsidere.</p>");


            return body.ToString();
        }
        private StringBuilder GetBody78_180(string nome, string cartao, string nomeCartao)
        {
            StringBuilder body = new StringBuilder();
            var contrato = GetContratos();

            nomeCartao = nomeCartao.Replace("CREDZ", "");

            AgreementSimulateResponse simulate = null;
            ParcelResponse avista = null;
            if (contrato == null)
                return null;

            simulate = GetValueAgreement(0, contrato);
            avista = simulate.ParcelResponse.OrderBy(p => p.NrParcel).First();

            decimal vlParcel = 90;
            var parcela = 24;
            for (int i = 24; i > 0; i--)
            {
                parcela = i;
                vlParcel = (simulate.VlFull - simulate.PctDiscount) / i;
                if (vlParcel > 90)
                    break;
            }

            //var parcelas = Convert.ToInt32(simulate.VlDue / 99);
            //if (parcelas > 24) parcelas = 24;
            simulate = GetValueAgreement(parcela, contrato);

            if (simulate == null || simulate.ParcelResponse?.Any() != true)
                return null;

            var parcelado = simulate.ParcelResponse.First();


            body.Append("<p>Olá ").Append(nome).Append(",</p>");

            // Aviso de bloqueio + CPF/SPC/Serasa
            body.Append("<p>Seu cartão <b>")
                    .Append(cartao).Append(" ").Append(nomeCartao)
                    .Append("</b> está bloqueado e seu CPF consta nos orgãos como SPC/Serasa. Faça já um acordo e regularize sua situação.</p>");

            // Juros informativos
            body.Append("<p>Lembre-se: enquanto não houver pagamento, os juros continuam sendo cobrados diariamente.</p>");

            // Desconto (se > R$10)
            if (avista.VlDiscount > 10)
            {
                body.Append("<p>Para te ajudar, oferecemos um desconto especial de até <b>R$ ")
                    .Append(avista.VlDiscount.ToString("N2"))
                    .Append("</b> nas condições abaixo:</p>");
            }
            else
            {
                body.Append("<p>Para te ajudar, oferecemos condições especiais de parcelamento para você regularizar sua situação.</p>");
            }

            // Parcelamento
            body.Append("<p><b>Parcelado:</b><br>");
            body.Append("Entrada: R$ ").Append(parcelado.ValueEntrace.ToString("N2")).Append("<br> mais ");
            body.Append(parcelado.NrParcel).Append(" parcelas de R$ ");
            body.Append(parcelado.VlParcel.ToString("N2")).Append("</p>");

            // Pagamento à vista
            body.Append("<p><b>À vista:</b><br>");
            body.Append("Valor total: R$ ").Append(avista.VlFull.ToString("N2")).Append("</p>");

            // Datas de validade
            body.Append("<p>Condições validas até ")
                .Append(DateTime.Today.AddDays(2).ToString("dd/MM/yyyy"))
                .Append(", para pagamento até ")
                .Append(simulate.DateEntrace.ToString("dd/MM/yyyy")).Append(".</p>");

            // Link para portal
            body.Append("<p>Para negociar ou ver mais detalhes, acesse:<br>");
            body.Append("<a href='https://fmc.digital/ecredz'>www.negociadorcredz.fmcbrasil.com.br</a></p>");

            return body;
        }



        private StringBuilder GetBody181_9999(string nome, string cartao, string nomeCartao, string desconto)
        {
            try
            {
                StringBuilder body = new StringBuilder();
                AgreementSimulateResponse simulate = null;

                //cartao = cartao.Replace("CREDZ", "");
                var contrato = GetContratos();
                if (contrato != null)
                    simulate = GetValueAgreement(0, contrato);

                if (simulate != null && simulate.ParcelResponse?.Any() == true && simulate.ParcelResponse.First().VlParcel > 5)
                {
                    var avista = simulate.ParcelResponse.OrderBy(p => p.NrParcel).First();

                    body.Append("<html>");
                    body.Append("<p>Olá ").Append(nome).Append(",</p>");

                    body.Append("<p>Consta em nosso sistema um débito relacionado ao seu ");
                    body.Append("<b>").Append(cartao).Append(" ").Append(nomeCartao).Append("</b>.</p>");

                    body.Append("<p>Para te ajudar regularizar sua situação e retirar seu CPF que consta nos orgãos como SPC/Serasa, oferecemos um desconto especial de até <b>R$ ")
                   .Append(avista.VlDiscount.ToString("N2"))
                   .Append("</b> nas condições abaixo:</p>");

                    body.Append("<p><b>Opção de pagamento à vista:</b><br>");
                    body.Append("Valor: <b>R$ ").Append(avista.ValueEntrace.ToString("N2")).Append("</b></p>");

                    if (avista.ValueEntrace > 200)
                    {
                        decimal vlParcel = 99;
                        var parcela = 24;
                        for (int i = 24; i > 0; i--)
                        {
                            parcela = i;
                            vlParcel = (simulate.VlFull - simulate.PctDiscount) / i;
                            if (vlParcel > 99)
                                break;
                        }

                        //var parcelas = Convert.ToInt32(simulate.VlDue / 99);
                        //if (parcelas > 24) parcelas = 24;
                        simulate = GetValueAgreement(parcela, contrato);

                        var parcelamento = simulate.ParcelResponse.OrderByDescending(p => p.NrParcel).First();

                        body.Append("<p><b>Opção de parcelamento:</b><br>");
                        body.Append("Entrada: <b>R$ ").Append(parcelamento.ValueEntrace.ToString("N2")).Append("</b><br> mais ");
                        body.Append(parcelamento.NrParcel).Append(" parcelas de <b>R$ ");
                        body.Append(parcelamento.VlParcel.ToString("N2")).Append("</b></p>");
                    }

                    body.Append("<p>Estas condições são validas até ");
                    body.Append(DateTime.Today.AddDays(2).ToString("dd/MM/yyyy"));
                    body.Append(", para pagamento até ");
                    body.Append(avista.DtParcel.ToString("dd/MM/yyyy")).Append(".</p>");

                    body.Append("<p>Para negociar ou verificar outras condições disponíveis, acesse:<br>");
                    body.Append("<a href='https://fmc.digital/ecredz'>www.negociadorcredz.fmcbrasil.com.br</a></p>");

                    return body;
                }

                return null;
            }
            catch
            {
                return null;
            }
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
