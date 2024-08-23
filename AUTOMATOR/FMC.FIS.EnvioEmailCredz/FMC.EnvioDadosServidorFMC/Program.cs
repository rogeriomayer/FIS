using FMC.FIS.BLL;
using FMC.FIS.Business.BLL;
using FMC.FIS.Business.Code.Api.Telegram;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading;
using System.Xml;
using TgSharp.TL;


namespace FMC.EnvioDadosServidorFMC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            Util.SaveFile("Iniciando processo de envio");
            SendDadosDiarios();
            SendPagamentos();

            SendDadosCredz();
            SendDadosServidores();
            SendDataCenter();
            Util.SaveFile("Processo finalizado!");
        }


        private static void SendPagamentos()
        {
            try
            {
                var result = TelegramAPI.SendTextMessage("6918027570:AAF_7-e5IhYn3jApDeZ7Q6ym0K85XQR9wtA", "-1002056284017", "- - - PAGAMENTOS - - -".ToString());
                var pgtsDiario = new GenericQueryBLL<PagamentosCredz>().GetCollection(QueryPgt(DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Today.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss")));

                StringBuilder message = new StringBuilder();
                if (pgtsDiario != null && pgtsDiario.Count() > 0)
                {
                    message.Append("DIARIO ->");
                    foreach (var pgt in pgtsDiario)
                        message.Append(" ").Append(pgt.Status).Append(":").Append(pgt.Total.ToString("N2"));
                }

                string dtIni = DateTime.Today.ToString("yyyy-MM-01 HH:mm:ss");
                string dtFim = Convert.ToDateTime(DateTime.Today.ToString("yyyy-MM-01")).AddMonths(1).AddSeconds(-1).ToString("yyyy-MM-dd HH:mm:ss");
                var pgtsMensal = new GenericQueryBLL<PagamentosCredz>().GetCollection(QueryPgt(dtIni, dtFim));

                if (pgtsMensal != null && pgtsMensal.Count() > 0)
                {
                    message.Append(" | MENSAL ->");
                    foreach (var pgt in pgtsMensal)
                        message.Append(" ").Append(pgt.Status).Append(":").Append(pgt.Total.ToString("N2"));
                }


                result = TelegramAPI.SendTextMessage("6918027570:AAF_7-e5IhYn3jApDeZ7Q6ym0K85XQR9wtA", "-1002056284017", message.ToString());
            }
            catch (Exception ex)
            {
                Util.SaveFile("Erro ao enviar PAGAMENTOS: " + ex.Message);
                var result = TelegramAPI.SendTextMessage("6918027570:AAF_7-e5IhYn3jApDeZ7Q6ym0K85XQR9wtA", "-1002056284017", "Erro ao gerar PAGAMENTOS".ToString());
            }
        }

        private static void SendDataCenter()
        {
            try
            {
                var result = TelegramAPI.SendTextMessage("6918027570:AAF_7-e5IhYn3jApDeZ7Q6ym0K85XQR9wtA", "-1002056284017", "- - - DATA CENTER - - -".ToString());

                var xml = RestApi.GetXML("http://10.35.4.20/data.xml");
                // var xml = RestApi.GetHttpClients<string>("http://10.35.4.20/data.xml");
                if (xml != null)
                {
                    StringBuilder message = new StringBuilder();

                    var tables = xml.ChildNodes[1];
                    var device = tables.ChildNodes[1];
                    foreach (XmlNode node in device.ChildNodes)
                    {
                        foreach (var field in node)
                        {

                            message.Append(((System.Xml.XmlElement)field).Attributes["niceName"].InnerText.Replace(" ", "_"));
                            message.Append(": ");
                            message.Append(((System.Xml.XmlElement)field).Attributes["value"].InnerText);
                            message.Append(" | ");
                        }
                    }

                    TelegramAPI.SendTextMessage("6918027570:AAF_7-e5IhYn3jApDeZ7Q6ym0K85XQR9wtA", "-1002056284017", message.ToString());
                }
            }
            catch (Exception ex)
            {
                Util.SaveFile("Erro ao enviar dados DATA CENTER: " + ex.Message);
                TelegramAPI.SendTextMessage("6918027570:AAF_7-e5IhYn3jApDeZ7Q6ym0K85XQR9wtA", "-1002056284017", "Erro ao gerar dados DATA CENTER");
            }
        }

        private static void SendDadosServidores()
        {
            try
            {
                var result = TelegramAPI.SendTextMessage("6918027570:AAF_7-e5IhYn3jApDeZ7Q6ym0K85XQR9wtA", "-1002056284017", "- - -PING NOS SERVIDORES - - - ");

                var listServer = new List<string> { "10.40.0.30", "10.40.109", "10.40.0.36", "10.40.0.12", "10.40.0.82", "10.40.0.21", "10.40.0.92", "10.40.0.93", "10.40.0.94" };


                StringBuilder message = new StringBuilder();
                foreach (var server in listServer)
                {
                    var ping = PingHost(server);
                    message.Append("PING_" + server + " : " + (ping ? "OK" : "INACESSIVEL") + " | ");
                }

                //string getUserAd = GetADUsers();
                //message.Append("Usuario CREDZ: ").Append(string.IsNullOrEmpty(getUserAd) ? "OK" : getUserAd);

                result = TelegramAPI.SendTextMessage("6918027570:AAF_7-e5IhYn3jApDeZ7Q6ym0K85XQR9wtA", "-1002056284017", message.ToString());
            }
            catch (Exception ex)
            {
                TelegramAPI.SendTextMessage("6918027570:AAF_7-e5IhYn3jApDeZ7Q6ym0K85XQR9wtA", "-1002056284017", "Erro ao gerar PING NOS SERVIDORES");
            }
        }

        private static void SendDadosDiarios()
        {
            try
            {





                var result = TelegramAPI.SendTextMessage("6918027570:AAF_7-e5IhYn3jApDeZ7Q6ym0K85XQR9wtA", "-1002056284017", "- - - DADOS GERENCIAIS- - - ");

                StringBuilder message = new StringBuilder();
                var email = new GenericQueryBLL<TotalCredz>().GetSingle("select MAX(dtinsert) as ultimo, COUNT(distinct Id) as total from Credz.SendEmail where DtInsert >= CONVERT(date, getdate())");

                if (email.ultimo != null)
                {
                    message.Append(" Ultimo_Email: ").Append(email.ultimo.Value.ToString("dd/MM/yyyy HH:mm"));
                    message.Append(" Emails_no_dia: ").Append(email.total);
                }

                var emailRemember = new GenericQueryBLL<TotalCredz>().GetSingle("select MAX(dtinsert) as ultimo, COUNT(distinct IdEmailRemember) as total from Credz.EmailRemember where DtInsert >= CONVERT(date, getdate())");
                if (emailRemember.ultimo != null)
                {
                    message.Append(" | Ultimo_Remember: ").Append(emailRemember.ultimo.Value.ToString("dd/MM/yyyy HH:mm"));
                    message.Append(" Remembers_no_dia: ").Append(emailRemember.total);
                }

                var emailUra = new GenericQueryBLL<TotalCredz>().GetSingle("select MAX(dtinsert) as ultimo, COUNT(distinct IdSendEmailUra) as total from Credz.SendEmailUra where DtInsert >= CONVERT(date, getdate()) and email like '%@%'");
                if (emailUra.ultimo != null)
                {
                    message.Append(" Ultimo_Email_URA: ").Append(emailUra.ultimo.Value.ToString("dd/MM/yyyy HH:mm"));
                    message.Append(" Emails_URA_no_dia: ").Append(emailUra.total);
                }

                var rcsUra = new GenericQueryBLL<TotalCredz>().GetSingle("select MAX(dtinsert) as ultimo, COUNT(distinct IdSendEmailUra) as total from Credz.SendEmailUra where DtInsert >= CONVERT(date, getdate()) and email not like '%@%'");
                if (rcsUra.ultimo != null)
                {
                    message.Append(" Ultimo_RCS_URA: ").Append(rcsUra.ultimo.Value.ToString("dd/MM/yyyy HH:mm"));
                    message.Append(" RCS_URA_no_dia: ").Append(rcsUra.total);
                }

                var sms = new GenericQueryBLL<TotalCredz>().GetSingle("select MAX(dtenvio) as ultimo, COUNT(distinct Id) as total from Credz.SMS where dtenvio >= CONVERT(date, getdate())");
                if (sms.ultimo != null)
                {
                    message.Append(" | Ultimo_SMS: ").Append(sms.ultimo.Value.ToString("dd/MM/yyyy HH:mm"));
                    message.Append(" SMS_no_dia: ").Append(sms.total);
                }

                var rcs = new GenericQueryBLL<TotalCredz>().GetSingle("select MAX(DtInsert) as ultimo, COUNT(distinct Id) as total from Credz.Sendrcs where DtInsert >= CONVERT(date, getdate())");
                if (rcs.ultimo != null)
                {
                    message.Append(" | Ultimo_RCS: ").Append(rcs.ultimo.Value.ToString("dd/MM/yyyy HH:mm"));
                    message.Append(" RCS_no_dia: ").Append(rcs.total);
                }

                var lead = new GenericQueryBLL<TotalCredz>().GetSingle("select MAX(dtinsert) as ultimo, COUNT(distinct IdLead) as total from FIS.dbo.Lead where DtInsert >= CONVERT(date, getdate())");
                if (lead.ultimo != null)
                {
                    message.Append(" | Ultima_Lead: ").Append(lead.ultimo.Value.ToString("dd/MM/yyyy HH:mm"));
                    message.Append(" Leads_no_dia: ").Append(lead.total);
                }

                var novas = new GenericQueryBLL<TotalCredz>().GetSingle(QueryContasNovas());
                if (novas.ultimo != null)
                {
                    message.Append(" | Ultima_Conta_Nova: ").Append(novas.ultimo.Value.ToString("dd/MM/yyyy HH:mm"));
                    message.Append(" Contas_Novas_no_dia: ").Append(novas.total);
                }

                result = TelegramAPI.SendTextMessage("6918027570:AAF_7-e5IhYn3jApDeZ7Q6ym0K85XQR9wtA", "-1002056284017", message.ToString());

            }
            catch (Exception ex)
            {
                Util.SaveFile("Erro ao enviar DADOS GERENCIAIS: " + ex.Message);
                TelegramAPI.SendTextMessage("6918027570:AAF_7-e5IhYn3jApDeZ7Q6ym0K85XQR9wtA", "-1002056284017", "Erro ao gerar DADOS GERENCIAIS");
            }
        }

        private static void SendDadosCredz()
        {
            try
            {
                var clicks = new GenericQueryBLL<TotalCredz>().GetSingle("select MAX(dtinsert) as ultimo, COUNT(distinct A.IdShortAccess) as total from FIS.DBO.ShortAccess A where DtInsert >= CONVERT(date, getdate())");
                var navigation = new GenericQueryBLL<TotalCredz>().GetSingle("select MAX(dtinsert) as ultimo, COUNT(distinct CPF) as total from CREDZ.dbo.Navigation where DtInsert >= CONVERT(date, getdate())");
                var simulate = new GenericQueryBLL<TotalCredz>().GetSingle("select MAX(dtinsert) as ultimo, COUNT(distinct IdProduct) as total from CREDZ.dbo.Simulate where DtInsert >= CONVERT(date, getdate())");
                var agreement = new GenericQueryBLL<TotalCredz>().GetSingle("select MAX(dtinsert) as ultimo, COUNT(distinct IdAgreement) as total from CREDZ.dbo.Agreement where DtInsert >= CONVERT(date, getdate())");
                var billet = new GenericQueryBLL<TotalCredz>().GetSingle("select MAX(dtinsert) as ultimo, COUNT(distinct a.Account) as total from CREDZ.dbo.Billet a where DtInsert >= CONVERT(date, getdate())");

                StringBuilder query = new StringBuilder();
                query.Append(" select MAX(a.dtinsert) as ultimo, COUNT(distinct CPF) as total ");
                query.Append(" from CREDZ.dbo.Navigation a ");
                query.Append(" 	inner join Person P ");
                query.Append(" 		ON p.NrCNPJCPF = a.CPF ");
                query.Append(" 	inner join Product pr ");
                query.Append(" 		on pr.IdPerson = p.IdPerson ");
                query.Append(" 	inner join Lead le ");
                query.Append(" 		on le.IdProduct = pr.IdProduct ");
                query.Append(" 	inner join StatusLead sl ");
                query.Append(" 		on sl.IdLead = le.IdLead ");
                query.Append(" 	inner join Agreement ag ");
                query.Append(" 		on ag.IdStatusLead = sl.IdStatusLead ");
                query.Append(" 		and IdAgreementStatus <> 2 ");
                query.Append(" 		and CONVERT(date, ag.DtInsert) <> CONVERT(Date, a.DtInsert) ");
                query.Append(" where a.DtInsert >= CONVERT(date, getdate()) ");

                var activeAgreement = new GenericQueryBLL<TotalCredz>().GetSingle(query.ToString());




                var result = TelegramAPI.SendTextMessage("6918027570:AAF_7-e5IhYn3jApDeZ7Q6ym0K85XQR9wtA", "-1002056284017", "- - - PORTAL CREDZ- - - ");

                StringBuilder message = new StringBuilder();
                if (navigation.ultimo != null)
                    message.Append("Ultimo_Click: ").Append(clicks.ultimo.Value.ToString("dd/MM/yyyy HH:mm"));
                message.Append(" Clicks_no_dia: ").Append(clicks.total);

                if (navigation.ultimo != null)
                    message.Append(" | Ultimo_Acesso: ").Append(navigation.ultimo.Value.ToString("dd/MM/yyyy HH:mm"));
                message.Append(" Acessos_no_dia: ").Append(navigation.total);

                if (simulate.ultimo != null)
                    message.Append(" | Ultima_Simulação: ").Append(simulate.ultimo.Value.ToString("dd/MM/yyyy HH:mm"));
                message.Append(" Simulações_no_dia: ").Append(simulate.total);


                if (activeAgreement.ultimo != null)
                    message.Append(" | Ultima_Conta_Com_Acordo: ").Append(activeAgreement.ultimo.Value.ToString("dd/MM/yyyy HH:mm"));
                message.Append(" Contas_Com_Acordo_no_dia: ").Append(activeAgreement.total);

                if (billet.ultimo != null)
                    message.Append(" | Ultimo_Boleto: ").Append(billet.ultimo.Value.ToString("dd/MM/yyyy HH:mm"));
                message.Append(" Boletos_no_dia: ").Append(billet.total);

                if (agreement.ultimo != null)
                    message.Append(" | Ultimo_Acordo: ").Append(agreement.ultimo.Value.ToString("dd/MM/yyyy HH:mm"));
                message.Append(" Acordos_no_dia: ").Append(agreement.total);


                result = TelegramAPI.SendTextMessage("6918027570:AAF_7-e5IhYn3jApDeZ7Q6ym0K85XQR9wtA", "-1002056284017", message.ToString());
            }
            catch (Exception ex)
            {
                Util.SaveFile("Erro ao enviar PORTAL CREDZ: " + ex.Message);
                TelegramAPI.SendTextMessage("6918027570:AAF_7-e5IhYn3jApDeZ7Q6ym0K85XQR9wtA", "-1002056284017", "Erro ao gerar PORTAL CREDZ");
            }
        }

        private static bool PingHost(string nameOrAddress)
        {
            bool pingable = false;
            Ping pinger = null;

            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send(nameOrAddress);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {
                return false;
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }

            return pingable;
        }

        private static string QueryPgt(string dtIni, string dtFim)
        {
            StringBuilder query = new StringBuilder();


            query.Append(" select sum(b.vlparcel) as Total, ");
            query.Append(" case when  d.IdPayment is not null or a.IdAgreementStatus = 5 then 'Pago'   ");
            query.Append(" when (c.IdAgreementStatus = 2 and CONVERT(Date,c.DtInsert) between '").Append(dtIni).Append("' and '").Append(dtFim).Append("') ").Append(" then 'Quebra'   ");
            query.Append(" when a.IdAgreement not in (5,2) and DtParcel >= CONVERT(date, getdate() -10) then 'Aguardando Pgto'  ");
            query.Append(" else 'Aguardando Pgto' ");
            query.Append(" end as 'Status'  ");
            query.Append(" from Agreement a  ");
            query.Append(" 	inner join AgreementParcel b  ");
            query.Append(" 		on a.IdAgreement = b.IdAgreement  ");
            query.Append(" 	left join AgreementStatusUpdate c ");
            query.Append(" 		on a.IdAgreement = c.IdAgreement ");
            query.Append(" 		and c.IdAgreementStatusUpdate = (select MAX(IdAgreementStatusUpdate) from AgreementStatusUpdate c1 where c1.IdAgreement = c.IdAgreement) ");
            query.Append("      and c.dtInsert  between '").Append(Convert.ToDateTime(dtIni).ToString("yyyy-MM-01 00:00")).Append("' and '").Append(dtFim).Append("' ");
            query.Append(" 	left join Payment d ");
            query.Append(" 		on d.IdAgreementParcel = b.IdAgreementParcel ");
            query.Append(" where a.CdParcelPlan = ''  ");
            query.Append(" and DtParcel  between '").Append(dtIni).Append("' and '").Append(dtFim).Append("' ");
            query.Append(" and not exists ");
            query.Append(" ( ");
            query.Append(" select idagreement ");
            query.Append(" from AgreementStatusUpdate f ");
            query.Append(" where f.IdAgreement = a.idagreement ");
            query.Append(" and f.DtInsert < '").Append(dtIni).Append("' ");
            query.Append(" and f.IdAgreementStatus = 2 ");
            query.Append(" ) ");
            query.Append(" group by  ");
            query.Append(" case when  d.IdPayment is not null or a.IdAgreementStatus = 5 then 'Pago'   ");
            query.Append(" when (c.IdAgreementStatus = 2 and CONVERT(Date,c.DtInsert) between '").Append(dtIni).Append("' and '").Append(dtFim).Append("') ").Append(" then 'Quebra'   ");
            query.Append(" when a.IdAgreement not in (5,2) and DtParcel >= CONVERT(date, getdate() -10) then 'Aguardando Pgto'  ");
            query.Append(" else 'Aguardando Pgto' ");
            query.Append(" end ");
            query.Append(" order by 1 desc  ");

            return query.ToString();
        }

        private static string QueryContasNovas()
        {
            StringBuilder query = new StringBuilder();

            query.Append(" select MAX(dtinsert) as ultimo, COUNT(distinct IdLead) as total ");
            query.Append(" from Lead a ");
            query.Append(" where DtInsert >= CONVERT(date, getdate()) ");
            query.Append(" and IdProduct not in  ");
            query.Append(" ( ");
            query.Append(" select IdProduct ");
            query.Append(" from Lead b ");
            query.Append(" where b.DtInsert between CONVERT(Date, getdate() -95) and CONVERT(date, getdate()) ");
            query.Append(" and a.IdProduct = b.IdProduct ");
            query.Append(" ) ");

            return query.ToString();
        }





        
        public static System.DirectoryServices.DirectoryEntry BuscaUsuarioDominio(string sam, System.DirectoryServices.DirectoryEntry root)
        {
            try
            {
                using (DirectorySearcher searcher = new DirectorySearcher(root, string.Format("(sAMAccountName={0})", sam)))
                {
                    SearchResult sr = searcher.FindOne();

                    if (!(sr == null)) return sr.GetDirectoryEntry();
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}

public class TotalCredz
{
    [Key]
    public int total { get; set; }
    public DateTime? ultimo { get; set; }

}

public class PagamentosCredz
{
    [Key]
    public string Status { get; set; }
    public Decimal Total { get; set; }
}
