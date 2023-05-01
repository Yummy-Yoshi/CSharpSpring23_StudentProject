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

namespace MAUI.LearningManagement.ViewModels
{
    public class AssignmentItemDetailViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Assignment Assignment { get; set; }
        public int Id { get; set; }

        public int ModuleId;

        public int CourseId;

        public AssignmentItemDetailViewModel(int moduleId = 0, int id = 0, int courseId = 0)
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
            var assignmentItem = ContentService.Current.GetById(id) as AssignmentItem;
            if (assignmentItem != null)
            {
                Name = assignmentItem.Name;
                Description = assignmentItem.Description;
                Assignment = assignmentItem.Assignment;
                Id = assignmentItem.Id;

            }

            NotifyPropertyChanged(nameof(Name));
            NotifyPropertyChanged(nameof(Assignment));
            NotifyPropertyChanged(nameof(Description));
        }

        public void AddAssignmentItem()
        {
            if (Id <= 0)
            {
                var assignmentItem = new AssignmentItem { Name = Name, Description = Description, Assignment = Assignment };
                if (SelectedAssignment != null)
                {
                    assignmentItem.Assignment = SelectedAssignment;
                }
                ContentService.Current.Add(assignmentItem);

                var refToUpdate = ModuleService.Current.GetById(ModuleId);
                ModuleService.Current.AddContent(refToUpdate, assignmentItem);
            }
            else
            {
                var refToUpdate = ContentService.Current.GetById(Id) as AssignmentItem;
                refToUpdate.Name = Name;
                refToUpdate.Description = Description;
                refToUpdate.Assignment = Assignment;
                if (SelectedAssignment != null)
                {
                    refToUpdate.Assignment = SelectedAssignment;
                }

                var module = ModuleService.Current.GetById(ModuleId);
                ModuleService.Current.RemoveContent(module, refToUpdate);
                ModuleService.Current.AddContent(module, refToUpdate);

            }
            //Shell.Current.GoToAsync("//ModuleDetail?moduleId={ModuleId}");
            Shell.Current.GoToAsync($"//ModuleDetail?moduleId={ModuleId}&courseId={CourseId}");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Assignment SelectedAssignment { get; set; }

        public ObservableCollection<Assignment> Assignments
        {
            get
            {
                
                    var refToUpdate = CourseService.Current.GetById(CourseId);

                    return new ObservableCollection<Assignment>(refToUpdate.Assignments);
                
             
            }
        }
    }
}
