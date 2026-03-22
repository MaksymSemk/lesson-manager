using LessonManager.Models;
using LessonManager.ViewModels;
using LessonManager.Views;
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

    public partial class LessonDetailsPage : Page
    {
        public LessonDetailsPage(LessonView lessonView)
        {
            InitializeComponent();
            DataContext = new LessonDetailsViewModel(lessonView);
        }


        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}