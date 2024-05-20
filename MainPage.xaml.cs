using Koolitused.Views;

namespace Koolitused;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private async void OnLoginClicked(object sender, EventArgs e)
	{
        await Navigation.PushAsync(new LoginPage());
    }
    private async void OnRegClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterPage());
    }
    private async void OnKursClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new KursPage());
    }
    private async void OnInfoClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new InfoPage());
    }

}

