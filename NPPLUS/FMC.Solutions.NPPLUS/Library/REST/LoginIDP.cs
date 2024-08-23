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
    public class LoginIDP
    {

        private static string CONTEXT_DEVICE_CODE = "rest/1.0/device/code";
        private static string CONTEXT_SESSIONS = "rest/1.0/sessions";
        private static string CONTEXT_VERIFY_USER_CODE = "rest/1.0/device/verifyusercode";
        private static string CONTEXT_ACCESS_TOKEN = "rest/1.0/accesstoken";
        private static string CHANGE_PASSWORD = "rest/1.0/users/reset-chq/";
        private static string USER_IDP = "rest/1.0/users/";

        private static string URL_IDP = "";//"https://login-uat.fisglobal.com/idp/FisBR";
        private static string CLIENT_ID = "";//"crmfmc";
        private static string SECRET_CODE = ""; // "8816ffe30f5a4040c6083db00ad51bf5335d";



        public static IdpAccessInfo Login(String username, String password, ICollection<ParameterResponse> parameters)
        {
            URL_IDP = parameters.Where(p => p.Key == "URL_IDP").FirstOrDefault().Value;
            CLIENT_ID = parameters.Where(p => p.Key == "CLIENT_ID").FirstOrDefault().Value;
            SECRET_CODE = parameters.Where(p => p.Key == "SECRET_CODE").FirstOrDefault().Value;

            RestAPI.Disable_CertificateValidation();

            IdpAccessInfo result = new IdpAccessInfo();
            result.success = false;
            Log.SaveFile("GetAccessToken");

            var accessToken = GetAccessToken(username, password);

            if (accessToken != null)
            {
                Log.SaveFile("GetAccessToken: sucesso -- " + accessToken.id_token);

                var accessTokeDecoded = DecodeJWT(accessToken.id_token);

                result.accessToken = accessToken.access_token;
                result.UID = accessTokeDecoded["UID"].ToString();
                result.aud = accessTokeDecoded["aud"].ToString();
                result.iss = accessTokeDecoded["iss"].ToString();
                result.jti = accessTokeDecoded["jti"].ToString();
                result.sub = accessTokeDecoded["sub"].ToString();
                result.exp = accessTokeDecoded["exp"].ToString();
                result.iat = accessTokeDecoded["iat"].ToString();
                result.ProfileList = accessTokeDecoded["ProfileList"].ToString();
                result.EmailAddress = accessTokeDecoded["EmailAddress"].ToString();
                result.firstName = accessTokeDecoded["firstName"].ToString();
                result.FullName = accessTokeDecoded["FullName"].ToString();
                result.message = "Sucesso";
                result.success = true;
            }
            else
            {
                result.message = "Nao foi possivel recuperar token";
            }
            //}
            //else
            //{
            //    result.message = "Nao foi possivel verificar codigo";
            //}
            //}
            //else
            //{
            //    result.message = "Usuario/Senha incorretos";
            //}
            //}
            //else
            //{
            //    result.message = "ClientID e/ou URL IDP Incorretos";
            //}

            return result;
        }

        public static UserIDP GetUserData(string uid, string accessToken)
        {
            var header = new Dictionary<string, string>();
            header.Add("Content-Type", "application/x-www-form-urlencoded");
            header.Add("X-SunGard-IdP-API-Key", "SunGard-IdP-UI");
            header.Add("Accept", "application/json");

            var result = RestAPI.Get<UserIDP>(URL_IDP, USER_IDP + uid, header, null, null, accessToken);

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

        public string ChangePassword(string accessToken, string uid, string password)
        {
            var header = new Dictionary<string, string>();
            header.Add("Content-Type", "application/json");
            header.Add("Accept ", "application/json");
            header.Add("X-SunGard-IdP-API-Key", "SunGard-IdP-UI");

            var newPassword = new NewPassword()
            {
                autoReset = false,
                enforceChangeOnNextLogon = false,
                emailNotification = false,
                passwordNeverExpires = false,
                newPassword = password,
                confirmPassword = password
            };

            var result = RestAPI.Put<string, NewPassword>(URL_IDP, CHANGE_PASSWORD + uid, newPassword, header, accessToken);

            return result;
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

        public static AccessTokenResponse GetAccessToken(string user, string pass)
        {
            try
            {
                var deviceCodeRequest = new Dictionary<string, string>();
                deviceCodeRequest.Add("client_id", CLIENT_ID);
                deviceCodeRequest.Add("client_secret", SECRET_CODE);
                deviceCodeRequest.Add("grant_type", "password");
                deviceCodeRequest.Add("username", user);
                deviceCodeRequest.Add("password", pass);

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

                Log.SaveFile(erro);

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

        public static IDictionary<string, object> DecodeJWT(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var decodedValue = handler.ReadJwtToken(token);
            return decodedValue.Payload.ToDictionary(p => p.Key, p => p.Value);
        }

    }

    public class IDPLogin
    {
        public string loginName { get; set; }
        public string password { get; set; }
    }

    public class IdpAccessInfo
    {
        public bool success { get; set; }
        public string message { get; set; }

        public string accessToken { get; set; }
        public string refreshToken { get; set; }
        public string idToken { get; set; }

        public string aud { get; set; }
        public string iss { get; set; }
        public string jti { get; set; }
        public string sub { get; set; }
        public string exp { get; set; }
        public string iat { get; set; }
        public string ProfileList { get; set; }
        public string EmailAddress { get; set; }
        public string firstName { get; set; }
        public string FullName { get; set; }

        public DateTime expiresIn { get; set; }

        public Nullable<DateTime> LastLoginDate { get; set; }
        public string UID { get; set; }

        //@Override
        //public String toString()
        //{
        //    return String.format("%s { success:%b, message:%s, accessToken:%s, refreshToken:%s, idToken:%s, expiresIn:%s }", this.getClass().getName(), success, message, accessToken, refreshToken, idToken, expiresIn);
        //}
    }

    public class NewPassword
    {
        public bool emailNotification { get; set; }

        public bool enforceChangeOnNextLogon { get; set; }

        public bool passwordNeverExpires { get; set; }

        public bool autoReset { get; set; }

        public string newPassword { get; set; }

        public string confirmPassword { get; set; }
    }


    public class UserIDP
    {
        public long id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string managerLoginName { get; set; }
        public Nullable<DateTime> createdDate { get; set; }
        public Nullable<DateTime> modifiedDate { get; set; }
        public Nullable<DateTime> loginModifiedDate { get; set; }
        public string type { get; set; }
        public string typeCode { get; set; }
        public string loginName { get; set; }
        public string password { get; set; }
        public long role { get; set; }
        public string userProfile { get; set; }
        public decimal rewardPoints { get; set; }
        public bool pwdNeverExpires { get; set; }
        public bool hardwareAuthenticator { get; set; }
        public bool optedOutMFA { get; set; }
        public int active { get; set; }
        public int autoSuspended { get; set; }
        public int adminSuspended { get; set; }
        public DateTime statusNewLoginDate { get; set; }
        public bool forcePasswordChange { get; set; }
        public bool forceLoginNameChange { get; set; }
        public Nullable<DateTime> firstLoginDate { get; set; }
        public Nullable<DateTime> lastLoginDate { get; set; }
        public Nullable<DateTime> passwordExpirationDate { get; set; }
        public Nullable<DateTime> passwordResetDate { get; set; }
        public Nullable<DateTime> statusPasswordChangeDate { get; set; }
        public bool otPViolation { get; set; }
        public bool passwordViolation { get; set; }
        public bool accountLockViolation { get; set; }
        public bool challengeViolation { get; set; }
        public bool mobileBiometricsViolation { get; set; }
        public int nbFailedLogin { get; set; }
        public int providerConfigurationID { get; set; }
        public int enablePasswordManagement { get; set; }
        public bool otpEnabled { get; set; }
        public bool forgetPasswordEnabled { get; set; }
        public bool challengeQuestionEnabled { get; set; }
        public bool autoGenerate { get; set; }
        public bool autoGeneratePassword { get; set; }
        public bool sendPasswordEmailNotification { get; set; }
        public bool sendEmailNotification { get; set; }
        public long assignableId { get; set; }
        public long sessionTimeOut { get; set; }
        public bool sessionTrace { get; set; }
        public ICollection<Departments> departments { get; set; }
        public string modifiedLoginName { get; set; }
        public string createdLoginName { get; set; }
        public string emailAddress { get; set; }
        public int firmAddressIndicator { get; set; }
        public int preferredPhone { get; set; }
        public string preferredPhoneText { get; set; }
        public ICollection<AuthorizationRegToLogins> authorizationRegToLogins { get; set; }
        public ICollection<string> userToWorkgroups { get; set; }
        public string userDisplayName { get; set; }
        public string authProvider { get; set; }
        public int otpType { get; set; }
        public Object customInfos { get; set; }
        public string guid { get; set; }
        public bool sendPwdResetLinkAtLogin { get; set; }
        public bool passwordUpdate { get; set; }
        public bool successfulLogin { get; set; }
        public bool rejectedLogin { get; set; }
        public bool emailUpdate { get; set; }
        public bool loginNameChange { get; set; }
        public bool userPatternViolation { get; set; }
        public int authenticationFactor { get; set; }
        public int alternateOTPMode { get; set; }
        public bool mobileNumberUpdate { get; set; }
        public bool challengeQuestionsUpdate { get; set; }
        public bool googleAuthenticatorUpdate { get; set; }
        public bool mobileBiometricsUpdate { get; set; }
        public bool registrationValidateViolation { get; set; }
        public bool editable { get; set; }
        public bool policyAgreementResponse { get; set; }
        public int totalFailedPin { get; set; }
        public int pinStatus { get; set; }
        public string pinStatusCode { get; set; }
        public string nbFailedExternalValidation { get; set; }
        public bool externalValidationViolation { get; set; }
        public int externalTokenProviderID { get; set; }
        public string externalTokenProviderName { get; set; }
        public bool externalTokenViolation { get; set; }
        public string loginRole { get; set; }
    }

    public class Departments
    {
        public long departmentId { get; set; }
        public string departmentName { get; set; }
        public long primaryDept { get; set; }
    }

    public class AuthorizationRegToLogins
    {
        public int id { get; set; }
        public string name { get; set; }
        public int exclusion { get; set; }
        public ICollection<object> policies { get; set; }
    }
}
