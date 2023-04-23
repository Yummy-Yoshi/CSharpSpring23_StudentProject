using Library.LearningManagement.Database;
using Library.LearningManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LearningManagement.Services
{
    public class CourseService
    {
        private static CourseService? _instance;

        public IEnumerable<Course?> Courses
        {
            get
            {
                return FakeDatabase.Courses.Where(c => c is Course).Select(c => c as Course);
            }
        }

        private CourseService()
        {
 
        }

        public static CourseService Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CourseService();
                }
                return _instance;
            }
        }

        public void Add(Course course)
        {
            FakeDatabase.Courses.Add(course);
        }

        public void Remove(Course course)
        {
            FakeDatabase.Courses.Remove(course);
        }

        public void AddStudent(Course course, Person person)
        {
            foreach (var item in FakeDatabase.Courses)
            {
                if (item.Name == course.Name)
                {
                    item.Roster.Add(person);
                }
            }
        }

        public void RemoveStudent(Course course, Person person)
        {
            foreach (var item in FakeDatabase.Courses)
            {
                if (item.Name == course.Name)
                {
                    item.Roster.Remove(person);
                }
            }
        }

        public void AddAssignment(Course course, Assignment assignment)
        {
            foreach (var item in FakeDatabase.Courses)
            {
                if (item.Name == course.Name)
                {
                    item.Assignments.Add(assignment);
                }
            }
        }

        public Course? GetById(int id)
        {
            return FakeDatabase.Courses.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Course?> Search(string query)
        {
            return Courses.Where(s => s.Name.ToUpper().Contains(query.ToUpper())
                || s.Description.ToUpper().Contains(query.ToUpper())
                || s.Code.ToUpper().Contains(query.ToUpper()));
        }

    }
}
