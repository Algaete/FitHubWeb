using System;

namespace CoreMain.Models
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? FullName { get; set; }
        public DateTime? Dob { get; set; }
        public string? Phone { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
        public string Role { get; set; } = "user";
        public DateTime CreatedAt { get; set; }
    }
} 