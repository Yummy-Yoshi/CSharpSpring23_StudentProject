using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Library.LearningManagement.Models
{
    public class Assignment
    {
        private static int lastId = 0;
        private int id = 0;
        public int Id { 
            get
            {
                if(id == 0)
                {
                    id = ++lastId;
                }
                return id;
            }
        }
        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal TotalAvailablePoints { get; set; }

        public DateTime DueDate { get; set; }

        public List<Student> Submissions { get; set; }

        public Assignment()
        {
            Submissions = new List<Student>();
        }


        public virtual string Display => $"{Id}. ({DueDate}) {Name} - {TotalAvailablePoints}\n{Description}";


        public override string ToString()
        {
            return Display;
        }

    }
}
