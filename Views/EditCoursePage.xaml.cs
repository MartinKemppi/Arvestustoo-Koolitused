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
            CourseNameEntry.Text = course.Nimi;
            CourseDescriptionEntry.Text = course.Kirjeldus;
        }
        else
        {
            _course = new Koolitus();
        }
    }

    private async void OnSaveCourseClicked(object sender, EventArgs e)
    {
        _course.Nimi = CourseNameEntry.Text;
        _course.Kirjeldus = CourseDescriptionEntry.Text;

        if (string.IsNullOrEmpty(_course.Nimi) || string.IsNullOrEmpty(_course.Kirjeldus))
        {
            await DisplayAlert("Viga", "Palun täitke kõik väljad.", "OK");
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