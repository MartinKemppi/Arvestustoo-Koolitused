using Koolitused.Models;
using Koolitused.Services;

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
        var courses = await _databaseService.GetCoursesAsync();
        var teachers = await _databaseService.GetTeachersAsync();

        foreach (var course in courses)
        {
            var teacher = teachers.FirstOrDefault(t => t.Id == course.OpetajaId);
            course.Opetajanimi = teacher != null ? teacher.Opetajanimi : "Unknown";
        }

        CoursesListView.ItemsSource = courses;
    }

    private async void OnAddCourseClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EditCoursePage());
        LoadCourses();
    }

    private async void OnEditCourseClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var course = button?.BindingContext as Koolitus;
        if (course != null)
        {
            bool confirmed = await DisplayAlert("Muuda kursus", $"Kas soovite muuta kursust '{course.Koolitusnimi}'?", "Jah", "Ei");
            if (confirmed)
            {
                await Navigation.PushAsync(new EditCoursePage(course));
                LoadCourses();
            }
        }
        else
        {
            await DisplayAlert("Viga", "Palun valige kursus, mida muuta.", "OK");
        }
    }

    private async void OnDeleteCourseClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var course = button?.BindingContext as Koolitus;
        if (course != null)
        {
            bool confirmed = await DisplayAlert("Kustuta kursus", $"Kas soovite kustutada kursust '{course.Koolitusnimi}'?", "Jah", "Ei");
            if (confirmed)
            {
                await _databaseService.DeleteCourseAsync(course);
                LoadCourses();
            }
        }
        else
        {
            await DisplayAlert("Viga", "Palun valige kursus, mida kustutada.", "OK");
        }
    }
}