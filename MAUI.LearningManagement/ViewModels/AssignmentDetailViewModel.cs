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
    class AssignmentDetailViewModel : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal TotalAvailablePoints { get; set; }
        public DateTime DueDate { get; set; }
        public int Id { get; set; }
        public List<Submission> Submissions { get; set; }

        public int AssignmentGroupId;

        public int CourseId;

        public AssignmentDetailViewModel(int assignmentgroupId = 0, int id = 0, int courseId = 0)
        {
            if (assignmentgroupId > 0)
            {
                AssignmentGroupId = assignmentgroupId;
            }

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
            var assignment = AssignmentService.Current.GetById(id);
            if (assignment != null)
            {
                Name = assignment.Name;
                Description = assignment.Description;
                TotalAvailablePoints= assignment.TotalAvailablePoints;
                DueDate = assignment.DueDate;
                Submissions = assignment.Submissions;
                Id = assignment.Id;

            }

            NotifyPropertyChanged(nameof(Name));
            NotifyPropertyChanged(nameof(TotalAvailablePoints));
            NotifyPropertyChanged(nameof(Description));
            NotifyPropertyChanged(nameof(DueDate));
            NotifyPropertyChanged(nameof(Submissions));
        }
        public void AddAssignment()
        {
            if (Id <= 0)
            {
                var assignment = new Assignment { Name = Name, Description = Description, TotalAvailablePoints = TotalAvailablePoints, DueDate = DueDate };
                if (SelectedSubmission != null)
                {
                    assignment.Submissions.Add(SelectedSubmission);
                }
                AssignmentService.Current.Add(assignment);
                var course = CourseService.Current.GetById(CourseId);
                course.Assignments.Add(assignment);

                var refToUpdate = AssignmentGroupService.Current.GetById(AssignmentGroupId);
                AssignmentGroupService.Current.AddAssignment(refToUpdate, assignment);
            }
            else
            {
                var refToUpdate = AssignmentService.Current.GetById(Id);
                refToUpdate.Name = Name;
                refToUpdate.Description = Description;
                refToUpdate.TotalAvailablePoints = TotalAvailablePoints;
                refToUpdate.DueDate = DueDate;
                if (SelectedSubmission != null)
                {
                    refToUpdate.Submissions.Add(SelectedSubmission);
                }
                var course = CourseService.Current.GetById(CourseId);
                course.Assignments.Add(refToUpdate);
                AssignmentService.Current.Add(refToUpdate);

                var assignmentGroup = AssignmentGroupService.Current.GetById(AssignmentGroupId);
                AssignmentGroupService.Current.RemoveAssignment(assignmentGroup, refToUpdate);
                AssignmentGroupService.Current.AddAssignment(assignmentGroup, refToUpdate);

            }
            Shell.Current.GoToAsync($"//AssignmentGroupDetail?assignmentgroupId={AssignmentGroupId}&courseId={CourseId}");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Submission SelectedSubmission { get; set; }

        public ObservableCollection<Submission> Submission
        {
            get
            {
                if (Id > 0)
                {
                    var refToUpdate = AssignmentService.Current.GetById(Id);

                    return new ObservableCollection<Submission>(refToUpdate.Submissions);
                }
                return null;
            }
        }
        public void AddSubmissionClick(Shell s)
        {
            var idParam = 0;
            s.GoToAsync($"//SubmissionDetail?assignmentId={Id}&submissionId={idParam}&assignmentgroupId={AssignmentGroupId}&courseId={CourseId}");
        }
        public void EditSubmissionClick(Shell s)
        {
            var idParam = SelectedSubmission?.Id ?? 0;
            s.GoToAsync($"//SubmissionDetail?assignmentId={Id}&submissionId={idParam}&assignmentgroupId={AssignmentGroupId}&courseId={CourseId}");
        }
        public void RemoveSubmissionClick(int assignmentgroupId)
        {
            if (SelectedSubmission == null) { return; }

            SubmissionService.Current.Remove(SelectedSubmission);

            var refToUpdate = AssignmentService.Current.GetById(assignmentgroupId);

            if (refToUpdate != null)
            {
                AssignmentService.Current.RemoveSubmission(refToUpdate, SelectedSubmission);
            }
            RefreshView();
        }
        public void RefreshView()
        {
            NotifyPropertyChanged(nameof(Submission));
        }
    }
}
