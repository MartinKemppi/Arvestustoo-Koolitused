using Koolitused.Models;
using Koolitused.Services;
using Microsoft.Maui.Controls;
using System;
using System.Linq;

namespace Koolitused.Views
{
    public partial class EditUserPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private Kasutaja _user;

        public EditUserPage(Kasutaja user = null)
        {
            InitializeComponent();
            _databaseService = new DatabaseService();

            if (user != null)
            {
                _user = user;
                UsernameEntry.Text = user.Kasutajanimi;
                PasswordEntry.Text = user.Kasutajasalasona;
            }
            else
            {
                _user = new Kasutaja();
            }

            LoadRoles();
        }

        private async void LoadRoles()
        {
            var roles = await _databaseService.GetRolesAsync();
            RolePicker.ItemsSource = roles;
            RolePicker.ItemDisplayBinding = new Binding("Rollnimi");

            if (_user.Roll != 0)
            {
                var selectedRole = roles.FirstOrDefault(r => r.Id == _user.Roll);
                RolePicker.SelectedItem = selectedRole;
            }
        }

        private async void OnSaveUserClicked(object sender, EventArgs e)
        {
            _user.Kasutajanimi = UsernameEntry.Text;
            _user.Kasutajasalasona = PasswordEntry.Text;
            var selectedRole = RolePicker.SelectedItem as Roll;

            if (selectedRole != null)
            {
                _user.Roll = selectedRole.Id;
            }

            if (string.IsNullOrEmpty(_user.Kasutajanimi) || string.IsNullOrEmpty(_user.Kasutajasalasona) || _user.Roll == 0)
            {
                await DisplayAlert("Viga", "Palun täitke kõik väljad õigesti.", "OK");
                return;
            }

            if (_user.Id == 0)
            {
                await _databaseService.SaveUserAsync(_user);
            }
            else
            {
                await _databaseService.UpdateUserAsync(_user);
            }

            await Navigation.PopAsync();
        }

        private void OnShowPasswordToggled(object sender, ToggledEventArgs e)
        {
            PasswordEntry.IsPassword = !e.Value;
        }
    }
}
