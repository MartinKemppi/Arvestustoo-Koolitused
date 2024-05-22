using Microsoft.Maui.Controls;
using Koolitused.Models;
using Koolitused.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Koolitused.Views;

public partial class ManageCoursesPage : ContentPage
{
    private readonly DatabaseService _databaseService;

    public ManageCoursesPage()
    {
        InitializeComponent();
        _databaseService = new DatabaseService();
        LoadCourses();
    }

    private async void LoadCourses()
    {
        List<Koolitus> courses = await _databaseService.GetCoursesAsync();
        CoursesListView.ItemsSource = courses;
    }

    private async void OnAddCourseClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EditCoursePage());
    }

    private async void OnEditCourseClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var course = button?.BindingContext as Koolitus;
        if (course != null)
        {
            await Navigation.PushAsync(new EditCoursePage(course));
        }
    }

    private async void OnDeleteCourseClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var course = button?.BindingContext as Koolitus;
        if (course != null)
        {
            await _databaseService.DeleteCourseAsync(course);
            LoadCourses();
        }
    }

}