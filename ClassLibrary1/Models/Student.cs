using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LearningManagement.Models
{
    public class Student : Person
    {
        public Dictionary<int, double> Grades { get; set; }
        public Dictionary<string, double> CourseAverage { get; set; }

        public double GPA { get; set; }
        public PersonClassification Classification { get; set; }

        public Student()
        {
            Grades = new Dictionary<int, double>();
            CourseAverage = new Dictionary<string, double>();
            GPA = 0.0;
        }

        public override string ToString()
        {
            return $"[{Id}] {Name} - {Classification}";
        }
    }
    public enum PersonClassification
    {
        Freshman, Sophomore, Junior, Senior
    }
}
