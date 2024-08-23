using Newtonsoft.Json;
using ServiceStack;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Xml;

namespace FMC.Fis.Utils
{
    public class HttpHelper
    {
        internal static T GET<T>(string uri)
        {
            try
            {
                Disable_CertificateValidation();

                var responseText = uri.GetJsonFromUrl();
                var obj = GetObjectFromString<T>(responseText);
                return obj;

                //var httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
                //httpWebRequest.ContentType = "text/json";
                //httpWebRequest.Accept = "application/json";
                ////httpWebRequest.ContentType = "application/json";
                //httpWebRequest.Method = "GET";

                //httpWebRequest.Timeout = 1000 * 60 * 2;

                //var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                //using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                //{
                //    var responseText = streamReader.ReadToEnd();
                //    return GetObjectFromString<T>(responseText);
                //    //JsonConvert.DeserializeObject<T>(responseText);
                //}
            }
            catch (Exception e)
            {
                throw e;
            }
        }



        internal static T POST<T, O>(string uri, O obj)
        {
            //try
            //{
            //    return GetObjectFromString<T>(uri.PostJsonToUrl(obj));
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}

            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
                httpWebRequest.ContentType = "text/json";
                httpWebRequest.Accept = "application/json";
                //httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                httpWebRequest.Timeout = 1000 * 60 * 2;

                Disable_CertificateValidation();

                string json = GetJSONFromObject(obj);

                if (!string.IsNullOrEmpty(json))
                {
                    if (string.IsNullOrEmpty(json)) json = "";
                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        streamWriter.Write(json);
                    }
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var responseText = streamReader.ReadToEnd();
                    return GetObjectFromString<T>(responseText);
                    //JsonConvert.DeserializeObject<T>(responseText);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void Disable_Certificate()
        {
            ServicePointManager.ServerCertificateValidationCallback
            += new RemoteCertificateValidationCallback(AllwaysGoodCertificate);
        }

        private static bool AllwaysGoodCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors policyErrors)
        {

            return true;
        }

        public static void Disable_CertificateValidation()
        {

            ServicePointManager.ServerCertificateValidationCallback =
                delegate (
                    object s,
                    X509Certificate certificate,
                    X509Chain chain,
                    SslPolicyErrors sslPolicyErrors
                )
                {
                    return true;
                };
        }


        /*
        internal static async Task<T> Post<T>(string uri, string json, string token)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    if (!string.IsNullOrEmpty(token))
                        httpClient.DefaultRequestHeaders.Add("Token", token);

                    if (json == null) json = "";

                    var contentString = new StringContent(json, Encoding.UTF8, "application/json");


                    using (var response = httpClient.PostAsync(uri, contentString))
                    {
                        Log.SaveFile(response.Status.ToString());
                        do
                        {
                            if (response.IsCompleted)
                            {
                                string apiResponse = await response.Result.Content.ReadAsStringAsync();
                                return GetObjectFromString<T>(apiResponse);
                            }
                        } while (true);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal static T Put<T>(string uri, string json, string token)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    if (!string.IsNullOrEmpty(token))
                        httpClient.DefaultRequestHeaders.Add("Token", token);

                    if (json == null) json = "";

                    var contentString = new StringContent(json, Encoding.UTF8, "application/json");

                    using (var response = httpClient.PutAsync(uri, contentString).GetAwaiter().GetResult())
                    {
                        string apiResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        return GetObjectFromString<T>(apiResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        internal static T Get<T>(string uri, string token)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    if (!string.IsNullOrEmpty(token))
                        httpClient.DefaultRequestHeaders.Add("Token", token);

                    using (var response = httpClient.GetAsync(uri).GetAwaiter().GetResult())
                    {
                        string apiResponse = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        return GetObjectFromString<T>(apiResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        */
        public static T GetObjectFromString<T>(object obj)
        {
            if (obj.ToString().Contains("erro"))
            {
                var exception = JsonConvert.DeserializeObject<object>(obj.ToString());
                throw new Exception(exception.ToString());
            }
            else
            {
                /*    if (typeof(T).ToString() == "System.String")
                        return (T)obj;
                    else*/
                return JsonConvert.DeserializeObject<T>(obj.ToString());
            }
        }

        public static T GetObjectFromString<T>(string obj)
        {
            //if (obj.ToString().Contains("erro"))
            //{
            //    var exception = JsonConvert.DeserializeObject<object>(obj);
            //    throw new Exception(exception.ToString());
            //}
            //else
            //{
            /*    if (typeof(T).ToString() == "System.String")
                    return (T)obj;
                else*/
            return JsonConvert.DeserializeObject<T>(obj);

            //}
        }

        internal static string GetJSONFromObject<T>(T obj)
        {
            string json = JsonConvert.SerializeObject(obj,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore,
                                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                            });

            return json;
        }
        enum MyEnum
        {

        }
        public enum TypeSubmit
        {
            Json,
            Xml
        }
    }
}
