using Library.LearningManagement.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LearningManagement.Models
{
    public class Person
    {
        private static int lastId = 0;

        public int Id
        {
            get; private set;
        }
        public string Name { get; set; }

        public virtual string Display => $"[{Id}] {Name}";

        public Person()
        {
            Name = string.Empty;
            Id = ++lastId;
        }

        public override string ToString()
        {
            return Display;
        }

        public Person(PersonDTO dto)
        {
            Id = dto.Id;
            Name = dto.Name;
        }
    }
}
