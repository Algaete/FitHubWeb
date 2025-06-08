using System;

namespace CoreMain.Models
{
    public class PlanDto
    {
        public Guid Id { get; set; }
        public Guid? GymId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string DurationDays { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public TimeSpan ValidityPeriod { get; set; }
    }
} 