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
    class AssignmentGroupDetailViewModel : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public List<Assignment> Assignments { get; set; }
        public decimal Weight { get; set; }
        public int Id { get; set; }

        public int CourseId;

        public AssignmentGroupDetailViewModel(int id = 0, int courseId = 0)
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
            var assignmentGroup = AssignmentGroupService.Current.GetById(id);
            if (assignmentGroup != null)
            {
                Name = assignmentGroup.Name;
                Assignments = assignmentGroup.Assignments;
                Weight= assignmentGroup.Weight;
                Id = assignmentGroup.Id;

            }

            NotifyPropertyChanged(nameof(Name));
            NotifyPropertyChanged(nameof(Assignments));
            NotifyPropertyChanged(nameof(Weight));
        }

        public void AddAssignmentGroup()
        {
            if (Id <= 0)
            {
                var assignmentGroup = new AssignmentGroup { Name = Name, Weight = Weight };
                if (SelectedAssignment != null)
                {
                    assignmentGroup.Assignments.Add(SelectedAssignment);
                }
                AssignmentGroupService.Current.Add(assignmentGroup);

                var refToUpdate = CourseService.Current.GetById(CourseId);
                CourseService.Current.AddAssignmentGroup(refToUpdate, assignmentGroup);
            }
            else
            {
                var refToUpdate = AssignmentGroupService.Current.GetById(Id);
                refToUpdate.Name = Name;
                refToUpdate.Weight = Weight;

                var course = CourseService.Current.GetById(CourseId);
                CourseService.Current.RemoveAssignmentGroup(course, refToUpdate);
                CourseService.Current.AddAssignmentGroup(course, refToUpdate);

            }
            Shell.Current.GoToAsync($"//CourseDetail?courseId={CourseId}");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddAssignmentClick(Shell s)
        {
            var idParam = 0;
            s.GoToAsync($"//AssignmentDetail?moduleId={Id}&assignmentId={idParam}&courseId={CourseId}");

        }
        public void EditAssignmentClick(Shell s)
        {
            var idParam = SelectedAssignment?.Id ?? 0;
            s.GoToAsync($"//AssignmentDetail?moduleId={Id}&assignmentId={idParam}&courseId={CourseId}");

        }

        public void RemoveAssignmentClick(int moduleId)
        {/*
            if (SelectedAssignment == null) { return; }

            AssignmentService.Current.Remove(SelectedAssignment);

            var refToUpdate = ModuleService.Current.GetById(moduleId);

            if (refToUpdate != null)
            {
                ModuleService.Current.RemoveContent(refToUpdate, SelectedAssignment);
            }
            RefreshView();*/
        }

        public Assignment SelectedAssignment { get; set; }
        public ObservableCollection<Assignment> Assignment
        {
            get
            {
                if (Id > 0)
                {
                    var filteredList = AssignmentGroupService.Current.GetById(Id);

                    var filter = filteredList.Assignments;

                    return new ObservableCollection<Assignment>(filter);
                }
                return null;
            }
        }

        public void RefreshView()
        {
            NotifyPropertyChanged(nameof(Assignment));
        }
    }
}
