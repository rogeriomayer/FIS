using FMC.Shortener.Utils.ViaCEP.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FMC.Shortener.Utils.ViaCEP.Service
{
    public class ViaCEPServices
    {
        public static string GetAddressByCEP(int cep, string type)
        {
            try
            {
                string result = string.Empty;

                string viaCEPUrl = $"https://viacep.com.br/ws/{cep}/{type}/";
                WebClient client = new WebClient();
                client.Encoding = System.Text.Encoding.UTF8;
                result = client.DownloadString(viaCEPUrl);

                return result;
            }
            catch (Exception ex)
            {
                throw new ViaCEPException(ex.Message);
            }
        }


        public static string GetByAddress(string state, string city, string address, string type)
        {
            try
            {
                string result = string.Empty;

                string viaCEPUrl = $"https://viacep.com.br/ws/{state}/{city}/{address}/{type}/";

                WebClient client = new WebClient();
                client.Encoding = System.Text.Encoding.UTF8;
                result = client.DownloadString(viaCEPUrl);

                return result;
            }
            catch (Exception ex)
            {
                throw new ViaCEPException(ex.Message);
            }
        }

    }
}
