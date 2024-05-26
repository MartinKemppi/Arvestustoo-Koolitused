using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Koolitused.Models;
using Koolitused.Services;
using Microsoft.Maui.Controls;

namespace Koolitused.Views
{
    public partial class MyStudentsPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private readonly string _loggedInUsername;

        public MyStudentsPage(string loggedInUsername)
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
            _loggedInUsername = loggedInUsername;
            LoadCoursesWithStudents();
        }

        private async void LoadCoursesWithStudents()
        {
            try
            {
                var teacher = await _databaseService.GetTeacherByUsernameAsync(_loggedInUsername);
                var courses = await _databaseService.GetCoursesByTeacherIdAsync(teacher.Id);

                var courseDetails = new List<CourseDetailsViewModel>();

                foreach (var course in courses)
                {
                    var students = await _databaseService.GetStudentsByCourseIdAsync(course.Id);
                    var courseDetailsViewModel = new CourseDetailsViewModel
                    {
                        CourseName = course.Koolitusnimi,
                        CourseDate = course.Kuupaev,
                        TeacherName = teacher.Opetajanimi,
                        Students = students
                    };

                    courseDetails.Add(courseDetailsViewModel);
                }

                courseDetails = courseDetails.OrderBy(cd => cd.CourseName).ToList();

                courseDetails = courseDetails.OrderBy(cd => DateTime.Parse(cd.CourseDate)).ToList();

                CoursesListView.ItemsSource = courseDetails;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Viga", $"Õpilaste laadimine ebaõnnestus: {ex.Message}", "OK");
            }
        }

    }
}