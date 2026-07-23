using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FMC.FIS.Business.Code.Api.Dichat
{
    

    public class DichatResponse
    {
        [JsonPropertyName("data")]
        public List<Agreement> Data { get; set; } = new();

        [JsonPropertyName("next_cursor")]
        public string? NextCursor { get; set; }

        [JsonPropertyName("has_more")]
        public bool HasMore { get; set; }
    }

    public class Agreement
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("conversation_id")]
        public Guid ConversationId { get; set; }

        [JsonPropertyName("cpf")]
        public string CPF { get; set; } = string.Empty;

        [JsonPropertyName("contract_id")]
        public string ContractId { get; set; } = string.Empty;

        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("total_amount")]
        public decimal? TotalAmount { get; set; }

        [JsonPropertyName("installments")]
        public int Installments { get; set; }

        [JsonPropertyName("installment_amount")]
        public decimal? InstallmentAmount { get; set; }

        [JsonPropertyName("discount_value")]
        public decimal? DiscountValue { get; set; }

        [JsonPropertyName("discount_pct")]
        public decimal? DiscountPct { get; set; }

        [JsonPropertyName("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; } = string.Empty;

        [JsonPropertyName("birth_date")]
        public DateTime BirthDate { get; set; }

        [JsonPropertyName("wa_number")]
        public string WaNumber { get; set; } = string.Empty;
    }


}
