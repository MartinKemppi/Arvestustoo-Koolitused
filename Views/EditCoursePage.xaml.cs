using Microsoft.Maui.Controls;
using Koolitused.Models;
using Koolitused.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Koolitused.Views
{
    public partial class EditCoursePage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private Koolitus _course;

        public EditCoursePage(Koolitus course = null)
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
            LoadTeachers();

            if (course != null)
            {
                _course = course;
                CourseNameEntry.Text = course.Koolitusnimi;
                CourseDatePicker.Date = DateTime.Parse(course.Kuupaev);
                PriceEntry.Text = course.Hind.ToString();

                // Set the selected teacher in the Picker
                var selectedTeacher = TeacherPicker.ItemsSource.Cast<Opetaja>().FirstOrDefault(t => t.Id == course.OpetajaId);
                if (selectedTeacher != null)
                {
                    TeacherPicker.SelectedItem = selectedTeacher;
                }
            }
            else
            {
                _course = new Koolitus();
            }
        }

        private async void LoadTeachers()
        {
            var teachers = await _databaseService.GetTeachersAsync();
            TeacherPicker.ItemsSource = teachers;
            TeacherPicker.ItemDisplayBinding = new Binding("Opetajanimi");
        }

        private async void OnSaveCourseClicked(object sender, EventArgs e)
        {
            _course.Koolitusnimi = CourseNameEntry.Text;

            if (TeacherPicker.SelectedItem is Opetaja selectedTeacher)
            {
                _course.OpetajaId = selectedTeacher.Id;
            }
            else
            {
                await DisplayAlert("Viga", "Palun valige õpetaja.", "OK");
                return;
            }

            _course.Kuupaev = CourseDatePicker.Date.ToString("yyyy-MM-dd");
            if (int.TryParse(PriceEntry.Text, out int price))
            {
                _course.Hind = price;
            }
            else
            {
                await DisplayAlert("Viga", "Palun sisestage kehtiv hind.", "OK");
                return;
            }

            if (string.IsNullOrEmpty(_course.Koolitusnimi) || _course.OpetajaId <= 0 || string.IsNullOrEmpty(_course.Kuupaev) || _course.Hind <= 0)
            {
                await DisplayAlert("Viga", "Palun täitke kõik väljad õigesti.", "OK");
                return;
            }

            if (_course.Id == 0)
            {
                await _databaseService.SaveCourseAsync(_course);
            }
            else
            {
                await _databaseService.UpdateCourseAsync(_course);
            }

            await Navigation.PopAsync();
        }
    }
}