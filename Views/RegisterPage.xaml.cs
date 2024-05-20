using System;
using Microsoft.Maui.Controls;
using Koolitused.Models;
using Koolitused.Services;

namespace Koolitused.Views
{
    public partial class RegisterPage : ContentPage
    {
        private readonly DatabaseService _databaseService;

        public RegisterPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
        }

        private async void OnRegistreeriClicked(object sender, EventArgs e)
        {
            var user = new Kasutaja
            {
                Kasutajanimi = Nimi.Text,
                Kasutajasalasona = Salasona.Text,
                Roll = 1
            };

            if (!string.IsNullOrEmpty(user.Kasutajanimi) && !string.IsNullOrEmpty(user.Kasutajasalasona))
            {
                await _databaseService.SaveUserAsync(user);
                await DisplayAlert("Success", "User registered successfully!", "OK");
                await Navigation.PushAsync(new LoginPage());
            }
            else
            {
                await DisplayAlert("Error", "Please enter a valid username and password.", "OK");
            }
        }

        private void OnShowPasswordToggled(object sender, ToggledEventArgs e)
        {
            Salasona.IsPassword = !e.Value;
        }
    }
}
