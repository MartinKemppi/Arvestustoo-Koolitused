using System;
using System.Collections.ObjectModel;
using Koolitused.Services;
using Koolitused.Models;
using Microsoft.Maui.Controls;

namespace Koolitused.Views
{
    public partial class MinuKursPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        public ObservableCollection<CourseDetailsViewModel> CourseList { get; set; } = new ObservableCollection<CourseDetailsViewModel>();

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
                    var course = await _databaseService.GetCourseByIdAsyncs(regCourse.CourseId);
                    if (course != null)
                    {
                        var courseDetails = new CourseDetailsViewModel
                        {
                            CourseName = course.Koolitusnimi,
                            CourseDate = course.Kuupaev,
                            TeacherName = course.Opetajanimi,
                            Students = await _databaseService.GetStudentsByCourseIdAsync(course.Id)
                        };
                        CourseList.Add(courseDetails);
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