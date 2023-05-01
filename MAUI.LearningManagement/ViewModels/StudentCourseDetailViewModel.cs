using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Library.LearningManagement.Models.Course;

namespace MAUI.LearningManagement.ViewModels
{
    public class StudentCourseDetailViewModel : INotifyPropertyChanged
    {
        public int StudentId { get; set; }
        public string SemesterString { get; set; }

        public Student Student { get; set; }

        public StudentCourseDetailViewModel(int Studentid = 0)
        {
            IsSpringVisible = true;
            IsSummerVisible = false;
            IsFallVisible = false;

            StudentId = Studentid;
            Student = (Student)StudentService.Current.GetById(StudentId);
        }
        public ObservableCollection<Course> Courses
        {
            get
            {
                var semester = StringToClass(SemesterString);

                var student = StudentService.Current.GetById(StudentId);

                var filteredList = CourseService
                    .Current
                    .Courses.Where(c => c.Semester == semester)
                    .Where(
                    c => c.Roster.Contains(student));
                return new ObservableCollection<Course>(filteredList);
            }
        }
        public string Title { get => $"{Student.Name}'s Courses"; }
        
        public string GPA { get => $"GPA: {Student.GPA}"; }

        public bool IsSpringVisible
        {
            get; set;
        }

        public bool IsSummerVisible
        {
            get; set;
        }
        public bool IsFallVisible
        {
            get; set;
        }
        public void ShowSpring()
        {
            IsSpringVisible = true;
            IsFallVisible = false;
            IsSummerVisible = false;
            SemesterString = "P";
            NotifyPropertyChanged("IsSpringVisible");
            NotifyPropertyChanged("IsSummerVisible");
            NotifyPropertyChanged("IsFallVisible");
            NotifyPropertyChanged("SemesterString");
            NotifyPropertyChanged("Courses");
        }
        public void ShowSummer()
        {
            IsSpringVisible = false;
            IsFallVisible = false;
            IsSummerVisible = true;
            SemesterString = "S";
            NotifyPropertyChanged("IsSpringVisible");
            NotifyPropertyChanged("IsSummerVisible");
            NotifyPropertyChanged("IsFallVisible");
            NotifyPropertyChanged("SemesterString");
            NotifyPropertyChanged("Courses");
        }
        public void ShowFall()
        {
            IsSpringVisible = false;
            IsFallVisible = true;
            IsSummerVisible = false;
            SemesterString = "F";
            NotifyPropertyChanged("IsSpringVisible");
            NotifyPropertyChanged("IsSummerVisible");
            NotifyPropertyChanged("IsFallVisible");
            NotifyPropertyChanged("SemesterString");
            NotifyPropertyChanged("Courses");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private CourseSemester StringToClass(string s)
        {
            CourseSemester semester;
            switch (s)
            {
                case "S":
                    semester = CourseSemester.Summer;
                    break;
                case "F":
                    semester = CourseSemester.Fall;
                    break;
                case "P":
                default:
                    semester = CourseSemester.Spring;
                    break;
            }

            return semester;
        }
        public Course SelectedCourse { get; set; }
        public void CheckCoursesClick(Shell s)
        {
            var idParam = SelectedCourse?.Id ?? 0;
            s.GoToAsync($"//StudentCheckCourseDetail?studentId={StudentId}&courseId={idParam}");
        }
    }
}
