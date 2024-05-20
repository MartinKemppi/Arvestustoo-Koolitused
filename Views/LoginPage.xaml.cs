namespace Koolitused.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}
	private async void OnLoginClicked(object sender, EventArgs e)
	{
        await Navigation.PushAsync(new MainPage());

    }
    private void OnShowPasswordCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        Salasona.IsPassword = !e.Value;
    }
}