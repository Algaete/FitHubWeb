using System;
using Supabase.Postgrest.Models;
using Supabase.Postgrest.Attributes;

namespace CoreMain.Models
{
    [Table("gyms")]
    public class Gym : BaseModel
    {
        [PrimaryKey("id", false)]
        public Guid Id { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("address")]
        public string? Address { get; set; }

        [Column("phone")]
        public string? Phone { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
} 