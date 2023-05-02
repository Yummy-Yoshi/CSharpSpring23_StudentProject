using Library.LearningManagement.DTO;
using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using MAUI.LearningManagement.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MAUI.LearningManagement.ViewModels
{
    public class InstructorViewViewModel : INotifyPropertyChanged
    {
        public InstructorViewViewModel()
        {
            IsEnrollmentsVisible = true;
            IsCoursesVisible = false;
        }
        /*public ObservableCollection<Person> People
        {
            get
            {

                var filteredList = StudentService
                    .Current
                    .Students
                    .Where(
                    s => s.Name.ToUpper().Contains(Query?.ToUpper() ?? string.Empty));
                return new ObservableCollection<Person>(filteredList);

            }
        }*/

        public IEnumerable<PersonViewModel> People
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5112/Person").Result;
                var returnVal = JsonConvert.DeserializeObject<List<PersonDTO>>(payload).Select(d => new PersonViewModel(d));
                return returnVal;
            }
        }

        public ObservableCollection<Course> Courses
        {
            get
            {
                var filteredList = CourseService
                    .Current
                    .Courses
                    .Where(
                    c => c.Name.ToUpper().Contains(Query?.ToUpper() ?? string.Empty));
                return new ObservableCollection<Course>(filteredList);
            }
        }

        public string Title { get => "Instructor / Administrator Menu"; }

        public bool IsEnrollmentsVisible
        {
            get; set;
        }

        public bool IsCoursesVisible
        {
            get; set;
        }

        public void ShowEnrollments()
        {
            IsEnrollmentsVisible = true;
            IsCoursesVisible = false;
            NotifyPropertyChanged("IsEnrollmentsVisible");
            NotifyPropertyChanged("IsCoursesVisible");
        }

        public void ShowCourses()
        {
            IsEnrollmentsVisible = false;
            IsCoursesVisible = true;
            NotifyPropertyChanged("IsEnrollmentsVisible");
            NotifyPropertyChanged("IsCoursesVisible");
        }
        public Person SelectedPerson { get; set; }
        public Course SelectedCourse { get; set; }

        private string query;
        public string Query
        {
            get => query;
            set
            {
                query = value;
                NotifyPropertyChanged(nameof(People));
                NotifyPropertyChanged(nameof(Courses));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddEnrollmentClick(Shell s)
        {
            var idParam = SelectedPerson?.Id ?? 0;
            s.GoToAsync($"//PersonDetail?personId={idParam}");
        }

        public void AddCourseClick(Shell s)
        {
            var idParam = 0;
            s.GoToAsync($"//CourseDetail?courseId={idParam}");
        }
        public void EditCourseClick(Shell s)
        {
            var idParam = SelectedCourse?.Id ?? 0;
            s.GoToAsync($"//CourseDetail?courseId={idParam}");
        }

        public void RemoveEnrollmentClick()
        {
            if (SelectedPerson == null) { return; }

            StudentService.Current.Remove(SelectedPerson);
            RefreshView();
        }

        public void RemoveCourseClick()
        {
            if (SelectedCourse == null) { return; }

            CourseService.Current.Remove(SelectedCourse);
            RefreshView();
        }

        public void RefreshView()
        {
            NotifyPropertyChanged(nameof(People));
            NotifyPropertyChanged(nameof(Courses));
        }

    }
}