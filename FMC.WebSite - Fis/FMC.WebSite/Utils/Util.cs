using FMC.Fis.Models;
using FMC.Fis.Utils.ViaCEP.Exceptions;
using FMC.Fis.Utils.ViaCEP.Service;
using FMC.Fis.Utils.ViaCEP.Types;
using FMC.Fis.Utils.ViaCEP.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FMC.Fis.Utils
{
    public class Util
    {
        public static bool IsEmail(string email)
        {
            try
            {
                MailAddress mail = new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static async Task<bool> IsCaptchaValid(string response)
        {
            try
            {
                string secret = AppSettings.ReCaptchaSecretKey;
                using (var client = new HttpClient())
                {

                    var verify = await client.GetStringAsync($"{AppSettings.ReCaptchaApi}?secret={secret}&response={response}");
                    var captchaResult = JsonConvert.DeserializeObject<TokenResponseModel>(verify);

                    return captchaResult.Success
                        && captchaResult.Action == "homepage"
                        && captchaResult.Score > Convert.ToDecimal(0.5);

                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static string RemoveSpecialCharacters(string input)
        {
            Regex r = new Regex(
                          "(?:[^a-zA-Z0-9 -])",
                          RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            return r.Replace(input, String.Empty);
        }


        public static string RemoverAcentos(string str)
        {
            try
            {
                /** Troca os caracteres acentuados por não acentuados **/
                string[] acentos = new string[] { "ç", "Ç", "á", "é", "í", "ó", "ú", "ý", "Á", "É", "Í", "Ó", "Ú", "Ý", "à", "è", "ì", "ò", "ù", "À", "È", "Ì", "Ò", "Ù", "ã", "õ", "ñ", "ä", "ë", "ï", "ö", "ü", "ÿ", "Ä", "Ë", "Ï", "Ö", "Ü", "Ã", "Õ", "Ñ", "â", "ê", "î", "ô", "û", "Â", "Ê", "Î", "Ô", "Û" };
                string[] semAcento = new string[] { "c", "C", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "Y", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U", "a", "o", "n", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "A", "O", "N", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U" };

                for (int i = 0; i < acentos.Length; i++)
                {
                    str = str.Replace(acentos[i], semAcento[i]);
                }

                /** Troca os caracteres especiais da string por "" **/
                string[] caracteresEspeciais = { "\\.", ",", "-", ":", "\\(", "\\)", "ª", "\\|", "\\\\", "°", "º", "ª", "?", "!", "#", "$", "%", "*", "\\r", "\\n" };

                for (int i = 0; i < caracteresEspeciais.Length; i++)
                {
                    str = str.Replace(caracteresEspeciais[i], " ");
                }

                /** Troca os espaços no início por "" **/
                str = str.Replace("^\\s+", "");
                /** Troca os espaços no início por "" **/
                str = str.Replace("\\s+$", "");
                /** Troca os espaços duplicados, tabulações e etc por  " " **/
                str = str.Replace("\\s+", " ");

                return str;
            }
            catch (Exception ex)
            {
                return str;
            }

        }

        public static string RemoverAcentos(string str, bool specialChar)
        {

            /** Troca os caracteres acentuados por não acentuados **/
            string[] acentos = new string[] { "ç", "Ç", "á", "é", "í", "ó", "ú", "ý", "Á", "É", "Í", "Ó", "Ú", "Ý", "à", "è", "ì", "ò", "ù", "À", "È", "Ì", "Ò", "Ù", "ã", "õ", "ñ", "ä", "ë", "ï", "ö", "ü", "ÿ", "Ä", "Ë", "Ï", "Ö", "Ü", "Ã", "Õ", "Ñ", "â", "ê", "î", "ô", "û", "Â", "Ê", "Î", "Ô", "Û" };
            string[] semAcento = new string[] { "c", "C", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "Y", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U", "a", "o", "n", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "A", "O", "N", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U" };

            for (int i = 0; i < acentos.Length; i++)
            {
                str = str.Replace(acentos[i], semAcento[i]);
            }
            str = str.Replace("\\n", "");
            str = str.Replace("\\r", "");

            if (specialChar)
            {
                /** Troca os caracteres especiais da string por "" **/
                string[] caracteresEspeciais = { "\\.", ",", "-", ":", "\\(", "\\)", "ª", "\\|", "\\\\", "°", "º", "ª", "?", "!", "#", "$", "%", "*" };

                for (int i = 0; i < caracteresEspeciais.Length; i++)
                {
                    str = str.Replace(caracteresEspeciais[i], " ");
                }
            }
            /** Troca os espaços no início por "" **/
            str = str.Replace("^\\s+", "");
            /** Troca os espaços no início por "" **/
            str = str.Replace("\\s+$", "");
            /** Troca os espaços duplicados, tabulações e etc por  " " **/
            str = str.Replace("\\s+", " ");

            return str;

        }

        public static ViaCEP.Model.ViaCEP ByZipCode(int zipCode)
        {
            try
            {
                var jsonResult = ViaCEPServices.GetAddressByCEP(zipCode, ViaCEPTypes.Json);
                var objectResult = JsonConvert.DeserializeObject<ViaCEP.Model.ViaCEP>(jsonResult);
                return objectResult;
            }
            catch (ViaCEPException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new ViaCEPException(ex.Message);
            }
        }

    }


}
