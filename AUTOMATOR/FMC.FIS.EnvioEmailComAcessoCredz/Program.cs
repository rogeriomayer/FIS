using FMC.FIS.BLL;
using FMC.FIS.Business.BLL;
using FMC.FIS.Business.Code.Api.Cobmais;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

namespace FMC.FIS.EnvioEmailComAcessoCredz
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

                var listSimulacoes = new GenericQueryBLL<Simulacao>().GetCollection(GetQuery);
                Util.SaveFile(listSimulacoes.Count + " simulações encontradas");

                string ultimoItem = "";

                foreach (var simulacao in listSimulacoes)
                {
                    string item = simulacao.IdPerson.ToString() + "-" + simulacao.DsEmail;
                    if (Util.IsEmail(simulacao.DsEmail) && ultimoItem != item)
                    {
                        ultimoItem = item;
                        if (SendEmail(simulacao))
                        {
                            new ResendEmailBLL().Add
                                (
                                    new Business.Models.CREDZ.ResendEmail()
                                    {
                                        idPerson = simulacao.IdPerson,
                                        IdProduct = simulacao.IdProduct,
                                        nrSimulation = simulacao.QtdSimulacoes,
                                        email = simulacao.DsEmail,
                                        dtInsert = DateTime.Now
                                    }
                                );
                        }
                    }
                }

                var listSimulacoesSMS = new GenericQueryBLL<Simulacao>().GetCollection(GetQuerySMS);
                Util.SaveFile(listSimulacoesSMS.Count + " simulações encontradas - SMS");

                ultimoItem = "";

                foreach (var simulacao in listSimulacoesSMS)
                {

                    var idPerson = simulacao.IdPerson.ToString();
                    if (ultimoItem != idPerson)
                    {
                        ultimoItem = idPerson;
                        var phones = GetPhones(simulacao.cpf);
                        foreach (var phone in phones.Where(p => p.Length >= 10 && Convert.ToInt32(p.Substring(2, 1)) >= 6).ToList())
                        {
                            if (SendSMS(simulacao, phone) == "OK")
                            {
                                new ResendEmailBLL().Add
                                    (
                                        new Business.Models.CREDZ.ResendEmail()
                                        {
                                            idPerson = simulacao.IdPerson,
                                            IdProduct = simulacao.IdProduct,
                                            nrSimulation = simulacao.QtdSimulacoes,
                                            email = phone,
                                            dtInsert = DateTime.Now
                                        }
                                    );
                            }
                        }
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
                Util.SaveFile(erro);
            }
        }

        private static bool SendEmail(Simulacao simulacao)
        {
            var body = GetBody(simulacao);
            if (Util.SendMail("ACESSO AO PORTAL DE NEGOCIAÇÃO " + simulacao.Description, body, simulacao.Description, "10.40.0.21", 25, "credz@fmccobranca.com.br", "UR3d@$23cmF", new List<string>() { simulacao.DsEmail }))
            {
                return true;
            }
            else
                return false;
        }

        private static string SendSMS(Simulacao simulacao, string phone)
        {
            var message = new StringBuilder();

            var firstName = simulacao.DsName.Split(' ').FirstOrDefault();


            // Versão principal — com contraste de valores
            message.Append(firstName);
            message.Append(", voce acessou o site CREDZ, mas nao concluiu o acordo. Fale com um especialista no WhatsApp e finalize seu acordo: https://zaps.chat/r/credz ");

            return new BvSmsBLL().SmsSingle
                (
                    new Business.Models.BvTelecom.SingleRequest()
                    {
                        carteiraId = 1064,
                        parceiroId = "credZ" + DateTime.Now.ToString("ddMMyyyyHHmmss"),
                        celular = phone,
                        mensagem = message.ToString(),

                    }
                );

        }

        private static string GetBody(Simulacao simulacao)
        {
            var body = new StringBuilder();
            body.Append("<html>");
            if (simulacao.QtdSimulacoes == 0)
                body = GetBody0(simulacao.DsName, simulacao.DtNav.ToString("dd/MM/yyyy"), simulacao.Store.Substring(3, 6) + "**********", simulacao.Description);
            else if (simulacao.QtdSimulacoes == 1)
                body = GetBody1(simulacao.DsName, simulacao.DtNav.ToString("dd/MM/yyyy"));
            else
                body = GetBody2(simulacao.DsName, simulacao.DtNav.ToString("dd/MM/yyyy"));

            body.Append("<br>");
            body.Append("<br>");
            body.Append("<p>Caso já tenha efetuado o pagamento favor desconsiderar este e-mail.</p>");
            body.Append("<br>");
            body.Append("<br>");
            body.Append("<p><b>Equipe Negociador DM</b></p>");
            body.Append("<p><b>Whatsapp: <a href='https://zaps.chat/r/credz'>34 99640-0333</a> </b> </p>");
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

        private static StringBuilder GetBody0(string nome, string dataAcesso, string nomeCartao)
        {
            var body = new StringBuilder();
            body.Append("<p>Olá ").Append(nome).Append("!").Append("</p>");
            body.Append("<p>Vimos que você acessou nosso portal de negociação no dia ").Append(dataAcesso);
            body.Append(", mas faltou validar os dados para garantir que você realmente é o titular da conta e ter acesso ao super desconto que oferecemos para o seu cartão ");
            body.Append(" ").Append(nomeCartao).Append(".</p>");
            body.Append("<p>Pedimos esta confirmação para que dados sensíveis do devedor não seja passado a terceiros!").Append("</p>");
            body.Append("<p>Reforçamos que o site é seguro e que pode realizar sua negociação com rapidez e segurança, ");
            body.Append("basta clicar no link abaixo.</p>");
            body.Append("<p>Portal Negociação DM: <a href='https://fmc.digital/edm'>www.negociadordm.fmcbrasil.com.br</a> </p>");
            body.Append("<p>Em caso de dúvidas, pode entrar em contato com nossa central de atendimento");
            body.Append("<p>pelo <b>Whatsapp: <a href='https://zaps.chat/r/dm'>34 99797-3742</a> </b> </p>");

            return body;
        }

        private static StringBuilder GetBody1(string nome, string dataAcesso)
        {
            var body = new StringBuilder();
            body.Append("<p>Olá ").Append(nome).Append("!").Append("</p>");
            body.Append("<p>Você acessou nosso portal de negociação no dia ").Append(dataAcesso);
            body.Append(", porém não concluiu a negociação!");
            body.Append("<p>Sabia que você pode simular várias formas de pagamento no nosso site?</p>");
            body.Append(" Para isso basta clicar em <b>Negociar Agora</b> no cartão desejado, depois alterar os campos <b>Valor de Entrada</b> e <b>Data de Entrada</b>");
            body.Append(" para o valor e data de entrada que melhor lhe atender e clicar em <b>CALCULAR</b>, serão apresentadas as opções de pagamento a vista e parcelado,");
            body.Append(" aí é só clicar em <b>CONTRATAR</b> na opção que preferir e confirmar a negociação!</p>");
            body.Append("<p>Pronto você aproveitará nosso super desconto e poderá quitar seu cartão de forma rápida e prática!</p>");
            body.Append("<p>Não perca essa oportunidade!</p>");
            body.Append("<p>Reforçamos que o site é seguro e que pode realizar sua negociação com rapidez e segurança, ");
            body.Append("basta clicar no link abaixo.</p>");
            body.Append("<p>Portal Negociação DM: <a href='https://fmc.digital/edm'>www.negociadordm.fmcbrasil.com.br</a> </p>");
            body.Append("<p>Em caso de dúvidas, pode entrar em contato com nossa central de atendimento");
            body.Append("<p>pelo <b>Whatsapp: <a href='https://zaps.chat/r/dm'>34 99797-3742</a> </b> </p>");
            return body;
        }

        private static StringBuilder GetBody2(string nome, string dataAcesso)
        {
            var body = new StringBuilder();
            body.Append("<p>Olá ").Append(nome).Append("!").Append("</p>");
            body.Append("<p>Você acessou nosso portal de negociação no dia ").Append(dataAcesso);
            body.Append(", porém não concluiu uma negociação!");
            body.Append(" Faltou apenas clicar em <b>CONTRATAR</b> na forma de pagamento e confirmar a negociação!</p>");
            body.Append("<p>Acesse nosso portal de negociação e aproveite os super descontos que oferecemos para quitar sua dívida!</p>");
            body.Append("<p>Não perca essa oportunidade!</p>");
            body.Append("<p>Reforçamos que o site é seguro e que pode realizar sua negociação com rapidez e segurança, ");
            body.Append("clique no link abaixo.</p>");
            body.Append("<p>Portal Negociação DM: <a href='https://fmc.digital/edm'>www.negociadordm.fmcbrasil.com.br</a> </p>");
            body.Append("<p>Em caso de dúvidas, pode entrar em contato com nossa central de atendimento");
            body.Append("<p>pelo <b>Whatsapp: <a href='https://zaps.chat/r/dm'>34 99797-3742</a> </b> </p>");
            return body;
        }

        private static IList<string> GetPhones(string nrCPF)
        {
            var person = CobmaisAPI.GetPessoa(nrCPF);

            var phones = new List<string>();

            var phoneUra = new GenericQueryBLL<PhoneUra>().GetCollection("select top 1 CONVERT(varchar(11),telefone) telefone, dtLigacao from CREDZ.dbo.RetornoUra where SUBSTRING(CONVERT(varchar(11), telefone), 3,1) > 6 and  cpf = '" + nrCPF + "' order by dtLigacao desc");

            if (phoneUra.Count() > 0)
                phones = phoneUra.Select(p => p.telefone).ToList();

            var phoneSite = new GenericQueryBLL<PhoneUra>().GetCollection("select distinct Phone as 'telefone' from CREDZ.dbo.Billet where Phone is not null and cpf = '" + nrCPF + "' ");
            if (phoneSite.Count() > 0)
                phones = phoneSite.Select(p => p.telefone).ToList();

            if (phones == null || phones.Count <= 0)
                phones = person.telefones.Where(p => p.ativo && p.contato && Convert.ToInt32(p.numero.Substring(2, 1)) >= 6).Select(p => p.numero).ToList();



            if (phones == null || phones.Count <= 0)
            {
                var phone = person.telefones.Where(p => p.ativo && Convert.ToInt32(p.numero.Substring(2, 1)) >= 6).Select(p => p.numero).ToList().FirstOrDefault();
                if (phone == null)
                    phone = person.telefones.Where(p => Convert.ToInt32(p.numero.Substring(2, 1)) >= 6).Select(p => p.numero).ToList().FirstOrDefault();

                if (phone != null)
                    phones.Add(phone);
            }

            return phones;
        }

        public static string GetQuery
        {
            get
            {
                var query = new StringBuilder();
                query.Append(" 	select distinct count(distinct pplan.IdAgreementPlan) as 'QtdSimulacoes', convert(date, nav.DtInsert) as 'DtNav', cpf, per.IdPerson, per.DsName, ");
                query.Append(" 	Store, em.DsEmail, IdProduct");
                query.Append(" 	--pro.IdProduct, DsName, DsProduct, MaxDiscount, ps.Description, ps.UrlImage,  ");
                query.Append(" 	from DIGICOB.dbo.PortalAccess nav ");
                query.Append(" 		inner join FIS.dbo.Person per ");
                query.Append(" 			on per.NrCNPJCPF = nav.CPF ");

                query.Append(" 		left join fis.dbo.Product pr ");
                query.Append(" 		        on pr.IdPerson = per.IdPerson ");
                query.Append(" 		left join FIS.dbo.Email em ");
                query.Append(" 			on em.IdPerson = per.IdPerson ");
                query.Append(" 			and flBloqueado = 0 ");
                query.Append(" 		inner join bi.dbo.Person biper ");
                query.Append(" 			on biper.NrCNPJCPF = nav.CPF ");
                query.Append(" 		LEFT join DIGICOB.dbo.Contract co ");
                query.Append(" 			on co.IdPerson = biper.IdPerson ");
                query.Append(" 		left join DIGICOB.dbo.PortalAccessAgrementPlan pplan ");
                query.Append(" 			on pplan.IdPortalAccess = nav.IdPortalAccess ");
                query.Append(" 		left join DIGICOB.dbo.AgreementPlan ap ");
                query.Append(" 			on ap.IdAgreementPlan = pplan.IdAgreementPlan ");
                query.Append(" 	where nav.Portfolio = 'DM' ");
                //query.Append(" 		and nav.dtinsert between  DATEADD(hour,-2,getdate()) and DATEADD(minute,-15,getdate()) ");
                query.Append(" 		and nav.dtinsert between  DATEADD(hour,-24,getdate()) and DATEADD(minute,-15,getdate()) ");
                query.Append(" 		and  nav.IdPortalAccess = (select MAX(nv.IdPortalAccess) from DIGICOB.dbo.PortalAccess nv where nv.CPF = nav.CPF) ");
                query.Append(" 		and not exists ");
                query.Append(" 		( ");
                query.Append(" 			select *  ");
                query.Append(" 			from DIGICOB.dbo.Agreement ag ");
                query.Append(" 			where ag.PlanUuid = ap.PlanUuid ");
                query.Append(" 		) ");
                query.Append(" 	and not exists ");
                query.Append(" 	( ");
                query.Append(" 		select *  ");
                query.Append(" 		from CREDZ.ResendEmail rem ");
                query.Append(" 		where rem.idperson = per.idperson ");
                query.Append(" 		and rem.dtinsert >= getdate() -1 ");
                query.Append(" 	) ");
                query.Append(" 	group by cpf, per.IdPerson, convert(date, nav.DtInsert),per.DsName, Store,em.DsEmail, IdProduct ");
                query.Append(" 	order by IdPerson, DsEmail, convert(date, nav.DtInsert) ");

                return query.ToString();
            }
        }

        public static string GetQuerySMS
        {
            get
            {
                var query = new StringBuilder();
                query.Append(" select distinct count(IdSimulate) as 'QtdSimulacoes', max(nav.DtInsert) as 'DtNav',cpf, per.IdPerson, pro.IdProduct, DsName, DsProduct, 0.0 as MaxDiscount, '' as Description, '' as UrlImage, '' as DsEmail ");
                query.Append(" from CREDZ.dbo.Navigation nav ");
                query.Append(" 	inner join CREDZ.dbo.Product prd  ");
                query.Append(" 		on nav.IdNavigation = prd.IdNavigation  ");
                query.Append(" 	left join CREDZ.dbo.Simulate sim  ");
                query.Append(" 		on prd.IdProduct = sim.IdProduct  ");
                query.Append(" 	inner join FIS.dbo.Person per  ");
                query.Append(" 		on per.NrCNPJCPF = nav.CPF  ");
                query.Append(" 	inner join FIS.dbo.Product pro  ");
                query.Append(" 		on pro.IdPerson = per.IdPerson  ");
                query.Append(" 			and pro.DsProduct = prd.Account ");
                query.Append(" 	inner join fis.dbo.Lead le  ");
                query.Append(" 		on le.IdProduct = pro.IdProduct   ");
                query.Append(" 		and le.DtInsert >= CONVERT(Date, getdate()-1)  ");
                query.Append(" where  nav.dtinsert between  DATEADD(hour,-3,getdate()) and DATEADD(hour,-1,getdate())  ");
                query.Append(" and  nav.IdNavigation = (select MAX(idnavigation) from CREDZ.dbo.Navigation nv where nv.CPF = nav.CPF)  ");
                query.Append(" and not exists  ");
                query.Append(" (  ");
                query.Append(" 	select *   ");
                query.Append(" 	from CREDZ.dbo.Agreement ag  ");
                query.Append(" 		inner join CREDZ.dbo.Product pr  ");
                query.Append(" 			on pr.IdProduct = ag.IdProduct  ");
                query.Append(" 	where pr.Account = prd.Account  ");
                query.Append(" )  ");
                query.Append(" and not exists  ");
                query.Append(" (  ");
                query.Append(" 	select *   ");
                query.Append(" 	from CREDZ.ResendEmail rem  ");
                query.Append(" 	where rem.idperson = per.idperson  ");
                query.Append(" 	and rem.dtinsert >= getdate() -1  ");
                query.Append(" 	and rem.email not like '%@%' ");
                query.Append(" )  ");
                query.Append(" group by cpf, per.IdPerson, pro.IdProduct, DsName, DsProduct ");
                query.Append(" order by IdPerson  ");

                return query.ToString();
            }
        }
    }

    [Keyless]
    public class Simulacao
    {
        public int QtdSimulacoes { get; set; }
        public DateTime DtNav { get; set; }
        public long IdPerson { get; set; }
        public long IdProduct { get; set; }
        public string cpf { get; set; }
        public string DsName { get; set; }
        public string Store { get; set; }
        //public decimal? MaxDiscount { get; set; }
        //public string? Description { get; set; }
        //public string? UrlImage { get; set; }
        public string? DsEmail { get; set; }
    }

    public class PhoneUra
    {
        [Key]
        public string telefone { get; set; }
    }
}
