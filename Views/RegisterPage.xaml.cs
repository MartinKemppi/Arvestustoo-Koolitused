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
                if (!IsValidPassword(user.Kasutajasalasona))
                {
                    await DisplayAlert("Viga", "Salasõna peab olema vähemalt 8 tähemärgi pikkune ja sisaldama nii tähti kui numbreid.", "OK");
                    return;
                }

                bool userExists = await _databaseService.UserExistsAsync(user.Kasutajanimi);

                if (userExists)
                {
                    await DisplayAlert("Viga", "Kasutaja sellega kontoga on juba olemas. Palun kasutage teine kasutajanimi.", "OK");
                }
                else
                {
                    await _databaseService.SaveUserAsync(user);
                    await DisplayAlert("Edu", "Õnnestus kasutaja registreerida", "OK");
                    await Navigation.PushAsync(new LoginPage());
                }
            }
            else
            {
                await DisplayAlert("Viga", "Palun sisestage kasutajanimi ja salasõna.", "OK");
            }
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
