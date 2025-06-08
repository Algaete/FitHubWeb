using System;
using System.Text.Json.Serialization;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace CoreMain.Models
{
    [Table("classes")]
    public class Class : BaseModel
    {
        [PrimaryKey("id")]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [Column("gym_id")]
        [JsonPropertyName("gym_id")]
        public Guid? GymId { get; set; }

        [Column("instructor_id")]
        [JsonPropertyName("instructor_id")]
        public Guid? InstructorId { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("start_time")]
        public DateTime StartTime { get; set; }

        [JsonPropertyName("capacity")]
        public int Capacity { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
    }
} 