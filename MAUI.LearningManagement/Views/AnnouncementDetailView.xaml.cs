using Library.LearningManagement.Models;
using MAUI.LearningManagement.ViewModels;

namespace MAUI.LearningManagement.Views;

[QueryProperty(nameof(AnnouncementId), "announcementId")]
[QueryProperty(nameof(ClasssId), "classsId")]
public partial class AnnouncementDetailView : ContentPage
{
    
    public AnnouncementDetailView()
	{
		InitializeComponent();
	}
    public int AnnouncementId
    {
        set; get;
    }

    public int ClasssId
    {
        set; get;
    }

    private void OkClick(object sender, EventArgs e)
    {
        (BindingContext as AnnouncementDetailViewModel).AddAnnouncement();
        
    }

    private void CancelClick(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Instructor");
    }
    private void OnLeaving(object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;
        //Shell.Current.GoToAsync("//CourseDetail?courseId={ClasssId}");
    }

    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new AnnouncementDetailViewModel(AnnouncementId, ClasssId);
    }
}