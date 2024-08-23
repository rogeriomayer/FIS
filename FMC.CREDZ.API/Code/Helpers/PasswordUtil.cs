using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FMC.CREDZ.API.Code.Helpers
{
    public class PasswordUtil
    {
        public enum PassStrong
        {
            unaccept,
            weak,
            accept,
            strong,
            safe
        }

        public static int GetPassPoint(string pass)
        {
            if (pass == null) return 0;
            int pontosPorTamanho = GetLengthPoint(pass);
            int pontosPorMinusculas = GetLowerCasePoint(pass);
            int pontosPorMaiusculas = GetUpperCasePoint(pass);
            int pontosPorDigitos = GetDigitPoint(pass);
            int pontosPorSimbolos = GetCaracterPoint(pass);
            int pontosPorRepeticao = GetRepeatPoint(pass);
            return pontosPorTamanho + pontosPorMinusculas + pontosPorMaiusculas + pontosPorDigitos + pontosPorSimbolos - pontosPorRepeticao;
        }

        private static int GetLengthPoint(string pass)
        {
            return Math.Min(10, pass.Length) * 7;
        }

        private static int GetLowerCasePoint(string pass)
        {
            int rawplacar = pass.Length - Regex.Replace(pass, "[a-z]", "").Length;
            return Math.Min(2, rawplacar) * 5;
        }

        private static int GetUpperCasePoint(string pass)
        {
            int rawplacar = pass.Length - Regex.Replace(pass, "[A-Z]", "").Length;
            return Math.Min(2, rawplacar) * 5;
        }

        private static int GetDigitPoint(string pass)
        {
            int rawplacar = pass.Length - Regex.Replace(pass, "[0-9]", "").Length;
            return Math.Min(2, rawplacar) * 6;
        }

        private static int GetCaracterPoint(string pass)
        {
            int rawplacar = Regex.Replace(pass, "[a-zA-Z0-9]", "").Length;
            return Math.Min(2, rawplacar) * 5;
        }

        private static int GetRepeatPoint(string pass)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"(\w)*.*\1");
            bool repete = regex.IsMatch(pass);
            if (repete)
            {
                return 30;
            }
            else
            {
                return 0;
            }
        }


        public static PassStrong GetPassStrong(string pass)
        {
            int placar = GetPassPoint(pass);

            if (placar < 50)
                return PassStrong.unaccept;
            else if (placar < 60)
                return PassStrong.weak;
            else if (placar < 80)
                return PassStrong.accept;
            else if (placar < 100)
                return PassStrong.strong;
            else
                return PassStrong.safe;
        }
    }
}
