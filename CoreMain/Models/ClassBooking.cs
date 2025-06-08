using System;
using System.Text.Json.Serialization;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace CoreMain.Models
{
    [Table("class_bookings")]
    public class ClassBooking : BaseModel
    {
        [PrimaryKey("id")]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [Column("user_id")]
        [JsonPropertyName("user_id")]
        public Guid UserId { get; set; }

        [Column("class_schedule_id")]
        [JsonPropertyName("class_schedule_id")]
        public Guid ClassScheduleId { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; } = "booked";

        [Column("booked_at")]
        [JsonPropertyName("booked_at")]
        public DateTime BookedAt { get; set; }

        [Column("cancelled_at")]
        [JsonPropertyName("cancelled_at")]
        public DateTime? CancelledAt { get; set; }

        [Column("attended_at")]
        [JsonPropertyName("attended_at")]
        public DateTime? AttendedAt { get; set; }
    }
} 