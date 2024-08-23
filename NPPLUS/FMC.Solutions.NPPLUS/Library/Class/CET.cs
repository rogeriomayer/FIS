using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMC.Solutions.NPPLUS.Library.Class
{
    public class CET
    {
        public static double CalcularCet(
           double valorFinanciado,
           double valorParcela,
           int prazo,
           DateTime dataContrato,
           DateTime dataPrimeiroVencimento)
        {
            double valorMaximoCet = 100000;
            double precisaoCalculo = 0.0000001;
            double cet = 0;

            while (true)
            {
                DateTime c = new DateTime();
                DateTime dj = new DateTime();
                double total = 0.0;

                for (int j = 0; j < prazo; j++)
                {
                    dj = dataPrimeiroVencimento;
                    if (j != 0)
                    {
                        c = dataPrimeiroVencimento;
                        dj = c.AddMonths(j);
                    }
                    double days = (dj - dataContrato).Days;
                    total += Math.Round(valorParcela / Math.Pow(1 + cet, days / 365), 4);
                }

                cet += precisaoCalculo;

                if (cet >= valorMaximoCet)
                {
                    return -1;
                }
                if (total - valorFinanciado <= 0)
                {
                    break;
                }
                else
                {
                    cet *= total / valorFinanciado;
                }
            }
            return cet * 100;
        }
    }
}
