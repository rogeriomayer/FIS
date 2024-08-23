using FMC.FIS.BLL;
using FMC.FIS.Business.BLL;
using FMC.FIS.Business.Code.Api.Cobmais;
using FMC.FIS.Business.Models.Cobmais;
using FMC.FIS.Business.Models.Customer;
using FMC.FIS.Business.Models.FIS;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FMC.FIS.CREZ.EnvioEmailQuebra
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var currentProcess = System.Diagnostics.Process.GetCurrentProcess();
            var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false);

            IConfiguration config = builder.Build();

            Constants.UserCobmaisCredz = config.GetValue<string>("UserCobmaisCredz");
            Constants.PassCobmaisCredz = config.GetValue<string>("PassCobmaisCredz");
            Constants.UrlApiCobmaisCredz = config.GetValue<string>("UrlApiCobmaisCredz");

            try
            {
                foreach (var process in System.Diagnostics.Process.GetProcessesByName(currentProcess.ProcessName))
                {

                    Util.SaveFile("Processo em execução:" + process.MainModule.FileName + " SessionID:" + process.Id);
                    Util.SaveFile("Processo corrente:" + currentProcess.MainModule.FileName + " SessionID:" + currentProcess.Id);
                    if (process.MainModule.FileName == currentProcess.MainModule.FileName && process.Id != currentProcess.Id)
                    {
                        //Log.SaveFile("Matando processo " + process.MainModule.FileName + " SessionID:" + process.Id);
                        process.Kill();
                    }
                }

                while (DateTime.Now.DayOfWeek != DayOfWeek.Sunday && (DateTime.Now.TimeOfDay > new TimeSpan(05, 00, 001) && DateTime.Now.TimeOfDay < new TimeSpan(23, 58, 001)))
                {

                    //IList<Person> listPerson = new PersonBLL().GetPersonSentedSMS().ToList();
                    IList<Product> listProduct = new ProductBLL().GetDebitBalance100().ToList();

                    IList<Discount> discounts = new DiscountBLL().GetByProductType(3).ToList();

                    /*
                    if (listPerson.Count > 0)
                    {
                        string cpf = "";
                        IList<Contrato> contracts = null;

                        foreach (var person in listPerson.OrderBy(p => p.IdPerson).Distinct().ToList())
                        {
                            var emails = person.Email.Where(p => Util.IsEmail(p.DsEmail)).Select(p => p.DsEmail).ToList();

                            try
                            {

                                if (cpf != person.NrCNPJCPF)
                                {
                                    cpf = person.NrCNPJCPF;
                                    if (emails.Count > 0)
                                        contracts = CobmaisAPI.GetContratos(cpf, "0", "0");
                                }

                                var lead = person.Product.Where(pr => pr.Lead.Where(p => p.DtInsert >= DateTime.Today).Any()).FirstOrDefault().Lead.OrderByDescending(p => p.IdLead).FirstOrDefault();

                                if (lead != null && emails.Count() > 0 && contracts != null && contracts.Count > 0)
                                {
                                    if (SendEmail(lead, emails, discounts.Where(p => lead.Age >= p.MinAge && lead.Age <= p.MaxAge && p.MaxParcel == 1).FirstOrDefault()))
                                    {
                                        var ems = "";
                                        emails.ForEach(p => ems = p + ";");

                                        new SendEmailBLL().Add
                                            (
                                                new Business.Models.CREDZ.SendEmail()
                                                {
                                                    idPerson = person.IdPerson,
                                                    IdProduct = lead.Product.IdProduct,
                                                    age = lead.Age,
                                                    email = ems,
                                                    dtInsert = DateTime.Now
                                                }
                                            );
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
                                Util.SaveFile("Laço: Erro ao enviar e-mail para " + emails.FirstOrDefault() + " cpf " + person.NrCNPJCPF);
                                Util.SaveFile(erro);
                            }


                        }
                    }
                    */
                    if (listProduct.Count > 0)
                    {
                        string cpf = "";
                        IList<Contrato> contracts = null;

                        foreach (var product in listProduct.OrderBy(p => p.IdPerson).Distinct().ToList())
                        {
                            var person = product.Person;
                            var emails = person.Email.Where(p => Util.IsEmail(p.DsEmail)).Select(p => p.DsEmail).ToList();
                            try
                            {

                                if (cpf != person.NrCNPJCPF)
                                {
                                    cpf = person.NrCNPJCPF;
                                    if (emails.Count > 0)
                                        contracts = CobmaisAPI.GetContratos(cpf, "0", "0");
                                }

                                var lead = product.Lead.OrderByDescending(p => p.IdLead).FirstOrDefault();
                                //var lead = person.Product.Where(pr => pr.Lead.Where(p => p.DtInsert >= DateTime.Today.AddDays(-1)).Any()).FirstOrDefault().Lead.OrderByDescending(p => p.IdLead).FirstOrDefault();
                                //if (lead != null && emails.Count() > 0 && contracts != null && contracts.Count > 0)
                                //{
                                //if (SendEmail(lead, emails, discounts.Where(p => lead.Age >= p.MinAge && lead.Age <= p.MaxAge && p.MaxParcel == 1).FirstOrDefault()))
                                if (contracts != null && contracts.Where(p => p.numero_contrato == product.DsProduct).Count() > 0)
                                    if (SendEmailDebitBallance100(lead, emails, contracts.Where(p => p.numero_contrato == product.DsProduct).FirstOrDefault(), discounts.Where(p => lead.Age >= p.MinAge && lead.Age <= p.MaxAge && p.MaxParcel == 1).FirstOrDefault())) ;
                                    {
                                        var ems = "";
                                        emails.ForEach(p => ems = p + ";");

                                        new SendEmailBLL().Add
                                            (
                                                new Business.Models.CREDZ.SendEmail()
                                                {
                                                    idPerson = person.IdPerson,
                                                    IdProduct = product.IdProduct,
                                                    age = lead != null ? lead.Age : 0,
                                                    email = ems,
                                                    dtInsert = DateTime.Now
                                                }
                                            );
                                    }
                                //}
                            }
                            catch (Exception ex)
                            {
                                string erro = ex.Message + " | " + ex.StackTrace + Environment.NewLine;
                                while (ex.InnerException != null)
                                {
                                    ex = ex.InnerException;
                                    erro += ex.Message + " | " + ex.StackTrace + Environment.NewLine;
                                }
                                Util.SaveFile("Laço: Erro ao enviar e-mail para " + emails.FirstOrDefault() + " cpf " + person.NrCNPJCPF);
                                Util.SaveFile(erro);
                            }


                        }
                    }
                    Util.SaveFile("Processo finalizado!");
                    break;
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    erro += " | " + ex.Message;
                }
                Util.SaveFile("Erro:" + erro);
            }
        }
        private static bool SendEmail(Lead lead, IList<string> emails, Discount discount)
        {
            var body = GetBody(lead, discount);
            if (body != null)
            {
                if (Util.SendMail("OFERTA ESPECIAL " + lead.Product.ProductSpecification.Description, body, lead.Product.ProductSpecification.Description, "10.40.0.21", 25, "credz@fmccobranca.com.br", "UR3d@$23cmF", emails))
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
        private static bool SendEmailDebitBallance100(Lead lead, IList<string> emails, Contrato contract, Discount discount)
        {
            Product product = lead.Product;

            var body = GetBody(lead, discount);
            if (body != null)
            {

                var description = (product.ProductSpecification != null ? product.ProductSpecification.Description : "Credz");
                //if (Util.SendMail("Instabilidade no portal corrigida, você pode já negociar seu " + description, body, description, "10.40.0.92", "credz@fmccobranca.com.br", "UR3d@$23cmF", emails))
                if (Util.SendMail("Instabilidade no portal Credz corrigida, você já pode negociar seu " + description, body, description, "10.40.0.21", 25, "credz@fmccobranca.com.br", "UR3d@$23cmF", emails))
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
        private static string GetBody(Lead lead, Discount discount)
        {
            var product = lead.Product;

            var body = new StringBuilder();
            body.Append("<html>");
            body.Append("<p>Olá ").Append(product.Person.DsName).Append("</p>");
            body.Append("<p>Você acessou o portal de negociação Credz, porém estávamos com instabilidade no nosso sistema!</p>");
            body.Append(" Nossa equipe ténica já corrigiu o problema e você pode negociar a dívida do seu cartão <b>").Append(product.DsProduct.Substring(3, 6)).Append("********* ").Append(product.ProductSpecification.Description.Trim()).Append("</b>.");
            body.Append(" aproveitando os descontos de até <b>");
            body.Append(Convert.ToInt16(discount.MaxDiscount)).Append("% </b>!").Append("</p>");
            body.Append("<p>Não perca essa oportunidade!</p>");
            body.Append("<br>");
            body.Append("<p>Reforçamos que o portal é seguro e que pode realizar sua negociação com rapidez e segurança, ");
            body.Append("basta clicar no link abaixo.</p>");
            body.Append("<p>Portal Negociação Credz: <a href='https://fmc.digital/ecredz'>www.negociadorcredz.fmcbrasil.com.br</a> </p>");
            body.Append("<br>");
            body.Append("<p>Pedimos desculpa pelo transtorno, e agradecemos a compreensão.</p>");
            body.Append("<p>Caso já tenha efetuado o pagamento favor desconsiderar este e-mail.</p>");
            body.Append("<br>");
            body.Append("<br>");
            body.Append("<p><b>Equipe Negociador Credz</b></p>");
            body.Append("<p><b>4003 4031(Capitais e Regiões Metropolitanas) ou 0800 880 4031(demais regiões)</b></p>");
            body.Append("<p><img alt=\"\" style=\"width:100px\" src=\"https://negociadorcredz.fmcbrasil.com.br/images/topo/credz-logo-new.png\">  </p>");
            body.Append("<p><img alt=\"\" style=\"width:150px\" src=\"").Append(product.ProductSpecification.UrlImage).Append("\">  </p>");
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

        private static AgreementSimulateResponse GetValueAgreement(Lead lead, Contrato contract)
        {

            ICollection<ComplementData> complementData = new HashSet<ComplementData>();


            complementData.Add(new ComplementData() { Name = "negociacao_id", Value = contract.negociacao_id.ToString() });
            complementData.Add(new ComplementData() { Name = "id", Value = contract.parcelas.OrderBy(p => p.vencimento).FirstOrDefault().id.ToString() });
            complementData.Add(new ComplementData() { Name = "numero", Value = contract.parcelas.OrderBy(p => p.vencimento).FirstOrDefault().numero.ToString() });
            complementData.Add(new ComplementData() { Name = "vencimento", Value = contract.parcelas.OrderBy(p => p.vencimento).FirstOrDefault().vencimento.ToString("yyyy-MM-dd") });
            complementData.Add(new ComplementData() { Name = "valor", Value = contract.parcelas.OrderBy(p => p.vencimento).FirstOrDefault().valor.ToString("N2") });

            return new AgreementBLL().GetAgreementSimulate
                (
                    new Business.Models.Customer.AgreementSimulateRequest()
                    {
                        Age = lead.Age,
                        CPF = lead.Product.Person.NrCNPJCPF,
                        DtEntrace = DateTime.Today.AddDays(7),
                        PctDiscount = 0,
                        NrParcel = 1,
                        VlEntrace = 0,
                        Product = lead.Product.DsProduct,
                        CdSimulate = "",
                        ComplementData = complementData
                    }
                    , Constants.ProductType.CREDZ
                );
        }

    }



}
