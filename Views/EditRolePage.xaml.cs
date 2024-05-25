using Koolitused.Models;
using Koolitused.Services;
using Microsoft.Maui.Controls;
using System;

namespace Koolitused.Views
{
    public partial class EditRolePage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private Roll _role;

        public EditRolePage(Roll role = null)
        {
            InitializeComponent();
            _databaseService = new DatabaseService();

            if (role != null)
            {
                _role = role;
                RoleNameEntry.Text = role.Rollnimi;
            }
            else
            {
                _role = new Roll();
            }
        }

        private async void OnSaveRoleClicked(object sender, EventArgs e)
        {
            string roleName = RoleNameEntry.Text;

            if (string.IsNullOrEmpty(roleName))
            {
                await DisplayAlert("Viga", "Palun sisestage rolli nimi.", "OK");
                return;
            }

            _role.Rollnimi = roleName;

            try
            {
                if (_role.Id == 0)
                {
                    await _databaseService.SaveRoleAsync(_role);
                }
                else
                {
                    await _databaseService.UpdateRoleAsync(_role);
                }

                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", $"Произошла ошибка при сохранении данных: {ex.Message}", "OK");
            }
        }
    }
}
