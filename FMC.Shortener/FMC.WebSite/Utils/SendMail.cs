using FMC.Fis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FMC.Fis.Utils
{
    public class SendMail
    {
        public static void ContactMail(Contato contato)
        {
            string mensagem = "<html><head><title>Contato do site</title></head><body style=\"font-family:'Trebuchet MS'\">";
            IList<string> destinatario = new List<string>();
            string tipo = "";
            if (contato.Tipo == "1")
            {
                tipo = "DÚVIDAS E SUGESTÕES";
                destinatario = AppSettings.DestinatarioEmailFaleConosco.Split(';');
            }
            else if (contato.Tipo == "2")
            {
                tipo = "COMERCIAL";
                destinatario = AppSettings.DestinatarioEmailComercial.Split(';');
            }
            else if (contato.Tipo == "3")
            {
                tipo = "RECRUTAMENTO E SELEÇÃO";
                destinatario = AppSettings.DestinatarioEmailRecrutamento.Split(';');
            }
            else if (contato.Tipo == "4")
            {
                tipo = "CONTRATOS BRADESCARD";
                destinatario = AppSettings.DestinatarioEmailBradescard.Split(';');
            }
            else if (contato.Tipo == "5")
            {
                tipo = "CONTRATOS BRADESCO";
                destinatario = AppSettings.DestinatarioEmailBradesco.Split(';');
            }
            else if (contato.Tipo == "6")
            {
                tipo = "CONTRATOS MULTILOJA";
                destinatario = AppSettings.DestinatarioEmailMultiloja.Split(';');
            }
            else if (contato.Tipo == "7")
            {
                tipo = "CONTRATOS PEPSICO";
                destinatario = AppSettings.DestinatarioEmailPepsico.Split(';');
            }
            else if (contato.Tipo == "8")
            {
                tipo = "CONTRATOS SIMPLIC";
                destinatario = AppSettings.DestinatarioEmailSimplic.Split(';');
            }
            else if (contato.Tipo == "9")
            {
                tipo = "OUTROS CONTRATOS";
                destinatario = AppSettings.DestinatarioEmailDiversos.Split(';');
            }

            mensagem += "<p><strong>Nome: </strong> " + contato.Nome + "<br /></p>";
            mensagem += "<p><strong>Email: </strong> " + contato.Email + "<br /></p>";
            mensagem += "<p><strong>Telefone Preferencial: </strong> " + contato.Telefone + "<br /></p>";
            mensagem += "<p><strong>Telefone Secundário: </strong> " + contato.TelefoneSecundario + "<br /></p>";
            mensagem += "<p><strong>Tipo: </strong> " + tipo + "<br /></p>";
            if (
                contato.Tipo == "4" ||
                contato.Tipo == "5" ||
                contato.Tipo == "6" ||
                contato.Tipo == "7" ||
                contato.Tipo == "8" ||
                contato.Tipo == "9"
                )
            {
                mensagem += "<p><strong>CPF/CNPJ: </strong> " + contato.CPF + "<br /></p>";
            }
            mensagem += "<p><strong>Mensagem: </strong> " + contato.Mensagem + "<br /></p>";
            mensagem += "</body></html>";

            SmtpClient smtp = Mail.Smtp(AppSettings.UserMail, AppSettings.PassMail);

            MailMessage message = Mail.Message(AppSettings.UserMail, destinatario, "[" + tipo + "] - Contato pelo site", mensagem);
            smtp.Send(message);
        }
        public static void CurriculoMail(Curriculo curriculo)
        {
            string mensagem = "<html><head><title>Currículo do site</title>" +
                "<style>" +
                "*{font-family: Arial}" +
                "h1{font-size: 20px}" +
                "h2{font-size: 15px; text-decoration:underline; padding: 7px; margin: 0}" +
                "p{font-size: 13px; line-height:14px}" +
                "</style>" +
                "</head><body style=\"font-family:'Arial'\">" +
                "<!--[if mso]>" +
                "<style type=\"text/css\">" +
                "body, table, td {font-family: Arial, Helvetica, sans-serif !important;}" +
                "</style>" +
                "<![endif]-->";
            IList<string> destinatario = new List<string>();
            string tipo = "TRABALHE CONOSCO";

            if (curriculo.Unidade == "Uberlândia")
                destinatario = AppSettings.DestinatarioEmailRecrutamento.Split(';');
            else
                destinatario = AppSettings.DestinatarioEmailRecrutamentoSP.Split(';');

            mensagem += "<h1>Trabalhe Conosco</h1>";
            mensagem += "<h2>Informações Pessoais</h2>";

            mensagem += "<p><strong>Nome Completo: </strong> " + curriculo.Nome + "<br />" +
                        "<strong>Data de Nascimento: </strong> " + curriculo.DataNascimento.ToString("dd/MM/yyyy") + "<br />" +
                        "<strong>Sexo: </strong> " + curriculo.Sexo + "<br />" +
                        "<strong>Observações: </strong> " + curriculo.Observacoes + "<br /><br />" +
                        "<strong>Disponibilidade de horário para trabalhar: </strong> " + curriculo.Disponibilidade + "</p>";

            mensagem += "<h2>Documentos Pessoais</h2>";

            mensagem += "<p><strong>CPF: </strong> " + curriculo.Cpf + "<br />" +
                        "<strong>Carteira de trabalho (CTPS): </strong> " + curriculo.Ctps + "<br />" +
                        "<strong>Série: </strong> " + curriculo.Serie + "<br />" +
                        "<strong>PIS: </strong> " + curriculo.Pis + "</p>";

            mensagem += "<table style='table-layout: fixed; width: 100%'><colgroup><col style='width: 50%'><col style='width: 50%'></colgroup>";
            mensagem += "<tr><td style=\"vertical-align: top \">";

            mensagem += "<h2>Dados de Endereço</h2>";

            mensagem += "<p><strong>CEP: </strong> " + curriculo.Cep + "<br />" +
                        "<strong>Endereço: </strong> " + curriculo.Endereco + "<br />" +
                        "<strong>Número: </strong> " + curriculo.Numero + "<br />" +
                        "<strong>Complemento: </strong> " + curriculo.Complemento + "<br />" +
                        "<strong>Estado: </strong> " + curriculo.Estado + "<br />" +
                        "<strong>Cidade: </strong> " + curriculo.Cidade + "<br />" +
                        "<strong>Bairro: </strong> " + curriculo.Bairro + "<br />" +
                        "<strong>Telefone Residencial: </strong> " + curriculo.TelefoneResidencial + "<br />" +
                        "<strong>Telefone Celular: </strong> " + curriculo.TelefoneCelular + "<br />" +
                        "<strong>Telefone para Recado: </strong> " + curriculo.TelefoneRecado + "</p>";

            mensagem += "</td><td style=\"vertical-align: top\">";
            mensagem += "<h2>Informações Educacionais</h2>";

            mensagem += "<p><strong>Nível Acadêmico: </strong> " + curriculo.NivelAcademico + "<br />" +
                        "<strong>Nome do Curso: </strong> " + curriculo.NomeCurso + "<br />" +
                        "<strong>Data de Início: </strong> " + curriculo.DataInicio?.ToString("dd/MM/yyyy") + "<br />" +
                        "<strong>Ano de Conclusão: </strong> " + curriculo.AnoConclusao?.ToString("dd/MM/yyyy") + "<br />" +
                        "<strong>Horário do Curso: </strong> " + curriculo.HorarioCurso + "<br />" +
                        "<strong>Conhecimento de Informática: </strong> " + curriculo.ConhecimentoInformatica + "</p>";

            mensagem += "</td></tr></table>";

            mensagem += "<h2>Experiências Profissionais</h2>";
            string primeiroEmprego = (curriculo.PrimeiroEmprego) ? "Sim" : "Não";
            mensagem += "<p><strong>Primeiro Emprego: </strong> " + primeiroEmprego + "<br /></p>";

            mensagem += "<table style='table-layout: fixed; width: 100%'><colgroup><col style='width: 50%'><col style='width: 50%'></colgroup>";
            mensagem += "<tr><td style=\"vertical-align: top; padding-right: 20px \">";
            mensagem += "<p><strong>EMPRESA 1</strong><br /><br />" +
                        "<strong>Nome: </strong> " + curriculo.Empresa1 + "<br />" +
                        "<strong>Função: </strong> " + curriculo.Funcao1 + "<br />" +
                        "<strong>Data de Início: </strong> " + curriculo.DataInicio1?.ToString("dd/MM/yyyy") + "<br />" +
                        "<strong>Data de Desligamento: </strong> " + curriculo.DataDesligamento1?.ToString("dd/MM/yyyy") + "<br />" +
                        "<strong>Resumo das atividades executadas: </strong> " + curriculo.Resumo1 + "</p>";

            mensagem += "</td><td style=\"vertical-align: top\">";
            mensagem += "<p><strong>EMPRESA 2</strong><br /><br />" +
                        "<strong>Nome: </strong> " + curriculo.Empresa2 + "<br />" +
                        "<strong>Função: </strong> " + curriculo.Funcao2 + "<br />" +
                        "<strong>Data de Início: </strong> " + curriculo.DataInicio2?.ToString("dd/MM/yyyy") + "<br />" +
                        "<strong>Data de Desligamento: </strong> " + curriculo.DataDesligamento2?.ToString("dd/MM/yyyy") + "<br />" +
                        "<strong>Resumo das atividades executadas: </strong> " + curriculo.Resumo2 + "</p>";

            mensagem += "</td></tr></table>";
            mensagem += "<p><strong>Como ficou sabendo da vaga? </strong>" + curriculo.FicouSabendo + "<br /></p>";
            mensagem += "<p><small><i>Currículo enviado em " + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " </i></small></p>";
            mensagem += "</body></html>";

            SmtpClient smtp = Mail.Smtp(AppSettings.UserMail, AppSettings.PassMail);

            MailMessage message = Mail.Message(AppSettings.UserMail, destinatario, "[" + tipo + "] - Contato pelo site", mensagem);
            smtp.Send(message);
        }
    }
}
