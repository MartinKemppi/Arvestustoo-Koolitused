using System;
using Koolitused.Models;
using Koolitused.Services;
using Microsoft.Maui.Controls;

namespace Koolitused.Views
{
    public partial class RegCoursePage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private readonly string _loggedInUsername;

        public RegCoursePage(string loggedInUsername)
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
            _loggedInUsername = loggedInUsername;
            LoadCourses();
        }

        private async void LoadCourses()
        {
            try
            {
                var courses = await _databaseService.GetCoursesAsync();
                CoursesListView.ItemsSource = courses;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Viga", $"Ei õnnestunud laadida kursusi: {ex.Message}", "OK");
            }
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            var selectedCourse = (sender as Button)?.BindingContext as Koolitus;
            if (selectedCourse != null)
            {
                await Navigation.PushAsync(new EditRegCoursePage(_loggedInUsername, selectedCourse));
            }
        }
    }
}