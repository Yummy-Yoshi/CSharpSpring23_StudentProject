using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using MAUI.LearningMangement.ViewModels;

namespace MAUI.LearningMangement.Views;

[QueryProperty(nameof(PersonId), "personId")]   
public partial class PersonDetailView : ContentPage
{
    public PersonDetailView()
    {
        InitializeComponent();
    }

    public int PersonId
    {
        set; get;
    }

    private void OkClick(object sender, EventArgs e)
    {
        (BindingContext as PersonDetailViewModel).AddPerson();
    }
    private void OnLeaving(object sender, NavigatedFromEventArgs e)
    {
        BindingContext = null;
    }

    private void OnArriving(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new PersonDetailViewModel(PersonId);

    }
}