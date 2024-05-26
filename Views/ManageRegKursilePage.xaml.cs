using System;
using System.Collections.Generic;
using Koolitused.Models;
using Koolitused.Services;
using Microsoft.Maui.Controls;

namespace Koolitused.Views
{
    public partial class ManageRegKursilePage : ContentPage
    {
        private readonly DatabaseService _databaseService;

        public ManageRegKursilePage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
            LoadAllRegistrations();
        }

        private async void LoadAllRegistrations()
        {
            try
            {
                var allRegistrations = await _databaseService.GetAllRegistrationsAsync();
                CoursesListView.ItemsSource = allRegistrations;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Viga", $"Ei õnnestunud laadida kõiki kirjeid tabelist RegKursile: {ex.Message}", "OK");
            }
        }


        private async void OnDeleteRegistrationClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is RegKursile registration)
            {
                try
                {
                    await _databaseService.DeleteRegistrationAsync(registration);

                    LoadAllRegistrations();

                    await DisplayAlert("Õnnestumine", "Kursuse registreerimine on edukalt eemaldatud", "OK");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Viga", $"Kursuse registreerimise kustutamine ebaõnnestus: {ex.Message}", "OK");
                }
            }
        }
    }
}