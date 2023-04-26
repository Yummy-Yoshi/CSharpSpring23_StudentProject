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
using System.Xml.Linq;

namespace MAUI.LearningManagement.ViewModels
{
    public class AnnouncementDetailViewModel
    {
        public string Title { get; set; }
        public Person Poster { get; set; }
        public string Info { get; set; }
        public int Id { get; set; }

        public int CourseId;
        public AnnouncementDetailViewModel(int id = 0, int courseId = 0)
        {
            if (id > 0)
            {
                LoadById(id);
            }

            if (courseId > 0)
            {
                CourseId = courseId;
            }
        }

        public void LoadById(int id)
        {
            if (id == 0) { return; }
            var announcement = AnnouncementService.Current.GetById(id);
            if (announcement != null)
            {
                Title = announcement.Title;
                Poster = announcement.Poster;
                Info = announcement.Info;
                Id = announcement.Id;
            }

            NotifyPropertyChanged(nameof(Title));
            NotifyPropertyChanged(nameof(Poster));
            NotifyPropertyChanged(nameof(Info));

        }

        public void AddAnnouncement()
        {
            if (Id <= 0)
            {
                var announcement = new Announcement { Title = Title, Poster = Poster, Info = Info };
                if(SelectedInstructor!= null)
                {
                    announcement.Poster = SelectedInstructor;
                }
                
                AnnouncementService.Current.Add(announcement);

                var refToUpdate = CourseService.Current.GetById(CourseId);
                CourseService.Current.AddAnnouncement(refToUpdate, announcement);
            }
            else
            {
                var refToUpdate = AnnouncementService.Current.GetById(Id);
                refToUpdate.Title = Title;
                refToUpdate.Poster = Poster;
                if (SelectedInstructor != null)
                {
                    refToUpdate.Poster = SelectedInstructor;
                }
                refToUpdate.Info = Info;

                var course = CourseService.Current.GetById(CourseId);
                CourseService.Current.RemoveAnnouncement(course, refToUpdate);
                CourseService.Current.AddAnnouncement(course, refToUpdate);

            }
            Shell.Current.GoToAsync($"//CourseDetail?courseId={CourseId}");
            //Shell.Current.GoToAsync("//Instructor");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Person SelectedInstructor { get; set; }

        public ObservableCollection<Person> People
        {
            get
            {

                var filteredList = StudentService
                    .Current
                    .Students
                    .Where(
                    s => s is Instructor || s is TeachingAssistant);
                return new ObservableCollection<Person>(filteredList);

            }
        }
    }
}
