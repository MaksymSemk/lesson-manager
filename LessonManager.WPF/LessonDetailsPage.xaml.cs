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
using LessonManager.Views;

namespace LessonManager.WPF
{

    public partial class LessonDetailsPage : Page
    {
        public LessonDetailsPage(LessonView lessonView)
        {
            InitializeComponent();
            DisplayLessonDetails(lessonView);
        }

        private void DisplayLessonDetails(LessonView view)
        {
            var lesson = view.BaseLesson;

            TxtTopic.Text = $"Тема: {lesson.Topic}";
            TxtType.Text = $"Тип заняття: {lesson.Type}";
            TxtDate.Text = $"Дата: {lesson.Date:dd.MM.yyyy}";
            TxtTime.Text = $"Час проведення: {lesson.StartTime} — {lesson.EndTime}";

            TxtDuration.Text = $"Тривалість заняття: {view.Duration:hh\\:mm}";
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}