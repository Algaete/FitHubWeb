using System;
using System.Text.Json.Serialization;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace CoreMain.Models
{
    [Table("instructor_gyms")]
    public class InstructorGym : BaseModel
    {
        [PrimaryKey("instructor_id")]
        [Column("instructor_id")]
        [JsonPropertyName("instructor_id")]
        public Guid InstructorId { get; set; }

        [PrimaryKey("gym_id")]
        [Column("gym_id")]
        [JsonPropertyName("gym_id")]
        public Guid GymId { get; set; }
    }
} 