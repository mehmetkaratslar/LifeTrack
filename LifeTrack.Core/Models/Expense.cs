using System;
using System.ComponentModel.DataAnnotations;

namespace LifeTrack.Core.Models
{
    public class Expense
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}