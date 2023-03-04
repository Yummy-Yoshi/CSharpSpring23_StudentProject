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
                SetupRoster(selectedCourse);
                SetupAssignments(selectedCourse);
                SetupModules(selectedCourse);
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

                    Console.WriteLine($"\n");
                    return item;
                }     
            }
            return null;
        }

        public void AddModule()
        {
            var modulehelper = Program.moduleHelper;

            Console.WriteLine("Enter the code for the course to add the module to:");
            courseService.Courses.ForEach(Console.WriteLine);
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection));   
            if (selectedCourse != null)
            {
                var newModule = modulehelper.CreateModuleRecord(selectedCourse);
                moduleService.Add(newModule);
                selectedCourse.Modules.Add(newModule);
            }
        }

        public void RemoveModule()
        {
            Console.WriteLine("Enter the code for the course:");
            courseService.Courses.ForEach(Console.WriteLine);
            var selection = Console.ReadLine();

            var selectedCourse = (courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase)));

            if (selectedCourse != null)
            {
                Console.WriteLine("Choose an module to delete:");
                selectedCourse.Modules.ForEach(Console.WriteLine);
                var selectionStr = Console.ReadLine() ?? string.Empty;
                var selectionInt = int.Parse(selectionStr);
                var selectedModule = selectedCourse.Modules.FirstOrDefault(m => m.Id == selectionInt);
                if (selectedModule != null)
                {
                    selectedCourse.Modules.Remove(selectedModule);
                }
            }
        }

        public void UpdateModule()
        {
            var moduleHelper = new ModuleHelper();

            Console.WriteLine("Enter code for the course");
            courseService.Courses.ForEach(Console.WriteLine);
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection));
            if (selectedCourse != null)
            {
                Console.WriteLine("Choose a module to update:");
                selectedCourse.Modules.ForEach(Console.WriteLine);
                var selectionStr = Console.ReadLine() ?? string.Empty;
                var selectionInt = int.Parse(selectionStr);
                var selectedModule = selectedCourse.Modules.FirstOrDefault(m => m.Id == selectionInt);
                if (selectedModule != null)
                {
                    var index = selectedCourse.Modules.IndexOf(selectedModule);
                    selectedCourse.Modules.RemoveAt(index);
                    selectedCourse.Modules.Insert(index, (moduleHelper.CreateModuleRecord(selectedCourse)));
                }
            }
        }

            public void UpdateAssignment()
        {
            var assignmentHelper = new AssignmentHelper();

            Console.WriteLine("Enter code for the course");
            courseService.Courses.ForEach(Console.WriteLine);
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection));
            if (selectedCourse != null)
            {
                Console.WriteLine("Choose an assignment to update:");
                selectedCourse.Assignments.ForEach(Console.WriteLine);
                var selectionStr = Console.ReadLine() ?? string.Empty;
                var selectionInt = int.Parse(selectionStr);
                var selectedAssignment = selectedCourse.Assignments.FirstOrDefault(a => a.Id == selectionInt);
                if (selectedAssignment != null)
                {
                    var index = selectedCourse.Assignments.IndexOf(selectedAssignment);
                    selectedCourse.Assignments.RemoveAt(index);
                    selectedCourse.Assignments.Insert(index, (assignmentHelper.CreateAssignmentRecord()));
                }
            }
        }

        public void RemoveAssignment()
        {
            var assignmentHelper = new AssignmentHelper();

            Console.WriteLine("Enter code for the course");
            courseService.Courses.ForEach(Console.WriteLine);
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection));
            if (selectedCourse != null)
            {
                Console.WriteLine("Choose an assignment to delete:");
                selectedCourse.Assignments.ForEach(Console.WriteLine);
                var selectionStr = Console.ReadLine() ?? string.Empty;
                var selectionInt = int.Parse(selectionStr);
                var selectedAssignment = selectedCourse.Assignments.FirstOrDefault(a => a.Id == selectionInt);
                if (selectedAssignment != null)
                {
                    selectedCourse.Assignments.Remove(selectedAssignment);
                }
            }
        }

        private void SetupRoster(Course c)
        {
            var studenthelper = Program.studentHelper;

            Console.WriteLine("Enter student in roster? (y/n):");
            string response = Console.ReadLine() ?? string.Empty;
            while (response.Equals("Y", StringComparison.InvariantCultureIgnoreCase) || studentService.Students.Any(s => !c.Roster.Any(s2 => s2.Id == s.Id)))
            {
                var newStudent = studenthelper.CreateStudentRecord();
                c.Roster.Add(newStudent);

                Console.WriteLine("Add more students to roster? (y/n):");
                response = Console.ReadLine() ?? string.Empty;
            }
        }

        private void SetupAssignments(Course c)
        {
            var assignmentHelper = new AssignmentHelper();

            Console.WriteLine("Enter assignments for course? (y/n):");
            string response = Console.ReadLine() ?? string.Empty;

            while (response.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {

                c.Assignments.Add(assignmentHelper.CreateAssignmentRecord());

                Console.WriteLine("Add more assignments to course? (y/n):");
                response = Console.ReadLine() ?? string.Empty;
            }
        }

        private void SetupModules(Course c)
        {
            var modulehelper = Program.moduleHelper;

            Console.WriteLine("Enter modules for course? (y/n):");
            string response = Console.ReadLine() ?? string.Empty;

            while (response.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                var newModule = modulehelper.CreateModuleRecord(c);
                moduleService.Add(newModule);
                c.Modules.Add(newModule);

                Console.WriteLine("Add more modules to course? (y/n)");
                response = Console.ReadLine() ?? string.Empty;
            }
        }

    }
}
