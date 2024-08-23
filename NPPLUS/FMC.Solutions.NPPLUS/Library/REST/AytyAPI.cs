using FMC.Solutions.NPPLUS.Library.Class;
using FMC.Solutions.NPPLUS.Library.REST.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FMC.Solutions.NPPLUS.Library.REST
{

    public class AytyAPI
    {
        private static string URL = @"https://portalaytyfis.fnis.com.br/Ayty/App/FisCob/AytyDialerSystemAPIWS_FISCBSS_AYTYAFINZDB/AytyDialerSystemJSON.asmx";

        public static int Login(string user)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("pCodeAgent", user);

            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Content-Type", "application/xml");

            var xmlRet = RestAPI.Get<XmlDocument>(URL, "Login", headers, null, param);

            AytyDialerSystemAPIWSResponse aytyDialerLogin = JsonConvert.DeserializeObject<AytyDialerSystemAPIWSResponse>(xmlRet.InnerText);

            return aytyDialerLogin.IdReturnAPI;
        }

        public static int Logout(string user)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("pCodeAgent", user);

            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Content-Type", "application/xml");

            var xmlRet = RestAPI.Get<XmlDocument>(URL, "Logout", headers, null, param);

            AytyDialerSystemAPIWSResponse aytyDialerLogin = JsonConvert.DeserializeObject<AytyDialerSystemAPIWSResponse>(xmlRet.InnerText);

            return aytyDialerLogin.IdReturnAPI;
        }


        public static int SetAvailable(string user)
        {

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("pCodeAgent", user);

            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Content-Type", "application/xml");

            var xmlRet = RestAPI.Get<XmlDocument>(URL, "SetAvailable", headers, null, param);

            AytyDialerSystemAPIWSResponse aytyDialerLogin = JsonConvert.DeserializeObject<AytyDialerSystemAPIWSResponse>(xmlRet.InnerText);

            return aytyDialerLogin.IdReturnAPI;
        }


        public static AytyAgentAPIResponse GetAgentAPI(string user)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("pCodeAgent", user);

            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Content-Type", "application/xml");

            var xmlRet = RestAPI.Get<XmlDocument>(URL, "GetAgentAPI", headers, null, param);

            AytyAgentAPIResponse aytyAgentAPIResponse = JsonConvert.DeserializeObject<AytyAgentAPIResponse>(xmlRet.InnerText);

            return aytyAgentAPIResponse;
        }

        public static AytyDialerSystemAPIWSResponse GetCodeStatusIPBXFromAgentRealtimeControl(string user)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("pCodeAgent", user);

            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Content-Type", "application/xml");

            var xmlRet = RestAPI.Get<XmlDocument>(URL, "GetCodeStatusIPBXFromAgentRealtimeControl", headers, null, param);

            AytyDialerSystemAPIWSResponse aytyAgentAPIResponse = JsonConvert.DeserializeObject<AytyDialerSystemAPIWSResponse>(xmlRet.InnerText);

            return aytyAgentAPIResponse;
        }

        public static async Task<AytyGetMailingAPIResponse> GetMailingAPIByWaitWithSetAvailable(string user)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("pCodeAgent", user);

            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Content-Type", "application/xml");


            Task<XmlDocument> xmlRet = RestAPI.GetAsync<XmlDocument>(URL, "GetMailingAPIByWaitWithSetAvailable", headers, null, param);

            xmlRet.Wait();

            var xmlOk = await xmlRet;

            AytyGetMailingAPIResponse aytyGetMailingAPIResponse = JsonConvert.DeserializeObject<AytyGetMailingAPIResponse>(xmlOk.InnerText);

            return aytyGetMailingAPIResponse;
        }

        public static AytyGetMailingAPIResponse GetMailingAPI(string agent, string device)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("pCodeAgent", agent);
            param.Add("pNmDevice", device);

            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Content-Type", "application/xml");

            XmlDocument xmlRet = RestAPI.Get<XmlDocument>(URL, "GetMailingAPI", headers, null, param);

            AytyGetMailingAPIResponse aytyGetMailingAPIResponse = JsonConvert.DeserializeObject<AytyGetMailingAPIResponse>(xmlRet.InnerText);
            Log.SaveFile(xmlRet.InnerText);
            return aytyGetMailingAPIResponse;
        }


        public static AytyDialerSystemAPIWSResponse StartPause(string agent, string idStatus)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("pCodeAgent", agent);
            param.Add("pIdStatus", idStatus);

            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Content-Type", "application/xml");

            var xmlRet = RestAPI.Get<XmlDocument>(URL, "StartPause", headers, null, param);

            AytyDialerSystemAPIWSResponse aytyDialerSystemAPIWS = JsonConvert.DeserializeObject<AytyDialerSystemAPIWSResponse>(xmlRet.InnerText);

            return aytyDialerSystemAPIWS;
        }

        public static AytyDialerSystemAPIWSResponse FinishCall(string agent, string idCall, string idStatus, bool pMustFinishCurrentAttend, string pDtBestTime, string pNuDDDToSchedule, string pNuPhoneToSchedule)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("pCodeAgent", agent);
            param.Add("pIdCall", idCall);
            param.Add("pIdStatus", idStatus);
            param.Add("pMustFinishCurrentAttend", pMustFinishCurrentAttend.ToString());
            param.Add("pDtBestTime", pDtBestTime);
            param.Add("pNuDDDToSchedule", pNuDDDToSchedule);
            param.Add("pNuPhoneToSchedule", pNuPhoneToSchedule);

            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Content-Type", "application/xml");

            var xmlRet = RestAPI.Get<XmlDocument>(URL, "FinishCall", headers, null, param);

            AytyDialerSystemAPIWSResponse aytyDialerSystemAPIWS = JsonConvert.DeserializeObject<AytyDialerSystemAPIWSResponse>(xmlRet.InnerText);

            return aytyDialerSystemAPIWS;
        }

        public static AytyGetMailingAPIResponse MakeCallRedial(string agent, string ddd, string phoneNumber)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("pCodeAgent", agent);
            param.Add("pNuDDD", ddd);
            param.Add("pNuPhone", phoneNumber);

            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Content-Type", "application/xml");

            var xmlRet = RestAPI.Get<XmlDocument>(URL, "MakeCallRedial", headers, null, param);

            AytyGetMailingAPIResponse aytyGetMailingAPIResponse = JsonConvert.DeserializeObject<AytyGetMailingAPIResponse>(xmlRet.InnerText);

            return aytyGetMailingAPIResponse;
        }

        public static AytyGetMailingAPIResponse ManualCall(string agent, string ddd, string phoneNumber)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("pCodeAgent", agent);
            param.Add("pNuDDD", ddd);
            param.Add("pNuPhone", phoneNumber);

            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Content-Type", "application/xml");

            var xmlRet = RestAPI.Get<XmlDocument>(URL, "MakeCallManual", headers, null, param);

            AytyGetMailingAPIResponse aytyGetMailingAPIResponse = JsonConvert.DeserializeObject<AytyGetMailingAPIResponse>(xmlRet.InnerText);

            return aytyGetMailingAPIResponse;
        }
    }
}
