using FMC.FIS.BLL;
using FMC.FIS.Business.BLL;
using FMC.FIS.Business.Code.Api.Cobmais;
using FMC.FIS.Business.Code.Api.Digicob;
using FMC.FIS.Business.Models.Cobmais;
using FMC.FIS.Business.Models.Customer;
using FMC.FIS.Business.Models.FIS;
using FMC.FIS.Credz.Remember;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


try
{
    var currentProcess = System.Diagnostics.Process.GetCurrentProcess();

    var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false);

    IConfiguration config = builder.Build();

    Constants.UserCobmaisCredz = config.GetValue<string>("UserCobmaisCredz");
    Constants.PassCobmaisCredz = config.GetValue<string>("PassCobmaisCredz");
    Constants.UrlApiCobmaisCredz = config.GetValue<string>("UrlApiCobmaisCredz");

    foreach (var process in System.Diagnostics.Process.GetProcessesByName(currentProcess.ProcessName))
    {
        if (process.MainModule.FileName == currentProcess.MainModule.FileName && process.Id != currentProcess.Id)
        {
            //Log.SaveFile("Matando processo " + process.MainModule.FileName + " SessionID:" + process.Id);
            process.Kill();
        }
    }

    while (DateTime.Now.DayOfWeek != DayOfWeek.Sunday)
    {
        DateTime dtIni = DateTime.Today.AddDays(-15);
        var agreementBLL = new AgreementBLL();
        var discounts = new DiscountBLL().GetByProductType(3);

        IList<Agreement> listAgreement = agreementBLL.GetRemember(dtIni, DateTime.Today.AddDays(15)).ToList();

        Util.SaveFile("Foram encontrados " + listAgreement.Count + "acordos!");
        if (listAgreement.Count > 0)
        {
            var listEmail = new List<string>();
            var listPhone = new List<string>();

            int countAgreement = 0;
            DateTime horaIni = DateTime.Now;
            //foreach (var agreement in listAgreement.OrderBy(p => p.StatusLead.Lead.IdProduct).ToList())
            foreach (var agreement in listAgreement.OrderBy(p => p.CdParcelPlan).ThenBy(p => p.DtInsert).ToList())
            {
                try
                {
                    AgreementParcel currentParcel = agreement.AgreementParcel.Where(p => p.DtParcel >= dtIni && p.DtParcel <= DateTime.Today.AddDays(30)).OrderBy(p => p.DtParcel).FirstOrDefault();
                    var product = agreement.StatusLead.Lead.Product;
                    var emails = product.Person.Email.Where(p => p.flBloqueado == false && Util.IsEmail(p.DsEmail)).Select(p => p.DsEmail).Distinct().ToList();

                    IList<FMC.Digicob.DM.Models.AgreementResponse> agreementsDigicob = new List<FMC.Digicob.DM.Models.AgreementResponse>();
                    DigicobAPI digicobAPI = new DigicobAPI();

                    countAgreement++;
                    System.Threading.Thread.Sleep(500);
                    Util.SaveFile("Buscando acordo no Cobmais " + agreement.IdAgreement);

                    //acordo = CobmaisAPI.Acordo(Convert.ToInt64(agreement.CdAgreement));
                    var contracts = await digicobAPI.GetContractAsync(product.Person.NrCNPJCPF, product.Person.DtBirth.Value.ToString("yyyy-MM-dd"));
                    foreach (var contract in contracts)
                        foreach (var agg in contract.Agreements)
                            agreementsDigicob.Add(agg);

                    bool havePayment = false;
                    if (agreementsDigicob != null && agreementsDigicob.Count() > 0)
                    {
                        var agreementDigicob = agreementsDigicob.OrderByDescending(x=> x.Id).FirstOrDefault();
                        var idStatus = agreementDigicob != null ? (agreementDigicob.Status == "Pago" ? 5 : (agreementDigicob.Status == "Quebrado" ? 2 : 1)) : 2;

                        if (agreementDigicob != null && currentParcel != null)
                        {
                            var contract = contracts.Where(p => p.Id == agreementDigicob.Contracts.FirstOrDefault().ContractId).FirstOrDefault();

                            var objectSend = new ObjectSend()
                            {
                                IdAgreement = agreement.IdAgreement,
                                IdAgreementParcel = currentParcel.IdAgreementParcel,
                                Name = product.Person.DsName.Trim(),
                                //CardName = product.ProductSpecification != null ? product.ProductSpecification.Description : "Cartão CredZ",
                                CardName = contract.Store,
                                //CardNumber = product.DsProduct.StartsWith("000") ? product.DsProduct.Substring(3, 8) + "********" : product.DsProduct.Substring(0, 8),
                                aVista = agreement.QtParcel == 0,
                                Parcel = currentParcel.NrParcel,
                                Product = product,
                                CardUrl = product.ProductSpecification != null ? product.ProductSpecification.UrlImage : "",
                            };

                            Util.SaveFile("Verificando parcelas");
                            //foreach (var installment in agreementDigicob.Installments.Where(p => p.id_pagamento != null && p.id_pagamento > 0).OrderBy(p => p.vencimento).ToList())
                            foreach (var installment in agreementDigicob.Installments.Where(p => p.Status == "Pago").OrderBy(p => p.DueDate).ToList())
                            {
                                try
                                {
                                    var parcel = agreement.AgreementParcel.Where(p => (p.NrParcel + 1) == installment.Number).FirstOrDefault();

                                    if (havePayment == false)
                                        havePayment = currentParcel != null && (currentParcel.IdAgreementParcel == parcel.IdAgreementParcel);

                                    if (parcel.Payment.Where(p => p.NmFile == "API Digicob - " + installment.UpdatedAt.ToString("yyyy-MM-dd")).Count() == 0)
                                    {

                                        Util.SaveFile("Salvando pagamento");
                                        new PaymentBLL().Add
                                         (
                                             new Payment()
                                             {
                                                 IdAgreementParcel = parcel.IdAgreementParcel,
                                                 VlPayment = installment.Value,
                                                 DtPayment = installment.UpdatedAt,
                                                 NmFile = "API Cobmais - " + installment.UpdatedAt.ToString("yyyy-MM-dd"),
                                                 DtInsert = DateTime.Now
                                             }
                                         );
                                        /* gerar boleto da proxima fatura -- Implementar*/

                                        /*
                                        if (string.IsNullOrEmpty(agreement.CdParcelPlan) || string.IsNullOrWhiteSpace(agreement.CdParcelPlan))
                                        {
                                            Util.SaveFile("gerando boleto proxima fatura");

                                            var payments = new PaymentBLL().GetPayments(agreement.IdAgreement);
                                            if (payments != null && currentParcel.DtParcel >= DateTime.Today.AddDays(-15))
                                            {
                                                var lastPayment = payments.OrderByDescending(p => p.DtPayment).FirstOrDefault();

                                                var parcelNewBillet = agreement.AgreementParcel.Where(p => p.NrParcel > lastPayment.AgreementParcel.NrParcel).OrderBy(p => p.NrParcel).FirstOrDefault();

                                                var billetResponse = SendRemember.GetBillet(agreement, parcelNewBillet, product);

                                                if (billetResponse != null)
                                                {
                                                    objectSend.Email = emails;
                                                    objectSend.DtParcel = billetResponse.DtBillet;
                                                    objectSend.Line = billetResponse.Line;
                                                    objectSend.BilletUrl = billetResponse.URL;
                                                    objectSend.Value = billetResponse.VlBillet;
                                                    objectSend.Pdf = new System.Net.WebClient().DownloadData(billetResponse.URL);
                                                    objectSend.IdAgreementParcel = currentParcel.IdAgreementParcel;
                                                    objectSend.Parcel = currentParcel.NrParcel;

                                                    var envioEmailThread = new SendRemember(objectSend);


                                                    Ag ag = RestApi.Get<Ag>("https://10.40.0.30/credz/api", "agreement/" + product.DsProduct);
                                                    IList<string> origem = new List<string> { "1", "/", "ura", "ope", "rcs", "d=d", "9" };
                                                    if (origem.Contains(ag.Product.Navigation.DsOrigem) || product.Person.Email.Count == 0)
                                                    {
                                                        objectSend.Phone = SendRemember.GetPhone(product.Person);
                                                        if (!string.IsNullOrEmpty(objectSend.Phone))
                                                            envioEmailThread.SendSMSPagamento();
                                                    }

                                                    if (emails.Count > 0)
                                                        envioEmailThread.SendMailPagamento();
                                                }
                                            }
                                        }

                                        */
                                    }
                                }
                                catch (Exception ex)
                                {

                                }
                            }

                            Util.SaveFile("Alterando Status " + agreement.IdAgreementStatus.ToString() + " => " + agreementDigicob.Status);


                            Console.WriteLine(agreementDigicob.Id + " - " + agreementDigicob.Status);

                            if (agreement.IdAgreementStatus != idStatus)
                            {
                                agreement.IdAgreementStatus = idStatus;
                                agreementBLL.Update(agreement);
                            }


                            if (idStatus == 2)
                            {
                                if (product.Lead.Where(p => p.DtInsert >= DateTime.Today.AddDays(-1)).Count() > 0)
                                {

                                    if (objectSend.Agreement == null)
                                        objectSend.Agreement = agreement;
                                    if (agreement.CdParcelPlan.Trim() == "API CREDZ")
                                    {
                                        Util.SaveFile("Enviando RCS quebra");
                                        objectSend.Product = product;
                                        objectSend.Phone = SendRemember.GetPhone(product.Person);
                                        var envioEmailThread = new SendRemember(objectSend);
                                        envioEmailThread.SendRCSBroken();
                                    }
                                    else
                                    {
                                        Util.SaveFile("SMS");
                                        objectSend.Product = product;
                                        objectSend.Phone = SendRemember.GetPhone(product.Person);
                                        var envioEmailThread = new SendRemember(objectSend);
                                        envioEmailThread.SendSMS();
                                    }


                                    if (product.Person.Email.Count() > 0)
                                    {
                                        Util.SaveFile("Enviando email quebra");
                                        objectSend.Email = product.Person.Email.Where(p => p.flBloqueado == false && Util.IsEmail(p.DsEmail)).Select(p => p.DsEmail).Distinct().ToList();
                                        // var lead = product.Lead.OrderByDescending(p => p.IdLead).FirstOrDefault();
                                        // objectSend.Discount = discounts.Where(p => (lead.Age >= p.MinAge && lead.Age <= p.MaxAge) && p.MaxParcel <= 1).FirstOrDefault();
                                        var envioEmailThread = new SendRemember(objectSend);
                                        envioEmailThread.SendEmailBroken();
                                    }
                                }
                            }
                            else if ((!havePayment) && currentParcel != null && (idStatus == 1 || idStatus == 6))
                            {
                                Util.SaveFile("Enviando remember");
                                if (string.IsNullOrEmpty(agreement.CdParcelPlan))
                                {
                                    var envios = new EmailRememberBLL().GetEmailRemember(currentParcel.IdAgreementParcel);

                                    var dtParcel = new List<DateTime> { DateTime.Today, DateTime.Today.AddDays(2), DateTime.Today.AddDays(-3), DateTime.Today.AddDays(-5), DateTime.Today.AddDays(-7) };

                                    if (envios.Where(p => p.DtInsert >= DateTime.Today).Count() == 0 && dtParcel.Contains(currentParcel.DtParcel))
                                    //if (envios.Where(p => p.DtInsert >= DateTime.Today).Count() == 0)
                                    {
                                        Util.SaveFile("Enviando sms/rcs");
                                        //Billet billet = null;
                                        FMC.Digicob.DM.Models.BilletResponse billet = null;
                                        var installment = agreementDigicob.Installments.Where(p => p.Number == currentParcel.NrParcel + 1).FirstOrDefault();

                                        if (installment.Billets != null && installment.Billets.Count > 0)
                                            billet = installment.Billets.FirstOrDefault();
                                        else
                                        {

                                        }
                                        /*
                                        if (currentParcel.Billet != null && currentParcel.Billet.Count > 0)
                                            billet = currentParcel.Billet.FirstOrDefault();
                                        else
                                        {
                                            var billetResponse = SendRemember.GetBillet(agreement, currentParcel, product);
                                            if (billetResponse != null)
                                                billet = new Billet()
                                                {
                                                    DtBillet = billetResponse.DtBillet,
                                                    Line = billetResponse.Line,
                                                    URL = billetResponse.URL,
                                                    VlBillet = billetResponse.VlBillet
                                                };
                                        }*/

                                        if (billet != null)
                                        {
                                            try
                                            {
                                                Ag ag = RestApi.Get<Ag>("https://10.40.0.30/credz/api", "agreement/" + product.DsProduct);
                                                IList<string> origem = new List<string> { "1", "/", "ura", "ope", "rcs" };
                                                if (origem.Contains(ag.Product.Navigation.DsOrigem) || emails.Count == 0 || currentParcel.DtParcel < DateTime.Today)
                                                {
                                                    /*if (
                                                            (string.IsNullOrEmpty(currentParcel.Agreement.CdParcelPlan) &&
                                                            currentParcel.Agreement.IdAgreementStatus == 1 &&
                                                            currentParcel.NrParcel == 0 && envios.Count == 0) ||
                                                            (currentParcel.DtParcel < DateTime.Today)
                                                        )
                                                    {
                                                        objectSend.DtParcel = billet.DtBillet;
                                                        objectSend.Line = billet.Line;
                                                        objectSend.BilletUrl = billet.URL;
                                                        objectSend.Value = billet.VlBillet;
                                                        objectSend.Product = product;

                                                        objectSend.Phone = SendRemember.GetPhone(product.Person);
                                                        var envioEmailThread = new SendRemember(objectSend);
                                                        envioEmailThread.SendRCS();

                                                    }
                                                    else
                                                    {*/
                                                    objectSend.Line = billet.PaymentLine;
                                                    objectSend.DtParcel = billet.DueDate;
                                                    objectSend.Phone = SendRemember.GetPhone(product.Person);

                                                    if (!string.IsNullOrEmpty(objectSend.Phone))
                                                    {
                                                        var envioEmailThread = new SendRemember(objectSend);
                                                        envioEmailThread.SendSMS();
                                                    }
                                                    //}
                                                }
                                            }
                                            catch (Exception ex)
                                            {

                                            }
                                            if (!listEmail.Where(p => product.Person.Email.Where(e => e.DsEmail == p).Any()).Any())
                                            {
                                                try
                                                {
                                                    if (emails != null && emails.Count() > 0)
                                                    {
                                                        Util.SaveFile("Enviando email");


                                                        if (billet != null)
                                                        {
                                                            objectSend.Email = emails;
                                                            objectSend.DtParcel = billet.DueDate;
                                                            objectSend.Line = billet.PaymentLine;
                                                            objectSend.BilletUrl = billet.BarcodeUrl;
                                                            objectSend.Value = billet.Value;
                                                            objectSend.Pdf = new System.Net.WebClient().DownloadData(billet.BarcodeUrl);
                                                            objectSend.Product = product;
                                                            var envioEmailThread = new SendRemember(objectSend);

                                                            envioEmailThread.SendMail();

                                                        }
                                                    }

                                                }
                                                catch (Exception ex)
                                                {
                                                    string erro = ex.Message + Environment.NewLine;
                                                    while (ex.InnerException != null)
                                                    {
                                                        ex = ex.InnerException;
                                                        erro += ex.Message + Environment.NewLine;
                                                    }
                                                    Util.SaveFile("Laço: Erro ao enviar e-mail para " + listEmail.FirstOrDefault() + " conta " + product.DsProduct);
                                                    Util.SaveFile(erro);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Util.SaveFile("Erro ao gerar o boleto para parcela: " + currentParcel.IdAgreementParcel);
                                        }
                                    }
                                }
                            }
                        }

                        if (agreement.IdAgreementStatus != idStatus)
                        {
                            Console.WriteLine(agreementDigicob.Id + " - " + agreementDigicob.Status);
                            agreement.IdAgreementStatus = idStatus;
                            agreementBLL.Update(agreement);



                            if (idStatus == 2)
                            {
                                var objectSend = new ObjectSend()
                                {
                                    IdAgreement = agreement.IdAgreement,
                                    Name = product.Person.DsName.Trim(),
                                    CardName = contracts.FirstOrDefault().Store,
                                    //CardNumber = product.DsProduct.StartsWith("000") ? product.DsProduct.Substring(3, 8) + "********" : product.DsProduct.Substring(0, 8),
                                    aVista = agreement.QtParcel == 0,
                                    Product = product,
                                    CardUrl = product.ProductSpecification != null ? product.ProductSpecification.UrlImage : "",
                                };
                                if (product.Lead.Where(p => p.DtInsert >= DateTime.Today.AddDays(-1)).Count() > 0)
                                {

                                    if (objectSend.Agreement == null)
                                        objectSend.Agreement = agreement;
                                    if (agreement.CdParcelPlan.Trim() == "API CREDZ")
                                    {
                                        Util.SaveFile("Enviando RCS quebra");
                                        objectSend.Product = product;
                                        objectSend.Phone = SendRemember.GetPhone(product.Person);
                                        var envioEmailThread = new SendRemember(objectSend);
                                        envioEmailThread.SendRCSBroken();
                                    }
                                    else
                                    {
                                        Util.SaveFile("SMS");
                                        objectSend.Product = product;
                                        objectSend.Phone = SendRemember.GetPhone(product.Person);
                                        var envioEmailThread = new SendRemember(objectSend);
                                        envioEmailThread.SendSMS();
                                    }


                                    if (product.Person.Email.Count() > 0)
                                    {
                                        Util.SaveFile("Enviando email quebra");
                                        objectSend.Email = product.Person.Email.Where(p => p.flBloqueado == false && Util.IsEmail(p.DsEmail)).Select(p => p.DsEmail).Distinct().ToList();
                                        // var lead = product.Lead.OrderByDescending(p => p.IdLead).FirstOrDefault();
                                        // objectSend.Discount = discounts.Where(p => (lead.Age >= p.MinAge && lead.Age <= p.MaxAge) && p.MaxParcel <= 1).FirstOrDefault();
                                        var envioEmailThread = new SendRemember(objectSend);
                                        envioEmailThread.SendEmailBroken();
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Util.SaveFile("Acordo-" + agreement.CdAgreement + " CPF:" + product.Person.NrCNPJCPF + " não encontrado");
                        Console.WriteLine("Acordo-" + agreement.CdAgreement + " CPF:" + product.Person.NrCNPJCPF + " não encontrado");
                    }
                }
                catch (Exception ex)
                {
                    string erro = ex.Message + Environment.NewLine;
                    while (ex.InnerException != null)
                    {
                        ex = ex.InnerException;
                        erro += ex.Message + Environment.NewLine;
                    }
                    Util.SaveFile(erro);
                }
            }

            Util.SaveFile("Processo finalizado!");

            break;

        }
    }
}
catch (Exception ex)
{
    string erro = ex.Message + Environment.NewLine;
    while (ex.InnerException != null)
    {
        ex = ex.InnerException;
        erro += ex.Message + Environment.NewLine;
    }
    Util.SaveFile("Erro ao enviar e-mail");
    Util.SaveFile(erro);
}
