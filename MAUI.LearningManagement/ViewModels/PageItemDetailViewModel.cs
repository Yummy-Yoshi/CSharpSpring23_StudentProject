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
    public class PageItemDetailViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string HtmlBody { get; set; }
        public int Id { get; set; }

        public int ModuleId;

        public int CourseId;

        public PageItemDetailViewModel(int moduleId = 0, int id = 0, int courseId = 0)
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
                CourseId = courseId;
            }
        }
        public void LoadById(int id)
        {
            if (id == 0) { return; }
            var pageItem = ContentService.Current.GetById(id) as PageItem;
            if (pageItem != null)
            {   
                Name = pageItem.Name;
                Description = pageItem.Description;
                HtmlBody = pageItem.HtmlBody;
                Id = pageItem.Id;

            }

            NotifyPropertyChanged(nameof(Name));
            NotifyPropertyChanged(nameof(HtmlBody));
            NotifyPropertyChanged(nameof(Description));
        }

        public void AddPageItem()
        {
            if (Id <= 0)
            {
                var pageItem = new PageItem { Name = Name, Description = Description, HtmlBody = HtmlBody };
                ContentService.Current.Add(pageItem);

                var refToUpdate = ModuleService.Current.GetById(ModuleId);
                ModuleService.Current.AddContent(refToUpdate, pageItem);
            }
            else
            {
                var refToUpdate = ContentService.Current.GetById(Id) as PageItem;
                refToUpdate.Name = Name;
                refToUpdate.Description = Description;
                refToUpdate.HtmlBody = HtmlBody;

                var module = ModuleService.Current.GetById(ModuleId);
                ModuleService.Current.RemoveContent(module, refToUpdate);
                ModuleService.Current.AddContent(module, refToUpdate);

            }
            Shell.Current.GoToAsync($"//ModuleDetail?moduleId={ModuleId}&courseId={CourseId}");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
