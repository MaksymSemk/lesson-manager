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

using System.Windows;
using System.Windows.Controls;
using LessonManager.Services;
using LessonManager.Views;
using Microsoft.Extensions.DependencyInjection;

namespace LessonManager.WPF
{

    public partial class SubjectDetailsPage : Page
    {
        private readonly SubjectView _subject;

        public SubjectDetailsPage(SubjectView subject)
        {
            InitializeComponent();
            _subject = subject;

            var dataService = App.ServiceProvider.GetRequiredService<IDataService>();

            dataService.LoadLessonsForSubject(_subject);

            TxtTitle.Text = _subject.BaseSubject.Title;
            TxtInfo.Text = $"Кредити: {_subject.BaseSubject.EctsCredits} | Сфера: {_subject.BaseSubject.Area}";
            LessonsList.ItemsSource = _subject.Lessons;
        }

        private void LessonsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LessonsList.SelectedItem is LessonView selectedLesson)
            {
                NavigationService.Navigate(new LessonDetailsPage(selectedLesson));
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack(); // Повернення назад
        }
    }
}