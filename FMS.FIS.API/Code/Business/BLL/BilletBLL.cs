using FMC.FIS.API.Code.Api.BvTelecom;
using FMC.FIS.API.Code.Api.Cobmais;
using FMC.FIS.API.Code.Api.OneB2K;
using FMC.FIS.API.Code.Api.Recupera;
using FMC.FIS.API.Code.Business.DAO;
using FMC.FIS.API.Models.Customer;
using FMC.FIS.API.Models.FIS;
using FMC.FIS.BLL;
using FMC.Generic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FMC.FIS.API.Code.Business.BLL
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

                Billet billetAviable = billets.Where(p => p.DtBillet >= billetRequest.DtBillet).OrderBy(p => p.DtBillet).FirstOrDefault();

                if (billetAviable == null)
                {

                    var billetCredz = CobmaisAPI.PostBoleto(Convert.ToInt64(billetRequest.CdAgreement), billetRequest.NrParcel);
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
                throw new Exception("metodo desabilitado");
            }

            /*
            if (productType == Constants.ProductType.AFINZ)
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
                Util.SendMail(pdf,
                               "Boleto para pagamento",
                               Util.BodyEmail(dtPayment.ToString("dd/MM/yyyy"), vlBillet.ToString("N2"), line),
                               Constants.HOST_SMTP,
                               Convert.ToInt32(Constants.PORT_SMTP),
                               Constants.USER_SMTP,
                               "FIS",
                               Constants.PASS_SMTP,
                               email);
            }

            return billetResponse;
        }


        private BilletResponse SendEmailCredz(string idBillet, DateTime dtPayment, decimal vlBillet, string line, string url, string email)
        {
            var pdf = new System.Net.WebClient().DownloadData(url);
            Util.SendMail(pdf,
                           "Boleto CredZ",
                           Util.BodyEmail(dtPayment.ToString("dd/MM/yyyy"), vlBillet.ToString("N2"), line),
                           Constants.HOST_SMTP,
                           Convert.ToInt32(Constants.PORT_SMTP),
                           Constants.USER_SMTP,
                           "CredZ",
                           Constants.PASS_SMTP,
                           email);

            var billetResponse = new BilletResponse();
            var billet = persistence.GetBykey(Convert.ToInt64(idBillet));

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
                    //BvTelecomAPI.SendSingle(sendSMSRequest.phone, message, 1064, "bolAPI" + billet.IdBillet.ToString());

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
