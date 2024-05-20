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
                await DisplayAlert("Viga", "Palun sisestage kasutajanimi ja salasõna.", "OK");
                return;
            }

            var user = await _databaseService.GetUserAsync(username, password);

            if (user != null)
            {
                SessionManager.SetSession(username);

                await Navigation.PushAsync(new LogitudPage());
            }
            else
            {
                await DisplayAlert("Viga", "Vale kasutajanimi või salasõna", "OK");
            }
        }

        private void OnShowPasswordToggled(object sender, ToggledEventArgs e)
        {
            Salasona.IsPassword = !e.Value;
        }
    }
}