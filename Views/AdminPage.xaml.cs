using Koolitused.Services;

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

    private async void OnManageUserClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new KasutajaPage());
    }

    private async void OnManageOpetajaClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ManageTeachersPage());
    }

    private async void OnManageRollClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ManageRolesPage());
    }

    private async void OnLogiValjaClicked(object sender, EventArgs e)
    {
        SessionManager.ClearSession();

        await Navigation.PushAsync(new MainPage());
    }
}