using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace App.LearningManagement.Helpers
{
    public class StudentHelper
    {
        private StudentService studentService;
        private CourseService courseService;
        private ListNavigator<Person> studentNavigator;

        public StudentHelper()
        {
            studentService = StudentService.Current;
            courseService = CourseService.Current;

            studentNavigator = new ListNavigator<Person>(studentService.Students);
        }
        public Person CreateStudentRecord(Person? selectedStudent = null)
        {
            bool isCreate = false;
            if (selectedStudent == null)
            {
                isCreate = true;
                Console.WriteLine("What kind of person are you adding? [(S)tudent, (T)eachingAssistant, (I)nstructor]:");
                var choice = Console.ReadLine() ?? string.Empty;
                if(string.IsNullOrEmpty(choice))
                {
                    return null;
                }
                if(choice.Equals("S", StringComparison.InvariantCultureIgnoreCase))
                {
                    selectedStudent = new Student();
                }
                else if(choice.Equals("T", StringComparison.InvariantCultureIgnoreCase))
                {
                    selectedStudent = new TeachingAssistant();
                }
                else if (choice.Equals("I", StringComparison.InvariantCultureIgnoreCase))
                {
                    selectedStudent = new Instructor();
                }
            }

            
            //Console.WriteLine("Enter student's ID:");
            //var id = Console.ReadLine();

            Console.WriteLine("Enter student's name:");
            var name = Console.ReadLine();
            if (selectedStudent is Student)
            {
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

                //Console.WriteLine("Enter student's grade:");
                //var grades = Console.ReadLine() ?? string.Empty;

                var studentRecord = selectedStudent as Student;
                if (studentRecord != null)
                {
                    //studentRecord.Id = int.Parse(id ?? "0");
                    studentRecord.Name = name ?? string.Empty;
                    studentRecord.Classification = classEnum;

                    if (isCreate)
                    {
                        studentService.Add(selectedStudent);
                    }
                }
            }
            else
            {
                if (selectedStudent != null)
                {
                    //selectedStudent.Id = int.Parse(id ?? "0");
                    selectedStudent.Name = name ?? string.Empty;
                    if (isCreate)
                    {
                        studentService.Add(selectedStudent);
                    }
                }
            }
            return selectedStudent;
        }

        public void UpdateStudentRecord()
        {
            Console.WriteLine("Select person to update");
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

        private void NavigateStudents(string? query = null)
        {
            ListNavigator<Person>? currentNavigator = null;
            if(query == null)
            {
                currentNavigator = studentNavigator;
            }
            else
            {
                currentNavigator = new ListNavigator<Person>(studentService.Search(query).ToList());
            }

            if(currentNavigator.HasNavigator)
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
                        var selectionInt = int.Parse(selectionStr ?? "0");

                        Console.WriteLine("Student Course List:");
                        courseService.Courses.Where(c => c.Roster.Any(s => s.Id == selectionInt)).ToList().ForEach(Console.WriteLine);
                        keepPaging = false;
                    }
                }
            }
        }

        public void ListStudents()
        {
            NavigateStudents();
            //studentService.Students.ForEach(Console.WriteLine);
        }

        public void SearchStudents()
        {
            //studentService.Students.ForEach(Console.WriteLine);
            Console.WriteLine("Enter student name to search:");
            var query = Console.ReadLine() ?? string.Empty;

            //studentService.Search(query).ToList().ForEach(Console.WriteLine);
            NavigateStudents(query);
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

        public void CreateStudentSubmission()
        {
            Console.WriteLine("Enter code for the course");
            courseService.Courses.ForEach(Console.WriteLine);
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection));

            if (selectedCourse != null)
            {
                Console.WriteLine("Which assignment to submit to?");
                selectedCourse.Assignments.ForEach(Console.WriteLine);
                var choice = int.Parse(Console.ReadLine() ?? "-1");
                var response = "Y";

                while (response.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                {
                    var assignment = selectedCourse.Assignments.FirstOrDefault(a => a.Id == choice);

                    Console.WriteLine("Which student's submission should be added?");

                    foreach (Person x in selectedCourse.Roster)
                    {
                        if (x is Student)
                            Console.WriteLine(x);
                    }

                    var Schoice = int.Parse(Console.ReadLine() ?? "-1");

                    if (Schoice >= 0)
                    {
                        var submission = (Student)selectedCourse.Roster.FirstOrDefault(r => r.Id == Schoice);
                        if (submission is Student)
                        {
                            Console.WriteLine($"Enter grade for submission (Max: {assignment.TotalAvailablePoints}):");
                            var grade = int.Parse(Console.ReadLine() ?? "-1");
                            submission.Grades.Add(assignment.Id, grade);

                            assignment.Submissions.Add(submission);
                        }
                        else
                            Console.WriteLine("Not a student!");
                    }

                    Console.WriteLine("Add another student submission? (y/n)");
                    response = Console.ReadLine() ?? string.Empty;
                }
            }
        }

        public void UpdateStudentSubmission()
        {
            Console.WriteLine("Enter code for the course");
            courseService.Courses.ForEach(Console.WriteLine);
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection));

            if (selectedCourse != null)
            {
                Console.WriteLine("Which assignment to update?");
                selectedCourse.Assignments.ForEach(Console.WriteLine);
                var choice = int.Parse(Console.ReadLine() ?? "-1");

                if (choice >= 0)
                {
                    var assignment = selectedCourse.Assignments.FirstOrDefault(a => a.Id == choice);
                    var response = "Y";

                    while (response.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                    {

                        Console.WriteLine("Which student's submission should be updated?");

                        foreach (var i in assignment.Submissions)
                        {
                            Console.Write($"{i}");
                            Console.Write($" ({i.Grades[assignment.Id]})\n");
                        }

                        var Schoice = int.Parse(Console.ReadLine() ?? "-1");

                        if (Schoice >= 0)
                        {
                            var submission = (Student)selectedCourse.Roster.FirstOrDefault(r => r.Id == Schoice);
                            if (submission is Student)
                            {
                                Console.WriteLine($"Enter grade for submission (Max: {assignment.TotalAvailablePoints}):");
                                var grade = int.Parse(Console.ReadLine() ?? "-1");
                                submission.Grades[assignment.Id] = grade;
                            }
                            else
                                Console.WriteLine("Not a student!");
                        }

                        Console.WriteLine("Update another student submission? (y/n)");
                        response = Console.ReadLine() ?? string.Empty;
                    }
                }
            }
        }

        public void DeleteStudentSubmission()
        {
            Console.WriteLine("Enter code for the course");
            courseService.Courses.ForEach(Console.WriteLine);
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection));

            if (selectedCourse != null)
            {
                Console.WriteLine("Which assignment?");
                selectedCourse.Assignments.ForEach(Console.WriteLine);
                var choice = int.Parse(Console.ReadLine() ?? "-1");

                if (choice >= 0)
                {
                    var assignment = selectedCourse.Assignments.FirstOrDefault(a => a.Id == choice);

                    Console.WriteLine("Which student's submission should be deleted?");

                    foreach (var i in assignment.Submissions)
                    {
                        Console.Write($"{i}");
                        Console.Write($" ({i.Grades[assignment.Id]})\n");
                    }

                    var Schoice = int.Parse(Console.ReadLine() ?? "-1");

                    if (Schoice >= 0)
                    {
                        var submission = assignment.Submissions.FirstOrDefault(r => r.Id == Schoice);
                        if (submission is Student)
                        {
                            assignment.Submissions.Remove(submission);
                        }
                        else
                            Console.WriteLine("Not a student!");
                    }
                }
            }
        }


        public void ListStudentCourse()
        {
            studentService.Students.ForEach(Console.WriteLine);
            Console.WriteLine("Enter student ID:");
            var selectionStr = Console.ReadLine();
            var selectionInt = int.Parse(selectionStr ?? "0");

            Console.WriteLine("Student Course List:");
            courseService.Courses.Where(c => c.Roster.Any(s => s.Id == selectionInt)).ToList().ForEach(Console.WriteLine);
        }
    }
}
