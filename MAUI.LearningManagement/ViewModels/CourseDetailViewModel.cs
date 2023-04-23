using Library.LearningManagement.Database;
using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MAUI.LearningManagement.ViewModels
{
    class CourseDetailViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public string Prefix { get; set; }

        public int Id { get; set; }

        public int CourseCode { get;}

        public List<Person> Roster { get; set; }
        /*
        public string Name
        {
            get => course?.Name ?? string.Empty;
            set { if (course != null) course.Name = value; }
        }
        public string Description
        {
            get => course?.Description ?? string.Empty;
            set { if (course != null) course.Description = value; }
        }
        public string Prefix
        {
            get => course?.Prefix ?? string.Empty;
            set { if (course != null) course.Prefix = value; }
        }
        public int Id { get; set; }

        public string CourseCode
        {
            get => course?.Code ?? string.Empty;
        }
        */
        public CourseDetailViewModel(int id=0)
        {
            if (id > 0)
            {
                LoadById(id);
            }

            //course = new Course();
        }

        public void LoadById(int id)
        {
            if (id == 0) { return; }
            var course = CourseService.Current.GetById(id);
            if (course != null)
            {
                Name = course.Name;
                Description = course.Description;
                Prefix = course.Prefix;
                Id = course.Id;



                Roster= course.Roster;
            }

            NotifyPropertyChanged(nameof(Name));
            NotifyPropertyChanged(nameof(Description));
            NotifyPropertyChanged(nameof(Prefix));
            NotifyPropertyChanged(nameof(Roster));
        }

        private Course course;

        public void AddCourse()
         {
            if (Id <= 0)
            {
                //CourseService.Current.Add(new Course { Name = Name, Description = Description, Prefix = Prefix });
                var course = new Course { Name = Name, Description = Description, Prefix = Prefix };
                course.Roster.Add(SelectedPerson);
                CourseService.Current.Add(course);
            }
            else
            {
                var refToUpdate = CourseService.Current.GetById(Id);
                refToUpdate.Name = Name;
                refToUpdate.Description = Description;
                refToUpdate.Prefix = Prefix;
            }
            Shell.Current.GoToAsync("//Instructor");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<Person> People
        {
            get
            {
                return new ObservableCollection<Person>(StudentService.Current.Students);
            }
        }
        
        public ObservableCollection<Person> Students
        {
            get
            {
                if (Id > 0)
                {
                    var refToUpdate = CourseService.Current.GetById(Id);
                    //FakeDatabase.People.Where(p => p is Student).Select(p => p as Student);
                    //FakeDatabase.Courses.Where(c => c is Course).Where(p => p is )
                    //return new ObservableCollection<Person>(StudentService.Current.Students);

                    

                    return new ObservableCollection<Person>(refToUpdate.Roster);

                }
                return null;
            }
        }

        public Person SelectedPerson { get; set; }
        public void AddEnrollmentClick(int courseId)
        {
            var refToUpdate = CourseService.Current.GetById(courseId);

            //var idParam = SelectedPerson?.Id ?? 0;
            if (refToUpdate != null)
            {
                CourseService.Current.AddStudent(refToUpdate, SelectedPerson);
                NotifyPropertyChanged(nameof(Roster));
            }
            //s.GoToAsync($"//PersonDetail?personId={idParam}");
            AddCourse();
        }

        public void RemoveEnrollmentClick(int courseId)
        {
            var refToUpdate = CourseService.Current.GetById(courseId);

            //var idParam = SelectedPerson?.Id ?? 0;
            if (refToUpdate != null)
            {
                CourseService.Current.RemoveStudent(refToUpdate, SelectedPerson);
                NotifyPropertyChanged(nameof(Roster));
            }
            //s.GoToAsync($"//PersonDetail?personId={idParam}");
            AddCourse();
        }

    }
}