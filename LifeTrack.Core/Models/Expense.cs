using DocumentFormat.OpenXml.Drawing.Diagrams;
using System;
using System.ComponentModel.DataAnnotations;

namespace LifeTrack.Core.Models
{
    public class Expense
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public string ReceiptImagePath { get; set; }

        public bool IsRecurring { get; set; }

        public RecurringPeriod? RecurringPeriod { get; set; }
    }

    public enum RecurringPeriod
    {
        Daily,
        Weekly,
        Monthly,
        Yearly
    }
}
