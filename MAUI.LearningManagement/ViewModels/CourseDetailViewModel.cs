using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
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
            }

            NotifyPropertyChanged(nameof(Name));
            NotifyPropertyChanged(nameof(Description));
            NotifyPropertyChanged(nameof(Prefix));
        }

        private Course course;

        public void AddCourse()
        {
            if (Id <= 0)
            {
                CourseService.Current.Add(new Course { Name = Name, Description = Description, Prefix = Prefix});
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
    }
}