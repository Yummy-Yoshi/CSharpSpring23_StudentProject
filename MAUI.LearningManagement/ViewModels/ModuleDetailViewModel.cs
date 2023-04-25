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
    public class ModuleDetailViewModel
    {
        public string Name { get; set; }
        public List<ContentItem> Content { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }

        public int CourseId;

        public ModuleDetailViewModel(int id = 0, int courseId = 0)
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
            var module = ModuleService.Current.GetById(id);
            if (module != null)
            {
                Name = module.Name;
                Content = module.Content;
                Description = module.Description;
                Id = module.Id;
                
            }

            NotifyPropertyChanged(nameof(Name));
            NotifyPropertyChanged(nameof(Content));
            NotifyPropertyChanged(nameof(Description));
        }

        public void AddModule()
        {
            if (Id <= 0)
            {
                var module = new Library.LearningManagement.Models.Module { Name = Name, Description = Description};
                if (SelectedContent != null)
                {
                    module.Content.Add(SelectedContent);
                }
                ModuleService.Current.Add(module);

                var refToUpdate = CourseService.Current.GetById(CourseId);
                CourseService.Current.AddModule(refToUpdate, module);
            }
            else
            {
                var refToUpdate = ModuleService.Current.GetById(Id);
                refToUpdate.Name = Name;                
                refToUpdate.Description = Description;

                var course = CourseService.Current.GetById(CourseId);
                CourseService.Current.RemoveModule(course, refToUpdate);
                CourseService.Current.AddModule(course, refToUpdate);

            }
            //Shell.Current.GoToAsync("//CourseDetail?courseId={CourseId}");
            Shell.Current.GoToAsync("//Instructor");

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddFileItemClick(Shell s)
        {
            var idParam = SelectedContent?.Id ?? 0;
            s.GoToAsync($"//FileItemDetail?moduleId={Id}&contentId={idParam}");

        }

        public void RemoveFileItemClick(int moduleId)
        {
            if (SelectedContent == null) { return; }

            ContentService.Current.Remove(SelectedContent);

            var refToUpdate = ModuleService.Current.GetById(moduleId);

            //var idParam = SelectedPerson?.Id ?? 0;
            if (refToUpdate != null)
            {
                ModuleService.Current.RemoveContent(refToUpdate, SelectedContent);
            }
            RefreshView();
        }

        public ContentItem SelectedContent { get; set; }
        public ObservableCollection<ContentItem> Contents
        {
            get
            {

                if (Id > 0)
                {
                    var refToUpdate = ModuleService.Current.GetById(Id);

                    return new ObservableCollection<ContentItem>(refToUpdate.Content);

                }
                return null;

            }
        }

        public void RefreshView()
        {
            NotifyPropertyChanged(nameof(Contents));
        }
    }
}
