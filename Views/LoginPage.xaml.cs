using System;
using Microsoft.Maui.Controls;
using Koolitused.Models;
using Koolitused.Services;

namespace Koolitused.Views
{
    public partial class LoginPage : ContentPage
    {
        private readonly DatabaseService _databaseService;

        public LoginPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string username = UsernameEntry.Text;
            string password = Salasona.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Error", "Please enter both username and password.", "OK");
                return;
            }

            var user = await _databaseService.GetUserAsync(username, password);

            if (user != null)
            {
                await Navigation.PushAsync(new MainPage());
            }
            else
            {
                await DisplayAlert("Error", "Invalid username or password.", "OK");
            }
        }

        private void OnShowPasswordToggled(object sender, ToggledEventArgs e)
        {
            Salasona.IsPassword = !e.Value;
        }
    }
}
