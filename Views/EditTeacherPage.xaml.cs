using Koolitused.Models;
using Koolitused.Services;
using Koolitused.ViewModels;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Koolitused.Views
{
    public partial class EditTeacherPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private bool isNewTeacher;
        private Opetaja teacher;

        public EditTeacherPage()
        {
            InitializeComponent();
            BindingContext = new OpetajaListViewModel();
            _databaseService = new DatabaseService();
            isNewTeacher = true;
        }

        public EditTeacherPage(Opetaja selectedTeacher) : this()
        {
            isNewTeacher = false;
            teacher = selectedTeacher;
            InitializePage();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadRoles();
        }

        private void InitializePage()
        {
            TeacherNameEntry.Text = teacher?.Opetajanimi;
            RolePicker.SelectedItem = teacher?.TeacherRole;
        }

        private async void LoadRoles()
        {
            List<Roll> roles = await _databaseService.GetRolesAsync();
            RolePicker.ItemsSource = roles;
        }

        private async void OnSaveTeacherClicked(object sender, EventArgs e)
        {
            Roll selectedRole = RolePicker.SelectedItem as Roll;

            Opetaja updatedTeacher = new Opetaja
            {
                Opetajanimi = TeacherNameEntry.Text,
                TeacherRole = selectedRole
            };

            if (isNewTeacher)
            {
                await _databaseService.SaveTeacherAsync(updatedTeacher);
            }
            else
            {
                updatedTeacher.Id = teacher.Id;
                await _databaseService.UpdateTeacherAsync(updatedTeacher);
            }
        }
    }
}
