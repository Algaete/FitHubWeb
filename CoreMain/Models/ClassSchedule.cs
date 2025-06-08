using System;
using System.Text.Json.Serialization;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace CoreMain.Models
{
    [Table("class_schedules")]
    public class ClassSchedule : BaseModel
    {
        [PrimaryKey("id")]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [Column("class_id")]
        [JsonPropertyName("class_id")]
        public Guid ClassId { get; set; }

        [JsonPropertyName("session_start")]
        public DateTime SessionStart { get; set; }

        [JsonPropertyName("session_end")]
        public DateTime SessionEnd { get; set; }

        [Column("instructor_id")]
        [JsonPropertyName("instructor_id")]
        public Guid? InstructorId { get; set; }

        [JsonPropertyName("capacity")]
        public int Capacity { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
    }
} 