using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;

public class Util
{
    public static bool FileNameValid(string fileName)
    {
        IList<string> splitName = fileName.Split('_');
        if (splitName.Count < 2)
        {
            return false;
        }
        else if (splitName[1] != "E")
        {
            return false;
        }
        else
        {
            string data = splitName.LastOrDefault();
            string day = data.Substring(0, 2);
            string month = data.Substring(2, 2);
            string year = data.Substring(4, 4);

            return TextUtil.IsDate(year + "-" + month + "-" + day);
        }

        return false;

    }

    public static IList<byte> ReadFile(string path)
    {
        if (File.Exists(path))
        {
            MemoryStream ms;
            using (FileStream stream = File.Open(path, FileMode.Open))
            {

                byte[] buffer = new byte[stream.Length];

                using (ms = new MemoryStream())
                {
                    int read;
                    while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }

                }
            }
            return ms.ToArray();
        }
        else
            return null;
    }

    public static string MaskCard(string card)
    {
        card = card.Trim();
        if (card.StartsWith("000"))
            return card.Substring(3, 6) + "XXXXXXX" + card.Substring(card.Length - 3, 3);
        else
            return card.Substring(0, 6) + "XXXXXXX" + card.Substring(card.Length - 3, 3);
    }

    public static bool IsCpf(string cpf)
    {
        int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        string tempCpf;
        string digito;
        int soma;
        int resto;

        cpf = cpf.Trim();
        cpf = cpf.Replace(".", "").Replace("-", "");

        if (cpf.Length != 11)
            return false;

        tempCpf = cpf.Substring(0, 9);
        soma = 0;

        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        digito = resto.ToString();

        tempCpf = tempCpf + digito;

        soma = 0;
        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        digito = digito + resto.ToString();

        return cpf.EndsWith(digito);
    }

    public static bool IsEmail(string email)
    {
        try
        {
            MailAddress ma = new MailAddress(email);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static bool SendMail(string subject, string body, string smtpName, string smtpServer, int port, string userSMTP, string passSMTP, IList<string> emails, Dictionary<string, byte[]> attachments = null)
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

        SmtpClient smtp = new SmtpClient(smtpServer);
        smtp.Port = port;
        smtp.EnableSsl = false;

        smtp.Credentials = new System.Net.NetworkCredential(userSMTP, passSMTP);

        try
        {

            if (attachments != null && attachments != null)
            {
                foreach (var item in attachments)
                {
                    System.IO.Stream stream = new System.IO.MemoryStream(item.Value);
                    System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType();
                    ct.MediaType = System.Net.Mime.MediaTypeNames.Application.Pdf;
                    ct.Name = item.Key;
                    mail.Attachments.Add(new Attachment(stream, ct));
                }
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

    public static string BodyEmail(DateTime dataPagamento, string valor, string linhaDigitavel)
    {
        StringBuilder body = new StringBuilder();
        body.AppendLine("<p>Segue o boleto para pagamento do seu Cartão com vencimento ").Append(dataPagamento.ToString("dd/MM/yyyy")).Append(" no valor de R$ ").Append(valor).Append(", e é muito importante cumprí-lo na data acordada.</p> ");
        body.AppendLine("<br> ");
        body.AppendLine("<br> ");
        body.AppendLine("<p>Segue a linha digitável: ").Append(linhaDigitavel).Append("</p> ");
        body.AppendLine("<br> ");
        body.AppendLine("<br>  ");
        body.AppendLine("<p>Caso prefira enviamos em anexo o seu boleto para pagamento.</p> ");
        body.AppendLine("<br> ");
        body.Append("<p><b>Caso não consiga realizar o pagamento do boleto até a data de vencimento este mesmo boleto é válido para pagamento até o dia ").Append(dataPagamento.AddDays(8).ToString("dd/MM/yyyy"));
        body.Append(" sem acréscimo de juros. </b></p>");
        body.Append("<br>");
        body.Append("<p><b>O boleto pode levar até 24 horas para ser registrado junto ao banco emissor, se ocorrer algum erro no pagamento favor aguardar 2 horas ou até o próximo dia útil para nova tentativa de pagamento do boleto. Caso o erro persiste favor entrar em contato.</b></p>");
        body.AppendLine("<br> ");
        body.AppendLine("<p>Em caso de dúvida entre em contato nos telefone: </p>   ");
        body.Append("<p><b>4003 4031(Capitais e Regiões Metropolitanas) ou 0800 880 4031(demais regiões)</b></p>");
        body.AppendLine("<br> ");
        body.AppendLine("<br> ");
        body.AppendLine("<br> ");
        body.AppendLine("<br> ");
        body.AppendLine("<br> ");
        body.AppendLine("<p>AVISO LEGAL ...Esta mensagem é destinada exclusivamente para a(s) pessoa(s) a quem é dirigida, podendo conter informação confidencial e/ou legalmente privilegiada.</p> ");
        body.AppendLine("<p>Se você não for destinatário desta mensagem, desde já fica notificado de abster-se a divulgar, copiar, distribuir, examinar ou, de qualquer forma, utilizar a informação contida nesta mensagem, por ser ilegal. Caso você tenha recebido esta mensagem por engano, pedimos que nos retorne este E-Mail, promovendo, desde logo, a eliminação do seu conteúdo em sua base de dados, registros ou sistema de controle.</p> ");
        body.AppendLine("<p>Fica desprovida de eficácia e validade a mensagem que contiver vínculos obrigacionais, expedida por quem não detenha poderes de representação. </p> ");

        return body.ToString();
    }

    public static string pathLog = AppDomain.CurrentDomain.BaseDirectory + "LOG";
public static void SaveFile(string message)
    {
        string fileName = pathLog + "\\LOG_" + Environment.CurrentManagedThreadId.ToString() + "_" + DateTime.Now.ToString("ddMMyyyyHH") + ".log";
        StreamWriter logExecution = null;
        try
        {
            if (!Directory.Exists(pathLog))
                Directory.CreateDirectory(pathLog);

            using (logExecution = new StreamWriter(fileName, true, ASCIIEncoding.Default))
            {
                logExecution.WriteLine(Environment.NewLine + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " => " + message);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    

}

public class UtilException : ControllerBase
{
    public IActionResult Error(Exception ex)
    {
        string error = ex.Message + Environment.NewLine + ex.StackTrace;
        while (ex.InnerException != null)
        {
            ex = ex.InnerException;
            error = ex.Message + Environment.NewLine + ex.StackTrace;
        }
        return BadRequest(ex.ToString());
    }
}

