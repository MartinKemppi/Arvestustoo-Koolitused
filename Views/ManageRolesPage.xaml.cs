using Koolitused.Models;
using Koolitused.Services;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

namespace Koolitused.Views
{
    public partial class ManageRolesPage : ContentPage
    {
        private readonly DatabaseService _databaseService;

        public ManageRolesPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
            LoadRoles();
        }

        private async void OnCreateRoleClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditRolePage());
        }

        private async void OnEditRoleClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var role = button?.BindingContext as Roll;
            if (role != null)
            {
                bool confirmed = await DisplayAlert("Muuda rolli", $"Kas soovite muuta rolli '{role.Rollnimi}'?", "Jah", "Ei");
                if (confirmed)
                {
                    await Navigation.PushAsync(new EditRolePage(role));
                }
            }
            else
            {
                await DisplayAlert("Viga", "Palun valige roll, mida muuta.", "OK");
            }
        }

        private async void OnDeleteRoleClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var role = button?.BindingContext as Roll;
            if (role != null)
            {
                bool confirmed = await DisplayAlert("Kustuta roll", $"Kas soovite kustutada rolli '{role.Rollnimi}'?", "Jah", "Ei");
                if (confirmed)
                {
                    await _databaseService.DeleteRoleAsync(role);
                    LoadRoles();
                }
            }
            else
            {
                await DisplayAlert("Viga", "Palun valige roll, mida kustutada.", "OK");
            }
        }

        private async void LoadRoles()
        {
            List<Roll> roles = await _databaseService.GetRolesAsync();
            RolesListView.ItemsSource = roles;
        }
    }
}
