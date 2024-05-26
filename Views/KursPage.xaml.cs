using Microsoft.Maui.Controls;
using Koolitused.Models;
using Koolitused.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Koolitused.Views
{
    public partial class KursPage : ContentPage, INotifyPropertyChanged
    {
        public ObservableCollection<Koolitus> Courses { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly DatabaseService _databaseService;

        public KursPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
            Courses = new ObservableCollection<Koolitus>();
            LoadCourses();
            BindingContext = this;
        }

        private async Task LoadCourses()
        {
            var courses = await _databaseService.GetCoursesAsync();
            foreach (var course in courses)
            {
                var teacher = await _databaseService.GetTeacherByIdAsync(course.OpetajaId);
                course.Opetajanimi = teacher?.Opetajanimi;
                Courses.Add(course);
            }
        }
    }
}