using ClassLibrary;
using ClassLibrary.Models;
using Library.LearningManagement.Models;
using System.Diagnostics;
using System.Xml.Linq;

namespace App.LearningManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var courseList = new List<Course>();
            var studentList = new List<Person>();
            bool cont = true;

            while (cont)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Create a course");
                Console.WriteLine("2. Create a student");
                Console.WriteLine("3. Add student to course");
                Console.WriteLine("4. Remove student from course");
                Console.WriteLine("5. List all courses");
                Console.WriteLine("6. Search for course");
                Console.WriteLine("7. List all students");
                Console.WriteLine("8. Search for student");
                Console.WriteLine("9. List all courses a student is taking");
                Console.WriteLine("10. Update a course's information");
                Console.WriteLine("11. Update a student's information");
                Console.WriteLine("12. Create an assignment");
                Console.WriteLine("13. Exit");

                string choice = Console.ReadLine() ?? string.Empty;

                if (int.TryParse(choice, out int choiceInt))
                {
                    if (choiceInt == 1)
                    {
                        var newCourse = new Course();

                        Console.WriteLine("Enter class code:");
                        newCourse.Code = Console.ReadLine() ?? string.Empty;

                        Console.WriteLine("Enter class name:");
                        newCourse.Name = Console.ReadLine() ?? string.Empty;

                        Console.WriteLine("Enter class description:");
                        newCourse.Description = Console.ReadLine() ?? string.Empty;

                        Console.WriteLine("Add students to roster:");
                        string response = "Y";

                        while (response.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                        {
                            var newStudent = new Person();

                            Console.WriteLine("Enter student's name:");
                            newStudent.Name = Console.ReadLine() ?? string.Empty;

                            Console.WriteLine("Enter student's classification:");
                            newStudent.Classification = Console.ReadLine() ?? string.Empty;

                            Console.WriteLine("Enter student's grade:");
                            newStudent.Grades = Console.ReadLine() ?? string.Empty;

                            newCourse.Roster.Add(newStudent);
                            studentList.Add(newStudent);

                            Console.WriteLine("Add more students to roster? (y/n)");
                            response = Console.ReadLine() ?? string.Empty;
                        }

                        Console.WriteLine("Create assignments for course:");
                        response = "Y";

                        while (response.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                        {
                            var newAssignment = new Assignment();

                            Console.WriteLine("Enter assignment name:");
                            newAssignment.Name = Console.ReadLine() ?? string.Empty;

                            Console.WriteLine("Enter assignment description:");
                            newAssignment.Description = Console.ReadLine() ?? string.Empty;

                            Console.WriteLine("Enter the total available points for this assignment:");
                            newAssignment.TotalAvailablePoints = Console.ReadLine() ?? string.Empty;

                            Console.WriteLine("Enter the due date for this assignment:");
                            newAssignment.DueDate = Console.ReadLine() ?? string.Empty;

                            newCourse.Assignments.Add(newAssignment);

                            Console.WriteLine("Add more assignments to course? (y/n)");
                            response = Console.ReadLine() ?? string.Empty;
                        }

                        Console.WriteLine("Create modules for course:");
                        response = "Y";

                        while (response.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                        {
                            var newModule = new Module();

                            Console.WriteLine("Enter module name:");
                            newModule.Name = Console.ReadLine() ?? string.Empty;

                            Console.WriteLine("Enter module description:");
                            newModule.Description = Console.ReadLine() ?? string.Empty;

                            Console.WriteLine("Create content for module:");
                            response = "Y";

                            while (response.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                            {
                                var newContentItem = new ContentItem();

                                Console.WriteLine("Enter content item name:");
                                newContentItem.Name = Console.ReadLine() ?? string.Empty;

                                Console.WriteLine("Enter content item description:");
                                newContentItem.Description = Console.ReadLine() ?? string.Empty;

                                Console.WriteLine("Enter content item path:");
                                newContentItem.Path = Console.ReadLine() ?? string.Empty;

                                newModule.Content.Add(newContentItem);

                                Console.WriteLine("Add more content items to module? (y/n)");
                                response = Console.ReadLine() ?? string.Empty;
                            }

                            newCourse.Modules.Add(newModule);

                            Console.WriteLine("Add more modules to course? (y/n)");
                            response = Console.ReadLine() ?? string.Empty;
                        }

                        courseList.Add(newCourse);

                    }
                    else if (choiceInt == 2)
                    {
                        var newStudent = new Person();

                        Console.WriteLine("Enter student's name:");
                        newStudent.Name = Console.ReadLine() ?? string.Empty;

                        Console.WriteLine("Enter student's classification:");
                        newStudent.Classification = Console.ReadLine() ?? string.Empty;

                        Console.WriteLine("Enter student's grade:");
                        newStudent.Grades = Console.ReadLine() ?? string.Empty;

                        studentList.Add(newStudent);

                    }
                    else if (choiceInt == 5)
                    {
                        courseList.ForEach(number => Console.WriteLine(number));
                    }
                    else if (choiceInt == 6)
                    {
                        Console.WriteLine("Enter course name or description to search:");
                        var query = Console.ReadLine();

                        var filteredCourse = courseList
                            .Where(t =>
                            (((t is Course) && (t.Name.Contains(query, StringComparison.InvariantCultureIgnoreCase)
                            || t.Description.Contains(query, StringComparison.InvariantCultureIgnoreCase))
                            )));

                        filteredCourse.ToList().ForEach(course => Console.WriteLine($"{course.Code} - {course.Name} \n {course.Description}" +
                            $"\n Roster: {course.Roster} \n Assignments: {course.Assignments} \n Modules: {course.Modules} "));

                    }
                    else if (choiceInt == 7)
                    {
                        studentList.ForEach(number => Console.WriteLine(number));
                    }
                    else if (choiceInt == 8)
                    {
                        Console.WriteLine("Enter student name to search:");
                        var query = Console.ReadLine();

                        var filteredStudents = studentList
                            .Where(t => ((t is Person) && 
                            (t.Name.Contains(query, StringComparison.InvariantCultureIgnoreCase))));

                        filteredStudents.ToList().ForEach(student => Console.WriteLine(student));
                    }
                    else if (choiceInt == 13)
                    {
                        cont = false;
                    }
                }
            }
        }
    }
}