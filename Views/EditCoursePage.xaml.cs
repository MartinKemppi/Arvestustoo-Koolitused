using Microsoft.Maui.Controls;
using Koolitused.Models;
using Koolitused.Services;
using System;

namespace Koolitused.Views;

public partial class EditCoursePage : ContentPage
{
    private readonly DatabaseService _databaseService;
    private Koolitus _course;

    public EditCoursePage(Koolitus course = null)
    {
        InitializeComponent();
        _databaseService = new DatabaseService();

        if (course != null)
        {
            _course = course;
            CourseNameEntry.Text = course.Koolitusnimi;
            TeacherEntry.Text = course.Opetaja;
            DateEntry.Text = course.Kuupaev;
            PriceEntry.Text = course.Hind.ToString();
        }
        else
        {
            _course = new Koolitus();
        }
    }

    private async void OnSaveCourseClicked(object sender, EventArgs e)
    {
        _course.Koolitusnimi = CourseNameEntry.Text;
        _course.Opetaja = TeacherEntry.Text;
        _course.Kuupaev = DateEntry.Text;
        int.TryParse(PriceEntry.Text, out int price);
        _course.Hind = price;

        if (string.IsNullOrEmpty(_course.Koolitusnimi) || string.IsNullOrEmpty(_course.Opetaja) || string.IsNullOrEmpty(_course.Kuupaev) || _course.Hind <= 0)
        {
            await DisplayAlert("Viga", "Palun t�itke k�ik v�ljad �igesti.", "OK");
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