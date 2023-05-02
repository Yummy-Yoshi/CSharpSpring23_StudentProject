using Library.LearningManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LearningManagement.Database
{
    public static class FakeDatabaseContext
    {
        public static List<Person> People = new List<Person>{
                new Student{Name = "Bimbop", Classification = PersonClassification.Freshman},
                new Student{Name = "Johnny", Classification = PersonClassification.Sophomore},
                new Student { Name = "Catherine", Classification = PersonClassification.Senior},
            };
    }
}
