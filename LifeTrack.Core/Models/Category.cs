using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LifeTrack.Core.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string IconName { get; set; }

        public string ColorHex { get; set; }
      

        public virtual ICollection<Expense> Expenses { get; set; }
    }
}