using System;
using System.Text.Json.Serialization;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace CoreMain.Models
{
    [Table("users")]
    public class User : BaseModel
    {
        [PrimaryKey("id")]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [Column("email")]
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [Column("password")]
        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;

        [Column("full_name")]
        [JsonPropertyName("full_name")]
        public string? FullName { get; set; }

        [Column("dob")]
        [JsonPropertyName("dob")]
        public DateTime? Dob { get; set; }

        [Column("phone")]
        [JsonPropertyName("phone")]
        public string? Phone { get; set; }

        [Column("emergency_contact_name")]
        [JsonPropertyName("emergency_contact_name")]
        public string? EmergencyContactName { get; set; }

        [Column("emergency_contact_phone")]
        [JsonPropertyName("emergency_contact_phone")]
        public string? EmergencyContactPhone { get; set; }

        [Column("role")]
        [JsonPropertyName("role")]
        public string Role { get; set; } = "user";

        [Column("created_at")]
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
    }
} 