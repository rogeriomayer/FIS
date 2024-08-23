using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMC.Fis.Utils
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

        public static double PercentageEntranceParcel
        {
            get
            {
                return (double) new System.Configuration.AppSettingsReader().GetValue("PERCENTAGE_ENTRANCE_PARCEL", typeof(double));
            }
        }

        public static double ValueEntranceParcel
        {
            get
            {
                return (double)new System.Configuration.AppSettingsReader().GetValue("VALUE_ENTRANCE_PARCEL", typeof(double));
            }
        }

        public static double PercentageEntranceNacc
        {
            get
            {
                return (double)new System.Configuration.AppSettingsReader().GetValue("PERCENTAGE_ENTRANCE_NACC", typeof(double));
            }
        }

        public static double PercentageEntranceNaccPEP
        {
            get
            {
                return (double)new System.Configuration.AppSettingsReader().GetValue("PERCENTAGE_ENTRANCE_NACC_PEP", typeof(double));
            }
        }

        public static double PercentageDiscountNaccParceled
        {
            get
            {
                return (double)new System.Configuration.AppSettingsReader().GetValue("PERCENTAGE_DISCOUNT_NACC_PARCELED", typeof(double));
            }
        }
        public static double PercentageDiscountNaccCash
        {
            get
            {
                return (double)new System.Configuration.AppSettingsReader().GetValue("PERCENTAGE_DISCOUNT_NACC_CASH", typeof(double));
            }
        }

        public static double PercentageDiscountNaccParceledENO
        {
            get
            {
                return (double)new System.Configuration.AppSettingsReader().GetValue("PERCENTAGE_DISCOUNT_NACC_PARCELED_ENO", typeof(double));
            }
        }
        public static double PercentageDiscountNaccCashENO
        {
            get
            {
                return (double)new System.Configuration.AppSettingsReader().GetValue("PERCENTAGE_DISCOUNT_NACC_CASH_ENO", typeof(double));
            }
        }
        public static double PercentageDiscountNaccParceledMMN
        {
            get
            {
                return (double)new System.Configuration.AppSettingsReader().GetValue("PERCENTAGE_DISCOUNT_NACC_PARCELED_MMN", typeof(double));
            }
        }
        public static double PercentageDiscountNaccCashMMN
        {
            get
            {
                return (double)new System.Configuration.AppSettingsReader().GetValue("PERCENTAGE_DISCOUNT_NACC_CASH_MMN", typeof(double));
            }
        }

        public static string CodUserNacc
        {
            get
            {
                return (string)new System.Configuration.AppSettingsReader().GetValue("COD_USER_NACC", typeof(string));
            }
        }

        public static string IdUserNacc
        {
            get
            {
                return (string)new System.Configuration.AppSettingsReader().GetValue("ID_USER_NACC", typeof(string));
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

        public static string DestinatarioEmailBradesco
        {
            get
            {
                return (string)new System.Configuration.AppSettingsReader().GetValue("DESTINATARIO_EMAIL_BRADESCO", typeof(string));
            }
        }

        public static string DestinatarioEmailPepsico
        {
            get
            {
                return (string)new System.Configuration.AppSettingsReader().GetValue("DESTINATARIO_EMAIL_PEPSICO", typeof(string));
            }
        }

        public static string DestinatarioEmailSimplic
        {
            get
            {
                return (string)new System.Configuration.AppSettingsReader().GetValue("DESTINATARIO_EMAIL_SIMPLIC", typeof(string));
            }
        }

        public static string DestinatarioEmailMultiloja
        {
            get
            {
                return (string)new System.Configuration.AppSettingsReader().GetValue("DESTINATARIO_EMAIL_MULTILOJA", typeof(string));
            }
        }
        public static string DestinatarioEmailDiversos
        {
            get
            {
                return (string)new System.Configuration.AppSettingsReader().GetValue("DESTINATARIO_EMAIL_DIVERSOS", typeof(string));
            }
        }
        public static decimal JurosAvistaIBI
        {
            get
            {
                return (decimal)new System.Configuration.AppSettingsReader().GetValue("JUROS_AVISTA_IBI", typeof(decimal));
            }
        }
        public static string PhoneWhatsappUberlandia
        {
            get
            {
                return (string)new System.Configuration.AppSettingsReader().GetValue("PHONE_WHATSAPP_UBERLANDIA", typeof(string));
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
        public static int AgeRedirect
        {
            get
            {
                return (int)new System.Configuration.AppSettingsReader().GetValue("AgeRedirect", typeof(int));
            }
        }
        public static bool EnableNacc 
        {
            get
            {
                return (bool)new System.Configuration.AppSettingsReader().GetValue("EnableNacc", typeof(bool));
            }
        }
        public static bool EnableIbi
        {
            get
            {
                return (bool)new System.Configuration.AppSettingsReader().GetValue("EnableIbi", typeof(bool));
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
        public static int MixPromisse
        {
            get
            {
                return (int)new System.Configuration.AppSettingsReader().GetValue("MIX_PROMISSE_AGREEMENT", typeof(int));
            }
        }

    }
}
