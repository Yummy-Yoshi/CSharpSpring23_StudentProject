using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.LearningManagement.Helpers
{
    public class AssignmentHelper
    {
        private AssignmentService assignmentService;
        private CourseService courseService;

        public AssignmentHelper()
        {
            assignmentService = AssignmentService.Current;
            courseService = CourseService.Current;
        }

        public Assignment CreateAssignmentRecord()
        {
            Console.WriteLine("Enter assignment name:");
            var name = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Enter assignment description:");
            var description = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Enter the total available points for this assignment:");
            decimal totalAvailablePoints = decimal.Parse(Console.ReadLine() ?? "100");

            Console.WriteLine("Enter the due date for this assignment:");
            DateTime dueDate = DateTime.Parse(Console.ReadLine() ?? "01/01/2023");

            var assignment = new Assignment
            {
                Name = name,
                Description = description,
                TotalAvailablePoints = totalAvailablePoints,
                DueDate = dueDate,
            };

            return assignment;

            assignmentService.Add(assignment);
        }

        public void AddAssignment()
        {
            var coursehelper = Program.courseHelper;

            var assign = CreateAssignmentRecord();

            courseService.Courses.ForEach(Console.WriteLine);
            Console.WriteLine("Enter course code:");
            var query2 = Console.ReadLine() ?? string.Empty;

            var search = courseService.Search(query2).ToList();
            var coure = new Course();

            foreach (var item in search)
            {
                if (item.Code == query2)
                    coure = item;
            }

            if (search is null)
                Console.WriteLine("Course not found!");
            else
                courseService.AddAssignment(coure, assign);
        }

    }
}
