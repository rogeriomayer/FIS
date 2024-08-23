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

    public static void SendMail(byte[] billet, string subject, string body, string smtp, int port, string userSMTP, string nameSMTP, string passSMTP, string email)
    {
        MailMessage mail = new MailMessage();

        mail.From = new MailAddress(userSMTP, nameSMTP);

        mail.To.Add(email);

        mail.IsBodyHtml = true;

        mail.Subject = subject;

        mail.Body = body;

        SmtpClient smtpClient = new SmtpClient(smtp);
        smtpClient.Port = port;

        smtpClient.Credentials = new System.Net.NetworkCredential(userSMTP, passSMTP);

        try
        {

            if (billet != null)
            {
                System.IO.Stream stream = new System.IO.MemoryStream(billet);
                System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType();
                ct.MediaType = System.Net.Mime.MediaTypeNames.Application.Pdf;
                ct.Name = "boleto.pdf";
                mail.Attachments.Add(new Attachment(stream, ct));
            }

            smtpClient.Send(mail);
            smtpClient.Dispose();
            mail.Dispose();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static string BodyEmail(string dataPagamento, string valor, string linhaDigitavel)
    {
        StringBuilder body = new StringBuilder();
        body.AppendLine("<p>Segue o boleto para pagamento do seu Cartão com vencimento ").Append(dataPagamento).Append(" no valor de R$ ").Append(valor).Append(", e é muito importante cumprí-lo na data acordada.</p> ");
        body.AppendLine("<br> ");
        body.AppendLine("<br> ");
        body.AppendLine("<br> ");
        body.AppendLine("<p>Segue a linha digitável: ").Append(linhaDigitavel).Append("</p> ");
        body.AppendLine("<br> ");
        body.AppendLine("<br>  ");
        body.AppendLine("<p>Caso prefira enviamos em anexo o seu boleto para pagamento.</p> ");
        body.AppendLine("<br> ");
        body.AppendLine("<br> ");
        body.AppendLine("<br> ");
        body.AppendLine("<p>Em caso de dúvida entre em contato no Telefone: </p>   ");
        body.AppendLine("<br> ");
        body.AppendLine("<br> ");
        body.AppendLine("<br> ");
        body.Append("<p><b>Evite fraudes com pagamento online:</b></p>");
        body.Append("<p>1.Observe se os seus dados (nome,  CPF,  endereço) constantes no boleto estão corretos e se há algum erro de português ou formatação.</p>");
        body.Append("<p>2.Verifique se os últimos números do código de barras correspondem ao valor do documento. Se forem diferentes, há uma grande chance de se tratar de uma fraude.");
        body.Append("<p>3.Confira se os 3 primeiros números do código de barras correspondem ao banco cuja logomarca aparece no boleto.");
        body.Append("<p>4.Sempre opte por pagar o boleto utilizando o leitor de códigos de barras disponível no aplicativo do seu banco. Em regra, boletos falsos possuem códigos de barras incompatíveis com esses leitores e obrigam a vítima a digitar o código número por número, manualmente, para efetivar o golpe.");
        body.Append("<p>5.Ao fazer a leitura do código de barras, verifique se o nome o beneficiário é realmente da empresa/pessoa contratada.");
        body.Append("<p>6.Sempre que possível, faça o download do boleto diretamente no site da empresa credora, utilizando, para tanto, uma conexão segura. Evite Wi-fi público. Se houver alguma suspeita, sempre entre em contato com a empresa.");
        body.Append("<br>");
        body.Append("<br>");
        body.AppendLine("<p>AVISO LEGAL ...Esta mensagem é destinada exclusivamente para a(s) pessoa(s) a quem é dirigida, podendo conter informação confidencial e/ou legalmente privilegiada.</p> ");
        body.AppendLine("<p>Se você não for destinatário desta mensagem, desde já fica notificado de abster-se a divulgar, copiar, distribuir, examinar ou, de qualquer forma, utilizar a informação contida nesta mensagem, por ser ilegal. Caso você tenha recebido esta mensagem por engano, pedimos que nos retorne este E-Mail, promovendo, desde logo, a eliminação do seu conteúdo em sua base de dados, registros ou sistema de controle.</p> ");
        body.AppendLine("<p>Fica desprovida de eficácia e validade a mensagem que contiver vínculos obrigacionais, expedida por quem não detenha poderes de representação. </p> ");

        return body.ToString();
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

