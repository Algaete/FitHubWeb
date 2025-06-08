using System;

namespace CoreMain.Models
{
    public class ClassDto
    {
        public Guid Id { get; set; }
        public Guid? GymId { get; set; }
        public Guid? InstructorId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime StartTime { get; set; }
        public int Capacity { get; set; }
        public DateTime CreatedAt { get; set; }
    }
} 