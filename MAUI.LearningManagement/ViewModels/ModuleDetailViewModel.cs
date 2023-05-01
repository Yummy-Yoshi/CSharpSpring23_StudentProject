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
    class ModuleDetailViewModel : INotifyPropertyChanged
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
                if (SelectedFileItem != null)
                {
                    module.Content.Add(SelectedFileItem);
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
            Shell.Current.GoToAsync($"//CourseDetail?courseId={CourseId}");
            //Shell.Current.GoToAsync("//Instructor");

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddFileItemClick(Shell s)
        {
            var idParam = 0;
            s.GoToAsync($"//FileItemDetail?moduleId={Id}&contentId={idParam}&courseId={CourseId}");

        }
        public void EditFileItemClick(Shell s)
        {
            var idParam = SelectedFileItem?.Id ?? 0;
            s.GoToAsync($"//FileItemDetail?moduleId={Id}&contentId={idParam}&courseId={CourseId}");

        }

        public void RemoveFileItemClick(int moduleId)
        {
            if (SelectedFileItem == null) { return; }

            ContentService.Current.Remove(SelectedFileItem);

            var refToUpdate = ModuleService.Current.GetById(moduleId);

            //var idParam = SelectedPerson?.Id ?? 0;
            if (refToUpdate != null)
            {
                ModuleService.Current.RemoveContent(refToUpdate, SelectedFileItem);
            }
            RefreshView();
        }

        public void AddAssignmentItemClick(Shell s)
        {
            var idParam = 0;
            s.GoToAsync($"//AssignmentItemDetail?moduleId={Id}&contentId={idParam}&courseId={CourseId}");
        }

        public void EditAssignmentItemClick(Shell s)
        {
            var idParam = SelectedAssignmentItem?.Id ?? 0;
            s.GoToAsync($"//AssignmentItemDetail?moduleId={Id}&contentId={idParam}&courseId={CourseId}");

        }

        public void RemoveAssignmentItemClick(int moduleId)
        {
            if (SelectedAssignmentItem == null) { return; }

            ContentService.Current.Remove(SelectedAssignmentItem);

            var refToUpdate = ModuleService.Current.GetById(moduleId);

            if (refToUpdate != null)
            {
                ModuleService.Current.RemoveContent(refToUpdate, SelectedAssignmentItem);
            }
            RefreshView();
        }

        public void AddPageItemClick(Shell s)
        {
            var idParam = 0;
            s.GoToAsync($"//PageItemDetail?moduleId={Id}&contentId={idParam}&courseId={CourseId}");
        }

        public void EditPageItemClick(Shell s)
        {
            var idParam = SelectedPageItem?.Id ?? 0;
            s.GoToAsync($"//PageItemDetail?moduleId={Id}&contentId={idParam}&courseId={CourseId}");

        }

        public void RemovePageItemClick(int moduleId)
        {
            if (SelectedPageItem == null) { return; }

            ContentService.Current.Remove(SelectedPageItem);

            var refToUpdate = ModuleService.Current.GetById(moduleId);

            if (refToUpdate != null)
            {
                ModuleService.Current.RemoveContent(refToUpdate, SelectedPageItem);
            }
            RefreshView();
        }

        public ContentItem SelectedFileItem { get; set; }
        public ObservableCollection<ContentItem> FileItems
        {
            get
            {

                if (Id > 0)
                {
                    var filteredList = ModuleService.Current.GetById(Id);

                    var filter = filteredList.Content.Where(c => c is FileItem);

                    return new ObservableCollection<ContentItem>(filter);
                }
                return null;
            }
        }

        public ContentItem SelectedAssignmentItem { get; set; }
        public ObservableCollection<ContentItem> AssignmentItems
        {
            get
            {
                if (Id > 0)
                {
                    var filteredList = ModuleService.Current.GetById(Id);

                    var filter = filteredList.Content.Where(c => c is AssignmentItem);

                    return new ObservableCollection<ContentItem>(filter);
                }
                return null;
            }
        }
        public ContentItem SelectedPageItem { get; set; }
        public ObservableCollection<ContentItem> PageItems
        {
            get
            {
                if (Id > 0)
                {
                    var filteredList = ModuleService.Current.GetById(Id);

                    var filter = filteredList.Content.Where(c => c is PageItem);

                    return new ObservableCollection<ContentItem>(filter);
                }
                return null;
            }
        }


        public void RefreshView()
        {
            NotifyPropertyChanged(nameof(FileItems));
            NotifyPropertyChanged(nameof(AssignmentItems));
            NotifyPropertyChanged(nameof(PageItems));
        }
        
    }
}
