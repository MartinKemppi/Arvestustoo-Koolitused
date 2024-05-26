using Koolitused.Services;
using Koolitused.Models;
using System;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Linq;

namespace Koolitused.Views;

public partial class LogitudPage : ContentPage
{
    private readonly DatabaseService _databaseService;
    public LogitudPage()
	{
		InitializeComponent();
	}
    private async void OnLogiValjaClicked(object sender, EventArgs e)
    {
        SessionManager.ClearSession();

        await Navigation.PushAsync(new MainPage());
    }
    private async void OnKursClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new KursPage());
    }
    private async void OnInfoClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new InfoPage());
    }
    private async void OnMinukursClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MinuKursPage());
    }
    private async void OnRegistreeriKursileClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegCoursePage(SessionManager.LoggedInUsername));
    }
}