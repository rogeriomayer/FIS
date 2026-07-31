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
        internal static void SendMail(PersonRet personRet)
        {
            try
            {
                string body = GetBody(personRet.DsName, personRet.Store);

                string title = "Regularize seu Cartão DM com condições especiais";
                var emails = personRet.Contato.Split(';').Where(p => Util.IsEmail(p)).ToList();
                if (emails.Count > 0)
                    if (SendMail(title, body, emails))
                        new SendEmailBLL().Add
                            (
                                new Business.Models.CREDZ.SendEmail()
                                {
                                    idPerson = personRet.IdPerson,
                                    IdProduct = personRet.IdProduct,
                                    age = personRet.Age,
                                    email = personRet.Contato,
                                    dtInsert = DateTime.Now
                                }
                            );
                    else
                        Util.SaveFile("Envio = false: Não foi possível o enviar e-mail para " + personRet.Contato + " conta " + personRet.IdProduct);
            }
            catch (Exception ex)
            {
                string erro = ex.Message + " | " + ex.StackTrace + Environment.NewLine;
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    erro += ex.Message + " | " + ex.StackTrace + Environment.NewLine;
                }
                Util.SaveFile("SendMail: Erro ao enviar e-mail para " + personRet.Contato + " conta " + personRet.IdProduct);
                Util.SaveFile(erro);
            }
            finally
            {

            }
        }

        private static bool SendMail(string subject, string body, IList<string> emails)
        {
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress("dm@fmcatendimento.com.br", "Pague DM<dm@fmcatendimento.com.br>");

            if (emails.Count() > 1)
                foreach (var email in emails)
                    mail.Bcc.Add(email);
            else
                mail.To.Add(emails.FirstOrDefault());

            mail.IsBodyHtml = true;



            mail.Subject = subject;

            mail.Body = body;
            SmtpClient smtp = new SmtpClient("10.40.0.21");
            smtp.Port = 25;

            smtp.EnableSsl = false;

            smtp.Credentials = new System.Net.NetworkCredential();

            try
            {

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


        public static string GetBody(string nomeCliente, string loja)
        {
            var html = new StringBuilder();

            html.AppendLine("<!DOCTYPE html>");
            html.AppendLine("<html lang=\"pt-BR\">");
            html.AppendLine("<head>");
            html.AppendLine("    <meta charset=\"UTF-8\">");
            html.AppendLine("    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
            html.AppendLine("    <title>DM - Regularize seu Cartão</title>");
            html.AppendLine("</head>");

            html.AppendLine("<body style=\"margin:0;padding:0;background:#f0f1f5;font-family:Arial,Helvetica,sans-serif;\">");

            html.AppendLine("<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" style=\"background:#f0f1f5;padding:20px 0;\">");
            html.AppendLine("<tr>");
            html.AppendLine("<td align=\"center\">");

            html.AppendLine("<table width=\"600\" cellpadding=\"0\" cellspacing=\"0\" style=\"background:#ffffff;border-radius:8px;padding:30px;\">");

            // Logo
            html.AppendLine("<tr>");
            html.AppendLine("<td align=\"center\" style=\"padding-bottom:25px;\">");
            html.AppendLine("<img src=\"https://negociadordm.fmcbrasil.com.br/images/logo-dm-azul.jpg\" alt=\"DM\" style=\"max-width:100px;height:auto;display:block;\">");
            html.AppendLine("</td>");
            html.AppendLine("</tr>");

            // Saudação
            html.AppendLine("<tr>");
            html.AppendLine("<td style=\"font-size:22px;color:#333;font-weight:bold;padding-bottom:15px;\">");
            html.AppendLine("Olá, " + nomeCliente + "!");
            html.AppendLine("</td>");
            html.AppendLine("</tr>");

            // Texto
            html.AppendLine("<tr>");
            html.AppendLine("<td style=\"font-size:15px;color:#555;line-height:24px;padding-bottom:20px;\">");
            html.AppendLine("Temos condições que podem facilitar a regularização do seu ");
            html.AppendLine("<strong>Cartão DM</strong> referente à loja ");
            html.AppendLine("<strong>" + loja + "</strong>.");
            html.AppendLine("<br><br>");
            html.AppendLine("Clique em uma das opções abaixo para acessar o Portal de Negociação ");
            html.AppendLine("ou falar conosco pelo WhatsApp e verificar as alternativas ");
            html.AppendLine("disponíveis para você.");
            html.AppendLine("</td>");
            html.AppendLine("</tr>");

            // Botões
            html.AppendLine("<tr>");
            html.AppendLine("<td align=\"center\" style=\"padding:15px 0 10px 0;\">");

            html.AppendLine("<a href=\"https://fmc.digital/edm\"");
            html.AppendLine("style=\"background:#00AEEF;color:#ffffff;text-decoration:none;padding:14px 28px;border-radius:5px;display:inline-block;font-size:15px;font-weight:bold;margin:5px;\">");
            html.AppendLine("Acessar Portal");
            html.AppendLine("</a>");

            html.AppendLine("&nbsp;");

            html.AppendLine("<a href=\"https://fmc.digital/wdm\"");
            html.AppendLine("style=\"background:#25D366;color:#ffffff;text-decoration:none;padding:14px 28px;border-radius:5px;display:inline-block;font-size:15px;font-weight:bold;margin:5px;\">");
            html.AppendLine("Falar no WhatsApp");
            html.AppendLine("</a>");

            html.AppendLine("</td>");
            html.AppendLine("</tr>");

            // Informações
            html.AppendLine("<tr>");
            html.AppendLine("<td style=\"padding-top:30px;font-size:14px;color:#555;line-height:22px;\">");
            html.AppendLine("Nossa equipe está disponível para apresentar as melhores condições para a regularização do seu cartão.");
            html.AppendLine("<br><br>");
            html.AppendLine("Se preferir, entre em contato com nossa Central de Atendimento.");
            html.AppendLine("</td>");
            html.AppendLine("</tr>");

            // Central
            html.AppendLine("<tr>");
            html.AppendLine("<td style=\"padding-top:15px;font-size:13px;color:#666;line-height:22px;\">");
            html.AppendLine("<strong>Central de Atendimento</strong><br>");
            html.AppendLine("0800 702 5004<br>");
            html.AppendLine("Segunda a Sexta: 08h às 20h<br>");
            html.AppendLine("Sábado: 08h às 14h");
            html.AppendLine("</td>");
            html.AppendLine("</tr>");

            // Rodapé
            html.AppendLine("<tr>");
            html.AppendLine("<td style=\"padding-top:35px;border-top:1px solid #eeeeee;font-size:11px;color:#999999;text-align:center;line-height:18px;\">");
            html.AppendLine("Este é um e-mail automático. Não responda esta mensagem.<br>");
            html.AppendLine("FMC Assessoria autorizada pela DM.<br>");
            html.AppendLine("Todos os direitos reservados.");
            html.AppendLine("</td>");
            html.AppendLine("</tr>");

            html.AppendLine("</table>");

            html.AppendLine("</td>");
            html.AppendLine("</tr>");
            html.AppendLine("</table>");

            html.AppendLine("</body>");
            html.AppendLine("</html>");

            return html.ToString();
        }
    }
}
