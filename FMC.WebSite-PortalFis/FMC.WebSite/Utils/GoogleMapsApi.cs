using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FMC.WebSite.FIS.Utils
{
    public class GoogleMapsApi
    {
        public static EnderecoGoogleMaps GetEnderecoByCep(string cep)
        {
            try
            {
                string url = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?key={0}&address={1}&sensor=false&language=pt", AppSettings.KeyGoogleMaps, cep.Trim());
                WebRequest request = WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    System.IO.Stream responseStream = response.GetResponseStream();

                    XDocument xd = XDocument.Load(responseStream);

                    string latitude = xd.Descendants("result").Descendants("geometry").Descendants("location").Elements("lat").FirstOrDefault().Value;
                    string longitude = xd.Descendants("result").Descendants("geometry").Descendants("location").Elements("lng").FirstOrDefault().Value;

                    string url2 = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?key={0}&latlng={1},{2}&sensor=true&language=pt", AppSettings.KeyGoogleMaps, latitude, longitude);
                    WebRequest request2 = WebRequest.Create(url2);
                    HttpWebResponse response2 = (HttpWebResponse)request2.GetResponse();
                    if (response2.StatusCode == HttpStatusCode.OK)
                    {
                        System.IO.MemoryStream ms2 = new System.IO.MemoryStream();
                        System.IO.Stream responseStream2 = response2.GetResponseStream();
                        XDocument xdEnd = XDocument.Load(responseStream2);
                        EnderecoGoogleMaps endereco = new EnderecoGoogleMaps();

                        foreach (XElement item in xdEnd.Descendants("result").Descendants("address_component"))
                        {
                            if (string.IsNullOrEmpty(endereco.CEP))
                            {
                                if (item.Elements().Where(p => p.Value.Contains("postal_code")).Any())
                                    endereco.CEP = item.Element("short_name").Value;
                            }
                            if (string.IsNullOrEmpty(endereco.Logradouro))
                            {
                                if (item.Elements().Where(p => p.Value.Contains("route")).Any())
                                    endereco.Logradouro = item.Element("short_name").Value;
                            }
                            if (string.IsNullOrEmpty(endereco.Numero))
                            {
                                if (item.Elements().Where(p => p.Value.Contains("street_number")).Any())
                                    endereco.Numero = item.Element("short_name").Value;
                            }
                            if (string.IsNullOrEmpty(endereco.Bairro))
                            {
                                if (item.Elements().Where(p => p.Value.Contains("sublocality_level_1")).Any())
                                    endereco.Bairro = item.Element("short_name").Value;
                            }
                            if (string.IsNullOrEmpty(endereco.Cidade))
                            {
                                if (item.Elements().Where(p => p.Value.Contains("administrative_area_level_2")).Any())
                                    endereco.Cidade = item.Element("short_name").Value;
                            }
                            if (string.IsNullOrEmpty(endereco.UF))
                            {
                                if (item.Elements().Where(p => p.Value.Contains("administrative_area_level_1")).Any())
                                    endereco.UF = item.Element("short_name").Value;
                            }
                            if (string.IsNullOrEmpty(endereco.Pais))
                            {
                                if (item.Elements().Where(p => p.Value.Contains("country")).Any())
                                    endereco.Pais = item.Element("long_name").Value;
                            }
                        }
                        return endereco;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public class EnderecoGoogleMaps
        {
            public string Logradouro { get; set; }
            public string Numero { get; set; }
            public string Bairro { get; set; }
            public string Cidade { get; set; }
            public string UF { get; set; }
            public string Pais { get; set; }
            public string CEP { get; set; }
        }
    }
}
