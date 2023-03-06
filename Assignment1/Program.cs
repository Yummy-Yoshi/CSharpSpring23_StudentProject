using App.LearningManagement.Helpers;
using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
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
                Console.WriteLine("1. Maintain People");
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
            Console.WriteLine("1. Create a person");                         
            Console.WriteLine("2. Add student to course");                    
            Console.WriteLine("3. Remove student from course");             
            Console.WriteLine("4. List all people");                      
            Console.WriteLine("5. Search for person");                       
            Console.WriteLine("6. List all courses a student is taking");      
            Console.WriteLine("7. Update a person's information");
            Console.WriteLine("8. Add a student's submission");
            Console.WriteLine("9. Update a student's submission");
            Console.WriteLine("10. Delete a student's submission");
            Console.WriteLine("11. Calculate a student's weighted grade");

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
                else if (result == 8)
                {
                    studentHelper.CreateStudentSubmission();
                }
                else if (result == 9)
                {
                    studentHelper.UpdateStudentSubmission();
                }
                else if (result == 10)
                {
                    studentHelper.DeleteStudentSubmission();
                }
                else if (result == 11)
                {
                    studentHelper.CalculateAverage();
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
            Console.WriteLine("8. Add a module to a course");
            Console.WriteLine("9. Remove a module from a course");
            Console.WriteLine("10. Update a module");
            Console.WriteLine("11. Create an assignment group");
            Console.WriteLine("12. List assignment groups for a course");
            Console.WriteLine("13. Create an announcement");
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
                else if (result == 8)
                {
                    courseHelper.AddModule();
                }
                else if (result == 9)
                {
                    courseHelper.RemoveModule();
                }
                else if (result == 10)
                {
                    courseHelper.UpdateModule();
                }
                else if (result == 11)
                {
                    courseHelper.CreateAssignmentGroup();
                }
                else if (result == 12)
                {
                    courseHelper.ListAssignmentGroups();
                }
                else if (result == 13)
                {
                    courseHelper.CreateAnnouncement();
                }
            }
        }
    }
}