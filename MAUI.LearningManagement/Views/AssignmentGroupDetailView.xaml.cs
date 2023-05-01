using Library.LearningManagement.Models;
using MAUI.LearningManagement.ViewModels;
using System.Reflection;

namespace MAUI.LearningManagement.Views;

[QueryProperty(nameof(AssignmentGroupId), "assignmentgroupId")]
[QueryProperty(nameof(CourseId), "courseId")]
public partial class AssignmentGroupDetailView : ContentPage
{
    public AssignmentGroupDetailView()
    {
        InitializeComponent();
    }

    public int AssignmentGroupId
    {
        set; get;
    }

    public int CourseId
    {
        set; get;
    }
    
    private void OkClick(object sender, EventArgs e)
    {
        (BindingContext as AssignmentGroupDetailViewModel).AddAssignmentGroup();

    }

    private void CancelClick(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync($"//CourseDetail?courseId={CourseId}");
    }
    private void OnLeaving(object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;
    }

    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new AssignmentGroupDetailViewModel(AssignmentGroupId, CourseId);
    }
    
    private void AddAssignmentClick(object sender, EventArgs e)
    {
        (BindingContext as AssignmentGroupDetailViewModel).AddAssignmentClick(Shell.Current);
    }
    private void EditAssignmentClick(object sender, EventArgs e)
    {
        (BindingContext as AssignmentGroupDetailViewModel).EditAssignmentClick(Shell.Current);
    }
    private void RemoveAssignmentClick(object sender, EventArgs e)
    {
        (BindingContext as AssignmentGroupDetailViewModel).RemoveAssignmentClick(CourseId);
    }
}