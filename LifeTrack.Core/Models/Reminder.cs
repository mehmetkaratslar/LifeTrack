using System;
using System.ComponentModel.DataAnnotations;

namespace LifeTrack.Core.Models
{
    public class Reminder
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public bool IsCompleted { get; set; }

        public ReminderPriority Priority { get; set; }

        public bool HasNotification { get; set; }

        public TimeSpan? NotificationTime { get; set; }
        public DateTime? CompletedAt { get; set; }

        public DateTime CreatedAt { get; set; }
    }
    
    public enum ReminderPriority
    {
        Low,
        Medium,
        High
    }
}