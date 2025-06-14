using System;
using System.Text.Json.Serialization;
using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;

namespace CoreMain.Models
{
    [Table("gyms")]
    public class Gym : BaseModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("address")]
        public string? Address { get; set; }

        [JsonPropertyName("phone")]
        public string? Phone { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("logo")]
        public string? Logo { get; set; }

        [JsonPropertyName("city")]
        public string? City { get; set; }

        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [JsonPropertyName("state")]
        public string? State { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("photo1")]
        public string? Photo1 { get; set; }

        [JsonPropertyName("photo2")]
        public string? Photo2 { get; set; }

        [JsonPropertyName("photo3")]
        public string? Photo3 { get; set; }

        [JsonPropertyName("longDescription")]
        public string? LongDescription { get; set; }

        [JsonPropertyName("urlGym")]
        public string? UrlGym { get; set; }
    }
} 