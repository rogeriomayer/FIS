using FMC.FIS.Business.Models.Customer;
using FMC.FIS.Business.Models.FIS;
using FMC.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FMC.FIS.Business.DAO
{
    public class StatusLeadDAO : AbstractRepositoryPersistence<StatusLead>
    {
        public StatusLeadDAO() : base("CNN_FIS") { }

        public ICollection<StatusLead> GetbyIdUser(int idUser, int idProductType, DateTime dtIni, DateTime dtFim)
        {
            return Context.Where(p => (p.IdUserLogin == idUser || idUser == 0) &&
                                      p.Lead.Product.IdProductType == idProductType &&
                                      (p.DtInsert >= dtIni && p.DtInsert <= dtFim)).ToList();
        }

        public ICollection<Models.Customer.StatusLeadResponse> GetbyIdsUser(ICollection<int> idUser, int idProductType, DateTime dtIni, DateTime dtFim)
        {
            return Context.Where(p => (idUser.Contains(p.IdUserLogin)) &&
                                      p.Lead.Product.IdProductType == idProductType &&
                                      (p.DtInsert >= dtIni && p.DtInsert <= dtFim))
                .Select
                (
                    p => new StatusLeadResponse()
                    {
                        IdStatusLead = p.IdStatusLead,
                        IdStatus = p.IdStatus,
                        IdUserLogin = p.IdUserLogin,
                        Status = p.Status.DsStatus,
                        DsProduct = p.Lead.Product.DsProduct,
                        UserLogin = p.UserLogin.DsName, //new FisLoginBLL().GetBykey(p.IdUserLogin).DsName,
                        Description = p.DsDescription,
                        DtStatusLead = p.DtInsert,
                        FlEfective = p.Status.FlEfective,
                        PromisseResponse = p.Promisse.Select
                            (
                                pr => new PromisseResponse()
                                {
                                    IdPromisse = pr.IdPromisse,
                                    DtPromisse = pr.DtPromisse,
                                    DtInsert = pr.DtInsert,
                                    VlPromisse = pr.VlPromisse,
                                    IdStatusLead = pr.IdStatusLead
                                }
                            ).FirstOrDefault(),
                        AgreementResponse = p.Agreement.Select
                            (
                                a => new AgreementResponse()
                                {
                                    IdAgreement = a.IdAgreement,
                                    IdStatusLead = a.IdStatusLead,
                                    VlEntrace = a.VlEntrace,
                                    DtEntrace = a.DtEntrace,
                                    PcDiscount = a.PcDiscount,
                                    QtParcel = a.QtParcel,
                                    VlParcel = a.VlParcel,
                                    VlAgreement = a.VlAgreement,
                                    CdPaymentOption = a.CdPaymentOption,
                                    CdParcelPlan = a.CdParcelPlan,
                                    CdAgreement = a.CdAgreement,
                                    DtInsert = a.DtInsert,
                                    IdAgreementStatus = a.IdAgreementStatus,
                                    Status = a.AgreementStatus != null ? a.AgreementStatus.DsAgreementStatus : "EmAberto",
                                }
                            ).FirstOrDefault()
                    }
                ).ToList();
        }

        public StatusLead GetByCodAgreementRecupera(string codAgreementRecupera)
        {
            return Context.Where(p => p.Agreement.Where(a => a.CdAgreement == codAgreementRecupera).Count() > 0).FirstOrDefault();
        }
    }
}
