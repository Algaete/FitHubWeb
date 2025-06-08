using System;

namespace CoreMain.Models
{
    public class ClassBookingDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ClassScheduleId { get; set; }
        public string Status { get; set; } = "booked";
        public DateTime BookedAt { get; set; }
        public DateTime? CancelledAt { get; set; }
        public DateTime? AttendedAt { get; set; }
    }
} 