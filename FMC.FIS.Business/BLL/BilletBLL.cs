using FMC.FIS.Business.Code.Api.BvTelecom;
using FMC.FIS.Business.Code.Api.Cobmais;
using FMC.FIS.Business.Code.Api.OneB2K;
using FMC.FIS.Business.Code.Api.Recupera;
using FMC.FIS.Business.DAO;
using FMC.FIS.Business.Models.Customer;
using FMC.FIS.Business.Models.FIS;
using FMC.FIS.BLL;
using FMC.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FMC.FIS.Business.Code.Api.Digicob;

namespace FMC.FIS.Business.BLL
{
    public class BilletBLL : BLL<Billet, BilletDAO>
    {
        public ICollection<BilletResponse> GetByIdProduct(long idProduct)
        {
            var listBillet = persistence.GetByIdProduct(idProduct);
            return listBillet.Select
                (
                    p => new BilletResponse()
                    {
                        IdBillet = p.IdBillet,
                        IdProduct = p.IdProduct,
                        IdAgreementParcel = p.IdAgreementParcel,
                        IdPromisse = p.IdPromisse,
                        VlBillet = p.VlBillet,
                        DtBillet = p.DtBillet,
                        Barcode = p.Barcode,
                        Line = p.Line,
                        DocumentNumber = p.DocumentNumber,
                        DtInsert = p.DtInsert,
                        CdAgreement = p.CdAgreement,
                        URL = p.URL,
                        CdBillet = p.CdBillet,
                        NrSendEmail = p.BilletEmail.Count(),
                        NrSendSMS = p.BilletSMS.Count(),
                        Parcel = p.AgreementParcel != null ? p.AgreementParcel.NrParcel : 0
                    }
                ).ToList();
        }

        /*
        public byte[] GetPDF(string cpf, string CdAgreement, DateTime dtPayment, Constants.ProductType productType)
        {
            byte[] billet = null;

            if (string.IsNullOrEmpty(CdAgreement))
            {
                var codPaymentOption = AfinzRecuperaAPI.SolicitarOpcoesPagamento(cpf, dtPayment, 0);
                var paymentOptions = AfinzRecuperaAPI.ConsultarOpcoesPagamento(cpf, codPaymentOption);

                if (paymentOptions != null && paymentOptions.OpcoesPagamento.Count > 0)
                {
                    var codPlan = paymentOptions.OpcoesPagamento.FirstOrDefault().Planos.Where(pl => pl.QtdeParcelas == "000").FirstOrDefault().CodigoPlano;
                    CdAgreement = AfinzRecuperaAPI.ConfirmarOpcaoPagamento(cpf, codPaymentOption, codPlan);
                }
            }


            if (productType == Constants.ProductType.AFINZ)
                billet = AfinzRecuperaAPI.EmitirBoletoPDF(cpf, CdAgreement, dtPayment, dtPayment);

            return billet;
        }
        */

        public BilletResponse AddNewBillet(NewBilletRequest billetRequest, Constants.ProductType productType)
        {
            BilletResponse billetResponse = new BilletResponse();

            if (productType == Constants.ProductType.CREDZ)
            {
                var billets = persistence.GetByCdAgreement(billetRequest.CdAgreement);

                Billet billetAviable = billets.Where(p => p.DtBillet.AddDays(10) >= billetRequest.DtBillet && p.AgreementParcel.Agreement.IdAgreementStatus != 2 && p.AgreementParcel.Payment.Count == 0).OrderBy(p => p.DtBillet).FirstOrDefault();

                if (billetAviable == null)
                {

                    var billetCredz = CobmaisAPI.PostBoleto(Convert.ToInt64(billetRequest.CdAgreement), billetRequest.CPF, billetRequest.NrParcel, billetRequest.DtBillet);
                    if (billetCredz != null)
                    {
                        billetAviable = persistence.Add
                            (
                                 new Billet()
                                 {
                                     IdProduct = billetRequest.IdProduct,
                                     DtBillet = billetCredz.vencimento,
                                     VlBillet = billetCredz.valor,
                                     Barcode = billetCredz.codigo_barra,
                                     Line = billetCredz.linha_digitavel,
                                     DocumentNumber = billetCredz.nosso_numero,
                                     URL = billetCredz.url,
                                     CdBillet = billetCredz.id.ToString(),
                                     CdAgreement = billetRequest.CdAgreement,
                                     IdAgreementParcel = billetRequest.IdAgreementParcel,
                                     DtInsert = DateTime.Now
                                 }
                            );
                    }
                    else
                    {
                        return null;
                    }
                }

                return new BilletResponse()
                {
                    IdBillet = billetAviable.IdBillet,
                    IdProduct = billetAviable.IdProduct,
                    IdAgreementParcel = billetAviable.IdAgreementParcel,
                    IdPromisse = billetAviable.IdPromisse,
                    VlBillet = billetAviable.VlBillet,
                    DtBillet = billetAviable.DtBillet,
                    Barcode = billetAviable.Barcode,
                    Line = billetAviable.Line,
                    DocumentNumber = billetAviable.DocumentNumber,
                    DtInsert = billetAviable.DtInsert,
                    CdAgreement = billetAviable.CdAgreement,
                    CdBillet = billetAviable.CdBillet,
                    URL = billetAviable.URL,
                    NrSendEmail = billetAviable.BilletEmail.Count(),
                    NrSendSMS = billetAviable.BilletSMS.Count(),
                    Parcel = billetAviable.AgreementParcel != null ? billetAviable.AgreementParcel.NrParcel : 0
                };
            }
            else
            {
                //throw new Exception("metodo desabilitado");
                return new BilletResponse()
                {
                    IdBillet = 1,
                    IdProduct = billetRequest.IdProduct,
                    IdAgreementParcel = null,
                    IdPromisse = null,
                    VlBillet = billetRequest.VlBillet,
                    DtBillet = billetRequest.DtBillet,
                    Barcode = "23799948400000209613391090004199420900101620",
                    Line = "23793.39100 90004.199429 09001.016204 9 94840000020961",
                    DocumentNumber = "09/00041994209-2",
                    DtInsert = Convert.ToDateTime("2024-01-31 11:37:57.573"),
                    CdAgreement = "123",
                    CdBillet = "",
                    URL = "http://cobranca.fmcbrasil.com.br/images/boletoteste.pdf",
                    NrSendEmail = 0,
                    NrSendSMS = 0,
                    Parcel = 0
                };
            }

            /*
            {

                if (string.IsNullOrEmpty(billetRequest.CdAgreement) || billetRequest.NrParcel > 0)
                {
                    var codPaymentOption = AfinzRecuperaAPI.SolicitarOpcoesPagamento(billetRequest.CPF, billetRequest.DtBillet, 0);
                    var paymentOptions = AfinzRecuperaAPI.ConsultarOpcoesPagamento(billetRequest.CPF, codPaymentOption);
                    if (paymentOptions != null && paymentOptions.OpcoesPagamento.Count > 0)
                    {
                        var codPlan = paymentOptions.OpcoesPagamento.FirstOrDefault().Planos.Where(pl => pl.QtdeParcelas == "000").FirstOrDefault().CodigoPlano;
                        billetRequest.CdAgreement = AfinzRecuperaAPI.ConfirmarOpcaoPagamento(billetRequest.CPF, codPaymentOption, codPlan);
                    }
                }


                var boletoResult = AfinzRecuperaAPI.EmitirBoleto(billetRequest.CPF, billetRequest.CdAgreement, billetRequest.DtBillet, billetRequest.DtBillet);

                if (boletoResult.Boleto != null)
                {
                    var boleto = null; boletoResult.Boleto;

                    var billet = persistence.GetByCdAgreement(billetRequest.CdAgreement);

                    Billet newBillet = null;

                    if (billet == null)
                        billet = new Billet();

                    var statusLead = new StatusLeadBLL().GetByCodAgreementRecupera(billetRequest.CdAgreement);
                    if (statusLead != null)
                    {
                        if (statusLead.Agreement.Count > 0)
                        {
                            AgreementParcel agreementParcel = statusLead.Agreement.FirstOrDefault().AgreementParcel.Where(p => p.NrParcel == billetRequest.NrParcel).FirstOrDefault();
                            if (agreementParcel != null)
                                billet.IdAgreementParcel = agreementParcel.IdAgreementParcel;
                            else
                                billet.IdAgreementParcel = 0;
                        }
                        else if (statusLead.Promisse.Count > 0)
                            billet.IdPromisse = statusLead.Promisse.FirstOrDefault().IdPromisse;
                        billet.DtInsert = DateTime.Now;
                    }
                    billet.IdProduct = billetRequest.IdProduct;
                    billet.DtBillet = boleto.DataVencimento;
                    billet.VlBillet = Convert.ToDecimal(boleto.ValorPagamento);
                    billet.Barcode = boleto.CodigoBarras;
                    billet.Line = boleto.LinhaDigitavel;
                    billet.DocumentNumber = boleto.NossoNumero;
                    billet.CdAgreement = billetRequest.CdAgreement;
                    billet.DtInsert = DateTime.Now;

                    if (billet.IdBillet == 0)
                        newBillet = Add(billet);
                    else
                        newBillet = Update(billet);

                    if (newBillet != null)
                    {
                        billetResponse.IdBillet = newBillet.IdBillet;
                        billetResponse.IdProduct = newBillet.IdProduct;
                        billetResponse.IdAgreementParcel = newBillet.IdAgreementParcel;
                        billetResponse.IdPromisse = newBillet.IdPromisse;
                        billetResponse.VlBillet = newBillet.VlBillet;
                        billetResponse.DtBillet = newBillet.DtBillet;
                        billetResponse.Barcode = newBillet.Barcode;
                        billetResponse.Line = newBillet.Line;
                        billetResponse.DocumentNumber = newBillet.DocumentNumber;
                        billetResponse.DtInsert = newBillet.DtInsert;
                        billetResponse.CdAgreement = newBillet.CdAgreement; //newBillet.AgreementParcel != null ? newBillet.AgreementParcel.Agreement.CdAgreement : (newBillet.Promisse != null ? newBillet.Promisse.CdAgreement : "");
                        billetResponse.NrSendEmail = newBillet.BilletEmail.Count();
                        billetResponse.NrSendSMS = newBillet.BilletSMS.Count();
                        billetResponse.Parcel = newBillet.AgreementParcel != null ? newBillet.AgreementParcel.NrParcel : 0;
                    }
                }
                else
                {
                    throw new Exception(boletoResult.StatusRetorno.MensagemRetorno);
                }
            }
            */


            throw (new Exception("Falha ao gerar o boleto"));
        }


        public BilletResponse SendEmail(SendEmailRequest sendEmailRequest, Constants.ProductType productType)
        {
            BilletResponse billetResponse = new BilletResponse();
            // if (productType == Constants.ProductType.AFINZ)
            // {
            //     billetResponse = SendEmailAfinz(sendEmailRequest.idProduct, sendEmailRequest.cpf, sendEmailRequest.codBillet, sendEmailRequest.parcel, sendEmailRequest.dtPayment, sendEmailRequest.email, sendEmailRequest.idUserLogin);
            // }
            if (productType == Constants.ProductType.CREDZ)
            {
                billetResponse = SendEmailCredz(sendEmailRequest.codBillet, sendEmailRequest.dtPayment, sendEmailRequest.vlBillet, sendEmailRequest.line, sendEmailRequest.urlPdf, sendEmailRequest.email);
            }
            else
            {
                billetResponse = SendEmailFIS(sendEmailRequest.cpf, sendEmailRequest.nrConta, sendEmailRequest.dtPayment, sendEmailRequest.vlBillet, sendEmailRequest.line, sendEmailRequest.email);
            }

            return billetResponse;
        }

        private BilletResponse SendEmailFIS(string cpf, string nrConta, DateTime dtPayment, decimal vlBillet, string line, string email)
        {
            BilletResponse billetResponse = new BilletResponse();

            var idPdf = OneB2KApi.GetIdFaturaPDF(cpf, nrConta, "PF", dtPayment.AddMonths(-36), dtPayment);
            if (idPdf != null && idPdf.responseData != null && idPdf.responseData.listaPDF != null && idPdf.responseData.listaPDF.Count > 0)
            {
                var ultimaFatura = idPdf.responseData.listaPDF.OrderByDescending(p => p.dtVencimento).FirstOrDefault();
                var pdf = OneB2KApi.GetFaturaPDF(ultimaFatura.idPDF);
                var attachments = new Dictionary<string, byte[]>();
                attachments.Add("boleto.pdf", pdf);
                Util.SendMail("Boleto para pagamento",
                               Util.BodyEmail(dtPayment, vlBillet.ToString("N2"), line),
                               "FIS",
                               Constants.HOST_SMTP,
                               25,
                               Constants.USER_SMTP,
                               Constants.PASS_SMTP,
                               new List<string> { email },
                               attachments);
            }

            return billetResponse;
        }


        private BilletResponse SendEmailCredz(string idBillet, DateTime dtPayment, decimal vlBillet, string line, string url, string email)
        {
            var pdf = new System.Net.WebClient().DownloadData(url);
            var attachments = new Dictionary<string, byte[]>();
            attachments.Add("boletoCredz.pdf", pdf);

            var billet = persistence.GetBykey(Convert.ToInt64(idBillet));

            var product = new ProductBLL().GetBykey(billet.IdProduct);

            if (product is null)
                product = billet.AgreementParcel.Agreement.StatusLead.Lead.Product;


            if (product != null && product.ProductSpecification != null)
            {
                Util.SendMail("BOLETO ACORDO" + product.ProductSpecification.Description.ToUpper(),
                           BodyEmail(billet, product),
                           product.ProductSpecification.Description.ToUpper(),
                           Constants.HOST_SMTP,
                           25,
                           Constants.USER_SMTP,
                           Constants.PASS_SMTP,
                           new List<string> { email },
                           attachments);
            }
            else
            {
                Util.SendMail("BOLETO ACORDO CREDZ VISA",
                           BodyEmail(billet, product),
                           "BOLETO ACORDO CREDZ VISA",
                           Constants.HOST_SMTP,
                           25,
                           Constants.USER_SMTP,
                           Constants.PASS_SMTP,
                           new List<string> { email },
                           attachments);
            }


            var billetResponse = new BilletResponse();


            billet.BilletEmail.Add
                (
                    new BilletEmail()
                    {
                        Email = email,
                        DtInsert = DateTime.Now,
                        IdUserLogin = 1
                    }
                );

            var newBillet = Update(billet);

            if (newBillet != null)
            {
                billetResponse.IdBillet = newBillet.IdBillet;
                billetResponse.IdAgreementParcel = newBillet.IdAgreementParcel;
                billetResponse.IdPromisse = newBillet.IdPromisse;
                billetResponse.VlBillet = newBillet.VlBillet;
                billetResponse.DtBillet = newBillet.DtBillet;
                billetResponse.Barcode = newBillet.Barcode;
                billetResponse.Line = newBillet.Line;
                billetResponse.DocumentNumber = newBillet.DocumentNumber;
                billetResponse.DtInsert = newBillet.DtInsert;
                billetResponse.CdAgreement = newBillet.CdAgreement;
                billetResponse.CdBillet = newBillet.CdBillet;
                billetResponse.URL = newBillet.URL;
                billetResponse.NrSendEmail = newBillet.BilletEmail.Count();
                billetResponse.NrSendSMS = newBillet.BilletSMS.Count();
                billetResponse.Parcel = newBillet.AgreementParcel != null ? newBillet.AgreementParcel.NrParcel : 0;
            }

            return billetResponse;

        }

        public BilletResponse SendSMS(SendSMSRequest sendSMSRequest, Constants.ProductType productType)
        {
            var billetResponse = new BilletResponse();
            var billet = persistence.GetBykey(Convert.ToInt64(sendSMSRequest.codBillet));

            if (billet != null)
            {
                string message = string.Format("Segue a linha digitavel para pagamento da parcela {0} do seu acordo {1}", sendSMSRequest.parcel, billet.Line);
                if (productType == Constants.ProductType.CREDZ)
                {
                    BvTelecomAPI.SendSingle(sendSMSRequest.phone, message, 1064, "bolAPI" + billet.IdBillet.ToString());
                }

                billet.BilletSMS.Add
                    (
                        new BilletSMS()
                        {
                            Phone = sendSMSRequest.phone,
                            DtInsert = DateTime.Now,
                            IdUserLogin = 1
                        }
                    );

                var newBillet = Update(billet);

                if (newBillet != null)
                {
                    billetResponse.IdBillet = newBillet.IdBillet;
                    billetResponse.IdAgreementParcel = newBillet.IdAgreementParcel;
                    billetResponse.IdPromisse = newBillet.IdPromisse;
                    billetResponse.VlBillet = newBillet.VlBillet;
                    billetResponse.DtBillet = newBillet.DtBillet;
                    billetResponse.Barcode = newBillet.Barcode;
                    billetResponse.Line = newBillet.Line;
                    billetResponse.DocumentNumber = newBillet.DocumentNumber;
                    billetResponse.DtInsert = newBillet.DtInsert;
                    billetResponse.CdAgreement = newBillet.CdAgreement;
                    billetResponse.CdBillet = newBillet.CdBillet;
                    billetResponse.URL = newBillet.URL;
                    billetResponse.NrSendEmail = newBillet.BilletEmail.Count();
                    billetResponse.NrSendSMS = newBillet.BilletSMS.Count();
                    billetResponse.Parcel = newBillet.AgreementParcel != null ? newBillet.AgreementParcel.NrParcel : 0;
                }

                return billetResponse;
            }
            else
                return null;
        }


        public static string BodyEmail(Billet billet, Product product)
        {
            StringBuilder body = new StringBuilder();
            body.Append("<html>");
            body.Append("<p>Olá ").Append(product.Person.DsName).Append("</p>");
            body.Append("<br>");
            if (billet.DtBillet < DateTime.Today)
            {
                body.Append("Não identificamos o pagamento ");
            }
            else
            {
                body.Append("<p>Segue o boleto para pagamento ");
            }
            if (billet.AgreementParcel.NrParcel == 0)
                if (billet.AgreementParcel.Agreement.QtParcel == 0)
                    body.Append("do seu acordo no valor de R$").Append(billet.VlBillet.ToString("N2"));
                else
                    body.Append("da entrada do seu acordo no valor de R$").Append(billet.VlBillet.ToString("N2"));
            else
                body.Append("da parcela ").Append(billet.AgreementParcel.NrParcel + 1).Append(" do seu acordo no valor de R$").Append(billet.VlBillet.ToString("N2"));

            body.Append(" referente ao cartão ").Append(product.DsProduct.Substring(0, 6)).Append("********** ").Append(product.ProductSpecification != null ? product.ProductSpecification.Description : "CREDZ Visa").Append(" com vencimento em ").Append(billet.DtBillet.ToString("dd/MM/yyyy"));
            body.Append("</p>");
            if (billet.AgreementParcel.NrParcel == 0)
                body.Append("<p>Lembrando que só ocorrerá a efetivação do acordo e a retirada da negativação do seu CPF dos orgão de proteção de crédito após constar o pagamento deste boleto. </p>");
            else
                body.Append("<p>Realize o pagamento da parcela e evite a quebra do seu acordo e com isso lançamento de novos encargos e nova negativação do seu CPF. </p>");

            body.Append("<p><b>Caso não consiga realizar o pagamento do boleto até a data de vencimento este mesmo boleto é válido para pagamento até o dia ").Append(billet.DtBillet.AddDays(8).ToString("dd/MM/yyyy"));
            body.Append(" sem acréscimo de juros. </b></p>");

            body.Append("<br>");

            body.Append("<p><b>O boleto pode levar até 24 horas para ser registrado junto ao banco emissor, se ocorrer algum erro no pagamento favor aguardar 2 horas ou até o próximo dia útil para nova tentativa de pagamento do boleto. Caso o erro persiste favor entrar em contato.</b></p>");

            body.Append("<p><b>Lembramos CREDZ foi adquirida pela DM CARD, a partir de agora os boletos serão emitidos em nome da DM FINANCEIRA S.A. - CRÉDITO, FINANCIAMENTO E INVESTIMENTO CNPJ: 91.669.747/0001-92.</b></p>");

            body.Append("<br>");

            body.Append("<p>Linha digitável para pagamento: </p> <b>").Append(billet.Line);
            body.Append("<br>");
            body.Append("<br> ");
            body.Append("<p>Segue também em anexo o seu boleto para pagamento.</p>");
            body.Append("<br>");
            body.Append("<br>");


            body.Append("<p>Você também pode retirar a segunda no nosso potal.</p>");
            body.Append("<p>Acesse agora: <a href='https://fmc.digital/ecredz'>www.negociadorcredz.fmcbrasil.com.br</a> </p>");
            body.Append("<p>ou diretamente no link abaixo.</p>");
            body.Append("<p>").Append("<a href='" + billet.URL + "'>" + billet.URL + "</a>").Append("</p>");
            body.Append("<br>");
            body.Append("<br>");
            body.Append("<br>");
            body.Append("<p>Caso já tenha efetuado o pagamento favor desconsiderar este e-mail.</p>");
            body.Append("<br>");
            body.Append("<br>");
            body.Append("<p><b>Equipe Negociador Credz</b></p>");
            body.Append("<p><b>4003 4031(Capitais e Regiões Metropolitanas) ou 0800 880 4031(demais regiões)</b></p>");
            body.Append("<p><img alt=\"\" style=\"width:100px\" src=\"https://negociadorcredz.fmcbrasil.com.br/images/topo/credz-logo-new.png\"></p>");
            if (product.ProductSpecification != null)
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

        /*
        private BilletResponse SendEmailAfinz(long idProduct, string cpf, string CdAgreement, int parcel, DateTime dtPayment, string email, int idUserLogin)
        {
            BilletResponse billetResponse = new BilletResponse();
            var boleto = AfinzRecuperaAPI.EmitirBoletoEmail(cpf, CdAgreement, dtPayment, dtPayment, email);
            if (boleto != null)
            {
                var billet = persistence.GetByCdAgreement(CdAgreement);
                Billet newBillet = null;
                if (billet == null)
                {
                    billet = new Billet();
                }
                var statusLead = new StatusLeadBLL().GetByCodAgreementRecupera(CdAgreement);
                if (statusLead != null)
                {
                    if (statusLead.Agreement.Count > 0)
                    {
                        AgreementParcel agreementParcel = statusLead.Agreement.FirstOrDefault().AgreementParcel.Where(p => p.NrParcel == parcel).FirstOrDefault();
                        if (agreementParcel != null)
                            billet.IdAgreementParcel = agreementParcel.IdAgreementParcel;
                        else
                            billet.IdAgreementParcel = 0;
                    }
                    else if (statusLead.Promisse.Count > 0)
                        billet.IdPromisse = statusLead.Promisse.FirstOrDefault().IdPromisse;
                    billet.DtInsert = DateTime.Now;
                }

                billet.IdProduct = idProduct;
                billet.DtBillet = boleto.DataVencimento;
                billet.VlBillet = Convert.ToDecimal(boleto.ValorPagamento);
                billet.Barcode = boleto.CodigoBarras;
                billet.Line = boleto.LinhaDigitavel;
                billet.DocumentNumber = boleto.NossoNumero;
                billet.CdAgreement = CdAgreement;
                billet.BilletEmail.Add
                    (
                        new BilletEmail()
                        {
                            Email = email,
                            DtInsert = DateTime.Now,
                            IdUserLogin = idUserLogin
                        }
                    );

                if (billet.IdBillet == 0)
                    newBillet = Add(billet);
                else
                    newBillet = Update(billet);

                if (newBillet != null)
                {
                    billetResponse.IdBillet = newBillet.IdBillet;
                    billetResponse.IdAgreementParcel = newBillet.IdAgreementParcel;
                    billetResponse.IdPromisse = newBillet.IdPromisse;
                    billetResponse.VlBillet = newBillet.VlBillet;
                    billetResponse.DtBillet = newBillet.DtBillet;
                    billetResponse.Barcode = newBillet.Barcode;
                    billetResponse.Line = newBillet.Line;
                    billetResponse.DocumentNumber = newBillet.DocumentNumber;
                    billetResponse.DtInsert = newBillet.DtInsert;
                    billetResponse.CdAgreement = newBillet.CdAgreement; //newBillet.AgreementParcel != null ? newBillet.AgreementParcel.Agreement.CdAgreement : (newBillet.Promisse != null ? newBillet.Promisse.CdAgreement : "");
                    billetResponse.NrSendEmail = newBillet.BilletEmail.Count();
                    billetResponse.NrSendSMS = newBillet.BilletSMS.Count();
                    billetResponse.Parcel = newBillet.AgreementParcel != null ? newBillet.AgreementParcel.NrParcel : 0;
                }
            }
            return billetResponse;
        }
        */

        /*
        public BilletResponse SendSMS(long idProduct, string cpf, string CdAgreement, int parcel, DateTime dtPayment, string phone, int idUserLogin, Constants.ProductType productType)
        {
            BilletResponse billetResponse = new BilletResponse();
            if (productType == Constants.ProductType.AFINZ)
            {
                var boleto = AfinzRecuperaAPI.EmitirBoletoSMS(cpf, CdAgreement, dtPayment, dtPayment, phone);
                if (boleto != null)
                {
                    var billet = persistence.GetByCdAgreement(CdAgreement);
                    Billet newBillet = null;
                    if (billet == null)
                    {
                        billet = new Billet();
                    }
                    var statusLead = new StatusLeadBLL().GetByCodAgreementRecupera(CdAgreement);
                    if (statusLead != null)
                    {
                        if (statusLead.Agreement.Count > 0)
                        {
                            AgreementParcel agreementParcel = statusLead.Agreement.FirstOrDefault().AgreementParcel.Where(p => p.NrParcel == parcel).FirstOrDefault();
                            if (agreementParcel != null)
                                billet.IdAgreementParcel = agreementParcel.IdAgreementParcel;
                            else
                                billet.IdAgreementParcel = 0;
                        }
                        else if (statusLead.Promisse.Count > 0)
                            billet.IdPromisse = statusLead.Promisse.FirstOrDefault().IdPromisse;
                        billet.DtInsert = DateTime.Now;
                    }
                    billet.IdProduct = idProduct;
                    billet.DtBillet = boleto.DataVencimento;
                    billet.VlBillet = Convert.ToDecimal(boleto.ValorPagamento);
                    billet.Barcode = boleto.CodigoBarras;
                    billet.Line = boleto.LinhaDigitavel;
                    billet.DocumentNumber = boleto.NossoNumero;
                    billet.CdAgreement = CdAgreement;
                    billet.BilletSMS.Add
                        (
                            new BilletSMS()
                            {
                                Phone = phone,
                                DtInsert = DateTime.Now,
                                IdUserLogin = idUserLogin
                            }
                        );

                    if (billet.IdBillet == 0)
                        newBillet = Add(billet);
                    else
                        newBillet = Update(billet);



                    if (newBillet != null)
                    {
                        billetResponse.IdBillet = newBillet.IdBillet;
                        billetResponse.IdProduct = newBillet.IdProduct;
                        billetResponse.IdAgreementParcel = newBillet.IdAgreementParcel;
                        billetResponse.IdPromisse = newBillet.IdPromisse;
                        billetResponse.VlBillet = newBillet.VlBillet;
                        billetResponse.DtBillet = newBillet.DtBillet;
                        billetResponse.Barcode = newBillet.Barcode;
                        billetResponse.Line = newBillet.Line;
                        billetResponse.DocumentNumber = newBillet.DocumentNumber;
                        billetResponse.DtInsert = newBillet.DtInsert;
                        billetResponse.CdAgreement = newBillet.CdAgreement;//newBillet.AgreementParcel != null ? newBillet.AgreementParcel.Agreement.CdAgreement : (newBillet.Promisse != null ? newBillet.Promisse.CdAgreement : "");
                        billetResponse.NrSendEmail = newBillet.BilletEmail.Count();
                        billetResponse.NrSendSMS = newBillet.BilletSMS.Count();
                        billetResponse.Parcel = newBillet.AgreementParcel != null ? newBillet.AgreementParcel.NrParcel : 0;
                    }
                }
            }
            return billetResponse;
        }
        */
    }
}
