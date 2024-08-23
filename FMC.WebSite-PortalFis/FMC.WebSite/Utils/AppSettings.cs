using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMC.WebSite.FIS.Utils
{
    public class AppSettings
    {
        public static string KeyGoogleMaps
        {
            get
            {
                return new System.Configuration.AppSettingsReader().GetValue("KEY_GOOGLE_MAPS", typeof(string)).ToString();
            }
        }

        public static decimal ValueEntranceParcel
        {
            get
            {
                return (decimal)new System.Configuration.AppSettingsReader().GetValue("VALUE_ENTRANCE_PARCEL", typeof(decimal));
            }
        }

        public static string UserMail
        {
            get
            {
                return (string)new System.Configuration.AppSettingsReader().GetValue("USER_EMAIL", typeof(string));
            }
        }
        public static string PassMail
        {
            get
            {
                return (string)new System.Configuration.AppSettingsReader().GetValue("PASS_EMAIL", typeof(string));
            }
        }

        public static string DestinatarioEmailUberlandia
        {
            get
            {
                return (string)new System.Configuration.AppSettingsReader().GetValue("DESTINATARIO_EMAIL_UBERLANDIA", typeof(string));
            }
        }
        public static string DestinatarioEmailSp
        {
            get
            {
                return (string)new System.Configuration.AppSettingsReader().GetValue("DESTINATARIO_EMAIL_SP", typeof(string));
            }
        }
        public static string DestinatarioEmailFaleConosco
        {
            get
            {
                return (string)new System.Configuration.AppSettingsReader().GetValue("DESTINATARIO_EMAIL_FALECONOSCO", typeof(string));
            }
        }
        public static string DestinatarioEmailComercial
        {
            get
            {
                return (string)new System.Configuration.AppSettingsReader().GetValue("DESTINATARIO_EMAIL_COMERCIAL", typeof(string));
            }
        }
        public static string DestinatarioEmailRecrutamento
        {
            get
            {
                return (string)new System.Configuration.AppSettingsReader().GetValue("DESTINATARIO_EMAIL_RECRUTAMENTO", typeof(string));
            }
        }
        public static string DestinatarioEmailRecrutamentoSP
        {
            get
            {
                return (string)new System.Configuration.AppSettingsReader().GetValue("DESTINATARIO_EMAIL_RECRUTAMENTO_SP", typeof(string));
            }
        }
        public static string Host
        {
            get
            {
                return (string)new System.Configuration.AppSettingsReader().GetValue("HOST", typeof(string));
            }
        }
        public static int PortHost
        {
            get
            {
                return (int)new System.Configuration.AppSettingsReader().GetValue("PORT_HOST", typeof(int));
            }
        }

        public static string DestinatarioEmailBradescard
        {
            get
            {
                return (string)new System.Configuration.AppSettingsReader().GetValue("DESTINATARIO_EMAIL_BRADESCARD", typeof(string));
            }
        }


        public static int Desconto
        {
            get
            {
                return (int)new System.Configuration.AppSettingsReader().GetValue("DESCONTO", typeof(int));
            }
        }
        public static string ReCaptchaPublicKey
        {
            get
            {
                return (string)new System.Configuration.AppSettingsReader().GetValue("ReCaptcha:SiteKey", typeof(string));
            }
        }
        public static string ReCaptchaSecretKey
        {
            get
            {
                return (string)new System.Configuration.AppSettingsReader().GetValue("ReCaptcha:SecretKey", typeof(string));
            }
        }
        public static string ReCaptchaApi
        {
            get
            {
                return (string)new System.Configuration.AppSettingsReader().GetValue("ReCaptcha:VerifyAPI", typeof(string));
            }
        }

        public static string HourInitial
        {
            get
            {
                return (string)new System.Configuration.AppSettingsReader().GetValue("HOUR_INITIAL", typeof(string));
            }
        }
        public static string HourFinal
        {
            get
            {
                return (string)new System.Configuration.AppSettingsReader().GetValue("HOUR_FINAL", typeof(string));
            }
        }
        public static int MinPromisse
        {
            get
            {
                return (int)new System.Configuration.AppSettingsReader().GetValue("MIN_PROMISSE", typeof(int));
            }
        }
        public static int MaxPromisse
        {
            get
            {
                return (int)new System.Configuration.AppSettingsReader().GetValue("MAX_PROMISSE", typeof(int));
            }
        }

        public static string URL_API_FIS
        {
            get
            {
                return (string)new System.Configuration.AppSettingsReader().GetValue("URL_API_FIS", typeof(string));
            }
        }

        public static string URL_API_AFINZ
        {
            get
            {
                return (string)new System.Configuration.AppSettingsReader().GetValue("URL_API_AFINZ", typeof(string));
            }
        }
    }
}
