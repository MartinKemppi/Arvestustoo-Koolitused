using Koolitused.Models;
using Koolitused.Services;
using Microsoft.Maui.Controls;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Koolitused.Views
{
    public partial class KasutajaPage : ContentPage
    {
        private readonly DatabaseService _databaseService;

        public KasutajaPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
            LoadUsers();
        }

        private async void LoadUsers()
        {
            var users = await _databaseService.GetUsersAsync();
            var roles = await _databaseService.GetRolesAsync();

            foreach (var user in users)
            {
                var role = roles.FirstOrDefault(r => r.Id == user.Roll);
                user.Rollnimi = role != null ? role.Rollnimi : "Unknown";
            }

            UsersListView.ItemsSource = users;
        }

        private async void OnAddUserClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditUserPage());
            LoadUsers();
        }

        private async void OnEditUserClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var user = button?.BindingContext as Kasutaja;
            if (user != null)
            {
                await Navigation.PushAsync(new EditUserPage(user));
                LoadUsers();
            }
        }

        private async void OnDeleteUserClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var user = button?.BindingContext as Kasutaja;
            if (user != null)
            {
                bool confirmed = await DisplayAlert("Kustuta kasutaja", $"Kas soovite kustutada kasutaja '{user.Kasutajanimi}'?", "Jah", "Ei");
                if (confirmed)
                {
                    await _databaseService.DeleteUserAsync(user);
                    LoadUsers();
                }
            }
            else
            {
                await DisplayAlert("Viga", "Palun valige kasutaja, mida kustutada.", "OK");
            }
        }
    }
}
