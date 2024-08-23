using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMC.WebSite.FIS.Utils
{
    public class CET
    {
        public static decimal CalcularCet(
           decimal valorFinanciado,
           decimal valorParcela,
           int prazo,
           DateTime dataContrato,
           DateTime dataPrimeiroVencimento)
        {
            decimal valorMaximoCet = Convert.ToDecimal(100000.00);
            decimal precisaoCalculo = Convert.ToDecimal(0.0000001);
            decimal cet = 0;

            while (true)
            {
                DateTime c = new DateTime();
                DateTime dj = new DateTime();
                decimal total = Convert.ToDecimal(0.00);

                for (int j = 0; j < prazo; j++)
                {
                    dj = dataPrimeiroVencimento;
                    if (j != 0)
                    {
                        c = dataPrimeiroVencimento;
                        dj = c.AddMonths(j);
                    }
                    decimal days = (dj - dataContrato).Days;
                    total += Convert.ToDecimal(Math.Round(Convert.ToDouble(valorParcela) / Math.Pow(Convert.ToDouble(1 + cet), Convert.ToDouble(days / 365)), 4));
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
