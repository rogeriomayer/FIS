using FMC.FIS.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMC.FIS.Model;
using FMC.Generic;
using FMC.FIS.Business.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using FMC.FIS.Business.Models.FIS;
using FMC.FIS.Business.DAO;
using FMC.FIS.Business.Code.Api.Recupera;
using FMC.FIS.Business.BLL;
using FMC.FIS.Business.Models.Customer;
using FMC.FIS.Business.Code.Api.Cobmais;

namespace FMC.FIS.BLL
{
    public class StatusLeadBLL : BLL<StatusLead, StatusLeadDAO>
    {

        public ICollection<StatusLeadResponse> GetByIdUser(int idUser, int idProductType, DateTime dtIni, DateTime dtFim)
        {
            return persistence.GetbyIdUser(idUser, idProductType, dtIni, dtFim).Select
                (
                    p => CreateStatusLeadResponse(p)
                ).ToList();
        }


        public ICollection<StatusLeadResponse> GetbyIdsUser(StatusLeadRequest statusLeadRequest, byte idProductType)
        {
            return persistence.GetbyIdsUser(statusLeadRequest.idUser, idProductType, statusLeadRequest.dtIni, statusLeadRequest.dtFim);
        }

        public StatusLeadResponse Add(StatusLead statusLead, string cpf, string telefone, Constants.ProductType productType)
        {

            var newStatusLead = Add(cpf, statusLead, productType);


            if (productType == Constants.ProductType.AFINZ)
            {
                string ocorrencia = new StatusBLL().GetBykey(newStatusLead.IdStatus).CdStatus;
                string complemento = newStatusLead.DsDescription;

                string dtAgenda = DateTime.Today.AddDays(newStatusLead.Status.DaysSystem).ToString("yyyy-MM-dd");
                if (newStatusLead.Promisse.Count > 0)
                {
                    var billetDAO = new BilletDAO();
                    var billets = billetDAO.GetByIdProduct(newStatusLead.Lead.IdProduct);
                    Billet billet = billets.Where(p => p.DtInsert >= DateTime.Today
                                                && p.VlBillet == statusLead.Promisse.FirstOrDefault().VlPromisse
                                                && p.DtBillet.Date == statusLead.Promisse.FirstOrDefault().DtPromisse).FirstOrDefault();
                    if (billet != null)
                    {
                        var statusLeadPromisse = new StatusLeadDAO().GetBykey(newStatusLead.IdStatusLead);
                        if (statusLeadPromisse.Promisse != null && statusLeadPromisse.Promisse.Count > 0)
                        {
                            billet.IdPromisse = statusLeadPromisse.Promisse.FirstOrDefault().IdPromisse;
                            billetDAO.Update(billet);
                        }
                    }

                    if (newStatusLead.Promisse.FirstOrDefault().DtPromisse < DateTime.Today.AddDays(4))
                        dtAgenda = newStatusLead.Promisse.FirstOrDefault().DtPromisse.AddDays(1).ToString("yyyy-MM-dd");
                    else
                        dtAgenda = newStatusLead.Promisse.FirstOrDefault().DtPromisse.ToString("yyyy-MM-dd");
                }
                if (newStatusLead.Agreement.Count > 0)
                {
                    var billetDAO = new BilletDAO();
                    var billets = billetDAO.GetByIdProduct(newStatusLead.Lead.IdProduct);
                    Billet billet = billets.Where(p => p.DtInsert >= DateTime.Today
                                                && p.VlBillet == statusLead.Promisse.FirstOrDefault().VlPromisse
                                                && p.DtBillet.Date == statusLead.Promisse.FirstOrDefault().DtPromisse).FirstOrDefault();
                    if (billet != null)
                    {
                        billet.IdProduct = billet.AgreementParcel.Agreement.StatusLead.Lead.IdLead;
                        billetDAO.Update(billet);
                    }
                }


                if (newStatusLead.CallBack != null && newStatusLead.CallBack.Count > 0)
                {
                    dtAgenda = newStatusLead.CallBack.FirstOrDefault().DtCallBack.ToString("yyyy-MM-dd");
                    telefone = newStatusLead.CallBack.FirstOrDefault().NrPhone;
                }


            }


            return CreateStatusLeadResponse(newStatusLead);
        }

        internal static StatusLeadResponse CreateStatusLeadResponse(StatusLead newStatusLead)
        {
            try
            {
                var statusLeadResponse = new StatusLeadResponse();

                statusLeadResponse.IdStatusLead = newStatusLead.IdStatusLead;
                statusLeadResponse.IdStatus = newStatusLead.IdStatus;
                statusLeadResponse.Status = newStatusLead.Status != null ? newStatusLead.Status.DsStatus : "";
                statusLeadResponse.Description = newStatusLead.DsDescription;
                statusLeadResponse.DtStatusLead = newStatusLead.DtInsert;
                statusLeadResponse.FlEfective = newStatusLead.Status != null ? newStatusLead.Status.FlEfective : true;
                statusLeadResponse.IdUserLogin = newStatusLead.IdUserLogin;
                var userLogin = new FisLoginBLL().GetBykey(newStatusLead.IdUserLogin);
                statusLeadResponse.UserLogin = userLogin != null ? userLogin.DsName : "";

                if (newStatusLead.Promisse != null && newStatusLead.Promisse.Count > 0)
                    statusLeadResponse.PromisseResponse =
                                            newStatusLead.Promisse.Select(pr => new PromisseResponse()
                                            {
                                                IdPromisse = pr.IdPromisse,
                                                DtPromisse = pr.DtPromisse,
                                                DtInsert = pr.DtInsert,
                                                VlPromisse = pr.VlPromisse,
                                            }).FirstOrDefault();

                if (newStatusLead.Agreement != null && newStatusLead.Agreement.Count > 0)
                    foreach (var a in newStatusLead.Agreement)
                    {
                        var agreementResponse = new AgreementResponse();
                        agreementResponse.IdAgreement = a.IdAgreement;
                        agreementResponse.IdStatusLead = a.IdStatusLead;
                        agreementResponse.QtParcel = a.QtParcel;
                        agreementResponse.VlEntrace = a.VlEntrace;
                        agreementResponse.DtEntrace = a.DtEntrace;
                        agreementResponse.VlParcel = a.VlParcel;
                        agreementResponse.VlAgreement = a.VlAgreement;
                        agreementResponse.PcDiscount = a.PcDiscount;
                        agreementResponse.CdAgreement = a.CdAgreement;
                        agreementResponse.CdParcelPlan = a.CdParcelPlan;
                        agreementResponse.CdPaymentOption = a.CdPaymentOption;
                        agreementResponse.DtInsert = a.DtInsert;
                        agreementResponse.IdAgreementStatus = a.IdAgreementStatus;
                        agreementResponse.Status = a.AgreementStatus != null ? a.AgreementStatus.DsAgreementStatus : "EmAberto";
                        foreach (var ap in a.AgreementParcel)
                        {
                            var agreementParcelResponse = new AgreementParcelResponse();
                            agreementParcelResponse.IdAgreementParcel = ap.IdAgreementParcel;
                            agreementParcelResponse.NrParcel = ap.NrParcel;
                            agreementParcelResponse.DtParcel = ap.DtParcel;
                            agreementParcelResponse.Status = ap.IdAgreementStatus == null ? "EmAberto" : (ap.IdAgreementStatus == 2 ? "Quebrado" : "Pago");
                            agreementParcelResponse.VlParcel = ap.VlParcel;

                            foreach (var b in ap.Billet)
                            {
                                var billetResponse = new Business.Models.Customer.BilletResponse();
                                billetResponse.IdBillet = b.IdBillet;
                                billetResponse.IdProduct = b.IdProduct;
                                billetResponse.IdAgreementParcel = b.IdAgreementParcel;
                                billetResponse.IdPromisse = b.IdPromisse;
                                billetResponse.VlBillet = b.VlBillet;
                                billetResponse.DtBillet = b.DtBillet;
                                billetResponse.Barcode = b.Barcode;
                                billetResponse.Line = b.Line;
                                billetResponse.DocumentNumber = b.DocumentNumber;
                                billetResponse.DtInsert = b.DtInsert;
                                billetResponse.CdAgreement = b.CdAgreement;
                                billetResponse.CdBillet = b.CdBillet;
                                billetResponse.URL = b.URL;
                                billetResponse.NrSendEmail = b.BilletEmail.Count();
                                billetResponse.NrSendSMS = b.BilletSMS.Count();
                                billetResponse.Parcel = b.AgreementParcel != null ? b.AgreementParcel.NrParcel : 0;

                                agreementParcelResponse.BilletResponse.Add(billetResponse);
                            }

                            agreementResponse.AgreementParcelResponse.Add(agreementParcelResponse);
                        }
                        statusLeadResponse.AgreementResponse = agreementResponse;
                    }

                return statusLeadResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private StatusLead Add(string cpf, StatusLead statusLead, Constants.ProductType productType)
        {
            if (productType == Constants.ProductType.AFINZ)
            {
                if (statusLead.Agreement != null && statusLead.Agreement.Count > 0)
                {
                    //var codAgreementRecupera = AfinzRecuperaAPI.ConfirmarOpcaoPagamento(cpf, statusLead.Agreement.FirstOrDefault().CdPaymentOption, statusLead.Agreement.FirstOrDefault().CdParcelPlan);

                    //var billetAfinz = AfinzRecuperaAPI.EmitirBoleto(cpf, codAgreementRecupera, statusLead.Agreement.FirstOrDefault().DtEntrace, statusLead.Agreement.FirstOrDefault().DtEntrace);

                    //statusLead.Agreement.FirstOrDefault().CdAgreement = codAgreementRecupera;

                    //if (billetAfinz != null)
                    //    CreateParcel(statusLead.Agreement.FirstOrDefault(), billetAfinz.Boleto);
                    //else
                    CreateParcelAfinz(statusLead.Agreement.FirstOrDefault(), null);
                }
            }

            if (productType == Constants.ProductType.CREDZ)
            {
                var billet = CobmaisAPI.PostBoleto(Convert.ToInt64(statusLead.Agreement.FirstOrDefault().CdAgreement), cpf, 1, statusLead.Agreement.FirstOrDefault().DtEntrace);
                if (billet != null)
                {
                    statusLead.Agreement.FirstOrDefault().AgreementParcel.Where(p => p.NrParcel == 0).FirstOrDefault().Billet.Add
                        (
                            new Billet()
                            {
                                CdAgreement = billet.acordo_id.ToString(),
                                Barcode = billet.codigo_barra,
                                Line = billet.linha_digitavel,
                                DocumentNumber = billet.nosso_numero,
                                DtBillet = billet.vencimento,
                                CdBillet = billet.id.ToString(),
                                URL = billet.url,
                                VlBillet = billet.valor,
                                DtInsert = DateTime.Now
                            }
                        );

                }
            }

            return this.Add(statusLead);
        }
        private void CreateParcelAfinz(Agreement agreement, RecuperaSoapApi.Boleto billetAfinz)
        {
            var listParcel = new List<AgreementParcel>();

            for (int i = 0; i <= agreement.QtParcel; i++)
            {
                listParcel.Add
                    (
                        new AgreementParcel()
                        {
                            NrParcel = i,
                            VlParcel = agreement.VlParcel,
                            DtParcel = agreement.DtEntrace.AddMonths(i)
                        }
                    );
            }

            if (billetAfinz != null)
                listParcel.Where(p => p.NrParcel == 0).FirstOrDefault().Billet.Add
                    (
                        new Business.Models.FIS.Billet()
                        {
                            VlBillet = Convert.ToDecimal(billetAfinz.ValorPagamento),
                            DtBillet = billetAfinz.DataVencimento,
                            Barcode = billetAfinz.CodigoBarras,
                            Line = billetAfinz.LinhaDigitavel,
                            DocumentNumber = billetAfinz.NossoNumero,
                            CdAgreement = agreement.CdAgreement,
                            DtInsert = DateTime.Now
                        }
                    );

            agreement.AgreementParcel = listParcel;
        }

        public void Add(string cpf, string cdStatus, int idUserLogin, Constants.ProductType productType)
        {
            var status = new StatusBLL().GetAll().Where(p => p.CdStatus == cdStatus).FirstOrDefault();
            Lead lead = new LeadBLL().GetByCPF(cpf, Convert.ToByte(productType));
            if (lead != null)
            {
                Add
                    (
                        new StatusLead()
                        {
                            IdLead = lead.IdLead,
                            IdStatus = status.IdStatus,
                            DsDescription = "",
                            DtInsert = DateTime.Now,
                            IdUserLogin = idUserLogin
                        }
                    );
            }
        }

        public StatusLead GetByCodAgreementRecupera(string codAgreementRecupera)
        {
            return persistence.GetByCodAgreementRecupera(codAgreementRecupera);
        }
    }

}
