﻿using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace App.LearningManagement.Helpers
{
    public class StudentHelper
    {
        private StudentService studentService;
        private CourseService courseService;

        public StudentHelper()
        {
            studentService = StudentService.Current;
            courseService = CourseService.Current;
        }
        public Person CreateStudentRecord(Student? selectedStudent = null)
        {
            Console.WriteLine("Enter student's ID:");
            var id = Console.ReadLine();

            Console.WriteLine("Enter student's name:");
            var name = Console.ReadLine();

            Console.WriteLine("Enter student's classification [(F)reshman, S(O)phomore, (J)unior, (S)enior]:");
            var classification = Console.ReadLine() ?? string.Empty;

            PersonClassification classEnum = PersonClassification.Freshman;

            if (classification.Equals("O", StringComparison.InvariantCultureIgnoreCase))
            {
                classEnum = PersonClassification.Sophomore;
            }
            else if (classification.Equals("J", StringComparison.InvariantCultureIgnoreCase))
            {
                classEnum = PersonClassification.Junior;
            }
            else if (classification.Equals("S", StringComparison.InvariantCultureIgnoreCase))
            {
                classEnum = PersonClassification.Senior;
            }

            Console.WriteLine("Enter student's grade:");
            var grades = Console.ReadLine() ?? string.Empty;


            bool isCreate = false;
            if (selectedStudent == null)
            {
                isCreate = true;
                selectedStudent = new Student();
            }

            selectedStudent.Id = int.Parse(id ?? "0");
            selectedStudent.Name = name ?? string.Empty;
            selectedStudent.Classification = classEnum;


            if (isCreate)
            {
                studentService.Add(selectedStudent);
            }

            return selectedStudent;
        }

        public void UpdateStudentRecord()
        {
            Console.WriteLine("Select student to update");
            ListStudents();

            var selectionStr = Console.ReadLine();

            if (int.TryParse(selectionStr, out int selectionInt))
            {
                var selectedStudent = studentService.Students.FirstOrDefault(s => s.Id == selectionInt);
                if (selectedStudent != null)
                {
                    CreateStudentRecord(selectedStudent);
                }
            }
        }

        public void ListStudents()
        {
            studentService.Students.ForEach(Console.WriteLine);

            /*
            Console.WriteLine("Select a student:");
            var selectionStr = Console.ReadLine();
            var selectionInt = int.Parse(selectionStr ?? "0");

            Console.WriteLine("Student Course List:");
            courseService.Courses.Where(c => c.Roster.Any(s => s.Id == selectionInt)).ToList().ForEach(Console.WriteLine);
            */
        }

        public void SearchStudents()
        {
            studentService.Students.ForEach(Console.WriteLine);
            Console.WriteLine("Enter student name to search:");
            var query = Console.ReadLine() ?? string.Empty;

            studentService.Search(query).ToList().ForEach(Console.WriteLine);

            /*
            Console.WriteLine("Select a student:");
            var selectionStr = Console.ReadLine();
            var selectionInt = int.Parse(selectionStr ?? "0");

            Console.WriteLine("Student Course List:");
            courseService.Courses.Where(c => c.Roster.Any(s => s.Id == selectionInt)).ToList().ForEach(Console.WriteLine);
            */
        }

        public void AddStudents()
        {
            var coursehelper = Program.courseHelper;

            studentService.Students.ForEach(Console.WriteLine);
            Console.WriteLine("Enter student ID to add:");

            var query = Console.ReadLine() ?? string.Empty;

            var find = studentService.Search(query).ToList();
            var pupil = new Person();

            foreach (var item in find)
            {
                if (item.Id == int.Parse(query))
                    pupil = item;
            }

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
                courseService.AddStudent(coure, pupil);
        }

        public void RemoveStudents()
        {
            var coursehelper = Program.courseHelper;
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

            Console.WriteLine(string.Join("",coure.Roster.Select(a => a.ToString()).ToArray()));

            Console.WriteLine("Enter student ID to remove:");

            var query = Console.ReadLine() ?? string.Empty;

            var find = studentService.Search(query).ToList();
            var pupil = new Person();

            foreach (var item in find)
            {
                if (item.Id == int.Parse(query))
                    pupil = item;
            }      



            if (search is null)
                Console.WriteLine("Course not found!");
            else
                courseService.RemoveStudent(coure, pupil);
        }

        public void ListStudentCourse()
        {
            Console.WriteLine("Enter student ID:");
            var selectionStr = Console.ReadLine();
            var selectionInt = int.Parse(selectionStr ?? "0");

            Console.WriteLine("Student Course List:");
            courseService.Courses.Where(c => c.Roster.Any(s => s.Id == selectionInt)).ToList().ForEach(Console.WriteLine);

        }
    }
}