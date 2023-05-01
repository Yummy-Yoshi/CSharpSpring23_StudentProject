using MAUI.LearningManagement.ViewModels;

namespace MAUI.LearningManagement.Views;

public partial class StudentView : ContentPage
{
	public StudentView()
	{
		InitializeComponent();
        BindingContext = new StudentViewViewModel();
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }
    private void SeeCoursesClick(object sender, EventArgs e)
    {
        (BindingContext as StudentViewViewModel).SeeCoursesClick(Shell.Current);
    }

}