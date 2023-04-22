using MAUI.LearningMangement.ViewModels;

namespace MAUI.LearningMangement.Views;

public partial class InstructorView : ContentPage
{
	public InstructorView()
	{
		InitializeComponent();
		BindingContext = new InstructorViewViewModel();
	}

	private void CancelClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//MainPage");
	}

    private void AddEnrollmentClick(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//PersonDetail");
    }

}