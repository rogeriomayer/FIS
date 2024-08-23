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
        private static IList<Discount> discounts = new List<Discount>();
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

                while (DateTime.Now.DayOfWeek != DayOfWeek.Sunday && (DateTime.Now.TimeOfDay > new TimeSpan(08, 00, 001) && DateTime.Now.TimeOfDay < new TimeSpan(23, 58, 001)))
                {
                    IList<Product> products = new ProductBLL().GetProductQuebraExterna().ToList();
                    discounts = new DiscountBLL().GetByProductType(3).ToList();
                    Util.SaveFile("Foram encontrados " + products.Count + " contas quebra interna ");
                    if (products.Count > 0)
                    {
                        string account = "";

                        foreach (var product in products.OrderBy(p => p.DsProduct).Distinct().ToList())
                        {
                            var emails = product.Person.Email.Where(p => Util.IsEmail(p.DsEmail)).Select(p => p.DsEmail).ToList();

                            if (account != product.DsProduct && emails != null && emails.Count() > 0)
                            {
                                account = product.DsProduct;

                                try
                                {
                                    var lead = product.Lead.Where(p => p.DtInsert >= DateTime.Today).OrderByDescending(p => p.IdLead).FirstOrDefault();

                                    if (lead != null)
                                    {
                                        if (SendEmail(product, emails, discounts))
                                        {
                                            var ems = "";
                                            emails.ForEach(p => ems = p + ";");

                                            new SendBrokenEmailBLL().Add
                                                (
                                                    new Business.Models.CREDZ.SendBrokenEmail()
                                                    {
                                                        idPerson = product.IdPerson,
                                                        IdProduct = product.IdProduct,
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
                                    Util.SaveFile("Laço: Erro ao enviar e-mail para " + emails.FirstOrDefault() + " conta " + product.DsProduct);
                                    Util.SaveFile(erro);
                                }
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
        private static bool SendEmail(Product product, IList<string> emails, IList<Discount> discounts)
        {
            var body = GetBody(product, discounts);
            if (body != null)
            {
                if (Util.SendMail("COMUNICADO IMPORTANTE CANCELAMENTO ACORDO " + product.ProductSpecification.Description, body, product.ProductSpecification.Description, "10.40.0.21", 25, "credz@fmccobranca.com.br", "UR3d@$23cmF", emails))
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
        private static string GetBody(Product product, IList<Discount> discounts)
        {
            var body = new StringBuilder();
            body.Append("<html>");
            body.Append("<html>");
            body = GetMidleBody(product);
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
            else
                return null;
        }

        private static StringBuilder GetMidleBody(Product product)
        {

            var body = new StringBuilder();
            body.Append("<p>Olá ").Append(product.Person.DsName.Trim()).Append("</p>");
            body.Append("<p>Você realizou uma renegociação no seu cartão <b>");
            body.Append(product.DsProduct.StartsWith("000") ? product.DsProduct.Substring(3, 8) + "********" : product.DsProduct.Substring(0, 8));
            body.Append(" ").Append(product.ProductSpecification.Description).Append("</b>, ");
            body.Append("infelizmente deve ter ocorrido algum imprevisto pois não localizamos o pagamento programado!");

            body.Append("<p>Acesse novamente nosso portal e reative seu acordo, ou simule com novo valor e data de entrada e escolha uma opção que caiba no seu orçamento!</p>");

            AgreementSimulateResponse simulate = null;
            var lead = product.Lead.OrderByDescending(p => p.DtInsert).FirstOrDefault();
            var contrato = GetContratos(lead);
            if (contrato != null)
                simulate = GetValueAgreement(0, contrato, lead);

            if (simulate != null && simulate.ParcelResponse != null && simulate.ParcelResponse.Count() > 0 && simulate.ParcelResponse.FirstOrDefault().VlParcel > 5)
            {
                var avista = simulate.ParcelResponse.OrderBy(p => p.NrParcel).FirstOrDefault();
                var desconto = discounts.Where(p => (lead.Age >= p.MinAge && lead.Age <= p.MaxAge) && p.MaxParcel == 1).FirstOrDefault().MaxDiscount;
                var totalParcel = 24;
                for (int i = 24; i > 0; i--)
                {
                    decimal vlParcel = 50;
                    totalParcel = i;
                    vlParcel = (contrato.parcelas.FirstOrDefault().valor - (contrato.parcelas.FirstOrDefault().valor * (desconto / 100))) / i;
                    if (vlParcel > 70)
                    {
                        break;
                    }
                }

                simulate = GetValueAgreement(totalParcel > 1 ? totalParcel : 1, contrato, lead);

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


            body.Append("<p>Não perca essa oportunidade!</p>");
            body.Append("<br>");
            body.Append("<p>Lembramos que é muito importante o pagamento das parcelas dentro do prazo acordado, pois caso não haja pagamento automaticamente seu CPF será novamente encaminhado para negativação.</p>");

            return body;
        }

        private static Contrato GetContratos(Lead lead)
        {
            var contracts = CobmaisAPI.GetContratos(lead.Product.Person.NrCNPJCPF, "0", "0");


            if (contracts != null)
            {
                var contract = contracts.Where(p => p.numero_contrato == lead.Product.DsProduct).FirstOrDefault();

                return contract;
            }
            return null;
        }

        private static AgreementSimulateResponse GetValueAgreement(int nrParcel, Contrato contract, Lead lead)
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
                if (nrParcel > 1 && ex.ToString().Contains("Valor de Parcela abaixo do valor mínimo permitido"))
                {
                    nrParcel = nrParcel - 2;
                    return GetValueAgreement(nrParcel, contract, lead);
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

}
