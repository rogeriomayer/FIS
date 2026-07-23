using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Xml;

public class RestApi
{
    public static string PostHttpClients()
    {
        HttpClientHandler clientHandler = new HttpClientHandler();
        clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        clientHandler.SslProtocols = System.Security.Authentication.SslProtocols.Tls12 | System.Security.Authentication.SslProtocols.Tls13 | System.Security.Authentication.SslProtocols.Tls11;
        clientHandler.UseDefaultCredentials = true;

        HttpClient client = new HttpClient(clientHandler);

        var request = new HttpRequestMessage(HttpMethod.Post, "https://smartsms.bvtelecom.com.br/webhook/api/delivery/single-sms");
        request.Headers.Add("ApiKey", "522f56e0-a4dc-4a44-b78a-bb0bbd773368");
        var content = new StringContent("{\"celular\":\"34996780810\",\"mensagem\":\"teste credz\"}\r\n   ", null, "application/json");
        request.Content = content;
        var response = client.SendAsync(request).GetAwaiter().GetResult();

        return response.Content.ToString();

    }

    public static Resp GetHttpClients<Resp>(string uri, Dictionary<string, string> parameters = null)
    {
        try
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            clientHandler.SslProtocols = System.Security.Authentication.SslProtocols.Tls12 | System.Security.Authentication.SslProtocols.Tls13 | System.Security.Authentication.SslProtocols.Tls11;
            clientHandler.UseDefaultCredentials = true;

            Disable_CertificateValidation();

            HttpClient client = new HttpClient(clientHandler);

            var builder = new UriBuilder(uri);
            builder.Port = -1;
            var query = HttpUtility.ParseQueryString(builder.Query);
            if (parameters != null)
                foreach (var param in parameters)
                    query[param.Key] = param.Value;

            builder.Query = query.ToString();
            string url = builder.ToString();

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var response = client.SendAsync(request).GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<Resp>(response.Content.ToString());
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
    public static Resp Get<Resp>(string api, string routeMethod, Dictionary<string, object> param = null, Dictionary<string, string> headers = null)
    {
        try
        {
            Disable_CertificateValidation();

            string url = string.Format("{0}/{1}", api, routeMethod);
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");

            if (headers != null)
                foreach (var item in headers)
                    request.AddHeader(item.Key, item.Value);

            if (param != null)
                foreach (var item in param)
                    request.AddParameter(item.Key, item.Value, ParameterType.QueryString);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<Resp>(response.Content);
            else
                throw new Exception(response.Content);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static XmlDocument GetXML(string url)
    {
        try
        {
            Disable_CertificateValidation();

            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(response.Content);
                return xmlDocument;
            }
            else
                throw new Exception(response.Content);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static byte[] Download(string api, string routeMethod, Dictionary<string, object> param = null, Dictionary<string, string> headers = null)
    {
        try
        {
            string url = string.Format("{0}/{1}", api, routeMethod);
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");

            if (headers != null)
                foreach (var item in headers)
                    request.AddHeader(item.Key, item.Value);

            if (param != null)
                foreach (var item in param)
                    request.AddParameter(item.Key, item.Value, ParameterType.QueryString);
            return client.DownloadData(request);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public static Resp Post<Resp, Req>(string api, string routeMethod, Req param, string token, int timeout = 0)
    {
        try
        {
            string url = string.Format("{0}/{1}", api, routeMethod);
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest(Method.POST);
            if (!string.IsNullOrEmpty(token))
                request.AddHeader("Authorization", string.Format("Bearer {0}", token));
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            if (timeout > 0)
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
                request.Timeout = timeout;
            }
            if (param != null)
                request.AddParameter("application/json", JsonConvert.SerializeObject(param), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<Resp>(response.Content);
            else
                throw new Exception(response.Content);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static Resp Post<Resp, Req>(string api, string routeMethod, string param, IDictionary<string, string> headers, int timeout = 0)
    {
        try
        {
            string url = string.Format("{0}/{1}", api, routeMethod);
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest(Method.POST);

            Disable_CertificateValidation();

            if (headers != null && headers.Count > 0)
                foreach (var header in headers)
                    request.AddHeader(header.Key, header.Value);

            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; //| SecurityProtocolType.Tls11 | SecurityProtocolType.Tls13;

            if (timeout > 0)
            {
                request.Timeout = timeout;
            }
            if (!string.IsNullOrEmpty(param))
                request.AddParameter("application/json", param, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<Resp>(response.Content);
            else
                throw new Exception(response.ErrorException + " - " + response.ErrorMessage + " - " + response.Content);
        }
        catch (Exception ex)
        {
            throw ex;

        }
    }

    public static Resp Put<Resp, Req>(string api, string routeMethod, Req param, int timeout = 0)
    {
        try
        {
            string url = string.Format("{0}/{1}", api, routeMethod);
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest(Method.PUT);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            if (timeout > 0)
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
                request.Timeout = timeout;
            }
            if (param != null)
                request.AddParameter("application/json", JsonConvert.SerializeObject(param), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<Resp>(response.Content);
            else
                throw new Exception(response.Content);
        }
        catch (Exception ex)
        {
            throw ex;

        }
    }

    public static Resp Put<Resp, Req>(string api, string routeMethod, Req param, IDictionary<string, string> headers, int timeout = 0)
    {
        try
        {
            string url = string.Format("{0}/{1}", api, routeMethod);
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest(Method.PUT);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            if (timeout > 0)
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
                request.Timeout = timeout;
            }

            if (headers != null && headers.Count > 0)
                foreach (var header in headers)
                    request.AddHeader(header.Key, header.Value);

            if (param != null)
                request.AddParameter("application/json", JsonConvert.SerializeObject(param), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<Resp>(response.Content);
            else
                throw new Exception(response.Content);
        }
        catch (Exception ex)
        {
            throw ex;

        }
    }

    public static Resp Post<Resp, Req>(string api, string routeMethod, Req param, IDictionary<string, string> header, string accessToken, string authorizationType, int timeout = 0)
    {
        try
        {
            Disable_CertificateValidation();
            //Disable_Certificate();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            string url = string.Format("{0}/{1}", api, routeMethod);
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest(Method.POST);


            if (!string.IsNullOrEmpty(accessToken))
                request.AddHeader("Authorization", string.Format("{0} {1}", authorizationType, accessToken));
            if (header != null)
                request.AddHeaders(header);


            if (timeout > 0)
            {
                request.Timeout = timeout;
            }

            if (param != null && param.GetType().ToString().Contains("Dictionary") &&
                    header.Keys.Contains("Content-Type") && header.Values.Contains("application/x-www-form-urlencoded"))
            {
                string urlencoded = $"";
                foreach (var item in (IEnumerable<KeyValuePair<string, string>>)param)
                    urlencoded += item.Key + "=" + item.Value + "&";
                urlencoded = urlencoded.Remove(urlencoded.Length - 1, 1);
                request.AddParameter("application/x-www-form-urlencoded", urlencoded, ParameterType.RequestBody);
            }
            else if (param != null)
                request.AddParameter("application/json", JsonConvert.SerializeObject(param), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<Resp>(response.Content);
            else if (response.ErrorException != null)
                throw response.ErrorException;
            else
                throw new Exception("Falha no metodo POST " + response.StatusCode.ToString() + " " + response.Content);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public Resp Delete<Resp, Req>(string api, string routeMethod, Req param, int timeout = 0)
    {
        try
        {
            string url = string.Format("{0}/{1}", api, routeMethod);
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest(Method.DELETE);
            //request.AddHeader("Authorization", string.Format("Bearer {0}", HttpContextAccessor.HttpContext.User.GetClaimValue(ClaimTypes.Authentication)));
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            if (timeout > 0)
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
                request.Timeout = timeout;
            }
            if (param != null)
                request.AddParameter("application/json", JsonConvert.SerializeObject(param), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<Resp>(response.Content);
            else
                throw new Exception(response.Content);
        }
        catch (Exception ex)
        {
            throw ex;
        }
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

    private static void Disable_Certificate()
    {
        ServicePointManager.ServerCertificateValidationCallback
        += new RemoteCertificateValidationCallback(AllwaysGoodCertificate);
    }

    private static bool AllwaysGoodCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors policyErrors)
    {

        return true;
    }
}
