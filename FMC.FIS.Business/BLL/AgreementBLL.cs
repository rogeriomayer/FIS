using FMC.FIS.Business.Code.Api.Cobmais;
using FMC.FIS.Business.Code.Api.OneB2K;
using FMC.FIS.Business.Code.Api.Recupera;
using FMC.FIS.Business.DAO;
using FMC.FIS.Business.Code.Helpers;
using FMC.FIS.Business.Models.Customer;
using FMC.FIS.Business.Models.FIS;
using FMC.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using FMC.FIS.Business.Models.OneB2K;
using System.Globalization;
using FMC.FIS.Business.Code.Api.Digicob;

namespace FMC.FIS.Business.BLL
{
    public class AgreementBLL : BLL<Agreement, AgreementDAO>
    {
        public Agreement GetAgreement(string cdAgreement)
        {
            return persistence.GetAgreement(cdAgreement);
        }

        public AgreementSimulateResponse GetAgreementSimulate(AgreementSimulateRequest agreementSimulateRequest, Constants.ProductType productType)
        {
            AgreementSimulateResponse agreementSimulate = null;
            //if (productType == Constants.ProductType.AFINZ)
            //{
            //    agreementSimulate = GetAgreementSimulateAfins(agreementSimulateRequest);
            //}

            if (productType == Constants.ProductType.CREDZ)
            {
                agreementSimulate = GetAgreementSimulateDigicob(agreementSimulateRequest);
                if (agreementSimulate == null)
                    agreementSimulate = GetAgreementSimulateCredz(agreementSimulateRequest);
            }
            else
            {
                agreementSimulate = GetAgreementSimulate(agreementSimulateRequest);
            }

            return agreementSimulate;
        }

        private AgreementSimulateResponse GetAgreementSimulate(AgreementSimulateRequest agreementSimulateRequest)
        {
            AgreementSimulateResponse agreementSimulate = new AgreementSimulateResponse();

            if (agreementSimulateRequest.DtEntrace < DateTime.Today) agreementSimulateRequest.DtEntrace = DateTime.Today;

            ResumoSaldoAtualizadoResponse resumoSaldoAtualizado = null;
            string projPayOffAmt = "1000.00";
            string interestRate = "10.00";
            try
            {
                resumoSaldoAtualizado = OneB2KApi.GetResumoSaldoAtualizado(agreementSimulateRequest.Product, agreementSimulateRequest.DtEntrace);

                if (resumoSaldoAtualizado != null && resumoSaldoAtualizado.responseData != null)
                {
                    projPayOffAmt = resumoSaldoAtualizado.responseData.projPayOffAmt.Trim();
                    interestRate = resumoSaldoAtualizado.responseData.interestRate.Trim();
                }
            }
            catch
            {

            }


            if (!interestRate.Contains(","))
                interestRate = interestRate + ",00";

            decimal vlBalance = Convert.ToDecimal(projPayOffAmt.Replace(".", "").Replace(",", "")) / Convert.ToDecimal(100.00);
            vlBalance += Convert.ToDecimal(interestRate.Trim().Replace(".", "").Replace(",", "")) / Convert.ToDecimal(100.00);

            agreementSimulate.DateEntrace = agreementSimulateRequest.DtEntrace;
            agreementSimulate.DateFirstParcel = agreementSimulateRequest.DtEntrace.AddMonths(1);
            agreementSimulate.VlDue = vlBalance;
            agreementSimulate.VlFull = vlBalance;
            agreementSimulate.PctDiscount = Convert.ToDecimal((vlBalance * Convert.ToDecimal(agreementSimulateRequest.PctDiscount)) / Convert.ToDecimal(100.00));
            agreementSimulate.PctInterest = 0;
            agreementSimulate.CdSimulate = "";

            int count = 0;

            do
            {
                try
                {
                    double pctYearCET = 0;
                    double pctMonthCET = 0;
                    decimal vlYearCET = 0;
                    decimal vlMonthCET = 0;

                    var nrParcel = count;
                    var dtParcel = agreementSimulateRequest.DtEntrace.AddMonths(nrParcel);
                    var vlDiscount = agreementSimulate.PctDiscount;
                    var vlParcel = nrParcel > 0 ? (agreementSimulate.VlFull - vlDiscount - agreementSimulateRequest.VlEntrace) / nrParcel : agreementSimulate.VlFull;

                    if (vlParcel >= 40)
                    {
                        if (vlDiscount < 0) vlDiscount = 0;

                        if (count > 0)
                        {
                            pctYearCET = CET.CalcularCet(Convert.ToDouble(agreementSimulate.VlFull) - Convert.ToDouble(agreementSimulateRequest.VlEntrace), Convert.ToDouble(vlParcel), nrParcel, DateTime.Today, dtParcel);
                            pctMonthCET = Math.Pow(1 + (pctYearCET / 100), 1 / 12d) - 1;
                            vlYearCET = Convert.ToDecimal(agreementSimulateRequest.Age * vlParcel * Convert.ToDecimal(pctYearCET)) / 100;
                            vlMonthCET = Convert.ToDecimal((agreementSimulateRequest.Age * vlParcel) * Convert.ToDecimal(pctMonthCET));
                        }

                        agreementSimulate.ParcelResponse.Add
                            (
                                new ParcelResponse()
                                {
                                    CdParcel = "",
                                    NrParcel = nrParcel,
                                    ValueEntrace = nrParcel == 0 ? vlBalance - vlDiscount : Convert.ToDecimal(agreementSimulateRequest.VlEntrace),
                                    DtParcel = dtParcel,
                                    VlDiscount = vlDiscount,
                                    VlParcel = Convert.ToDecimal(vlParcel),
                                    VlFull = Convert.ToDecimal(vlBalance - vlDiscount),
                                    PctMonthCET = Convert.ToDecimal(pctMonthCET),
                                    PctYearCET = Convert.ToDecimal(pctYearCET),
                                    VlMonthCET = Convert.ToDecimal(vlMonthCET),
                                    VlYearCET = Convert.ToDecimal(vlYearCET)
                                }
                            );
                    }
                    count++;
                }
                catch (Exception ex)
                {
                }
            } while (count <= 24);

            return agreementSimulate;
        }

        private AgreementSimulateResponse GetAgreementSimulateDigicob(AgreementSimulateRequest agreementSimulateRequest)
        {
            try
            {
                AgreementSimulateResponse agreementSimulate = null;

                try
                {
                    var agreementPlans = new DigicobAPI().PostAgreementPlansAsync
                        (
                           Convert.ToInt64(agreementSimulateRequest.ParcelaCredz.FirstOrDefault().numero_parcela_original),
                            new Digicob.DM.Models.AgreementPlanRequest()
                            {
                                IdPortalAccess = 0,
                                Channel = "Whatsapp",
                                DownPaymentDate = DateTime.Today.AddDays(7).ToString("yyyy-MM-dd"),
                                Contracts = new List<Digicob.DM.Models.AgreementContractRequest>()
                                {
                                    new Digicob.DM.Models.AgreementContractRequest()
                                        {
                                            IdContract = Convert.ToInt64( agreementSimulateRequest.ParcelaCredz.FirstOrDefault().valor),
                                            ContractId = agreementSimulateRequest.ParcelaCredz.FirstOrDefault().negociacao_id,
                                            CollectionIds = agreementSimulateRequest.ParcelaCredz.Select(p => p.id_parcela_original).ToList()
                                        }
                                }
                            }
                        ).GetAwaiter().GetResult();

                    if (agreementPlans.Count > 0)
                    {
                        var agreementPlan = agreementPlans.OrderBy(p => p.InstallmentCount).FirstOrDefault();

                        agreementSimulate = new AgreementSimulateResponse();
                        agreementSimulate.PctDiscount = agreementPlan.DiscountValue;
                        agreementSimulate.DateEntrace = agreementPlan.DownPaymentDate;
                        agreementSimulate.DateFirstParcel = agreementPlan.SecondInstallmentDate.HasValue ? agreementPlan.SecondInstallmentDate.Value : agreementPlan.DownPaymentDate;

                        agreementSimulate.VlDue = agreementPlan.PrincipalValue;

                        agreementSimulate.VlFull = agreementPlan.UpdatedValue;

                        agreementSimulate.PctInterest = agreementPlan.InterestValue;

                        foreach (var parcel in agreementPlans.OrderBy(p => p.InstallmentCount).ToList())
                            agreementSimulate.ParcelResponse.Add
                                (
                                    new ParcelResponse()
                                    {
                                        CdParcel = parcel.PlanUuid,
                                        NrParcel = parcel.InstallmentCount - 1,
                                        ValueEntrace = parcel.DownPaymentValue,
                                        DtParcel = parcel.Installments.FirstOrDefault().DueDate,
                                        VlDiscount = agreementPlan.UpdatedValue - parcel.TotalValue,
                                        VlParcel = parcel.InstallmentValue == 0 ? parcel.DownPaymentValue : parcel.InstallmentValue,
                                        VlFull = parcel.TotalValue,
                                        //PctMonthCET = Convert.ToDecimal(pctMonthCET),
                                        //PctYearCET = Convert.ToDecimal(pctYearCET),
                                        //VlMonthCET = Convert.ToDecimal(vlMonthCET),
                                        //VlYearCET = Convert.ToDecimal(vlYearCET)
                                    }
                                );
                    }
                }
                catch
                {

                }

                return agreementSimulate;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private AgreementSimulateResponse GetAgreementSimulateCredz(AgreementSimulateRequest agreementSimulateRequest)
        {
            try
            {
                AgreementSimulateResponse agreementSimulate = new AgreementSimulateResponse();

                var listDiscount = new DiscountBLL().GetByProductType(3);

                for (int i = 1; i <= agreementSimulateRequest.NrParcel; i++)
                {

                    try
                    {
                        var vlFull = (agreementSimulate != null && agreementSimulate.VlDue > 0) ? agreementSimulate.VlDue : 0;
                        var discount = listDiscount.Where(p => (agreementSimulateRequest.Age >= p.MinAge && agreementSimulateRequest.Age <= p.MaxAge)
                                                            && (i >= p.MinParcel && i <= p.MaxParcel)).FirstOrDefault();

                        var paymentOptions = GetSimulacaoAcordoCredz(agreementSimulateRequest, i, discount != null ? discount.MaxDiscount : Convert.ToDecimal(10.00), vlFull);


                        if (paymentOptions.acordo.parcelas_novas.Count > 0)
                        {
                            var entrada = paymentOptions.acordo.parcelas_novas.Where(p => p.numero == "1").FirstOrDefault();

                            agreementSimulate.PctDiscount = (paymentOptions.acordo.parcelas_originais.FirstOrDefault().desconto_principal > 0 ? paymentOptions.acordo.parcelas_originais.FirstOrDefault().desconto_principal : 0) +
                                                            (paymentOptions.acordo.parcelas_originais.FirstOrDefault().desconto_juros > 0 ? paymentOptions.acordo.parcelas_originais.FirstOrDefault().desconto_juros : 0) +
                                                            (paymentOptions.acordo.parcelas_originais.FirstOrDefault().desconto_multa > 0 ? paymentOptions.acordo.parcelas_originais.FirstOrDefault().desconto_multa : 0) +
                                                            (paymentOptions.acordo.parcelas_originais.FirstOrDefault().desconto_honorario > 0 ? paymentOptions.acordo.parcelas_originais.FirstOrDefault().desconto_honorario : 0);

                            agreementSimulate.DateEntrace = entrada.vencimento;

                            agreementSimulate.DateFirstParcel = paymentOptions.acordo.parcelas_novas.Count() > 1 ?
                                                                            paymentOptions.acordo.parcelas_novas.Where(p => p.numero == "2").FirstOrDefault().vencimento
                                                                            : entrada.vencimento;

                            agreementSimulate.VlDue = paymentOptions.acordo.parcelas_originais.FirstOrDefault().principal;

                            //alteração porque está mostrando valor diferente no totla da API ao corrigir voltar
                            //agreementSimulate.VlFull = paymentOptions.acordo.parcelas_originais.FirstOrDefault().total;
                            if (i == 1)
                                agreementSimulate.VlFull = paymentOptions.acordo.parcelas_novas.Sum(p => p.valor) + agreementSimulate.PctDiscount;

                            agreementSimulate.PctInterest = (paymentOptions.acordo.parcelas_originais.FirstOrDefault().juros > 0 ? paymentOptions.acordo.parcelas_originais.FirstOrDefault().juros : 0) +
                                                            (paymentOptions.acordo.parcelas_originais.FirstOrDefault().multa > 0 ? paymentOptions.acordo.parcelas_originais.FirstOrDefault().multa : 0) +
                                                            (paymentOptions.acordo.parcelas_originais.FirstOrDefault().honorarios > 0 ? paymentOptions.acordo.parcelas_originais.FirstOrDefault().honorarios : 0);

                            var vlParcel = paymentOptions.acordo.parcelas_novas.Count() > 1 ? paymentOptions.acordo.parcelas_novas[1].valor : paymentOptions.acordo.parcelas_novas[0].valor;
                            if (paymentOptions.acordo.parcelas_novas.Count() > 1 && vlParcel < 40) break;

                            var dtParcel = paymentOptions.acordo.parcelas_novas.Count() > 1 ? paymentOptions.acordo.parcelas_novas[1].vencimento : paymentOptions.acordo.parcelas_novas[0].vencimento;
                            var vlDiscount = Convert.ToDecimal(agreementSimulate.PctDiscount);

                            /*
                            var pctYearCET = CET.CalcularCet(Convert.ToDouble(agreementSimulate.VlFull - entrada.valor), Convert.ToDouble(vlParcel), i + 1, DateTime.Today, dtParcel);
                            var pctMonthCET = Math.Pow(1 + (pctYearCET / 100), 1 / 12d) - 1;
                            var vlYearCET = Convert.ToDecimal(agreementSimulateRequest.Age * vlParcel * Convert.ToDecimal(pctYearCET)) / 100;
                            var vlMonthCET = Convert.ToDecimal((agreementSimulateRequest.Age * vlParcel) * Convert.ToDecimal(pctMonthCET));
                            */

                            if (vlDiscount < 0) vlDiscount = 0;

                            agreementSimulate.ParcelResponse.Add
                                (
                                    new ParcelResponse()
                                    {
                                        NrParcel = i - 1,
                                        ValueEntrace = entrada.valor,
                                        DtParcel = dtParcel,
                                        VlDiscount = vlDiscount,
                                        VlParcel = vlParcel,
                                        VlFull = paymentOptions.acordo.parcelas_novas.Sum(p => p.valor),
                                        //PctMonthCET = Convert.ToDecimal(pctMonthCET),
                                        //PctYearCET = Convert.ToDecimal(pctYearCET),
                                        //VlMonthCET = Convert.ToDecimal(vlMonthCET),
                                        //VlYearCET = Convert.ToDecimal(vlYearCET)
                                    }
                                );
                        }

                        else
                        {
                            throw new Exception("Contrato não disponível para renegociação/acordo!");
                        }
                    }
                    catch
                    {
                        break;
                    }
                }

                return agreementSimulate;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AgreementSimulateResponse GetOnlyOneSimulateCredz(AgreementSimulateRequest agreementSimulateRequest)
        {
            try
            {
                AgreementSimulateResponse agreementSimulate = new AgreementSimulateResponse();

                var listDiscount = new DiscountBLL().GetByProductType(3);
                var vlFull = (agreementSimulate != null && agreementSimulate.VlDue > 0) ? agreementSimulate.VlDue : 0;

                var discount = listDiscount.Where(p => (agreementSimulateRequest.Age >= p.MinAge && agreementSimulateRequest.Age <= p.MaxAge)
                                                    && (agreementSimulateRequest.NrParcel >= p.MinParcel && agreementSimulateRequest.NrParcel <= p.MaxParcel)).FirstOrDefault();

                var paymentOptions = GetSimulacaoAcordoCredz(agreementSimulateRequest, agreementSimulateRequest.NrParcel, discount != null ? discount.MaxDiscount : Convert.ToDecimal(10.00), vlFull);


                if (paymentOptions.acordo.parcelas_novas.Count > 0)
                {
                    var entrada = paymentOptions.acordo.parcelas_novas.Where(p => p.numero == "1").FirstOrDefault();

                    agreementSimulate.PctDiscount = (paymentOptions.acordo.parcelas_originais.FirstOrDefault().desconto_principal > 0 ? paymentOptions.acordo.parcelas_originais.FirstOrDefault().desconto_principal : 0) +
                                                    (paymentOptions.acordo.parcelas_originais.FirstOrDefault().desconto_juros > 0 ? paymentOptions.acordo.parcelas_originais.FirstOrDefault().desconto_juros : 0) +
                                                    (paymentOptions.acordo.parcelas_originais.FirstOrDefault().desconto_multa > 0 ? paymentOptions.acordo.parcelas_originais.FirstOrDefault().desconto_multa : 0) +
                                                    (paymentOptions.acordo.parcelas_originais.FirstOrDefault().desconto_honorario > 0 ? paymentOptions.acordo.parcelas_originais.FirstOrDefault().desconto_honorario : 0);

                    agreementSimulate.DateEntrace = entrada.vencimento;

                    agreementSimulate.DateFirstParcel = paymentOptions.acordo.parcelas_novas.Count() > 1 ?
                                                                    paymentOptions.acordo.parcelas_novas.Where(p => p.numero == "2").FirstOrDefault().vencimento
                                                                    : entrada.vencimento;

                    agreementSimulate.VlDue = paymentOptions.acordo.parcelas_originais.FirstOrDefault().principal;

                    agreementSimulate.VlFull = paymentOptions.acordo.parcelas_novas.Sum(p => p.valor) + agreementSimulate.PctDiscount;

                    agreementSimulate.PctInterest = (paymentOptions.acordo.parcelas_originais.FirstOrDefault().juros > 0 ? paymentOptions.acordo.parcelas_originais.FirstOrDefault().juros : 0) +
                                                    (paymentOptions.acordo.parcelas_originais.FirstOrDefault().multa > 0 ? paymentOptions.acordo.parcelas_originais.FirstOrDefault().multa : 0) +
                                                    (paymentOptions.acordo.parcelas_originais.FirstOrDefault().honorarios > 0 ? paymentOptions.acordo.parcelas_originais.FirstOrDefault().honorarios : 0);

                    var vlParcel = paymentOptions.acordo.parcelas_novas.Count() > 1 ? paymentOptions.acordo.parcelas_novas[1].valor : paymentOptions.acordo.parcelas_novas[0].valor;
                    if (paymentOptions.acordo.parcelas_novas.Count() > 1 && vlParcel < 40)
                    {
                        agreementSimulateRequest.NrParcel--;

                        GetOnlyOneSimulateCredz(agreementSimulateRequest);
                    }

                    var dtParcel = paymentOptions.acordo.parcelas_novas.Count() > 1 ? paymentOptions.acordo.parcelas_novas[1].vencimento : paymentOptions.acordo.parcelas_novas[0].vencimento;
                    var vlDiscount = Convert.ToDecimal(agreementSimulate.PctDiscount);

                    if (vlDiscount < 0) vlDiscount = 0;

                    agreementSimulate.ParcelResponse.Add
                        (
                            new ParcelResponse()
                            {
                                NrParcel = paymentOptions.acordo.parcelas_novas.Count() > 1 ? Convert.ToInt32(paymentOptions.acordo.parcelas_novas[paymentOptions.acordo.parcelas_novas.Count() - 1].numero) - 1 : 0,
                                ValueEntrace = entrada.valor,
                                DtParcel = dtParcel,
                                VlDiscount = vlDiscount,
                                VlParcel = vlParcel,
                                VlFull = paymentOptions.acordo.valorTotal,
                            }
                        );
                }

                else
                {
                    throw new Exception("Contrato não disponível para renegociação/acordo!");
                }


                return agreementSimulate;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Models.Cobmais.SimulacaoAcordoResponse GetSimulacaoAcordoCredz(AgreementSimulateRequest agreementSimulateRequest, int nrParcel, decimal discount, decimal vlFull)
        {
            try
            {
                var vlEntrace = agreementSimulateRequest.VlEntrace;

                if (vlEntrace > 0 && vlFull > 0 && nrParcel > 1 && !agreementSimulateRequest.FixedEntraceValue)
                    vlEntrace = (vlFull - vlFull * (discount / Convert.ToDecimal(100.00))) / nrParcel;

                return CobmaisAPI.GetSimulacao(new Models.Cobmais.SimulacaoAcordoRequest()
                {
                    valor_entrada = vlEntrace,
                    data_calculo = agreementSimulateRequest.DtEntrace,
                    descontos = new Models.Cobmais.Descontos()
                    {
                        principal = discount,
                        multa = Convert.ToDecimal(100.00),
                        juros = Convert.ToDecimal(100.00),
                        honorarios = 0,
                        desconto_maximo = true,
                        desconto_relativo_campanha = false
                    },
                    forma_pagamento = "Boleto",
                    quantidade_parcelas = nrParcel,
                    parcelas_originais = agreementSimulateRequest.ParcelaCredz.Select(p =>
                            new Models.Cobmais.ParcelaOriginal()
                            {
                                negociacao_id = p.negociacao_id,
                                id = p.id_parcela_original,
                                numero = p.numero_parcela_original,
                                vencimento = p.vencimento,
                                valor = Convert.ToDecimal(p.valor)
                            }
                        ).ToList()
                    /*
                    parcelas_originais = new System.Collections.Generic.List<Models.Cobmais.ParcelaOriginal>()
                        {
                            new Models.Cobmais.ParcelaOriginal()
                            {
                                negociacao_id = Convert.ToInt64(agreementSimulateRequest.ComplementData.Where(p=> p.Name == "negociacao_id").FirstOrDefault().Value),
                                id = Convert.ToInt64(agreementSimulateRequest.ComplementData.Where(p=> p.Name == "id").FirstOrDefault().Value),
                                numero = agreementSimulateRequest.ComplementData.Where(p=> p.Name == "numero").FirstOrDefault().Value,
                                vencimento = Convert.ToDateTime(agreementSimulateRequest.ComplementData.Where(p=> p.Name == "vencimento").FirstOrDefault().Value),
                                valor = Convert.ToDecimal(agreementSimulateRequest.ComplementData.Where(p=> p.Name == "valor").FirstOrDefault().Value),
                            }
                        }
                    */
                });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Entrada do Parcelamento não configurada"))
                {
                    agreementSimulateRequest.VlEntrace = 0;
                    return GetSimulacaoAcordoCredz(agreementSimulateRequest, nrParcel, discount, vlFull);
                }
                else if (ex.Message.Contains("Valor de Parcela abaixo do valor mínimo permitido"))
                {
                    var match = System.Text.RegularExpressions.Regex.Match(ex.Message, @"R\$ ?(\d+[\.,]?\d*)");

                    if (match.Success)
                    {
                        decimal vlEntrace = decimal.Parse(match.Groups[1].Value.Replace(",", "."), CultureInfo.InvariantCulture);
                        if (agreementSimulateRequest.VlEntrace >= vlEntrace)
                            throw ex;

                        agreementSimulateRequest.VlEntrace = vlEntrace;
                    }
                    else
                    {
                        agreementSimulateRequest.VlEntrace = 99;
                    }
                    return GetSimulacaoAcordoCredz(agreementSimulateRequest, nrParcel, discount, vlFull);
                }
                else
                {
                    throw ex;
                }
            }

        }

        public bool UpdateAgreementStatus(long idAgreement, int idAgreementStatus)
        {
            try
            {
                var agreement = persistence.GetBykey(idAgreement);
                agreement.IdAgreementStatus = idAgreementStatus;
                persistence.Update(agreement);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /*
        private AgreementSimulateResponse GetAgreementSimulateAfins(AgreementSimulateRequest agreementSimulateRequest)
        {
            AgreementSimulateResponse agreementSimulate = new AgreementSimulateResponse();
            var codigoSolicitacao = agreementSimulateRequest.CdSimulate;

            if (string.IsNullOrEmpty(codigoSolicitacao))
            {
                if (agreementSimulateRequest.VlEntrace > 0)
                    codigoSolicitacao = AfinzRecuperaAPI.SolicitarOpcoesPagamentoComEntrada(agreementSimulateRequest.CPF, agreementSimulateRequest.DtEntrace, agreementSimulateRequest.VlEntrace, agreementSimulateRequest.PctDiscount);
                else
                    codigoSolicitacao = AfinzRecuperaAPI.SolicitarOpcoesPagamento(agreementSimulateRequest.CPF, agreementSimulateRequest.DtEntrace, agreementSimulateRequest.PctDiscount);

            }
            var paymentOptions = AfinzRecuperaAPI.ConsultarOpcoesPagamento(agreementSimulateRequest.CPF, codigoSolicitacao);

            if (paymentOptions.OpcoesPagamento.Count > 0 && paymentOptions.OpcoesPagamento.Where(p => p.Contratos.Where(c => c.CodigoContrato == agreementSimulateRequest.Product).Any()).Any())
            {
                var paymentOption = paymentOptions.OpcoesPagamento.Where(p => p.Contratos.Where(c => c.CodigoContrato == agreementSimulateRequest.Product).Any()).FirstOrDefault();
                var contract = paymentOption.Contratos.Where(c => c.CodigoContrato == agreementSimulateRequest.Product).FirstOrDefault();

                agreementSimulate.DateEntrace = paymentOption.DataPagamento;
                agreementSimulate.DateFirstParcel = paymentOption.DataPagamento.AddMonths(1);
                agreementSimulate.VlDue = Convert.ToDecimal(contract.ValorPrincipal);
                agreementSimulate.VlFull = Convert.ToDecimal(contract.ValorPagamento);
                agreementSimulate.PctDiscount = Convert.ToDecimal(contract.ValorDesconto);
                agreementSimulate.PctInterest = Convert.ToDecimal(contract.ValorJuros);
                agreementSimulate.CdSimulate = paymentOptions.CodigoSolicitacaoOpcoesPagamento;

                foreach (var plano in paymentOption.Planos)
                {
                    var nrParcel = Convert.ToInt32(plano.QtdeParcelas);
                    var vlParcel = nrParcel > 0 ? (plano.Valor - plano.ValorEntrada) / nrParcel : 0;
                    var dtParcel = paymentOption.DataPagamento.AddMonths(nrParcel);
                    var vlDiscount = Convert.ToDecimal(contract.ValorDesconto);

                    if (vlDiscount < 0) vlDiscount = 0;

                    var pctYearCET = CET.CalcularCet(plano.Valor - plano.ValorEntrada, vlParcel, nrParcel, DateTime.Today, paymentOption.DataPagamento);
                    var pctMonthCET = Math.Pow(1 + (pctYearCET / 100), 1 / 12d) - 1;
                    var vlYearCET = Convert.ToDecimal(agreementSimulateRequest.Age * vlParcel * pctYearCET) / 100;
                    var vlMonthCET = Convert.ToDecimal((agreementSimulateRequest.Age * vlParcel) * (pctMonthCET));

                    agreementSimulate.ParcelResponse.Add
                        (
                            new ParcelResponse()
                            {
                                CdParcel = plano.CodigoPlano,
                                NrParcel = nrParcel,
                                ValueEntrace = Convert.ToDecimal(plano.ValorEntrada),
                                DtParcel = dtParcel,
                                VlDiscount = vlDiscount,
                                VlParcel = Convert.ToDecimal(vlParcel),
                                VlFull = Convert.ToDecimal(plano.Valor),
                                PctMonthCET = Convert.ToDecimal(pctMonthCET),
                                PctYearCET = Convert.ToDecimal(pctYearCET),
                                VlMonthCET = Convert.ToDecimal(vlMonthCET),
                                VlYearCET = Convert.ToDecimal(vlYearCET)
                            }
                        );
                }
            }
            else
            {
                throw new Exception("Contrato não disponível para renegociação/acordo!");
            }

            return agreementSimulate;
        }
        */
        public Models.Cobmais.Acordo AddAgreementCredz(AgreementSimulateRequest agreementSimulateRequest)
        {
            try
            {

                var agreement = new DigicobAPI().GetAgreementAsync(Convert.ToInt64(agreementSimulateRequest.ParcelaCredz.FirstOrDefault().numero_parcela_original), agreementSimulateRequest.CdSimulate).GetAwaiter().GetResult();

                var entrace = agreement.Installments.OrderBy(p => p.Number).FirstOrDefault();

                if (entrace.Billets == null || entrace.Billets.Count == 0)
                    agreement = new DigicobAPI().GetAgreementBilletAsync(agreement.CustomerId, agreement.Id, entrace.Id).GetAwaiter().GetResult();

                return new Models.Cobmais.Acordo()
                {
                    id = agreement.Id,
                    status_descricao = agreement.Status,
                    status = agreement.Status,
                    status_id = 1,
                    id_status = 1,
                    numero = agreement.ParentAgreementId.ToString(),
                    data = agreement.DownPaymentDate.Value,
                    quantidade_parcelas = agreement.InstallmentCount,
                    principal = agreement.PrincipalValue,
                    multa = agreement.PenaltyValue,
                    juros = agreement.InterestValue,
                    honorarios = agreement.InterestRateValue,
                    despesas = agreement.FinancialCostValue,
                    taxa_parcelamento = agreement.FinancialCostPercentage,
                    desconto_principal = agreement.PrincipalDiscountValue,
                    desconto_multa = agreement.PrincipalDiscountValue,
                    desconto_juros = agreement.InterestDiscountValue,
                    desconto_honorario = agreement.InterestDiscountValue,
                    total = agreement.TotalValue,
                    repasse_principal = 0,
                    repasse_multa = 0,
                    repasse_juros = 0,
                    repasse_despesas = 0,
                    repasse_taxa_parcelamento = 0,
                    total_repasse = 0,
                    operador_vinculado = agreement.User,
                    parcial = false,
                    parcelas_originais = null,
                    parcelas_novas = agreement.Installments.Select(p => new Models.Cobmais.ParcelasNova()
                    {
                        id = p.Id,
                        parcela = p.Number.ToString(),
                        vencimento = p.DueDate,
                        valor = p.Value,
                        id_contrato = p.CollectionId,
                        data_pagamento = null,
                        id_pagamento = null
                    }).ToList(),
                    boletos = agreement.Installments.FirstOrDefault().Billets.Select(p => new Models.Cobmais.BoletoSimples()
                    {
                        id = p.Id,
                        acordo_id = p.IdAgreementInstallment,
                        nosso_numero = p.OurNumber,
                        vencimento = p.DueDate,
                        valor = p.Value,
                        codigo_barra = p.Barcode,
                        linha_digitavel = p.PaymentLine,
                        url = p.BilletUrl

                    }).ToList()
                };

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ICollection<Agreement> GetRemember(DateTime dtIni, DateTime dtFim)
        {
            return persistence.GetRemember(dtIni, dtFim);
        }
    }
}
