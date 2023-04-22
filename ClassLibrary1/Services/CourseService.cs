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

        private CourseService()
        {
 
        }

        public void Add(Course course)
        {
            FakeDatabase.Courses.Add(course);
        }

        public List<Course> Courses
        {
            get
            {
                return FakeDatabase.Courses;
            }
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

        public IEnumerable<Course> Search(string query)
        {
            return Courses.Where(s => s.Name.ToUpper().Contains(query.ToUpper())
                || s.Description.ToUpper().Contains(query.ToUpper())
                || s.Code.ToUpper().Contains(query.ToUpper()));
        }

    }
}
