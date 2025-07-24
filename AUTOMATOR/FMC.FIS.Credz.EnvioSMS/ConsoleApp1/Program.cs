using FMC.FIS.BLL;
using FMC.FIS.Business.BLL;
using FMC.FIS.Business.Code.Api.Cobmais;
using FMC.FIS.Business.Models.BvTelecom;
using FMC.FIS.Business.Models.Cobmais;
using FMC.FIS.Business.Models.Customer;
using FMC.FIS.Business.Models.FIS;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

                DateTime dtLead = DateTime.Now.Hour > 11 ? DateTime.Today : DateTime.Today.AddDays(-1);
                //IList<Person> listPerson = new PersonBLL().GetPersonSendSMS(dtLead).ToList();
                IList<Person> listPerson = new PersonBLL().GetPersonSendRCSNews().ToList();

                IList<Discount> discounts = new DiscountBLL().GetByProductType(3).ToList();

                if (listPerson.Count > 0)
                {
                    string cpf = "";
                    Contrato contract = null;

                    IList<SingleRequest> listSend = new List<SingleRequest>();
                    IList<SMS> listSMS = new List<SMS>();

                    foreach (var person in listPerson.OrderBy(p => p.IdPerson).Distinct().ToList())
                    {
                        Console.WriteLine(person.NrCNPJCPF);
                        //var phones = person.Phone.Where(p => p.IdPhoneStatus == 1 && Convert.ToInt32(p.NrPhone.Substring(2, 1)) >= 6).Select(p => p.NrPhone).ToList();
                        //if (phones == null || phones.Count == 0)
                        //{
                        //    phones = new List<string>();
                        //    phones.Add(person.Phone.Where(p => p.Blacklist == false && p.IdPhoneStatus < 4 && Convert.ToInt32(p.NrPhone.Substring(2, 1)) >= 6).Select(p => p.NrPhone).FirstOrDefault());
                        //}
                        try
                        {
                            var lead = person.Product.Where(pr => pr.Lead.Where(p => p.DtInsert >= DateTime.Today.AddDays(-2)).Any()).FirstOrDefault().Lead.OrderByDescending(p => p.IdLead).FirstOrDefault();

                            if (cpf != person.NrCNPJCPF)
                            {
                                cpf = person.NrCNPJCPF;
                                    contract = GetContratos(lead);
                            }

                            

                            if (lead != null && phones.Count() > 0 && contract != null )
                            {
                                var product = person.Product.FirstOrDefault();
                                var products = person.Product.Select(p => p.DsProduct).ToList();
                                var phone = phones.FirstOrDefault();
                                if (contract != null)
                                {
                                    var obj = SendSMS(lead, phone, contract, discounts);
                                    if (obj != null)
                                    {
                                        listSend.Add(obj);
                                        listSMS.Add
                                            (
                                                new Business.Models.FIS.SMS()
                                                {
                                                    idPerson = person.IdPerson,
                                                    age = lead.Age,
                                                    telefone = Convert.ToInt64(phone),
                                                    dtEnvio = DateTime.Now
                                                }
                                            );
                                        Console.WriteLine(listSend.Count);
                                        if (listSend.Count > 20)
                                        {
                                            if (new BvSmsBLL().SmsBulk(new BulkRequest() { bulk = listSend }) == "OK")
                                            {
                                                try
                                                {
                                                    new SmsBLL().AddRangeNormal(listSMS.ToList());
                                                }
                                                catch (Exception ex)
                                                {

                                                }
                                            }
                                            listSend = new List<SingleRequest>();
                                            listSMS = new List<SMS>();
                                        }
                                    }
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
                            Util.SaveFile("Laço: Erro ao enviar sms para " + phones.FirstOrDefault() + " cpf " + person.NrCNPJCPF);
                            Util.SaveFile(erro);
                        }


                    }

                    if (listSend.Count > 1)
                    {
                        if (new BvSmsBLL().SmsBulk(new BulkRequest() { bulk = listSend }) == "OK")
                            new SmsBLL().AddRangeNormal(listSMS.ToList());
                    }
                    Util.SaveFile("Processo finalizado!");
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
        private static SingleRequest SendSMS(Lead lead, string phone, Contrato contract, IList<Discount> discounts)
        {
            StringBuilder message = new StringBuilder();

            //if (lead.Age > 281)
            //{
            var contrato = GetContratos(lead);

            AgreementSimulateResponse simulate = null;
            var discount = discounts.Where(p => (lead.Age >= p.MinAge && lead.Age <= p.MaxAge) && p.MaxParcel == 1).FirstOrDefault().MaxDiscount;
            if (contrato != null)
            {
                /*
                decimal vlParcel = 50;
                var parcela = 24;
                for (int i = 24; i > 0; i--)
                {
                    parcela = i;
                    vlParcel = (contrato.parcelas.FirstOrDefault().valor - (contrato.parcelas.FirstOrDefault().valor * (discount / 100))) / i;
                    if (vlParcel > 70)
                    {
                        break;
                    }
                }*/
                simulate = GetValueAgreement(0, contrato, lead);
            }

            if (simulate != null && simulate.ParcelResponse != null && simulate.ParcelResponse.Count > 0)
            {

                decimal vlPgt = simulate.ParcelResponse.Where(p => p.NrParcel == 0).FirstOrDefault().VlParcel;
                //message.Append(" não perca essa chance quite seu ").Append(lead.Product.ProductSpecification.Description.Replace("CREDZ", "").Replace("VISA", ""));
                //message.Append(" de R$").Append(simulate.VlFull.ToString("N2"));
                //message.Append(" por apenas R$").Append(vlPgt.ToString("N2"));
                //if (vlPgt > 200)
                //    message.Append(" ou parcele");
                //message.Append(" em https://fmc.digital/credz ou ligue 40034031");

                message.Append(lead.Product.Person.DsName.Split(' ').FirstOrDefault());
                message.Append(" quite o seu ");
                if (lead.Product.ProductSpecification != null)
                    message.Append(lead.Product.ProductSpecification.Description);
                else
                    message.Append("Credz Visa");

                message.Append(" por apenas R$");
                message.Append((simulate.ParcelResponse.FirstOrDefault().VlFull).ToString("N2"));
                message.Append(" a vista ");
                message.Append(" ou parcele");
                message.Append(" em http://fmc.digital/credz ou ligue 40034031");
                if (message.Length > 160)
                {
                    message.Clear();
                    message.Append(lead.Product.Person.DsName.Split(' ').FirstOrDefault());
                    message.Append(" quite o seu ");
                    message.Append(lead.Product.ProductSpecification.Description.Replace("CREDZ", "").Replace("VISA", ""));
                    message.Append(" por apenas R$");
                    message.Append((simulate.ParcelResponse.FirstOrDefault().VlFull).ToString("N2"));
                    message.Append(" a vista, ou parcele");
                    message.Append(" em http://fmc.digital/credz ou ligue 40034031");
                }

            }
            else
                return null;
            /*}
            else
            {
                message.Append(lead.Product.Person.DsName.Split(' ').FirstOrDefault());
                message.Append(" não vire o ano com dividas, renegocie agora seu ");
                message.Append(lead.Product.ProductSpecification.Description);
                message.Append(" em https://fmc.digital/credz ou ligue 40034031");
            }*/

            if (message.Length <= 160)
            {
                //var ret = new BvSmsBLL().SmsSingle
                //               (
                return new SingleRequest()
                {
                    celular = phone,
                    mensagem = message.ToString(),
                    carteiraId = 1064,
                    parceiroId = "credZ" + DateTime.Now.ToString("ddMMyyyyHHmmss")
                };
                //               );

                //return ret.ToUpper() == "OK";

            }
            else
            {
                return null;
            }
        }

        static IList<string> phones = new List<string>();
        private static Contrato GetContratos(Lead lead)
        {

           
            var person = CobmaisAPI.GetPessoa(lead.Product.Person.NrCNPJCPF);

            
            var phoneUra = new GenericQueryBLL<PhoneUra>().GetCollection("select top 1 CONVERT(varchar(11),telefone) telefone, dtLigacao from CREDZ.dbo.RetornoUra where SUBSTRING(CONVERT(varchar(11), telefone), 3,1) > 6 and  cpf = '" + lead.Product.Person.NrCNPJCPF + "' order by dtLigacao desc");

            if (phoneUra.Count() > 0)
                phones = phoneUra.Select(p => p.telefone).ToList();

            if (phones == null || phones.Count <= 0)
                phones = person.telefones.Where(p => p.ativo && p.contato && Convert.ToInt32(p.numero.Substring(2, 1)) >= 6).Select(p => p.numero).ToList();



            if (phones == null || phones.Count <= 0)
            {
                var phone = person.telefones.Where(p => p.ativo && Convert.ToInt32(p.numero.Substring(2, 1)) >= 6).Select(p => p.numero).ToList().FirstOrDefault();
                if (phone != null)
                    phones.Add(phone);
            }

            if (phones.Count > 0)
            {
                
                var contracts = CobmaisAPI.GetContratos(lead.Product.Person.NrCNPJCPF, "0", "0");

                if (contracts != null)
                {
                    var contract = contracts.Where(p => p.numero_contrato == lead.Product.DsProduct).FirstOrDefault();

                    return contract;
                }
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
                    return GetValueAgreement(nrParcel, contract,lead);
                }
                else
                    return null;
            }
        }

    }

    public class PhoneUra
    {
        [Key]
        public string telefone { get; set; }
    }
}
