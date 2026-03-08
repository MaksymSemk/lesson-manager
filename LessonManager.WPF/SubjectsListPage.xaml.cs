using LessonManager.Services;
using LessonManager.Views;
using LessonManager.WPF;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LessonManager.WPF
{
    public partial class SubjectsListPage : Page
    {
        public SubjectsListPage()
        {
            InitializeComponent();

            var dataService = App.ServiceProvider.GetRequiredService<IDataService>();

            var subjects = dataService.GetAllSubjects();

            SubjectsList.ItemsSource = subjects;
        }

        private void SubjectsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SubjectsList.SelectedItem is SubjectView selectedSubject)
            {
                NavigationService.Navigate(new SubjectDetailsPage(selectedSubject));

                SubjectsList.SelectedItem = null;
            }
        }
    }
}
