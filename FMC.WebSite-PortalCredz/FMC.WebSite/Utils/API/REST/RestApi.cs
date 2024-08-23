using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public class RestAPI
{
    public static Resp Get<Resp>(string api, string routeMethod, Dictionary<string, object> param = null, string accessToken = "")
    {
        try
        {
            var url = api + "/" + routeMethod;
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest(Method.GET);

            Disable_CertificateValidation();

            if (!string.IsNullOrEmpty(accessToken))
                request.AddHeader("Authorization", string.Format("Bearer {0}", accessToken));
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            if (param != null)
                foreach (var item in param)
                    request.AddParameter(item.Key, item.Value, ParameterType.QueryString);

            var response = client.Execute<Resp>(request);

            if (response.StatusCode == HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<Resp>(response.Content);
            else
                throw new Exception("Falha no metodo GET " + response.StatusCode.ToString() + " " + response.Content);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static Resp Get<Resp>(string api, string routeMethod, IDictionary<string, string> headers, IDictionary<string, string> cookies, IDictionary<string, object> param = null, string accessToken = "")
    {
        try
        {
            var url = api + "/" + routeMethod;
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest(Method.GET);

            Disable_CertificateValidation();

            if (!string.IsNullOrEmpty(accessToken))
                request.AddHeader("Authorization", string.Format("Bearer {0}", accessToken));
            if (headers != null)
                request.AddHeaders(headers);
            if (cookies != null)
                foreach (var cookie in cookies)
                    request.AddCookie(cookie.Key, cookie.Value);

            if (param != null)
                foreach (var item in param)
                    request.AddParameter(item.Key, item.Value, ParameterType.QueryString);

            var response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<Resp>(response.Content);
            else
                throw new Exception("Falha no metodo GET " + response.StatusCode.ToString() + " " + response.Content);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static Resp Post<Resp, Req>(string api, string routeMethod, Req param, IDictionary<string, string> header, string accessToken = "", int timeout = 0)
    {
        try
        {
            string url = string.Format("{0}/{1}", api, routeMethod);
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest(Method.POST);

            Disable_CertificateValidation();

            if (!string.IsNullOrEmpty(accessToken))
                request.AddHeader("Authorization", string.Format("Bearer {0}", accessToken));
            if (header != null)
                request.AddHeaders(header);

            if (timeout > 0)
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
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
            else if (param != null && header.Values.Contains("application/json"))
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

    public static Resp Put<Resp, Req>(string api, string routeMethod, Req param, IDictionary<string, string> header, string accessToken = "", int timeout = 0)
    {
        try
        {
            string url = string.Format("{0}/{1}", api, routeMethod);
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest(Method.PUT);

            Disable_CertificateValidation();

            if (!string.IsNullOrEmpty(accessToken))
                request.AddHeader("Authorization", string.Format("Bearer {0}", accessToken));
            if (header != null)
                request.AddHeaders(header);

            if (timeout > 0)
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
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
            else if (param != null && header.Values.Contains("application/json"))
                request.AddParameter("application/json", JsonConvert.SerializeObject(param), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<Resp>(response.Content);
            else if (response.ErrorException != null)
                throw response.ErrorException;
            else
                throw new Exception("Falha no metodo PUT " + response.StatusCode.ToString() + " " + response.Content);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static IList<RestResponseCookie> PostRetCookie<Resp, Req>(string api, string routeMethod, Req param, IDictionary<string, string> header, string accessToken = "", int timeout = 0)
    {
        try
        {
            string url = string.Format("{0}/{1}/", api, routeMethod);
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest(Method.POST);

            Disable_CertificateValidation();

            if (!string.IsNullOrEmpty(accessToken))
                request.AddHeader("Authorization", string.Format("Bearer {0}", accessToken));
            if (header != null)
                request.AddHeaders(header);

            if (timeout > 0)
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
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
            else if (param != null && header.Values.Contains("application/json"))
                request.AddParameter("application/json", JsonConvert.SerializeObject(param), ParameterType.RequestBody);


            Disable_CertificateValidation();

            IRestResponse response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return response.Cookies;
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


    public static Resp Post<Resp, Req>(string api, string routeMethod, Req param, string accessToken = "", int timeout = 0)
    {
        try
        {
            string url = string.Format("{0}/{1}/", api, routeMethod);
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest(Method.POST);

            Disable_CertificateValidation();

            if (!string.IsNullOrEmpty(accessToken))
                request.AddHeader("Authorization", string.Format("Bearer {0}", accessToken));
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
                throw new Exception("Falha no metodo POST " + response.StatusCode.ToString() + " " + response.Content);
        }
        catch (Exception ex)
        {
            throw ex;

        }
    }

    public Resp Put<Resp, Req>(string api, string routeMethod, Req param, string accessToken = "", int timeout = 0)
    {
        try
        {
            string url = string.Format("{0}/{1}/", api, routeMethod);
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest(Method.PUT);
            if (!string.IsNullOrEmpty(accessToken))
                request.AddHeader("Authorization", string.Format("Bearer {0}", accessToken));
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
                throw new Exception("Falha no metodo PUT " + response.StatusCode.ToString() + " " + response.Content);
        }
        catch (Exception ex)
        {
            throw ex;

        }
    }

    public Resp Delete<Resp, Req>(string api, string routeMethod, Req param, string accessToken = "", int timeout = 0)
    {
        try
        {
            string url = string.Format("{0}/{1}/", api, routeMethod);
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest(Method.DELETE);
            if (!string.IsNullOrEmpty(accessToken))
                request.AddHeader("Authorization", string.Format("Bearer {0}", accessToken));
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
                throw new Exception("Falha no metodo DELETE " + response.StatusCode.ToString() + " " + response.Content);
        }
        catch (Exception ex)
        {
            throw ex;
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

}