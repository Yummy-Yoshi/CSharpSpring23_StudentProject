using Library.LearningManagement.Database;
using Library.LearningManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LearningManagement.Services
{
    public class StudentService
    {
        private static StudentService? _instance;

        public IEnumerable<Person?> Students
        {
            get
            {
                return FakeDatabase.People.Where(p => p is Person);
            }
        }

        private StudentService()
        {
            
        }

        public static StudentService Current
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new StudentService();
                }
                return _instance;
            }
        }

        public void Add(Person student)
        {
            FakeDatabase.People.Add(student);
        }

        public void Remove(Person student)
        {
            FakeDatabase.People.Remove(student);
        }

        public Person? GetById(int id)
        {
            return FakeDatabase.People.FirstOrDefault(p => p.Id == id);
        }


        public IEnumerable<Person?> Search(string query)
        {
            return Students.Where(s => (s != null) && s.Name.ToUpper().Contains(query.ToUpper())
                || s.Id.ToString().Contains(query.ToUpper()));
        }
    }
}
