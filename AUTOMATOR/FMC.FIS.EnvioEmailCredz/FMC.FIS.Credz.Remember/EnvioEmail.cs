using FMC.FIS.BLL;
using FMC.FIS.Business.BLL;
using FMC.FIS.Business.Code.Api.Cobmais;
using FMC.FIS.Business.Models.BvTelecom;
using FMC.FIS.Business.Models.Cobmais;
using FMC.FIS.Business.Models.Customer;
using FMC.FIS.Business.Models.FIS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FMC.FIS.Credz.Remember
{
    internal class SendRemember
    {
        public ObjectSend _objectSend;

        public SendRemember(ObjectSend objectSend)
        {
            _objectSend = objectSend;
        }

        internal void SendSMS()
        {
            StringBuilder message = new StringBuilder();
            message.Append("Ola ").Append(_objectSend.Name.Split(' ').FirstOrDefault());

            if (_objectSend.DtParcel <= DateTime.Today)
                message.Append(" evite novas negativações no CPF e encargos no cartão, entre no nosso portal e retire o boleto do seu acordo. https://fmc.digital/credz");
            else
                message.Append(" caso ainda não tenha o boleto para pagamento do seu acordo entre em nosso portal para retirá-lo. https://fmc.digital/credz");

            var ret = new BvSmsBLL().SmsSingle
                           (
                               new SingleRequest()
                               {
                                   celular = _objectSend.Phone,
                                   mensagem = message.ToString(),
                                   carteiraId = 1064,
                                   parceiroId = "credZ" + DateTime.Now.ToString("ddMMyyyyHHmmss")
                               }
                           );

            if (ret.ToUpper() == "OK")
                new EmailRememberBLL().Add
                    (
                        new Business.Models.CREDZ.EmailRemember()
                        {
                            IdAgreement = _objectSend.IdAgreement,
                            IdAgreementParcel = _objectSend.IdAgreementParcel,
                            IdPerson = _objectSend.Product.IdPerson,
                            Email = _objectSend.Phone,
                            DtInsert = DateTime.Now
                        }
                    );
        }

        internal void SendMail()
        {
            try
            {
                string email = "";
                string subject = "BOLETO PARA PAGAMENTO ";
                if (_objectSend.Parcel == 0)
                    subject += "ACORDO " + _objectSend.CardName;
                else
                    subject += " PARCELA " + (_objectSend.Parcel + 1).ToString() + " ACORDO " + _objectSend.CardName;

                if (SendMail(_objectSend.Pdf, subject, EmailBody(), _objectSend.CardName, "credz@fmccobranca.com.br", "UR3d@$23cmF", _objectSend.Email))
                {
                    _objectSend.Email.ToList().ForEach(p => email += p + ";");
                    new EmailRememberBLL().Add
                    (
                        new Business.Models.CREDZ.EmailRemember()
                        {
                            IdAgreement = _objectSend.IdAgreement,
                            IdAgreementParcel = _objectSend.IdAgreementParcel,
                            IdPerson = _objectSend.Product.IdPerson,
                            Email = email,
                            DtInsert = DateTime.Now
                        }
                    );
                }
                else
                {
                    Util.SaveFile("False: Erro ao enviar e-mail para " + email + " conta " + _objectSend.CardNumber);
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message + "| " + ex.StackTrace + Environment.NewLine;
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    erro += ex.Message + "| " + ex.StackTrace + Environment.NewLine;
                }
                Util.SaveFile("Send Email Erro ao enviar e-mail para " + _objectSend.Email.FirstOrDefault() + " conta " + _objectSend.CardNumber);
                Util.SaveFile(erro);
            }
        }



        private string EmailBody()
        {
            StringBuilder body = new StringBuilder();
            body.Append("<html>");
            body.Append("<p>Olá ").Append(_objectSend.Name).Append("</p>");
            body.Append("<br>");
            if (_objectSend.DtParcel < DateTime.Today)
            {
                body.Append("Não identificamos o pagamento ");
            }
            else
            {
                body.Append("<p>Segue o boleto para pagamento ");
            }
            if (_objectSend.Parcel == 0)
                if (_objectSend.aVista)
                    body.Append("do seu acordo no valor de R$").Append(_objectSend.Value.ToString("N2"));
                else
                    body.Append("da entrada do seu acordo no valor de R$").Append(_objectSend.Value.ToString("N2"));
            else
                body.Append("da parcela ").Append(_objectSend.Parcel + 1).Append(" do seu acordo no valor de R$").Append(_objectSend.Value.ToString("N2"));

            body.Append(" referente ao cartão ").Append(_objectSend.CardNumber).Append(" ").Append(_objectSend.CardName).Append(" com vencimento em ").Append(_objectSend.DtParcel.ToString("dd/MM/yyyy"));
            body.Append("</p>");
            if (_objectSend.Parcel == 0)
                body.Append("<p>Lembrando que só ocorrerá a efetivação do acordo e a retirada da negativação do seu CPF dos orgão de proteção de crédito após constar o pagamento deste boleto. </p>");
            else
                body.Append("<p>Realize o pagamento da parcela e evite a quebra do seu acordo e com isso lançamento de novos encargos e nova negativação do seu CPF. </p>");

            body.Append("<p><b>Caso não consiga realizar o pagamento do boleto até a data de vencimento este mesmo boleto é válido para pagamento até o dia ").Append(_objectSend.DtParcel.AddDays(8).ToString("dd/MM/yyyy"));
            body.Append(" sem acréscimo de juros. </b></p>");

            body.Append("<br>");

            body.Append("<p><b>O boleto pode levar até 24 horas para ser registrado junto ao banco emissor, se ocorrer algum erro no pagamento favor aguardar 2 horas ou até o próximo dia útil para nova tentativa de pagamento do boleto. Caso o erro persiste favor entrar em contato.</b></p>");

            body.Append("<p><b>Lembramos CREDZ foi adquirida pela DM CARD, a partir de agora os boletos serão emitidos em nome da DM FINANCEIRA S.A. - CRÉDITO, FINANCIAMENTO E INVESTIMENTO CNPJ: 91.669.747/0001-92.</b></p>");

            body.Append("<br>");

            if (_objectSend.Pdf != null)
            {
                body.Append("<p>Linha digitável para pagamento: </p> <b>").Append(_objectSend.Line);
                body.Append("<br>");
                body.Append("<br> ");
                body.Append("<p>Segue também em anexo o seu boleto para pagamento.</p>");
                body.Append("<br>");
                body.Append("<br>");
            }

            body.Append("<p>Você também pode retirar a segunda no nosso potal.</p>");
            body.Append("<p>Acesse agora: <a href='https://fmc.digital/ecredz'>www.negociadorcredz.fmcbrasil.com.br</a> </p>");
            body.Append("<p>ou diretamente no link abaixo.</p>");
            body.Append("<p>").Append("<a href='" + _objectSend.BilletUrl + "'>" + _objectSend.BilletUrl + "</a>").Append("</p>");
            body.Append("<br>");
            body.Append("<br>");
            body.Append("<br>");
            body.Append("<p>Caso já tenha efetuado o pagamento favor desconsiderar este e-mail.</p>");
            body.Append("<br>");
            body.Append("<br>");
            body.Append("<p><b>Equipe Negociador Credz</b></p>");
            body.Append("<p><b>4003 4031(Capitais e Regiões Metropolitanas) ou 0800 880 4031(demais regiões)</b></p>");
            body.Append("<p><img alt=\"\" style=\"width:100px\" src=\"https://negociadorcredz.fmcbrasil.com.br/images/topo/credz-logo-new.png\"></p>");
            if (!string.IsNullOrEmpty(_objectSend.CardUrl))
                body.Append("<p><img alt=\"\" style=\"width:150px\" src=\"").Append(_objectSend.CardUrl).Append("\">  </p>");
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



        private bool SendMail(byte[] billet, string subject, string body, string smtpName, string userSMTP, string passSMTP, IList<string> emails)
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


        /// Email Quebra
        public bool SendEmailBroken()
        {
            var mails = "";
            _objectSend.Email.ToList().ForEach(p => mails = p + ";");
            var sends = new SendBrokenEmailBLL().GetEmails(_objectSend.Product.IdProduct, DateTime.Today.AddDays(-30), mails);
            if (sends.Count() == 0 && _objectSend.Product.Lead.Where(p => p.DtInsert >= DateTime.Today.AddDays(-1)).Count() > 0)
            {
                var body = GetBodyQuebra();
                if (body != null)
                {
                    if (Util.SendMail("COMUNICADO IMPORTANTE CANCELAMENTO ACORDO " + _objectSend.CardName, body, _objectSend.CardName, "10.40.0.21", 25, "credz@fmccobranca.com.br", "UR3d@$23cmF", _objectSend.Email))
                    {
                        new SendBrokenEmailBLL().Add
                        (
                             new Business.Models.CREDZ.SendBrokenEmail()
                             {
                                 age = _objectSend.Product.Lead.OrderByDescending(p => p.IdLead).FirstOrDefault().Age,
                                 email = mails,
                                 IdProduct = _objectSend.Product.IdProduct,
                                 idPerson = _objectSend.Product.IdPerson,
                                 dtInsert = DateTime.Now
                             }
                        );

                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            return false;
        }
        private string GetBodyQuebra()
        {
            var body = new StringBuilder();

            body = GetMidleBody();
            if (body != null)
            {
                body.Append("<br>");
                body.Append("<p>Reforçamos que o site é seguro e que pode realizar sua negociação com rapidez e segurança, ");
                body.Append("basta clicar no link abaixo.</p>");
                body.Append("<p>Portal Negociação Credz: <a href='https://fmc.digital/ecredz'>www.negociadorcredz.fmcbrasil.com.br</a> </p>");
                body.Append("<p>Em caso de dúvidas, pode entrar em contato com nossa central de atendimento");
                body.Append(" nos telefones <b>4003 4031(Capitais e Regiões Metropolitanas) ou 0800 880 4031(Demais Regiões)</b>.</p>");
                body.Append("<br>");
                body.Append("<br>");
                body.Append("<p>Caso já tenha efetuado o pagamento favor desconsiderar este e-mail.</p>");
                body.Append("<br>");
                body.Append("<br>");
                body.Append("<p><b>Equipe Negociador Credz</b></p>");
                body.Append("<p><b>4003 4031(Capitais e Regiões Metropolitanas) ou 0800 880 4031(demais regiões)</b></p>");
                body.Append("<p><img alt=\"\" style=\"width:100px\" src=\"https://negociadorcredz.fmcbrasil.com.br/images/topo/credz-logo-new.png\">  </p>");
                if (_objectSend.Product.ProductSpecification != null)
                    body.Append("<p><img alt=\"\" style=\"width:150px\" src=\"").Append(_objectSend.Product.ProductSpecification.UrlImage).Append("\">  </p>");
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

                return body.ToString();
            }
            else
                return null;
        }

        private StringBuilder GetMidleBody()
        {

            var body = new StringBuilder();
            body.Append("<p>Olá ").Append(_objectSend.Name).Append("</p>");
            body.Append("<p>Você realizou uma renegociação no seu cartão <b>").Append(_objectSend.CardNumber).Append(" ").Append(_objectSend.CardName).Append("</b>, ");
            body.Append("infelizmente deve ter ocorrido algum imprevisto pois não localizamos o pagamento programado de ");

            var lead = _objectSend.Product.Lead.Where(p => p.DtInsert > _objectSend.Agreement.DtInsert).FirstOrDefault();
            if (lead != null)
            {
                var parcel = _objectSend.Agreement.AgreementParcel.Where(p => p.DtParcel < lead.DtInsert).OrderByDescending(p => p.DtParcel).FirstOrDefault();
                if (parcel != null)
                {

                    body.Append("R$").Append(parcel.VlParcel.ToString("N2")).Append(" para ").Append(parcel.DtParcel.ToString("dd/MM/yyyy")).Append(".</p>");
                    body.Append("<p>Acesse novamente nosso portal e reative seu acordo, ou simule com novo valor e data de entrada e escolha uma opção que caiba no seu orçamento!</p>");

                    AgreementSimulateResponse simulate = null;

                    var contrato = GetContratos();
                    if (contrato != null)
                        simulate = GetValueAgreement(0, contrato);

                    if (simulate != null && simulate.ParcelResponse != null && simulate.ParcelResponse.Count() > 0 && simulate.ParcelResponse.FirstOrDefault().VlParcel > 5)
                    {
                        var avista = simulate.ParcelResponse.OrderBy(p => p.NrParcel).FirstOrDefault();
                        var totalParcel = _objectSend.Agreement.AgreementParcel.Count() - parcel.NrParcel - 1;

                        simulate = GetValueAgreement(totalParcel > 1 ? totalParcel : 1, contrato);

                        var parcelamento = simulate.ParcelResponse.OrderByDescending(p => p.NrParcel).FirstOrDefault();


                        body.Append("</p>");
                        if (avista.ValueEntrace <= 400)
                            body.Append("Pague apenas R$").Append(avista.ValueEntrace.ToString("N2")).Append(" no pagamento a vista!");
                        else
                        {
                            if (parcelamento != null)
                            {
                                body.Append("Você pode refazer seu acordo com desconto de <b>R$").Append(parcelamento.VlDiscount.ToString("N2"));
                                body.Append("</b>, pagando uma entrada de <b>R$").Append(parcelamento.ValueEntrace.ToString("N2"));
                                body.Append("</b> e ").Append(parcelamento.NrParcel).Append(" parcelas de <b>R$");
                                body.Append(parcelamento.VlParcel).Append(" </b>.");
                            }
                            else
                                return null;
                        }

                        body.Append("</p>");
                        body.Append("<br>");
                        body.Append("<p>Esta oferta é válida até ").Append(DateTime.Today.AddDays(2).ToString("dd/MM/yyyy")).Append(" para pagamento até ").Append(avista.DtParcel.ToString("dd/MM/yyyy")).Append(".</p>");
                    }
                    else
                        return null;
                }
                else
                    return null;
            }
            else
                return null;

            body.Append("<p>Não perca essa oportunidade!</p>");
            body.Append("<br>");
            body.Append("<p>Lembramos que é muito importante o pagamento das parcelas dentro do prazo acordado, pois caso não haja pagamento automaticamente seu CPF será novamente encaminhado para negativação.</p>");

            return body;
        }

        private Contrato GetContratos()
        {
            var contracts = CobmaisAPI.GetContratos(_objectSend.Product.Person.NrCNPJCPF, "0", "0");


            if (contracts != null)
            {
                var contract = contracts.Where(p => p.numero_contrato == _objectSend.Product.DsProduct).FirstOrDefault();

                return contract;
            }
            return null;
        }

        private AgreementSimulateResponse GetValueAgreement(int nrParcel, Contrato contract)
        {
            try
            {

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
                            Age = _objectSend.Product.Lead.OrderByDescending(p => p.IdLead).FirstOrDefault().Age,
                            CPF = _objectSend.Product.Person.NrCNPJCPF,
                            DtEntrace = DateTime.Today.AddDays(7),
                            PctDiscount = 0,
                            NrParcel = nrParcel,
                            VlEntrace = 0,
                            Product = _objectSend.Product.DsProduct,
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

    public class Parcela
    {
        public decimal valor { get; set; }
        public DateTime vencimento { get; set; }
    }

    public class ObjectSend
    {
        public IList<string> Email { get; set; }

        public string Phone { get; set; }
        public string Name { get; set; }
        public long IdAgreement { get; set; }
        public long IdAgreementParcel { get; set; }
        public bool aVista { get; set; }
        public int Parcel { get; set; }
        public decimal Value { get; set; }
        public DateTime DtParcel { get; set; }
        public string CardNumber { get; set; }
        public string CardName { get; set; }
        public string BilletUrl { get; set; }
        public string CardUrl { get; set; }
        public string Line { get; set; }
        public byte[] Pdf { get; set; }
        public Product Product { get; set; }
        public Discount Discount { get; set; }
        public Agreement Agreement { get; set; }
    }

    public class Ag
    {
        public Pr Product { get; set; }
    }

    public class Pr
    {
        public Navigation Navigation { get; set; }
    }

    public class Navigation
    {
        public string DsOrigem { get; set; }
        public DateTime DtInsert { get; set; }
    }

}
