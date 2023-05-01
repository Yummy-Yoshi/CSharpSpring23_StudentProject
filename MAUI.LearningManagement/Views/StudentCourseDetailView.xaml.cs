using Library.LearningManagement.Models;
using MAUI.LearningManagement.ViewModels;

namespace MAUI.LearningManagement.Views;

[QueryProperty(nameof(StudentId), "studentId")]
public partial class StudentCourseDetailView : ContentPage
{
	public StudentCourseDetailView()
	{
		InitializeComponent();
	}
    public int StudentId
    {
        set; get;
    }
    private void CheckCoursesClick(object sender, EventArgs e)
    {
        (BindingContext as StudentCourseDetailViewModel).CheckCoursesClick(Shell.Current);
    }
    private void BackClick(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync($"//Student");
    }
    private void OnLeaving(object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;
    }
    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new StudentCourseDetailViewModel(StudentId);
    }
    private void Toolbar_SpringClicked(object sender, EventArgs e)
    {
        (BindingContext as StudentCourseDetailViewModel).ShowSpring();
    }
    private void Toolbar_SummerClicked(object sender, EventArgs e)
    {
        (BindingContext as StudentCourseDetailViewModel).ShowSummer();
    }
    private void Toolbar_FallClicked(object sender, EventArgs e)
    {
        (BindingContext as StudentCourseDetailViewModel).ShowFall();
    }
}