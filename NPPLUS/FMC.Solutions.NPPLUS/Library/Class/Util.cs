using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

    public class Util
    {
        public static bool IsNumeric(string s)
        {
            float output;
            return float.TryParse(s, out output);
        }

        public static bool IsDate(string s)
        {
            DateTime output;
            return DateTime.TryParse(s, out output);
        }

        public static string GetLocalIp
        {
            get
            {
                string host = Dns.GetHostName();
                IPAddress[] ip = Dns.GetHostAddresses(host);
                return ip.LastOrDefault().ToString();
            }
        }

        public static string MaskCard(string card)
        {
            if (card.StartsWith("000"))
                return card.Substring(3, 6) + "XXXXXXXXXX";
            else
                return card.Substring(0, 6) + "XXXXXXXXXX";
        }

        public static bool IsCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
                return false;

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCpf = tempCpf + digito;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

        public static bool IsEmail(string email)
        {
            try
            {
                MailAddress ma = new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsTelefone(string phone)
        {
            try
            {
                if (Regex.IsMatch(phone, @"^\(?\d{2}\)?[\s-]?[\s9]?\d{4}-?\d{4}$"))
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static ICollection<string> Split(string str, int chunkSize)
        {
            decimal total = Math.Ceiling(Convert.ToDecimal(Convert.ToDecimal(str.Length) / Convert.ToDecimal(chunkSize)));
            ICollection<string> list = new List<string>();

            for (int i = 0; i < total; i++)
            {
                int end = 0;
                if ((i * chunkSize) + chunkSize > str.Length)
                    end = str.Length - (i * chunkSize);
                else
                    end = chunkSize;

                list.Add(Util.RemoverAcentos(str.Substring(i * chunkSize, end)));
            }

            return list;
        }




        public string str = "";
        public void ConvertMoney(System.Windows.Forms.TextBox textBox, System.Windows.Forms.KeyEventArgs keyEventArgs)
        {
            char KeyCode = GetChar(keyEventArgs);

            if (!IsNumeric(KeyCode.ToString())
                && keyEventArgs.KeyValue != 46 && keyEventArgs.KeyValue != 8)
            {
                keyEventArgs.Handled = true;
                return;
            }
            else
            {
                keyEventArgs.Handled = true;
            }
            if (((KeyCode == 8) || (KeyCode == 46)) && (str.Length > 0))
            {
                str = str.Substring(0, str.Length - 1);
            }
            else if (!((KeyCode == 8) || (KeyCode == 46)))
            {
                str = str + Convert.ToChar(KeyCode);
            }
            if (str.Length == 0)
            {
                textBox.Text = "";
            }
            if (str.Length == 1)
            {
                textBox.Text = "0,0" + str;
            }
            else if (str.Length == 2)
            {
                textBox.Text = "0," + str;
            }
            else if (str.Length > 2)
            {
                textBox.Text = str.Substring(0, str.Length - 2) + "," + str.Substring(str.Length - 2);
            }
        }

        static char GetChar(System.Windows.Forms.KeyEventArgs e)
        {
            int keyValue = e.KeyValue;

            if (!e.Shift && keyValue >= (int)Keys.A && keyValue <= (int)Keys.Z)
                return (char)(keyValue + 32);

            switch (e.KeyData)
            {

                case Keys.NumPad0:

                case Keys.NumPad1:

                case Keys.NumPad2:

                case Keys.NumPad3:

                case Keys.NumPad4:

                case Keys.NumPad5:

                case Keys.NumPad6:

                case Keys.NumPad7:

                case Keys.NumPad8:

                case Keys.NumPad9:
                    {

                        // Calcula o caracter correto

                        keyValue = (char)(e.KeyValue - 48);

                        break;

                    }
            }
            return (char)keyValue;
        }

        public static string RemoverCharacter(string str)
        {
            /** Troca os caracteres especiais da string por "" **/
            string[] caracteresEspeciais = { "\\.", ",", "-", ":", "\\(", "\\)", "ª", "\\|", "\\\\", "°", "º", "ª", "?", "!", "#", "$", "%", "*", "\\n", "\\r" };

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

        public static Boolean SendMailSMTP(MailMessage message, string smtpServer, int smtpPort, string userSendMail, string passSendMail)
        {
            SmtpClient client = new SmtpClient();

            client.Port = smtpPort;
            client.Host = smtpServer;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(userSendMail, passSendMail);
            client.Send(message);
            //sGravaLog(enmLogType.enmInfo, "Boleto enviado", False)

            return true;

        }

        public static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return origin.AddSeconds(timestamp);
        }

        public static double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }

        public static decimal GetDecimal(string valor, string nomeParametro)
        {
            if (valor.Contains(".") || valor.Contains(","))
            {
                //var vlEntrada = valorEntrada.Replace(",", "").Replace(".", "");
                var vlEntrada = string.Empty;

                for (int i = valor.Length - 1; i >= 0; i--)
                {
                    if (valor[i] == '.' || valor[i] == ',')
                    {
                        if (vlEntrada.Length < 2)
                            vlEntrada = vlEntrada + "0";
                    }
                    else
                        vlEntrada = valor[i] + vlEntrada;
                }

                if (!IsNumeric(vlEntrada))
                    throw new Exception("PARAMETRO " + nomeParametro + " DEVE SER NUMERICO!");
                else
                    return Math.Round(Convert.ToDecimal(vlEntrada) / 100, 2);
            }
            else
            {
                if (!IsNumeric(valor))
                    throw new Exception("PARAMETRO " + nomeParametro + " DEVE SER NUMERICO!");
                else
                    return Math.Round(Convert.ToDecimal(valor));
            }
        }

        public static string FormatValue(string value)
        {
            if (value.Contains("."))
            {
                string formatedValue = string.Empty;
                string dec = value.Split('.').LastOrDefault();
                string integer = value.Split('.').FirstOrDefault();
                if (dec.Length == 1)
                    formatedValue = integer + "," + dec + "0";
                else if (dec.Length >= 2)
                    formatedValue = integer + "," + dec.Substring(0, 2);
                else
                    formatedValue = integer;

                return formatedValue;
            }
            else if (value.Contains(","))
            {
                if (value.Split(',').LastOrDefault().ToString().Length == 2)
                    return value;
                else if (string.IsNullOrEmpty(value) || !IsNumeric(value))
                    return "0,00";
                else
                    return value + ",00";
            }
            else
            {
                if (string.IsNullOrEmpty(value) || !IsNumeric(value))
                    return "0,00";
                else
                    return value + ",00";
            }

        }

        //private static string pathLog = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LOG", Environment.UserName);
        private static string pathLog = Path.Combine(@"c:/NTU", "LOG", Environment.UserName);

        public static void GravaArquivoLog(string message, bool save = false)
        {
            if (new System.Configuration.AppSettingsReader().GetValue("VERSION_TYPE", typeof(string)).ToString() != "HOMEOFFICE")
            {
                string fileName = pathLog + "\\LOG_" + DateTime.Now.ToString("ddMMyyyyHH") + ".log";
                StreamWriter logExecution = null;
                try
                {
                    if (!Directory.Exists(pathLog))
                        Directory.CreateDirectory(pathLog);

                    using (logExecution = new StreamWriter(fileName, true, ASCIIEncoding.Default))
                    {
                        logExecution.WriteLine(Environment.NewLine + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " => " + message);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }