using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LearningManagement.Models
{
    public class Assignment
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal TotalAvailablePoints { get; set; }

        public DateTime DueDate { get; set; }
        public virtual string Display => $"({DueDate}) {Name} - {TotalAvailablePoints}\n{Description}";

        public override string ToString()
        {
            return Display;
        }

    }
}
