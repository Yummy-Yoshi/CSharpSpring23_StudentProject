using Library.LearningManagement.Models;
using System.Reflection;

namespace MAUI.LearningManagement.Views;

[QueryProperty(nameof(AssignmentGroupId), "assignmentgroupId")]
[QueryProperty(nameof(AssignmentId), "assignmentId")]
[QueryProperty(nameof(CourseId), "courseId")]
public partial class AssignmentDetailView : ContentPage
{
	public AssignmentDetailView()
	{
		InitializeComponent();
	}
    public int AssignmentGroupId
    {
        set; get;
    }

    public int AssignmentId
    {
        set; get;
    }
    public int CourseId
    {
        set; get;
    }
    private void OkClick(object sender, EventArgs e)
    {
        (BindingContext as AssignmentDetailViewModel).AddAssignment();
        //Shell.Current.GoToAsync("//ModuleDetail?moduleId={ModuleId}");
    }

    private void CancelClick(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync($"//AssignmentDetail?assignmentgroupId={AssignmentGroupId}&courseId={CourseId}");
    }

    private void OnLeaving(object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;
    }

    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new AssignmentDetailViewModel(AssignmentGroupId, AssignmentId, CourseId);
    }
}