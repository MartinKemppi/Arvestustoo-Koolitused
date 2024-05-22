namespace Koolitused.Views;

public partial class AdminPage : ContentPage
{
	public AdminPage()
	{
		InitializeComponent();
	}

    private async void OnManageCoursesClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ManageCoursesPage());
    }
}