
using FMC.Solutions.NPPLUS.Library.Class.ViaCEP.Exceptions;
using FMC.Solutions.NPPLUS.Library.Class.ViaCEP.Service;
using FMC.Solutions.NPPLUS.Library.Class.ViaCEP.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMC.Solutions.NPPLUS.Library.Class.ViaCEP
{
    public class Search
    {
        /// <summary>
        /// Search address by Zip Code
        /// </summary>
        /// <param name="zipCode">Zip code value</param>
        /// <param name="type">The type to search address. Use ViaCEPTypes object to help. Possible values include: 'json', 'xml', 'piped' and 'querty'</param>
        /// <returns>String with result in type selected</returns>
        /// 
        public static string ByZipCode(int zipCode, string type)
        {
            try
            {
                var result = ViaCEPServices.GetAddressByCEP(zipCode, type);
                return result;
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

        /// <summary>
        /// Search address by Zip Code
        /// </summary>
        /// <param name="zipCode">Zip code value</param>
        /// <returns>Object with address result</returns>
        public static Model.ViaCEP ByZipCode(int zipCode)
        {
            try
            {
                var jsonResult = ViaCEPServices.GetAddressByCEP(zipCode, ViaCEPTypes.Json);
                var objectResult = JsonConvert.DeserializeObject<Model.ViaCEP>(jsonResult);
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


        /// <summary>
        /// Search address by Zip Code
        /// </summary>
        /// <param name="state">State</param>
        /// <param name="city">City</param>
        /// <param name="address">Address</param>
        /// <param name="type">The type to search address. Use ViaCEPTypes object to help. Possible values include: 'json', 'xml', 'piped' and 'querty'</param>
        /// <returns>String with result in type selected</returns>
        /// 
        public static string ByAddress(string state, string city, string address, string type)
        {
            try
            {
                var result = ViaCEPServices.GetByAddress(state, city, address, type);
                return result;
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

        /// <summary>
        /// Search address by Zip Code
        /// </summary>
        /// <param name="state">State</param>
        /// <param name="city">City</param>
        /// <param name="address">Address</param>
        /// <returns>Object with address result</returns>
        /// 
        public static ICollection<Model.ViaCEP> ByAddress(string state, string city, string address)
        {
            try
            {
                var jsonResult = ViaCEPServices.GetByAddress(Util.RemoverAcentos(state), Util.RemoverAcentos(city), Util.RemoverAcentos(address), ViaCEPTypes.Json);
                ICollection<Model.ViaCEP> objectResult = JsonConvert.DeserializeObject<ICollection<Model.ViaCEP>>(jsonResult);
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
