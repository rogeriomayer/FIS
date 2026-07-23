using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FMC.Digicob.DM.Models
{
    public class AgreementPlanRequest
    {
        [JsonPropertyName("down_payment_date")]
        public string DownPaymentDate { get; set; }

        [JsonPropertyName("channel")]
        public string Channel { get; set; } = string.Empty;

        public long? IdPortalAccess { get; set; }

        [JsonPropertyName("contracts")]
        public List<AgreementContractRequest> Contracts { get; set; } = new();
    }

    public class AgreementContractRequest
    {
        [JsonPropertyName("contract_id")]
        public long ContractId { get; set; }

        [JsonPropertyName("IdContract")]
        public long IdContract { get; set; }

        [JsonPropertyName("collection_ids")]
        public List<long> CollectionIds { get; set; } = new();
    }
}

