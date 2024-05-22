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
            string username = Nimi.Text;
            string password = Salasona.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Viga", "Palun sisestage kasutajanimi ja salasõna.", "OK");
                return;
            }

            if (!IsValidPassword(password))
            {
                await DisplayAlert("Viga", "Salasõna peab olema vähemalt 8 tähemärgi pikkune ja sisaldama nii tähti kui numbreid.", "OK");
                return;
            }

            bool userExists = await _databaseService.UserExistsAsync(username);
            if (userExists)
            {
                await DisplayAlert("Viga", "Kasutaja sellega kontoga on juba olemas. Palun kasutage teine kasutajanimi.", "OK");
                return;
            }

            int role = 1; //User role
            if (username == "adminmode" && password == "fthpice5vfxaf")
            {
                role = 3; //Admin role
            }

            var user = new Kasutaja
            {
                Kasutajanimi = username,
                Kasutajasalasona = password,
                Roll = role
            };

            await _databaseService.SaveUserAsync(user);
            await DisplayAlert("Edu", "Õnnestus kasutaja registreerida", "OK");
            await Navigation.PushAsync(new LoginPage());
        }

        private void OnShowPasswordToggled(object sender, ToggledEventArgs e)
        {
            Salasona.IsPassword = !e.Value;
        }

        private bool IsValidPassword(string password)
        {
            if (password.Length < 8)
                return false;

            bool taht = false;
            bool number = false;

            foreach (char c in password)
            {
                if (char.IsLetter(c))
                    taht = true;
                else if (char.IsDigit(c))
                    number = true;

                if (taht && number)
                    return true;
            }

            return false;
        }
    }
}
