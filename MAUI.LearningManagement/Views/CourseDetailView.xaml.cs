using MAUI.LearningManagement.ViewModels;

namespace MAUI.LearningManagement.Views;

[QueryProperty(nameof(CourseId), "courseId")]
public partial class CourseDetailView : ContentPage
{
    public CourseDetailView()
    {
        InitializeComponent();
        BindingContext = new CourseDetailViewModel();
    }

    public int CourseId
    {
        set; get;
    }

    private void OkClick(object sender, EventArgs e)
    {
        (BindingContext as CourseDetailViewModel).AddCourse();
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
        BindingContext = new CourseDetailViewModel(CourseId);
    }

    private void AddEnrollmentClick(object sender, EventArgs e)
    {
        (BindingContext as CourseDetailViewModel).AddEnrollmentClick(CourseId);
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as CourseDetailViewModel).RefreshView();
    }

    private void RemoveEnrollmentClick(object sender, EventArgs e)
    {
        (BindingContext as CourseDetailViewModel).RemoveEnrollmentClick(CourseId);
    }
    private void AddAnnouncementClick(object sender, EventArgs e)
    {
        (BindingContext as CourseDetailViewModel).AddAnnouncementClick(Shell.Current);
    }

    private void RemoveAnnouncementClick(object sender, EventArgs e)
    {
        (BindingContext as CourseDetailViewModel).RemoveAnnouncementClick(CourseId);
    }

    private void EditAnnouncementClick(object sender, EventArgs e)
    {
        (BindingContext as CourseDetailViewModel).EditAnnouncementClick(Shell.Current);
    }

    private void AddModuleClick(object sender, EventArgs e)
    {
        (BindingContext as CourseDetailViewModel).AddModuleClick(Shell.Current);
    }
    private void EditModuleClick(object sender, EventArgs e)
    {
        (BindingContext as CourseDetailViewModel).EditModuleClick(Shell.Current);
    }
    private void RemoveModuleClick(object sender, EventArgs e)
    {
        (BindingContext as CourseDetailViewModel).RemoveModuleClick(CourseId);
    }

    private void AddAssignmentGroupClick(object sender, EventArgs e)
    {
        (BindingContext as CourseDetailViewModel).AddAssignmentGroupClick(Shell.Current);
    }
    private void EditAssignmentGroupClick(object sender, EventArgs e)
    {
        (BindingContext as CourseDetailViewModel).EditAssignmentGroupClick(Shell.Current);
    }
    private void RemoveAssignmentGroupClick(object sender, EventArgs e)
    {
        (BindingContext as CourseDetailViewModel).RemoveAssignmentGroupClick(CourseId);
    }
}

