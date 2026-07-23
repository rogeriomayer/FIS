using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FMC.Digicob.Business.Model
{
    [Table("Customer")]
    public class Customer
    {
        [Key]
        public long IdCustomer { get; set; }

        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("document")]
        public string Document { get; set; } = string.Empty;

        [JsonPropertyName("masked_document")]
        public string MaskedDocument { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("alias")]
        public string Alias { get; set; } = string.Empty;

        /// <summary>
        /// pf / pj
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("birth_date")]
        public DateTime? BirthDate { get; set; }

        [JsonPropertyName("age")]
        public int Age { get; set; }

        [JsonPropertyName("gender")]
        public string Gender { get; set; } = string.Empty;

        [JsonPropertyName("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
