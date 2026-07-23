using FMC.Digicob.Business.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FMC.Digicob.Business.Model
{

    [Table("Agreement")]
    public class Agreement
    {
        [Key]
        public long IdAgreement { get; set; }

        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("plan_uuid")]
        public Guid PlanUuid { get; set; }

        [JsonPropertyName("installment_count")]
        public int InstallmentCount { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("agreement_source")]
        public string AgreementSource { get; set; }

        [JsonPropertyName("channel")]
        public string Channel { get; set; }

        [JsonPropertyName("customer_id")]
        public long CustomerId { get; set; }

        [JsonPropertyName("user")]
        public string User { get; set; }

        [JsonPropertyName("request_log_id")]
        public long RequestLogId { get; set; }

        [JsonPropertyName("parent_agreement_id")]
        public long ParentAgreementId { get; set; }

        [JsonPropertyName("principal_value")]
        public decimal PrincipalValue { get; set; }

        [JsonPropertyName("principal_discount_value")]
        public decimal PrincipalDiscountValue { get; set; }

        [JsonPropertyName("principal_discount_percentage")]
        public decimal PrincipalDiscountPercentage { get; set; }

        [JsonPropertyName("interest_value")]
        public decimal InterestValue { get; set; }

        [JsonPropertyName("interest_discount_value")]
        public decimal InterestDiscountValue { get; set; }

        [JsonPropertyName("interest_discount_percentage")]
        public decimal InterestDiscountPercentage { get; set; }

        [JsonPropertyName("interest_rate_percentage")]
        public decimal InterestRatePercentage { get; set; }

        [JsonPropertyName("interest_rate_value")]
        public decimal InterestRateValue { get; set; }

        [JsonPropertyName("penalty_value")]
        public decimal PenaltyValue { get; set; }

        [JsonPropertyName("updated_value")]
        public decimal UpdatedValue { get; set; }

        [JsonPropertyName("total_value")]
        public decimal TotalValue { get; set; }

        [JsonPropertyName("discount_value")]
        public decimal DiscountValue { get; set; }

        [JsonPropertyName("discount_percentage")]
        public decimal DiscountPercentage { get; set; }

        [JsonPropertyName("down_payment_date")]
        public DateTime DownPaymentDate { get; set; }

        [JsonPropertyName("down_payment_value")]
        public decimal DownPaymentValue { get; set; }

        [JsonPropertyName("down_payment_percentage")]
        public decimal DownPaymentPercentage { get; set; }

        [JsonPropertyName("second_installment_date")]
        public DateTime? SecondInstallmentDate { get; set; }

        [JsonPropertyName("installment_value")]
        public decimal InstallmentValue { get; set; }

        [JsonPropertyName("financial_cost_value")]
        public decimal FinancialCostValue { get; set; }

        [JsonPropertyName("financial_cost_percentage")]
        public decimal FinancialCostPercentage { get; set; }

        [JsonPropertyName("plan_description")]
        public string PlanDescription { get; set; }

        [JsonPropertyName("activated_at")]
        public DateTime? ActivatedAt { get; set; }

        [JsonPropertyName("canceled_at")]
        public DateTime? CanceledAt { get; set; }

        [JsonPropertyName("canceled_by")]
        public long? CanceledBy { get; set; }

        [JsonPropertyName("cancelation_reason")]
        public string? CancelationReason { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("installments")]
        public virtual List<AgreementInstallment> Installments { get; set; }

        [JsonPropertyName("contracts")]
        public virtual List<AgreementContract> Contracts { get; set; }

    }

    [Table("AgreementInstallment")]
    public class AgreementInstallment
    {
        [Key]
        public long IdAgreementInstallment { get; set; }

        public long IdAgreement { get; set; }

        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("number")]
        public int Number { get; set; }

        [JsonPropertyName("due_date")]
        public DateTime DueDate { get; set; }

        [JsonPropertyName("value")]
        public decimal Value { get; set; }

        [JsonPropertyName("collection_id")]
        public long? CollectionId { get; set; }

        [JsonPropertyName("external_installment_id")]
        public string ExternalInstallmentId { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("billets")]
        public List<Billet> Billets { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(IdAgreement))]
        public virtual Agreement Agreement { get; set; }
    }

    [Table("Billet")]
    public class Billet
    {
        [Key]
        public long IdBillet { get; set; }

        public long IdAgreementInstallment { get; set; }

        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("external_billet_id")]
        public string? ExternalBilletId { get; set; }

        [JsonPropertyName("billet_uuid")]
        public Guid BilletUuid { get; set; }

        [JsonPropertyName("due_date")]
        public DateTime DueDate { get; set; }

        [JsonPropertyName("value")]
        public decimal Value { get; set; }

        [JsonPropertyName("payment_line")]
        public string? PaymentLine { get; set; }

        [JsonPropertyName("masked_payment_line")]
        public string? MaskedPaymentLine { get; set; }

        [JsonPropertyName("barcode")]
        public string? Barcode { get; set; }

        [JsonPropertyName("our_number")]
        public string? OurNumber { get; set; }

        [JsonPropertyName("document_number")]
        public string? DocumentNumber { get; set; }

        [JsonPropertyName("document_date")]
        public string? DocumentDate { get; set; }

        [JsonPropertyName("billet_url")]
        public string? BilletUrl { get; set; }

        [JsonPropertyName("barcode_url")]
        public string? BarcodeUrl { get; set; }

        [JsonPropertyName("pix_code")]
        public string? PixCode { get; set; }

        [JsonPropertyName("pix_qrcode_url")]
        public string? PixQrcodeUrl { get; set; }

        [JsonPropertyName("pix_billet_url")]
        public string? PixBilletUrl { get; set; }

        [JsonPropertyName("processed_on")]
        public DateTime? ProcessedOn { get; set; }

        [JsonPropertyName("user")]
        public string User { get; set; }

        [JsonPropertyName("request_log")]
        public long RequestLog { get; set; }

        [JsonPropertyName("json_billet")]
        public string? JsonBillet { get; set; }

        [JsonPropertyName("is_active")]
        public bool IsActive { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(IdAgreementInstallment))]
        public virtual AgreementInstallment AgreementInstallment { get; set; }
    }



    [Table("AgreementContract")]
    public class AgreementContract
    {
        [Key]
        public long IdAgreementContract { get; set; }

        public long IdAgreement { get; set; }

        [JsonPropertyName("contract_id")]
        public long ContractId { get; set; }

        [JsonPropertyName("external_contract_id")]
        public string ExternalContractId { get; set; }

        [JsonPropertyName("collection_ids")]
        [JsonConverter(typeof(CollectionIdsToStringConverter))]
        public string CollectionIds { get; set; }

        [JsonIgnore]

        [ForeignKey(nameof(IdAgreement))]
        public virtual Agreement Agreement { get; set; }
    }


    public class CollectionIdsToStringConverter : JsonConverter<string>
    {
        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.StartArray)
            {
                var list = JsonSerializer.Deserialize<List<long>>(ref reader, options);
                return string.Join(",", list);
            }

            return null;
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            var values = value.Split(',', StringSplitOptions.RemoveEmptyEntries)
                              .Select(long.Parse)
                              .ToList();

            JsonSerializer.Serialize(writer, values, options);
        }
    }

}
