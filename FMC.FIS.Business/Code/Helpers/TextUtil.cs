using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Net.Mail;
using System.Security.Cryptography;

public class TextUtil
{
    public static string GeneratePasswordAleatory()
    {
        Random rand = new System.Random();
        char[] vetCaracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
        string password = "";
        do
        {
            password = "";
            for (int i = 0; i < 8; i++)
            {
                password += vetCaracteres[rand.Next(vetCaracteres.Length)];
            }
        }
        while (password.Where(param => char.IsLetter(param)).Count() <= 0);
        return password;
    }

    public static string GenerateCriptografy(string value)
    {
        byte[] valorBytes = System.Text.Encoding.Unicode.GetBytes(value);

        SHA256Managed sha = new SHA256Managed();
        valorBytes = sha.ComputeHash(valorBytes, 0, valorBytes.Length);

        return System.Text.Encoding.ASCII.GetString(valorBytes);
    }

    public static string FormatCPF(string cpf)
    {
        if (!string.IsNullOrWhiteSpace(cpf))
            return cpf.Insert(3, ".").Insert(7, ".").Insert(11, "-");
        else
            return string.Empty;
    }

    public static string FormatZipCode(string zipCode)
    {
        if (!string.IsNullOrWhiteSpace(zipCode))
            return zipCode.Insert(5, "-");
        else
            return string.Empty;
    }

    public static string FormatCNPJ(string cnpj)
    {
        if (!string.IsNullOrWhiteSpace(cnpj))
        {
            cnpj = cnpj.PadLeft(14, '0');
            return cnpj.Insert(2, ".").Insert(6, ".").Insert(10, "/").Insert(15, "-");
        }
        else
            return string.Empty;
    }

    public static string FormatPhone(string phone)
    {
        if (!string.IsNullOrWhiteSpace(phone))
        {
            if (phone.Length == 10)
                return phone.Insert(0, "(").Insert(3, ")").Insert(8, "-");
            else if (phone.Length >= 11)
                return phone.Insert(0, "(").Insert(3, ")").Insert(9, "-");
            else
                return "(00)0000-0000";
        }
        else
            return string.Empty;

    }

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
    public static string RemovePhoneMask(string phone)
    {
        return phone.Replace(" ", string.Empty)
                       .Replace("(", string.Empty)
                       .Replace(")", string.Empty)
                       .Replace("-", string.Empty);
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

    public static bool IsCnpj(string cnpj)
    {
        int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int soma;
        int resto;
        string digito;
        string tempCnpj;
        cnpj = cnpj.Trim();
        cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
        if (cnpj.Length != 14)
            return false;
        tempCnpj = cnpj.Substring(0, 12);
        soma = 0;
        for (int i = 0; i < 12; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
        resto = (soma % 11);
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;
        digito = resto.ToString();
        tempCnpj = tempCnpj + digito;
        soma = 0;
        for (int i = 0; i < 13; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
        resto = (soma % 11);
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;
        digito = digito + resto.ToString();
        return cnpj.EndsWith(digito);
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

    public static bool IsCep(string cep)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(cep, ("[0-9]{5}-[0-9]{3}"));
    }

    public static string GetTextNoHTML(string htmlText)
    {
        string cleanText = System.Text.RegularExpressions.Regex.Replace(htmlText, "<[^>]*>", " ");
        cleanText = cleanText.Replace("  ", " ");
        cleanText = cleanText.Replace("\n", " ");
        cleanText = cleanText.Replace("\r", " ");

        cleanText = cleanText.Replace("&aacute;", "á");
        cleanText = cleanText.Replace("&acirc;", "â");
        cleanText = cleanText.Replace("&agrave;", "à");
        cleanText = cleanText.Replace("&atilde;", "ã");
        cleanText = cleanText.Replace("&ccedil;", "ç");
        cleanText = cleanText.Replace("&eacute;", "é");
        cleanText = cleanText.Replace("&ecirc;", "ê");
        cleanText = cleanText.Replace("&iacute;", "í");
        cleanText = cleanText.Replace("&oacute;", "ó");
        cleanText = cleanText.Replace("&ocirc;", "ô");
        cleanText = cleanText.Replace("&otilde;", "õ");
        cleanText = cleanText.Replace("&uacute;", "ú");
        cleanText = cleanText.Replace("&uuml;", "ü");
        cleanText = cleanText.Replace("&Aacute;", "Á");
        cleanText = cleanText.Replace("&Acirc;", "Â");
        cleanText = cleanText.Replace("&Agrave;", "À");
        cleanText = cleanText.Replace("&Atilde;", "Ã");
        cleanText = cleanText.Replace("&Ccedil;", "Ç");
        cleanText = cleanText.Replace("&Eacute;", "É");
        cleanText = cleanText.Replace("&Ecirc;", "Ê");
        cleanText = cleanText.Replace("&Iacute;", "Í");
        cleanText = cleanText.Replace("&Oacute;", "Ó");
        cleanText = cleanText.Replace("&Ocirc;", "Ô");
        cleanText = cleanText.Replace("&Otilde;", "Õ");
        cleanText = cleanText.Replace("&Uacute;", "Ú");
        cleanText = cleanText.Replace("&Uuml;", "Ü");
        cleanText = cleanText.Replace("\"", "&quot;");
        cleanText = cleanText.Replace("&nbsp;", " ");
        cleanText = cleanText.Replace(";", ".");

        return cleanText;
    }


    public static string RemoveSpecialCaracteres(string input)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;
        else
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("iso-8859-8").GetBytes(input);
            return System.Text.Encoding.UTF8.GetString(bytes);
        }
    }

    public static string GetOnlyNumber(string input)
    {
        if (!string.IsNullOrEmpty(input))
            return String.Join("", System.Text.RegularExpressions.Regex.Split(input, @"[^\d]"));
        else
            return "";
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
}
