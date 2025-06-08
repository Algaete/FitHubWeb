using System;
using System.Text.Json.Serialization;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace CoreMain.Models
{
    [Table("user_plans")]
    public class UserPlan : BaseModel
    {
        [PrimaryKey("id")]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [Column("user_id")]
        [JsonPropertyName("user_id")]
        public Guid UserId { get; set; }

        [Column("plan_id")]
        [JsonPropertyName("plan_id")]
        public Guid PlanId { get; set; }

        [Column("start_date")]
        [JsonPropertyName("start_date")]
        public DateTime StartDate { get; set; }

        [Column("end_date")]
        [JsonPropertyName("end_date")]
        public DateTime EndDate { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; } = "active";

        [Column("auto_renew")]
        [JsonPropertyName("auto_renew")]
        public bool AutoRenew { get; set; }

        [Column("created_at")]
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
} 