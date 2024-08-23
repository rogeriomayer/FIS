using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMC.Solutions.NPPLUS.Library.REST.Models
{
    public class AytyDialerSystemAPIWSResponse
    {
        public int IdReturnAPI { get; set; }

        public string DeReturnAPI { get; set; }

        public IList<object> DeReturnAPIList { get; set; }
    }

    public class AytyAgentAPIResponse
    {
        public int IdReturnAPI { get; set; }
        public string DeReturnAPI { get; set; }
        public int IdStatusPauseSchedule { get; set; }
        public bool IsAvailable { get; set; }
        public bool AllowMakeCallRedial { get; set; }
        public bool AllowGetMailingAPIByWait { get; set; }
        public int IdAgent { get; set; }
        public string CodeAgent { get; set; }
        public bool AllowStartPause { get; set; }
        public bool IsLogged { get; set; }
    }

    public class AytyGetMailingAPIResponse
    {
        public int IdMailing { get; set; }
        public string NmMailing { get; set; }
        public string NuRegistration { get; set; }
        public string NmCampaign { get; set; }
        public int IdCampaign { get; set; }
        public int IdCall { get; set; }
        public int IdStatus { get; set; }
        public bool IsFromPersonalSchedule { get; set; }
        public bool IsInbound { get; set; }
        public string NuDDD { get; set; }
        public string NuPhone { get; set; }
        public string CodeMailingClient { get; set; }
        public int IdReturnAPI { get; set; }
        public string DeReturnAPI { get; set; }
        public int IdTelephone { get; set; }
        public int IdQueue { get; set; }
        public string IdAstChannel { get; set; }
        public string NmDevice { get; set; }
        public string DtRegisterIPBX { get; set; }
        public string NmCityA1 { get; set; }
        public string NmCountryStateA1 { get; set; }
        public string NmNeighborhoodA1 { get; set; }
        public string DeCustom1 { get; set; }
        public string DeCustom2 { get; set; }
        public string DeCustom3 { get; set; }
        public string DeCustom4 { get; set; }
        public string DeCustom5 { get; set; }
        public string DeCustom6 { get; set; }
        public string DeCustom7 { get; set; }
        public string DeCustom8 { get; set; }
        public string DeCustom9 { get; set; }
        public string DeCustom10 { get; set; }
        public string DeCustom11 { get; set; }
        public string DeCustom12 { get; set; }
        public string DeCustom13 { get; set; }
        public string DeCustom14 { get; set; }
        public string DeCustom15 { get; set; }
        public string DeCustom16 { get; set; }
        public string DeCustom17 { get; set; }
        public string DeCustom18 { get; set; }
        public string DeCustom19 { get; set; }
        public string DeCustom20 { get; set; }
        public int IdCampaignType { get; set; }
        public int IdProject { get; set; }
        public int IdCallType { get; set; }
        public int IdCRMConfiguration { get; set; }
        public string Obs { get; set; }
        public string DeTransferData { get; set; }
        public string DeIntegrationData { get; set; }
        public string NmFile { get; set; }
        public bool HasAnsweredSMS { get; set; }
        public string CodeQueue { get; set; }
        public int IdCallIpbx { get; set; }
    }

}
