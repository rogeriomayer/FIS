using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FMC.Digicob.Business.Model
{
    public class CustomerContact
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("customer_id")]
        public long CustomerId { get; set; }

        [JsonPropertyName("contact")]
        [Required]
        public Contact Contact { get; set; }

        [JsonPropertyName("score")]
        [MaxLength(50)]
        public string Score { get; set; }

        [JsonPropertyName("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }

        [JsonPropertyName("sources")]
        public List<Source> Sources { get; set; } = new();
    }

    public class Contact
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("value")]
        [Required]
        [MaxLength(200)]
        public string Value { get; set; }

        [JsonPropertyName("type")]
        [Required]
        [MaxLength(50)]
        public string Type { get; set; }

        [JsonPropertyName("status")]
        [MaxLength(50)]
        public string Status { get; set; }

        [JsonPropertyName("provider")]
        [MaxLength(100)]
        public string Provider { get; set; }

        [JsonPropertyName("score")]
        [MaxLength(50)]
        public string Score { get; set; }

        [JsonPropertyName("provider_updated_at")]
        public DateTimeOffset? ProviderUpdatedAt { get; set; }

        [JsonPropertyName("status_updated_at")]
        public DateTimeOffset? StatusUpdatedAt { get; set; }

        [JsonPropertyName("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }
    }

    public class Source
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("name")]
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        [Required]
        [MaxLength(50)]
        public string Type { get; set; }

        [JsonPropertyName("portfolio")]
        [MaxLength(100)]
        public string Portfolio { get; set; }

        [JsonPropertyName("created_at")]
        public DateTimeOffset CreatedAt { get; set; }
    }

}