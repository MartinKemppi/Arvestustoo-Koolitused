using System;
using System.Collections.ObjectModel;
using Koolitused.Services;
using Microsoft.Maui.Controls;

namespace Koolitused.Views
{
    public partial class MinuKursPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        public ObservableCollection<string> CourseList { get; set; } = new ObservableCollection<string>();

        public MinuKursPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
            LoadRegisteredCourses();
        }

        private async void LoadRegisteredCourses()
        {
            try
            {
                var registeredCourses = await _databaseService.GetRegisteredCoursesAsync(SessionManager.LoggedInUsername);

                foreach (var regCourse in registeredCourses)
                {
                    var course = await _databaseService.GetCourseByIdAsync(regCourse.CourseId);
                    if (course != null)
                    {
                        var courseInfo = $"{course.Koolitusnimi}, {course.Kuupaev}";
                        CourseList.Add(courseInfo);
                    }
                }

                CoursesListView.ItemsSource = CourseList;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Viga", $"Ei õnnestunud laadida registreeritud kursusi: {ex.Message}", "OK");
            }
        }
    }
}
