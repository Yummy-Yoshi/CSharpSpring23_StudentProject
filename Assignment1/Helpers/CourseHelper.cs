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
            var studenthelper = Program.studentHelper;
            var coursehelper = Program.courseHelper;
            var modulehelper = Program.moduleHelper;

            //var studentHelper = new StudentHelper();
            var assignmentHelper = new AssignmentHelper();
            //var roster = new List<Person>();
            var assignments = new List<Assignment>();
            //var modules = new List<Module>();

            Console.WriteLine("Enter class code:");
            var code = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Enter class name:");
            var name = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Enter class description:");
            var description = Console.ReadLine() ?? string.Empty;

            /*
            Console.WriteLine("Which students should be enrolled in this course? ('Q' to quit)");
            var roster = new List<Person>();
            bool continueAdding = true;

            while (continueAdding)
            {
                studentService.Students.Where(s => !roster.Any(s2 => s2.Id == s.Id)).ToList().ForEach(Console.WriteLine);
                var selection = "Q";
                
                if(studentService.Students.Any(s => !roster.Any(s2 => s2.Id => Id)))
                {
                    selection = Console.Readline() ?? string.Empty;
                }
              

                if(selection.Equals("Q", StringComparison.InvariantCultureIgnoreCase))
                {
                    continueAdding = false;
                }
                else
                {
                    var selectedId = int.Parse(selection);
                    var selectedStudent = studentService.Students.FirstOrDefault(s => s.Id == selectedId);

                    if (selectedStudent != null)
                    {
                        roster.Add(selectedStudent);
                    }
                }
            }
            */



            
            Console.WriteLine("Enter student in roster? (y/n):");
            string response = Console.ReadLine() ?? string.Empty;
            var roster = new List<Person>();
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
            var modules = new List<Library.LearningManagement.Models.Module>();

            while (response.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                var newModule = modulehelper.CreateModuleRecord();
                moduleService.Add(newModule);
                modules.Add(newModule);

                Console.WriteLine("Add more modules to course? (y/n)");
                response = Console.ReadLine() ?? string.Empty;
            }


            bool isNewCourse = false;
            if(selectedCourse == null)
            {
                isNewCourse = true;
                selectedCourse = new Course();
            }

            selectedCourse.Code = code;
            selectedCourse.Name = name;
            selectedCourse.Description = description;
            selectedCourse.Roster = new List<Person>();
            selectedCourse.Roster = roster;
            //selectedCourse.Roster.AddRange(roster);
            selectedCourse.Assignments = assignments;
            selectedCourse.Modules = modules;

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
