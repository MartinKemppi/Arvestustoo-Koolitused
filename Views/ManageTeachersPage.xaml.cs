using Koolitused.Models;
using Koolitused.Services;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

namespace Koolitused.Views;

public partial class ManageTeachersPage : ContentPage
{
    private readonly DatabaseService _databaseService;

    public ManageTeachersPage()
	{
		InitializeComponent();
        _databaseService = new DatabaseService();
        LoadTeachers();
    }

    private async void OnCreateTeacherClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EditTeacherPage());
    }

    private async void OnEditTeacherClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var teacher = button?.BindingContext as Opetaja;
        if (teacher != null)
        {
            bool confirmed = await DisplayAlert("Muuda õpetaja", $"Kas soovite muuta õpetajat '{teacher.Opetajanimi}'?", "Jah", "Ei");
            if (confirmed)
            {
                await Navigation.PushAsync(new EditTeacherPage(teacher));
            }
        }
        else
        {
            await DisplayAlert("Viga", "Palun valige õpetaja, keda muuta.", "OK");
        }
    }

    private async void OnDeleteTeacherClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var teacher = button?.BindingContext as Opetaja;
        if (teacher != null)
        {
            bool confirmed = await DisplayAlert("Kustuta õpetaja", $"Kas soovite kustutada õpetajat '{teacher.Opetajanimi}'?", "Jah", "Ei");
            if (confirmed)
            {
                int rowsDeleted = await _databaseService.DeleteTeacherAsync(teacher);

                if (rowsDeleted > 0)
                {
                    LoadTeachers();
                }
                else
                {
                    await DisplayAlert("Ошибка", "Произошла ошибка при удалении учителя.", "OK");
                }
            }
        }
        else
        {
            await DisplayAlert("Ошибка", "Не удалось получить информацию об учителе.", "OK");
        }
    }


    private async void LoadTeachers()
    {
        List<Opetaja> teachers = await _databaseService.GetTeachersAsync();
        TeachersListView.ItemsSource = teachers;
    }
}