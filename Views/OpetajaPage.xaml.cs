using Microsoft.Maui.Controls;

namespace Koolitused.Views
{
    public partial class OpetajaPage : ContentPage
    {
        private readonly string _loggedInUsername;

        public OpetajaPage(string loggedInUsername)
        {
            InitializeComponent();
            _loggedInUsername = loggedInUsername;
        }

        private async void OnMyStudentsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyStudentsPage(_loggedInUsername));
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}