using Library.LearningManagement.Models;
using MAUI.LearningManagement.ViewModels;

namespace MAUI.LearningManagement.Views;

[QueryProperty(nameof(StudentId), "studentId")]
[QueryProperty(nameof(CourseId), "courseId")]
public partial class StudentCheckCourseDetailView : ContentPage
{
	public StudentCheckCourseDetailView()
	{
		InitializeComponent();
	}
    public int StudentId
    {
        set; get;
    }
    public int CourseId
    {
        set; get;
    }
    private void OnLeaving(object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;
    }
    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new StudentCheckCourseDetailViewModel(CourseId, StudentId);
    }
    private void BackClick(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync($"//StudentCourseDetail?studentId={StudentId}&courseId={CourseId}");
    }
}