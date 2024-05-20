namespace Koolitused.Views;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
	}
	private async void OnRegistreeriClicked(object sender, EventArgs e)
	{
		
    }
    private void OnShowPasswordCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        Salasona.IsPassword = !e.Value;
    }
}