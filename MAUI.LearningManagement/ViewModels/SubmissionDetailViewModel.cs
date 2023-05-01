using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MAUI.LearningManagement.ViewModels
{
    public class SubmissionDetailViewModel
    {
        public double Grade { get; set; }
        public Student Student { get; set; }
        public int Id { get; set; }

        public int AssignmentId;
        public int AssignmentGroupId;
        public int CourseId;

        public SubmissionDetailViewModel(int assignmentId = 0, int id = 0, int assignmentGroupId = 0, int courseId = 0)
        {
            if (id > 0)
            {
                LoadById(id);
            }

            if (assignmentId > 0)
            {
                AssignmentId = assignmentId;
            }

            if (assignmentGroupId > 0)
            {
                AssignmentGroupId = assignmentGroupId;
            }

            if (courseId > 0)
            {
                CourseId = courseId;
            }
        }
        public void LoadById(int id)
        {
            if (id == 0) { return; }
            var submission = SubmissionService.Current.GetById(id);
            if (submission != null)
            {
                Grade = submission.Grade;
                Student = submission.Student;
                Id = submission.Id;
            }
            NotifyPropertyChanged(nameof(Grade));
            NotifyPropertyChanged(nameof(Student));
        }
        public void AddSubmission()
        {
            if (Id <= 0)
            {
                var submission = new Submission { Grade = Grade, Student = Student };
                if (SelectedStudent != null)
                {
                    submission.Student = SelectedStudent;
                }
                SubmissionService.Current.Add(submission);
                if (!submission.Student.Grades.ContainsKey(AssignmentId))
                {
                    submission.Student.Grades.Add(AssignmentId, Grade);
                }
                else
                {
                    submission.Student.Grades[AssignmentId] = Grade;
                }

                var refToUpdate = AssignmentService.Current.GetById(AssignmentId);
                AssignmentService.Current.AddSubmission(refToUpdate, submission);
            }
            else
            {
                var refToUpdate = SubmissionService.Current.GetById(Id);
                refToUpdate.Grade = Grade;
                refToUpdate.Student= Student;
                if (SelectedStudent != null)
                {
                    refToUpdate.Student = Student;
                }
                if (!refToUpdate.Student.Grades.ContainsKey(AssignmentId))
                {
                    refToUpdate.Student.Grades.Add(AssignmentId, Grade);
                }
                else
                {
                    refToUpdate.Student.Grades[AssignmentId] = Grade;
                }

                var assignment = AssignmentService.Current.GetById(AssignmentId);
                AssignmentService.Current.RemoveSubmission(assignment, refToUpdate);
                AssignmentService.Current.AddSubmission(assignment, refToUpdate);
            }
            if (SelectedStudent != null)
            {
                var student = SelectedStudent;

                if (student != null)
                {
                    double grades = 0;
                    double total = 0;
                    double assignments = 0;
                    double weight = 0;

                    var course = CourseService.Current.GetById(CourseId);

                    foreach (var assignmentGroup in course.AssignmentGroups)
                    {
                        grades = 0;
                        assignments = 0;
                        foreach (var assignment in assignmentGroup.Assignments)
                        {
                            grades += ((student.Grades[assignment.Id]) / (double)assignment.TotalAvailablePoints);
                            assignments++;

                        }
                        grades = ((grades / assignments) * 100) * (double)assignmentGroup.Weight;
                        weight += (double)assignmentGroup.Weight;
                        total += grades;
                    }
                    total = total / weight;

                    Student stu = (Student)StudentService.Current.GetById(student.Id);

                    if (!stu.CourseAverage.ContainsKey(course.Code))
                    {
                        stu.CourseAverage.Add(course.Code, total);

                        StudentService.Current.Remove(stu);
                        StudentService.Current.Add(stu);
                    }
                    else
                    {
                        stu.CourseAverage[course.Code] = total;
                        StudentService.Current.Remove(stu);
                        StudentService.Current.Add(stu);
                    }

                }
                if (student != null)
                {
                    double points = 0;
                    double totalHours = 0;
                    double hours = 0;
                    double totalPoints = 0;

                    foreach (var assignment in student.CourseAverage)
                    {
                        var course = CourseService.Current.GetById(CourseId);

                        if (course != null)
                        {
                            hours = course.CreditHours;

                            if (assignment.Value >= 93)
                                points = 4.0;
                            else if (assignment.Value >= 90)
                                points = 3.7;
                            else if (assignment.Value >= 87)
                                points = 3.3;
                            else if (assignment.Value >= 83)
                                points = 3.0;
                            else if (assignment.Value >= 80)
                                points = 2.7;
                            else if (assignment.Value >= 77)
                                points = 2.3;
                            else if (assignment.Value >= 73)
                                points = 2.0;
                            else if (assignment.Value >= 70)
                                points = 1.7;
                            else if (assignment.Value >= 67)
                                points = 1.3;
                            else if (assignment.Value >= 65)
                                points = 1.0;
                        }
                        totalPoints += hours * points;
                        totalHours += hours;
                    }
                    Student stu = (Student)StudentService.Current.GetById(student.Id);
                    stu.GPA = totalPoints / totalHours;
                    StudentService.Current.Remove(stu);
                    StudentService.Current.Add(stu);
                }
            }
            Shell.Current.GoToAsync($"//AssignmentDetail?assignmentId={AssignmentId}&assignmentgroupId={AssignmentGroupId}&courseId={CourseId}");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Student SelectedStudent { get; set; }

        public ObservableCollection<Person> Students
        {
            get
            {     
                    var course = CourseService.Current.GetById(CourseId);

                    var filteredList = course.Roster.Where(s => s is Student);

                    return new ObservableCollection<Person>(filteredList);
            }
        }
    }
}
