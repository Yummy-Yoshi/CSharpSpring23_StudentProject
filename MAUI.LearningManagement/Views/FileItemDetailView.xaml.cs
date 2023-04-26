using Library.LearningManagement.Models;
using MAUI.LearningManagement.ViewModels;

namespace MAUI.LearningManagement.Views;

[QueryProperty(nameof(ModuleId), "moduleId")]
[QueryProperty(nameof(ContentId), "contentId")]
[QueryProperty(nameof(CourseId), "courseId")]
public partial class FileItemDetailView : ContentPage
{
	public FileItemDetailView()
	{
		InitializeComponent();
	}
    public int ModuleId
    {
        set; get;
    }

    public int ContentId
    {
        set; get;
    }
    public int CourseId
    {
        set; get;
    }
    private void OkClick(object sender, EventArgs e)
    {
        (BindingContext as FileItemDetailViewModel).AddFileItem();
        //Shell.Current.GoToAsync("//ModuleDetail?moduleId={ModuleId}");
    }

    private void CancelClick(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync($"//ModuleDetail?moduleId={ModuleId}&courseId={CourseId}");
    }

    private void OnLeaving(object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;
    }

    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new FileItemDetailViewModel(ModuleId, ContentId,CourseId);
    }
}