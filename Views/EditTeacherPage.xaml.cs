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
            LoadRoles();

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

        private async void LoadRoles()
        {
            var roles = await _databaseService.GetRolesAsync();
            foreach (var role in roles)
            {
                var roleLabel = new Label
                {
                    Text = $"{role.Id}: {role.Rollnimi}",
                    FontSize = 18,
                    VerticalOptions = LayoutOptions.Center
                };
                RolesContainer.Children.Add(roleLabel);
            }
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
                var existingTeacher = await _databaseService.GetTeacherByNameAsync(_teacher.Opetajanimi);
                if (existingTeacher != null && existingTeacher.Id != _teacher.Id)
                {
                    await DisplayAlert("Viga", "Selline õpetaja on juba olemas.", "OK");
                    return;
                }

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