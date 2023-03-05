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
        private ListNavigator<Course> courseNavigator;

        public CourseHelper()
        {
            studentService = StudentService.Current;
            courseService = CourseService.Current;

            courseNavigator = new ListNavigator<Course>(courseService.Courses);
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
                var entering = true;
                while(entering)
                {
                    Console.WriteLine("Enter class code:");
                    var codeChoice = Console.ReadLine() ?? string.Empty;

                    var code = courseService.Courses.FirstOrDefault(s => s.Code.Equals(codeChoice, StringComparison.CurrentCultureIgnoreCase));
                    if (code == null)
                    {
                        selectedCourse.Code = codeChoice;
                        entering = false;
                    }
                    else
                    {
                        Console.WriteLine("Course code already used, try again!");
                    }
                }                
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
            courseService.Courses.ForEach(Console.WriteLine);

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

        private Course NavigateCourses(string? query = null)
        {
            ListNavigator<Course>? currentNavigator = null;
            if (query == null)
            {
                currentNavigator = courseNavigator;
            }
            else
            {
                currentNavigator = new ListNavigator<Course>(courseService.Search(query).ToList());
            }

            if (currentNavigator.HasNavigator)
            {
                bool keepPaging = true;
                while (keepPaging)
                {
                    Console.WriteLine("Make a selection:");
                    foreach (var pair in currentNavigator.GetCurrentPage())
                    {
                        Console.WriteLine($"{pair.Key}. {pair.Value}");
                    }

                    if (currentNavigator.HasPreviousPage && currentNavigator.HasNextPage)
                    {
                        Console.WriteLine("<- (P)revious\t(Q)uit\t(N)ext ->");
                    }
                    else if (currentNavigator.HasNextPage)
                    {
                        Console.WriteLine("   (Q)uit\t(N)ext ->");
                    }
                    else if (currentNavigator.HasPreviousPage)
                    {
                        Console.WriteLine("<- (P)revious\t(Q)uit");
                    }
                    else
                    {
                        Console.WriteLine("   (Q)uit");
                    }
                    var selectionStr = Console.ReadLine();

                    if ((selectionStr?.Equals("P", StringComparison.InvariantCultureIgnoreCase) ?? false)
                        || (selectionStr?.Equals("N", StringComparison.InvariantCultureIgnoreCase) ?? false)
                        || (selectionStr?.Equals("Q", StringComparison.InvariantCultureIgnoreCase) ?? false))
                    {
                        if (selectionStr.Equals("P", StringComparison.InvariantCultureIgnoreCase))
                        {
                            currentNavigator.GoBackward();
                        }
                        else if (selectionStr.Equals("N", StringComparison.InvariantCultureIgnoreCase))
                        {
                            currentNavigator.GoForward();
                        }
                        else if (selectionStr.Equals("Q", StringComparison.InvariantCultureIgnoreCase))
                        {
                            keepPaging = false;
                        }
                    }
                    else
                    {
                        var search = courseService.Search(selectionStr ?? "").ToList();

                        foreach (var item in search)
                        {
                            if (item.Code == selectionStr)
                            {
                                Console.WriteLine($"{item.Code} - {item.Name}\n{item.Description}\n");

                                Console.WriteLine("Roster:");

                                foreach (var x in item.Roster)
                                    Console.WriteLine($"{x}");

                                Console.WriteLine("\nAssignments:");

                                foreach (var x in item.Assignments)
                                {
                                    Console.WriteLine($"{x}");

                                    Console.Write($"\tSubmissions:");

                                    foreach (var i in x.Submissions)
                                    {
                                        Console.Write($"\n\t{i}");
                                        Console.Write($" ({i.Grades[x.Id]})");
                                    }
                                }

                                Console.WriteLine("\n\nModules:");

                                foreach (var x in item.Modules)
                                    Console.WriteLine($"{x}");

                                Console.Write($"\n");
                                return item;
                            }
                        }
                        return null;
                        keepPaging = false;
                    }
                }
            }
            return null;
        }

        public void ListCourses()
        {
            NavigateCourses();
            //courseService.Courses.ForEach(Console.WriteLine);
        }

        public Course SearchCourses()
        {
            //courseService.Courses.ForEach(Console.WriteLine);
            Console.WriteLine("Enter course code to search:");
            var query = Console.ReadLine() ?? string.Empty;

            //courseService.Search(query).ToList().ForEach(Console.WriteLine);
            return NavigateCourses();
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
            if (selectedCourse != null && selectedCourse.Modules.Any())
            {
                Console.WriteLine("Choose the id for the module you want to update:");
                selectedCourse.Modules.ForEach(Console.WriteLine);

                selection = Console.ReadLine();
                var selectedModule = selectedCourse.Modules.FirstOrDefault(m => m.Id.ToString().Equals(selection, StringComparison.InvariantCultureIgnoreCase));

                if (selectedModule != null)
                {
                    Console.WriteLine("Update module name?");
                    selection = Console.ReadLine();
                    if (selection?.Equals("Y", StringComparison.InvariantCultureIgnoreCase) ?? false)
                    {
                        Console.WriteLine("Enter module name:");
                        selectedModule.Name = Console.ReadLine();
                    }

                    Console.WriteLine("Update module description?");
                    selection = Console.ReadLine();
                    if (selection?.Equals("Y", StringComparison.InvariantCultureIgnoreCase) ?? false)
                    {
                        Console.WriteLine("Enter module description:");
                        selectedModule.Description = Console.ReadLine();
                    }

                    Console.WriteLine("Delete content items in module?");
                    selection = Console.ReadLine();
                    if (selection?.Equals("Y", StringComparison.InvariantCultureIgnoreCase) ?? false)
                    {
                        var keepRemoving = true;

                        while (keepRemoving)
                        {
                            selectedModule.Content.ForEach(Console.WriteLine);
                            selection = Console.ReadLine();

                            var contentToRemove = selectedModule.Content.FirstOrDefault(c => c.Id.ToString().Equals(selection, StringComparison.InvariantCultureIgnoreCase));
                            if (contentToRemove != null)
                            {
                                selectedModule.Content.Remove(contentToRemove);
                            }

                            Console.WriteLine("Remove more content?");
                            selection = Console.ReadLine();
                            if (selection?.Equals("N", StringComparison.InvariantCultureIgnoreCase) ?? false)
                            {
                                keepRemoving = false;
                            }
                        }

                    }

                    Console.WriteLine("Create content for module? (y/n):");
                    var response = Console.ReadLine() ?? string.Empty;

                    while (response.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                    {
                        Console.WriteLine("What type of content would you like to add?");
                        Console.WriteLine("1. Assignment");
                        Console.WriteLine("2. File");
                        Console.WriteLine("3. Page");
                        var contentChoice = int.Parse(Console.ReadLine() ?? "0");

                        switch (contentChoice)
                        {
                            case 1:
                                var newAssignmentContent = moduleHelper.CreateAssignmentItem(selectedCourse);
                                if (newAssignmentContent != null)
                                {
                                    selectedModule?.Content.Add(newAssignmentContent);
                                }
                                break;
                            case 2:
                                var newFileContent = moduleHelper.CreateFileItem(selectedCourse);
                                if (newFileContent != null)
                                {
                                    selectedModule?.Content.Add(newFileContent);
                                }
                                break;
                            case 3:
                                var newPageContent = moduleHelper.CreatePageItem(selectedCourse);
                                if (newPageContent != null)
                                {
                                    selectedModule?.Content.Add(newPageContent);
                                }
                                break;
                            default:
                                break;
                        }

                        Console.WriteLine("Add more content items to module? (y/n)");
                        response = Console.ReadLine() ?? string.Empty;
                    }
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
