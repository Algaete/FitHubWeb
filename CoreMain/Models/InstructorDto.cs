using System;

namespace CoreMain.Models
{
    public class InstructorDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? Bio { get; set; }
        public string? Phone { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; }
    }
} 