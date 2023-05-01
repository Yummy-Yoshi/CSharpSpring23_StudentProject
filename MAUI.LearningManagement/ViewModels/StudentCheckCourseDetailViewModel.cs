using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI.LearningManagement.ViewModels
{
    public class StudentCheckCourseDetailViewModel
    {
        public int CourseId;
        public int StudentId;
        public Course Course { get; set; }
        public Student Student { get; set; }
        public StudentCheckCourseDetailViewModel(int courseId = 0, int studentId = 0)
        {
            if (courseId > 0)
            {
                CourseId = courseId;
                Course = CourseService.Current.GetById(CourseId);
            }
            if (studentId > 0)
            {
                StudentId = studentId;
                Student = (Student)StudentService.Current.GetById(StudentId);
            }
        }
        public string Title { get => $"Course: {Course.Code} {Course.Name} [{Course.CreditHours} Credits]"; }
        public string Grade
        { 
            get
            {
                if (Student.CourseAverage.ContainsKey(Course.Code))
                {
                    return $"{Student.Name}'s grade in {Course.Name}: {Student.CourseAverage[Course.Code]}";
                }
                else
                    return "";
            }
        }
        public string Description { get => $"{Course.Description}"; }
        public string Info { get => $"Taught in: {Course.Semester} -- Located at: {Course.Room}"; }
        public string Roster { get => $"Roster:\n {string.Join("\n", Course.Roster.Select(s => s.ToString()).ToArray())}"; }
        public string AssignmentGroups { get => $"Assignment Groups:\n {string.Join("\n", Course.AssignmentGroups.Select(s => s.ToString()).ToArray())}"; }
        public string Assignments { get => $"Assignments:\n {string.Join("\n", Course.Assignments.Select(s => s.ToString()).ToArray())}"; }
        public string Modules { get => $"Modules:\n {string.Join("\n", Course.Modules.Select(s => s.ToString()).ToArray())}"; }
        public string Announcements { get => $"Announcements:\n {string.Join("\n", Course.Announcements.Select(s => s.ToString()).ToArray())}"; }


        /*
        public Course CourseOutput
        {
            get
            {

                var item = Course;
                   

                        Console.WriteLine($"{item.Code} - {item.Name} ({item.CreditHours} Credits)\n{item.Description}\n");

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
                                //Console.Write($" ({i.Grades[x.Id]})");
                            }
                            Console.Write($"\n");
                        }

                        Console.WriteLine("\nModules:");

                        foreach (var x in item.Modules)
                            Console.WriteLine($"{x}");
         
                        Console.WriteLine("\nAnnouncements:");

                        foreach (var x in item.Announcements)
                            Console.WriteLine($"{x}");
                        Console.Write($"\n");


                return item;
            }
        
            
        }*/

    }

}
