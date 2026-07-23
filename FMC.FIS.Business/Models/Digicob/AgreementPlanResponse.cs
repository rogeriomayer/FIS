using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FMC.Digicob.DM.Models
{
    public class AgreementPlanResponse
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("plan_uuid")]
        public string PlanUuid { get; set; }

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

        [JsonPropertyName("contracts")]
        public List<AgreementContractResponse> Contracts { get; set; }

        [JsonPropertyName("user")]
        public string User { get; set; }

        [JsonPropertyName("request_log_id")]
        public long RequestLogId { get; set; }

        [JsonPropertyName("parent_agreement_id")]
        public long? ParentAgreementId { get; set; }

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
        public string CanceledBy { get; set; }

        [JsonPropertyName("cancelation_reason")]
        public string CancelationReason { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("installments")]
        public List<InstallmentResponse> Installments { get; set; }
    }

    public class AgreementContractResponse
    {
        [JsonPropertyName("contract_id")]
        public long ContractId { get; set; }

        [JsonPropertyName("external_contract_id")]
        public string ExternalContractId { get; set; }

        [JsonPropertyName("collection_ids")]
        public List<long> CollectionIds { get; set; }
    }

    public class InstallmentResponse
    {
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
        public List<BilletResponse> Billets { get; set; }
    }

    
}