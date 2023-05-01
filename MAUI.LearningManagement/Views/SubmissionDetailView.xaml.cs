using Library.LearningManagement.Models;
using MAUI.LearningManagement.ViewModels;
using System.Reflection;

namespace MAUI.LearningManagement.Views;

[QueryProperty(nameof(AssignmentGroupId), "assignmentgroupId")]
[QueryProperty(nameof(AssignmentId), "assignmentId")]
[QueryProperty(nameof(CourseId), "courseId")]
[QueryProperty(nameof(SubmissionId), "submissionId")]
public partial class SubmissionDetailView : ContentPage
{
	public SubmissionDetailView()
	{
		InitializeComponent();
	}
    public int AssignmentId
    {
        set; get;
    }

    public int CourseId
    {
        set; get;
    }
    public int AssignmentGroupId
    {
        set; get;
    }
    public int SubmissionId
    {
        set; get;
    }
    private void OkClick(object sender, EventArgs e)
    {
        (BindingContext as SubmissionDetailViewModel).AddSubmission();
    }
    private void CancelClick(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync($"//AssignmentDetail?assignmentId={AssignmentId}&assignmentgroupId={AssignmentGroupId}&courseId={CourseId}");
    }
    private void OnLeaving(object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;
    }
    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new SubmissionDetailViewModel(AssignmentId, SubmissionId, AssignmentGroupId, CourseId);
    }

}