using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace App.LearningManagement.Helpers
{
    public class CourseHelper
    {
        private CourseService courseService;
        private StudentService studentService;
        private ModuleService moduleService = new ModuleService();

        public CourseHelper()
        {
            studentService = StudentService.Current;
            courseService = CourseService.Current;
        }

        public Course CreateCourseRecord(Course? selectedCourse = null)
        {
            bool isNewCourse = false;
            if (selectedCourse == null)
            {
                isNewCourse = true;
                selectedCourse = new Course();
            }

            var studenthelper = Program.studentHelper;
            var coursehelper = Program.courseHelper;
            var modulehelper = Program.moduleHelper;
            var assignmentHelper = new AssignmentHelper();

            var choice = "Y";
            if (!isNewCourse)
            {
                Console.WriteLine("Update the course code? (y/n):");
                choice = Console.ReadLine() ?? "N";
            }
            if(choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("Enter class code:");
                selectedCourse.Code = Console.ReadLine() ?? string.Empty;
            }

            if (!isNewCourse)
            {
                Console.WriteLine("Update the course name? (y/n):");
                choice = Console.ReadLine() ?? "N";
            }
            else
            {
                choice = "Y";
            }
            if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("Enter class name:");
                selectedCourse.Name = Console.ReadLine() ?? string.Empty;
            }

            if (!isNewCourse)
            {
                Console.WriteLine("Update the course description? (y/n):");
                choice = Console.ReadLine() ?? "N";
            }
            else
            {
                choice = "Y";
            }
            if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("Enter class description:");
                selectedCourse.Description = Console.ReadLine() ?? string.Empty;
            }

            if (isNewCourse)
            {
                var roster = new List<Person>();
                var assignments = new List<Assignment>();
                var modules = new List<Library.LearningManagement.Models.Module>();

                Console.WriteLine("Enter student in roster? (y/n):");
                string response = Console.ReadLine() ?? string.Empty;
                while (response.Equals("Y", StringComparison.InvariantCultureIgnoreCase) || studentService.Students.Any(s => !roster.Any(s2 => s2.Id == s.Id)))
                {

                    var newStudent = studenthelper.CreateStudentRecord();
                    //studentService.Add(newStudent);
                    roster.Add(newStudent);
                    //studentHelper.Add(newStudent);


                    Console.WriteLine("Add more students to roster? (y/n):");
                    response = Console.ReadLine() ?? string.Empty;
                }


                Console.WriteLine("Enter assignments for course? (y/n):");
                response = Console.ReadLine() ?? string.Empty;

                while (response.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                {

                    assignments.Add(assignmentHelper.CreateAssignmentRecord());

                    Console.WriteLine("Add more assignments to course? (y/n):");
                    response = Console.ReadLine() ?? string.Empty;
                }

                Console.WriteLine("Enter modules for course? (y/n):");
                response = Console.ReadLine() ?? string.Empty;

                while (response.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                {
                    var newModule = modulehelper.CreateModuleRecord();
                    moduleService.Add(newModule);
                    modules.Add(newModule);

                    Console.WriteLine("Add more modules to course? (y/n)");
                    response = Console.ReadLine() ?? string.Empty;
                }

                selectedCourse.Roster = new List<Person>();
                selectedCourse.Roster = roster;
                selectedCourse.Assignments = assignments;
                selectedCourse.Modules = modules;
            }

            if (isNewCourse)
            {
                courseService.Add(selectedCourse);
            }

            return selectedCourse;
        }

        public void UpdateCourseRecord()
        {
            Console.WriteLine("Enter the code for the course to update");
            ListCourses();

            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.CurrentCultureIgnoreCase));
            if (selectedCourse != null)
            {
                CreateCourseRecord(selectedCourse);
            }

        }

        public void ListStudentCourse()
        {
            Console.WriteLine("Enter the name for the course to update");
            ListCourses();

            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.CurrentCultureIgnoreCase));
            if (selectedCourse != null)
            {
                CreateCourseRecord(selectedCourse);
            }

        }

        public void ListCourses()
        {
            courseService.Courses.ForEach(Console.WriteLine);
        }

        public Course SearchCourses()
        {
            courseService.Courses.ForEach(Console.WriteLine);
            Console.WriteLine("Enter course code to search:");
            var query = Console.ReadLine() ?? string.Empty;

            //courseService.Search(query).ToList().ForEach(Console.WriteLine);

            var search = courseService.Search(query).ToList();

            //var selectedCourse = courseService.Courses.FirstOrDefault(c => c.Name.Equals(query, StringComparison.InvariantCultureIgnoreCase));
            //if (selectedCourse != null) { }

            foreach (var item in search)
            {
                if (item.Code == query)
                {
                    Console.WriteLine($"{item.Code} - {item.Name}\n{item.Description}\n");

                    Console.WriteLine("Roster:");

                    foreach (var x in item.Roster)
                        Console.WriteLine($"{x}");

                    Console.WriteLine("\nAssignments:");

                    foreach (var x in item.Assignments)
                        Console.WriteLine($"{x}");

                    Console.WriteLine("\nModules:");

                    foreach (var x in item.Modules)
                        Console.WriteLine($"{x}");


                    return item;
                }     
            }

            return null;

        }

    }
}
