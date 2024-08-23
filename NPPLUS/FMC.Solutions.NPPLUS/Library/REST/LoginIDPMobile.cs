using FMC.Solutions.NPPLUS.Library.REST.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FMC.Solutions.NPPLUS.Library.REST
{
    public class LoginIDPMobile
    {

        private string CONTEXT_DEVICE_CODE = "rest/1.0/device/code";
        private string CONTEXT_SESSIONS = "rest/1.0/sessions";
        private string CONTEXT_VERIFY_USER_CODE = "rest/1.0/device/verifyusercode";
        private string CONTEXT_ACCESS_TOKEN = "rest/issueToken/accesstoken";


        private string URL_IDP;
        private string CLIENT_ID = "NewCobrancaLocal";
        private string SECRET_CODE = "ae4fe7fa0a02b04e440b5db09f0a5cb7ed00";



        public IdpAccessInfo Login(String username, String password, ICollection<ParameterResponse> parameters)
        {
            URL_IDP = parameters.Where(p => p.Key == "URL_IDP").FirstOrDefault().Value;
            CLIENT_ID = parameters.Where(p => p.Key == "CLIENT_ID").FirstOrDefault().Value;
            SECRET_CODE = parameters.Where(p => p.Key == "SECRET_CODE").FirstOrDefault().Value;

            RestAPI.Disable_CertificateValidation();

            IdpAccessInfo result = new IdpAccessInfo();
            result.success = false;
            var resultCode = GetDeviceCode();
            if (resultCode != null)
            {
                Log.SaveFile("GetDeviceCode: sucesso -- " + resultCode.device_code + "  " + resultCode.user_code);
                var session = GetSession(username, password);
                if (session.Count > 0)
                {
                    Log.SaveFile("GetSession: sucesso -- " + session);

                    string validUser = ValidateUserToken(resultCode.user_code, session);
                    if (!string.IsNullOrEmpty(validUser))
                    {
                        Log.SaveFile("ValidateUserToken: sucesso -- " + validUser);

                        var accessToken = GetAccessToken(resultCode.device_code);

                        if (accessToken != null)
                        {
                            Log.SaveFile("GetAccessToken: sucesso -- " + accessToken.id_token);

                            var accessTokeDecoded = DecodeJWT(accessToken.id_token);

                            result.aud = accessTokeDecoded["aud"].ToString();
                            result.iss = accessTokeDecoded["iss"].ToString();
                            result.jti = accessTokeDecoded["jti"].ToString();
                            result.sub = accessTokeDecoded["sub"].ToString();
                            result.exp = accessTokeDecoded["exp"].ToString();
                            result.iat = accessTokeDecoded["iat"].ToString();
                            result.ProfileList = accessTokeDecoded["ProfileList"].ToString();
                            result.EmailAddress = accessTokeDecoded["EmailAddress"].ToString();
                            result.message = "Sucesso";
                            result.success = true;
                        }
                        else
                        {
                            result.message = "Nao foi possivel recuperar token";
                        }
                    }
                    else
                    {
                        result.message = "Nao foi possivel verificar codigo";
                    }
                }
                else
                {
                    result.message = "Usuario/Senha incorretos";
                }
            }
            else
            {
                result.message = "ClientID e/ou URL IDP Incorretos";
            }

            return result;
        }

        public DeviceCodeResponse GetDeviceCode()
        {
            try
            {
                var deviceCodeRequest = new Dictionary<string, string>();
                deviceCodeRequest.Add("client_id", CLIENT_ID);
                deviceCodeRequest.Add("response_type", "code");
                deviceCodeRequest.Add("scope", "openid");

                var header = new Dictionary<string, string>();
                header.Add("Content-Type", "application/x-www-form-urlencoded");
                header.Add("X-SunGard-IdP-API-Key", "SunGard-IdP-UI");
                header.Add("Accept", "application/json");

                var result = RestAPI.Post<DeviceCodeResponse, Dictionary<string, string>>(URL_IDP, CONTEXT_DEVICE_CODE, deviceCodeRequest, header);

                if (result != null)
                    return result;
                else
                    return null;

            }
            catch (Exception ex)
            {
                string erro = ex.Message;
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    erro += " -- " + ex.Message;
                }

                Log.SaveFile(erro);

                return null;
            }

        }

        public IList<RestResponseCookie> GetSession(String username, String password)
        {
            try
            {

                //var idpLogin = new IDPLogin() { loginName = "rogerio.mayer@fmcbrasil.com.br", password = "FMCbrasil@01" };

                var idpLogin = new IDPLogin() { loginName = username, password = password };

                var header = new Dictionary<string, string>();
                header.Add("Content-Type", "application/json");
                header.Add("X-SunGard-IdP-API-Key", "SunGard-IdP-UI");
                header.Add("Accept", "application/json");

                var result = RestAPI.PostRetCookie<string, IDPLogin>(URL_IDP, CONTEXT_SESSIONS, idpLogin, header);

                return result;

            }
            catch (Exception ex)
            {
                string erro = ex.Message;
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    erro += " -- " + ex.Message;
                }
                Log.SaveFile(erro);

                return null;
            }

        }

        public string ValidateUserToken(string userCode, IList<RestResponseCookie> cookies)
        {
            try
            {
                var header = new Dictionary<string, string>();
                header.Add("X-SunGard-IdP-API-Key", "SunGard-IdP-UI");

                IDictionary<string, string> dicCookies = null;

                if (cookies != null && cookies.Count > 0)
                    dicCookies = cookies.ToDictionary(p => p.Name, p => p.Value);

                var result = RestAPI.Get<string>(URL_IDP, CONTEXT_VERIFY_USER_CODE + "?user_code=" + userCode + "&is_authorized=yes&scope=openid", header, dicCookies);

                return result;
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    erro += " -- " + ex.Message;
                }

                return null;
            }

        }

        public AccessTokenResponse GetAccessToken(string deviceCode)
        {
            try
            {
                var deviceCodeRequest = new Dictionary<string, string>();
                deviceCodeRequest.Add("client_id", CLIENT_ID);
                deviceCodeRequest.Add("client_secret", SECRET_CODE);
                deviceCodeRequest.Add("code", deviceCode);
                deviceCodeRequest.Add("grant_type", @"http://oauth.net/grant_type/device/1.0");

                var header = new Dictionary<string, string>();
                header.Add("Content-Type", "application/x-www-form-urlencoded");
                header.Add("X-SunGard-IdP-API-Key", "SunGard-IdP-UI");
                header.Add("Accept", "application/json");

                var result = RestAPI.Post<AccessTokenResponse, Dictionary<string, string>>(URL_IDP, CONTEXT_ACCESS_TOKEN, deviceCodeRequest, header);

                return result;

            }
            catch (Exception ex)
            {
                string erro = ex.Message;
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    erro += " -- " + ex.Message;
                }

                return null;
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

        public IDictionary<string, object> DecodeJWT(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var decodedValue = handler.ReadJwtToken(token);
            return decodedValue.Payload.ToDictionary(p => p.Key, p => p.Value);
        }

    }
}
