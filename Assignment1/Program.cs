using App.LearningManagement.Helpers;
using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System.Diagnostics;
using System.Xml.Linq;

namespace App.LearningManagement
{
    internal class Program
    {
        public static StudentHelper studentHelper = new StudentHelper();
        public static CourseHelper courseHelper = new CourseHelper();
        public static ContentHelper contentHelper = new ContentHelper();
        public static ModuleHelper moduleHelper = new ModuleHelper();
        public static AssignmentHelper assignmentHelper = new AssignmentHelper();

        static void Main(string[] args)
        {
            var studenthelper = studentHelper;
            var coursehelper = courseHelper;

            bool cont = true;

            while (cont)
            {
                Console.WriteLine("1. Maintain Students");
                Console.WriteLine("2. Maintain Courses");
                Console.WriteLine("3. Exit");                            

                var input = Console.ReadLine();

                if (int.TryParse(input, out int result))
                {
                    if(result == 1)
                    {
                        ShowStudentMenu(studenthelper);
                    }
                    else if (result == 2)
                    {
                        ShowCourseMenu(coursehelper);
                    }
                    else if (result == 3)
                    {
                        cont = false;
                    }
                }
            }
        }

        static void ShowStudentMenu(StudentHelper studentHelper)
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Create a student");                           // student
            Console.WriteLine("2. Add student to course");                      // student & course
            Console.WriteLine("3. Remove student from course");                 // student & course
            Console.WriteLine("4. List all students");                          // student
            Console.WriteLine("5. Search for student");                         // student
            Console.WriteLine("6. List all courses a student is taking");       // student & course
            Console.WriteLine("7. Update a student's information");            // student
            var input = Console.ReadLine();
            if (int.TryParse(input, out int result))
            {
                if (result == 1)
                {
                    studentHelper.CreateStudentRecord();
                }
                else if (result == 2)
                {
                    studentHelper.AddStudents();
                }
                else if (result == 3)
                {
                    studentHelper.RemoveStudents();
                }
                else if (result == 4)
                {
                    studentHelper.ListStudents();
                }
                else if (result == 5)
                {
                    studentHelper.SearchStudents();
                }
                else if (result == 6)
                {
                    studentHelper.ListStudentCourse();
                }
                else if (result == 7)
                {
                    studentHelper.UpdateStudentRecord();
                }
            }

        }
        static void ShowCourseMenu(CourseHelper courseHelper)
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Create a course");
            Console.WriteLine("2. List all courses");               
            Console.WriteLine("3. Search for course");
            Console.WriteLine("4. Update a course's information");
            Console.WriteLine("5. Create an assignment");
            Console.WriteLine("6. Update an assignment");
            Console.WriteLine("7. Remove an assignment");
            var input = Console.ReadLine();
            if (int.TryParse(input, out int result))
            {
                if (result == 1)
                {
                    courseHelper.CreateCourseRecord();
                }
                else if (result == 2)
                {
                    courseHelper.ListCourses();
                }
                else if (result == 3)
                {
                    courseHelper.SearchCourses();
                }
                else if (result == 4)
                {
                    courseHelper.UpdateCourseRecord();
                }
                else if (result == 5)
                {
                    assignmentHelper.AddAssignment();
                }
                else if (result == 6)
                {
                    courseHelper.UpdateAssignment();
                }
                else if (result == 7)
                {
                    courseHelper.RemoveAssignment();
                }
            }
        }
    }
}