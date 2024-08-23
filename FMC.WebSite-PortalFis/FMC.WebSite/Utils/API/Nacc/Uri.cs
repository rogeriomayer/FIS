using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace FMC.WebSite.Afinz.Utils.API.Nacc
{
    public static class Uri
    {
        public static string URL()
        {
            //return "http://189.112.212.179/FMC.WebService/DebtorService.svc/";
            return "https://ws.apifmcbrasil.com.br/FMC.WebService/DebtorService.svc/";
        
        }

        #region GET
        public static string GetPessoa(string cpfCnpj, string tipo)
        {
            return string.Concat(URL(), "GetPessoa/", cpfCnpj, "/", tipo);
        }

        public static string GetConta(string cpf, string dtEntrada, string carteira)
        {
            return string.Concat(URL(), "GetConta/", cpf, "/", dtEntrada, "/", carteira);
        }
        public static string GetParcelamento(string listIdConta, string vlEntrada, string vlDesconto, string dtEntrada, string carteira)
        {
            return string.Concat(URL(), "GetParcelamento/", listIdConta, "/", vlEntrada, "/", vlDesconto, "/", dtEntrada, "/", carteira);
        }


        public static string SetAcordo(string listIdConta, string valorEntrada, string valorParcela, string valorDesconto, string numeroParcelas, string dtEntrada, string codUserNacc, string idUserNacc, string carteira)
        {
            return string.Concat(URL(), "SetAcordo/", listIdConta, "/", valorEntrada, "/", valorParcela, "/", valorDesconto, "/", numeroParcelas, "/", dtEntrada, "/", codUserNacc, "/", idUserNacc, "/", carteira);
        }

        public static string GetBoleto(string idAccount, string idAcordo, string parcela, string retornarPDF, string carteira)
        {
            return string.Concat(URL(), "GetBoleto/", idAccount, "/", idAcordo, "/", parcela, "/", retornarPDF, "/", carteira);
        }

        public static string EnviarBoletoEmail(string idBoleto, string email, string codUserNacc)
        {
            return string.Concat(URL(), "EnviarBoletoEmail/", idBoleto, "/", email, "/", codUserNacc);
        }

        public static string EnviarSMS(string nrPhone, string msg, string carteira)
        {
            return string.Concat(URL(), "EnviarSMS/", nrPhone, "/", msg, "/", carteira);
        }

        #endregion

        #region SET
        public static string AtualizaDadosCadastrais()
        {
            return string.Concat(URL(), "AtualizaDadosCadastrais");
        }
        public static string SetComentarios()
        {
            return string.Concat(URL(), "SetComentario");
        }

        #endregion

    }
}
