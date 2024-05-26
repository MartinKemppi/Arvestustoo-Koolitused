using Koolitused.Models;
using Koolitused.Services;
using System;
using Microsoft.Maui.Controls;

namespace Koolitused.Views
{
    public partial class EditRegCoursePage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private readonly string _loggedInUsername;
        private readonly int _selectedCourseId;

        public EditRegCoursePage(string loggedInUsername, Koolitus selectedCourse)
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
            _loggedInUsername = loggedInUsername;
            _selectedCourseId = selectedCourse.Id;
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            var regInfo = new RegKursile
            {
                CourseId = _selectedCourseId,
                Username = _loggedInUsername,
                Name = FirstNameEntry.Text,
                Surname = LastNameEntry.Text,
                Phone = PhoneEntry.Text,
                Email = EmailEntry.Text,
                AdditionalInfo = AdditionalInfoEntry.Text
            };

            try
            {
                await _databaseService.RegisterCourseAsync(regInfo);
                await DisplayAlert("Õnnestumine", "Olete kursusele edukalt registreerunud.", "OK");
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Viga", $"Registreerimine ebaõnnestus: {ex.Message}", "OK");
            }
        }
    }

}
