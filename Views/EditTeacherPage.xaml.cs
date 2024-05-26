using Koolitused.Models;
using Koolitused.Services;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

namespace Koolitused.Views
{
    public partial class EditTeacherPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private Opetaja _teacher;

        public EditTeacherPage(Opetaja teacher = null)
        {
            InitializeComponent();
            _databaseService = new DatabaseService();

            if (teacher != null)
            {
                _teacher = teacher;
                LoadTeacherData();
            }
            else
            {
                _teacher = new Opetaja();
            }
        }

        private void LoadTeacherData()
        {
            TeacherNameEntry.Text = _teacher.Opetajanimi;
            RollEntry.Text = _teacher.Roll.ToString();
        }

        private async void OnSaveTeacherClicked(object sender, EventArgs e)
        {
            _teacher.Opetajanimi = TeacherNameEntry.Text;
            int.TryParse(RollEntry.Text, out int roll);
            _teacher.Roll = roll;

            if (string.IsNullOrWhiteSpace(_teacher.Opetajanimi))
            {
                await DisplayAlert("Viga", "Palun sisesta õpetaja nimi.", "OK");
                return;
            }

            try
            {
                if (_teacher.Id == 0)
                {
                    await _databaseService.SaveTeacherAsync(_teacher);
                }
                else
                {
                    await _databaseService.UpdateTeacherAsync(_teacher);
                }

                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Viga", $"Andmete salvestamisel tekkis viga: {ex.Message}", "OK");
            }
        }
    }
}