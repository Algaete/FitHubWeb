using System;
using System.Text.Json.Serialization;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace CoreMain.Models
{
    [Table("instructors")]
    public class Instructor : BaseModel
    {
        [PrimaryKey("id")]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("full_name")]
        public string FullName { get; set; } = string.Empty;

        [JsonPropertyName("bio")]
        public string? Bio { get; set; }

        [JsonPropertyName("phone")]
        public string? Phone { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("user_id")]
        [JsonPropertyName("user_id")]
        public Guid UserId { get; set; }
    }
} 