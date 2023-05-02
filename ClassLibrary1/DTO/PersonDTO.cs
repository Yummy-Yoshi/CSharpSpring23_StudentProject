using Library.LearningManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LearningManagement.DTO
{
    public class PersonDTO
    {
        public PersonDTO(Person p)
        {
            Id= p.Id;
            Name= p.Name;
        }
        public PersonDTO() { }

        public int Id
        {
            get; private set;
        }
        public string Name { get; set; }
    }
}
