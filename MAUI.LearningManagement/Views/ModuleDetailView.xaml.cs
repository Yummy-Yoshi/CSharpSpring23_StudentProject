using Library.LearningManagement.Models;
using MAUI.LearningManagement.ViewModels;

namespace MAUI.LearningManagement.Views;

[QueryProperty(nameof(ModuleId), "moduleId")]
[QueryProperty(nameof(CourseId), "courseId")]
public partial class ModuleDetailView : ContentPage
{
	public ModuleDetailView()
	{
		InitializeComponent();
	}

    public int ModuleId
    {
        set; get;
    }

    public int CourseId
    {
        set; get;
    }

    private void OkClick(object sender, EventArgs e)
    {
        (BindingContext as ModuleDetailViewModel).AddModule();

    }

    private void CancelClick(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Instructor");
    }

    private void OnLeaving(object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;
    }

    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new ModuleDetailViewModel(ModuleId, CourseId);
    }

    private void AddFileItemClick(object sender, EventArgs e)
    {                                               
        (BindingContext as ModuleDetailViewModel).AddFileItemClick(Shell.Current);
    }
    private void EditFileItem(object sender, EventArgs e)
    {
        (BindingContext as ModuleDetailViewModel).AddFileItemClick(Shell.Current);
    }
    private void RemoveFileItemClick(object sender, EventArgs e)
    {
        (BindingContext as ModuleDetailViewModel).RemoveFileItemClick(CourseId);
    }
}