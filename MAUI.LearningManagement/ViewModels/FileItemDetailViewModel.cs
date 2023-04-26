using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MAUI.LearningManagement.ViewModels
{
    public class FileItemDetailViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public int Id { get; set; }

        public int ModuleId;

        public int CourseId;

        public FileItemDetailViewModel(int moduleId = 0, int id = 0, int courseId= 0)
        {
            if (id > 0)
            {
                LoadById(id);
            }

            if (moduleId > 0)
            {
                ModuleId = moduleId;
            }

            if (courseId > 0)
            {
                CourseId = moduleId;
            }
        }

        public void LoadById(int id)
        {
            if (id == 0) { return; }
            var fileItem = ContentService.Current.GetById(id) as FileItem;
            if (fileItem != null)
            {
                Name = fileItem.Name;
                Description = fileItem.Description;
                Path = fileItem.Path;
                Id = fileItem.Id;

            }

            NotifyPropertyChanged(nameof(Name));
            NotifyPropertyChanged(nameof(Path));
            NotifyPropertyChanged(nameof(Description));
        }

        public void AddFileItem()
        {
            if (Id <= 0)
            {
                var fileItem = new FileItem { Name = Name, Description = Description, Path = Path };
                ContentService.Current.Add(fileItem);

                var refToUpdate = ModuleService.Current.GetById(ModuleId);
                ModuleService.Current.AddContent(refToUpdate, fileItem);
            }
            else
            {
                var refToUpdate = ContentService.Current.GetById(Id) as FileItem;
                refToUpdate.Name = Name;
                refToUpdate.Description = Description;
                refToUpdate.Path = Path;

                var module = ModuleService.Current.GetById(ModuleId);
                ModuleService.Current.RemoveContent(module, refToUpdate);
                ModuleService.Current.AddContent(module, refToUpdate);

            }
            //Shell.Current.GoToAsync("//Instructor");
            Shell.Current.GoToAsync($"//ModuleDetail?moduleId={ModuleId}&courseId={CourseId}");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
