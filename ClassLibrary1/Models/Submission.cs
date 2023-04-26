using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LearningManagement.Models
{
    public class Submission
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
        public Student Student { get; set; }
        public int Grade { get; set; }


        public Submission()
        {
            Student = new Student();
            Grade = 0;
        }

        public override string ToString()
        {
            return $"{Student.Name} - {Grade}";
        }
    }
}
