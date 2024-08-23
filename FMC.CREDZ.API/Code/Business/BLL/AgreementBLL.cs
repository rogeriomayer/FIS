using FMC.CREDZ.API.Models;
using FMC.CREDZ.DAO.Persistence;
using FMC.Generic;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace FMC.CREDZ.API.Code.Business.BLL
{
    public class AgreementBLL : BLL<Agreement, AgreementDAO>
    {
        public ICollection<Agreement> GetAgreementToday()
        {
            // AgreementPersistence persistence = new AgreementPersistence(true);
            return persistence.GetAgreementToday();
        }

        public ICollection<Agreement> GetByPeriod(DateTime dateInitial, DateTime dateEnd)
        {
            return persistence.GetByPeriod(dateInitial, dateEnd);
        }

        public ICollection<Agreement> GetAgreementTodayByCard(string nrCard)
        {
            // AgreementPersistence persistence = new AgreementPersistence(true);
            return persistence.GetAgreementTodayByCard(nrCard);
        }

        public Agreement GetLastAgreement(string nrAccount)
        {
            return persistence.GetLastAgreement(nrAccount);
        }

        public override Agreement UpdateNoContext(Agreement Agreement)
        {
            AgreementDAO persistence = new AgreementDAO();
            return persistence.UpdateNoContext(Agreement);
        }

        public bool SendMail(string mailFrom, string passwordMailFrom, IList<string> mailTo,
             IList<string> mailBBC, string customer,
             string smtpServer, int port, byte[] pdf)
        {
            try
            {
                MailMessage mail = new MailMessage();
                StringBuilder body = new StringBuilder();

                body.Append("Prezado(a) Senhor(a) ");
                body.Append(customer);
                body.Append(Environment.NewLine);
                body.Append(Environment.NewLine);
                body.Append("Segue boleto para pagamento do seu cartão Bradescard");
                body.Append(Environment.NewLine);
                body.Append(Environment.NewLine);
                body.Append("Caso não consiga realizar o pagamento do boleto imediatamente favor aguardar o prazo de três horas ou o próximo dia útil para que o boleto seja registrado junto ao banco emissor!");
                body.Append(Environment.NewLine);
                body.Append(Environment.NewLine);
                body.Append("Em caso de dúvida entre em contato no Telefone: 4004-1203 ou 0800-979-0203   " + Environment.NewLine);
                body.Append(Environment.NewLine + Environment.NewLine);
                body.Append("PRESTADOR DE SERVIÇOS CARTÕES BRADESCARD  " + Environment.NewLine);
                body.Append("FMC - FINANCIAL MANAGEMENT CONTROL DO BRASIL SERVIÇOS DE COBRANÇA LTDA." + Environment.NewLine);
                body.Append("CNPJ:06.696.163/0001-37 " + Environment.NewLine);
                body.Append("Endereço: Av. Afonso Penna 4101 - Uberlândia - MG " + Environment.NewLine);
                body.Append(Environment.NewLine);
                body.Append(Environment.NewLine);
                body.Append("AVISO LEGAL" + Environment.NewLine);
                body.Append("...Esta mensagem é destinada exclusivamente para a(s) pessoa(s) a quem é dirigida, podendo conter informação confidencial e/ou legalmente privilegiada. " + Environment.NewLine);
                body.Append("Se você não for destinatário desta mensagem, desde já fica notificado de abster-se a divulgar, copiar, distribuir, examinar ou, de qualquer forma, utilizar a informação contida nesta mensagem, " + Environment.NewLine);
                body.Append("por ser ilegal. Caso você tenha recebido esta mensagem por engano, pedimos que nos retorne este E-Mail, promovendo, desde logo, " + Environment.NewLine);
                body.Append("a eliminação do seu conteúdo em sua base de dados, registros ou sistema de controle. " + Environment.NewLine);
                body.Append("Fica desprovida de eficácia e validade a mensagem que contiver vínculos obrigacionais, expedida por quem não detenha poderes de representação. " + Environment.NewLine + Environment.NewLine);
                body.Append("LEGAL ADVICE " + Environment.NewLine);
                body.Append("...This message is exclusively destined for the people to whom it is directed, and it can bear private and/or legally exceptional information. " + Environment.NewLine);
                body.Append("If you are not addressee of this message, since now you are advised to not release, copy, distribute, check or, otherwise, " + Environment.NewLine);
                body.Append("use the information contained in this message, because it is illegal. If you received this message by mistake, we ask you to return this email, " + Environment.NewLine);
                body.Append("making possible, as soon as possible, the elimination of its contents of your database, registrations or controls system. " + Environment.NewLine);
                body.Append(" The message that bears any mandatory links, issued by someone who has no representation powers, shall be null or void. ");

                mail.Subject = "Cartões Bradescard.";
                mail.IsBodyHtml = false;
                mail.Body = body.ToString();
                mail.From = new MailAddress(mailFrom, "Cartões Bradescard");
                foreach (var item in mailTo)
                    mail.To.Add(new MailAddress(item.Trim()));

                System.IO.Stream stream = new System.IO.MemoryStream(pdf);
                System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType();
                ct.MediaType = System.Net.Mime.MediaTypeNames.Application.Pdf;
                ct.Name = "boleto" + customer + ".pdf";
                mail.Attachments.Add(new Attachment(stream, ct));

                SmtpClient client = new SmtpClient();
                client.Port = port;
                client.Host = smtpServer;
                client.Timeout = 60000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(mailFrom, passwordMailFrom);
                client.Send(mail);

                return true;

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                while (ex.InnerException != null)
                {
                    error += ex.Message + Environment.NewLine;
                    ex = ex.InnerException;
                }

                //Log.SaveFile(error);

                return false;
            }
        }

    }
}
