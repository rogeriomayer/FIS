using FMC.FIS.Business.Models.FIS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace FMC.Digicob.Business.Model
{
    [Table("Contract")]
    public class Contract
    {
        public Contract()
        {
            Collections = new HashSet<CollectionModel>();
            Agreements = new HashSet<Agreement>();

        }

        [Key]
        public long IdContract { get; set; }
        public long IdPerson { get; set; }
        public int IdProductType { get; set; }

        [NotMapped]
        public DateTime DtBirth { get; set; }

        [NotMapped]
        public string Name { get; set; }

        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("customer_id")]
        public long CustomerId { get; set; }

        [JsonPropertyName("portfolio_id")]
        public long PortfolioId { get; set; }

        [JsonPropertyName("portfolio_owner")]
        public string PortfolioOwner { get; set; }

        [JsonPropertyName("portfolio")]
        public string Portfolio { get; set; }

        [JsonPropertyName("product")]
        public string Product { get; set; }

        [JsonPropertyName("subproduct")]
        public string Subproduct { get; set; }

        [JsonPropertyName("external_contract_id")]
        public string ExternalContractId { get; set; }

        [JsonPropertyName("external_parent_contract_id")]
        public string? ExternalParentContractId { get; set; }

        [JsonPropertyName("external_customer_id")]
        public string ExternalCustomerId { get; set; }

        [JsonPropertyName("external_customer_score")]
        public string ExternalCustomerScore { get; set; }

        [JsonPropertyName("collection_count")]
        public int CollectionCount { get; set; }

        [JsonPropertyName("oldest_due_date")]
        public DateTime? OldestDueDate { get; set; }

        [JsonPropertyName("aging_max")]
        public int AgingMax { get; set; }

        [JsonPropertyName("updated_value")]
        public decimal? UpdatedValue { get; set; }

        [JsonPropertyName("principal_value")]
        public decimal? PrincipalValue { get; set; }

        [JsonPropertyName("penalty_value")]
        public decimal? PenaltyValue { get; set; }

        [JsonPropertyName("interest_value")]
        public decimal? InterestValue { get; set; }

        [JsonPropertyName("other_value")]
        public decimal? OtherValue { get; set; }

        [JsonPropertyName("discount_value")]
        public decimal? DiscountValue { get; set; }

        [JsonPropertyName("interest_rate")]
        public decimal? InterestRate { get; set; }

        [JsonPropertyName("penalty_rate")]
        public decimal? PenaltyRate { get; set; }

        [JsonPropertyName("last_payment_on")]
        public DateTime? LastPaymentOn { get; set; }

        [JsonPropertyName("is_restricted")]
        public bool? IsRestricted { get; set; }

        [JsonPropertyName("contracted_on")]
        public DateTime? ContractedOn { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("collections")]
        public virtual ICollection<CollectionModel> Collections { get; set; }

        [ForeignKey("IdProductType")]
        public virtual ProductType ProductType { get; set; }

        [NotMapped]
        public virtual ICollection<Agreement> Agreements { get; set; }
    }

    [Table("Collection")]
    public class CollectionModel
    {
        [Key]
        public long IdCollect { get; set; }
        public long IdContract { get; set; }


        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("external_collection_id")]
        public string ExternalCollectionId { get; set; }

        [JsonPropertyName("number")]
        public string Number { get; set; }

        [JsonPropertyName("due_date")]
        public DateTime DueDate { get; set; }

        [JsonPropertyName("aging")]
        public int Aging { get; set; }

        [JsonPropertyName("updated_value")]
        public decimal UpdatedValue { get; set; }

        [JsonPropertyName("principal_value")]
        public decimal PrincipalValue { get; set; }

        [JsonPropertyName("penalty_value")]
        public decimal? PenaltyValue { get; set; }

        [JsonPropertyName("interest_value")]
        public decimal InterestValue { get; set; }

        [JsonPropertyName("other_value")]
        public decimal? OtherValue { get; set; }

        [JsonPropertyName("discount_value")]
        public decimal? DiscountValue { get; set; }

        [JsonPropertyName("mailing")]
        public int Mailing { get; set; }

        [JsonPropertyName("batch_date")]
        public DateTime BatchDate { get; set; }

        [JsonPropertyName("external_updated_on")]
        public DateTime ExternalUpdatedOn { get; set; }

        [JsonPropertyName("status_updated_at")]
        public DateTime StatusUpdatedAt { get; set; }

        [JsonPropertyName("recovery_started_at")]
        public DateTime RecoveryStartedAt { get; set; }

        [JsonPropertyName("recovery_ended_at")]
        public DateTime? RecoveryEndedAt { get; set; }

        [NotMapped]
        [JsonPropertyName("extra_field")]
        public Dictionary<string, int>? ExtraField { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonIgnore]
        [ForeignKey("IdContract")]
        public virtual Contract ContractResponse { get; set; }
    }
}