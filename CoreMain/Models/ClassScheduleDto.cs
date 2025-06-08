using System;

namespace CoreMain.Models
{
    public class ClassScheduleDto
    {
        public Guid Id { get; set; }
        public Guid ClassId { get; set; }
        public DateTime SessionStart { get; set; }
        public DateTime SessionEnd { get; set; }
        public Guid? InstructorId { get; set; }
        public int Capacity { get; set; }
        public DateTime CreatedAt { get; set; }
    }
} 