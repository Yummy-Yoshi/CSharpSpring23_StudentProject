using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Library.LearningManagement.Models
{
    public class AssignmentGroup
    {
        private static int lastId = 0;
        private int id = 0;
        public int Id
        {
            get
            {
                if (id == 0)
                {
                    id = ++lastId;
                }
                return id;
            }
        }
        public string? Name { get; set; }

        public decimal Weight { get; set; }

        public List<Assignment> Assignments { get; set; }

        public AssignmentGroup()
        {
            Assignments = new List<Assignment>();
        }

        public virtual string Display => $"{Id}. {Name} - {Weight}%\n" +
            $"Assignments:\n{string.Join("\n", Assignments.Select(a => a.ToString()).ToArray())}\n";

        public override string ToString()
        {
            return Display;
        }
    }
}
