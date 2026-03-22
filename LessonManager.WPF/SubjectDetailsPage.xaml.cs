using LessonManager.Services;
using LessonManager.ViewModels;
using LessonManager.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows;
using System.Windows.Controls;
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

    public partial class SubjectDetailsPage : Page
    {

        public SubjectDetailsPage(SubjectView subject)
        {
            InitializeComponent();
            var dataService = App.ServiceProvider.GetRequiredService<IDataService>();
            DataContext = new SubjectDetailsViewModel(dataService, subject);
        }

        private void LessonsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ListBox)sender).SelectedItem is LessonView selectedLesson)
            {
                NavigationService.Navigate(new LessonDetailsPage(selectedLesson));
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e) => NavigationService.GoBack();
    }
}