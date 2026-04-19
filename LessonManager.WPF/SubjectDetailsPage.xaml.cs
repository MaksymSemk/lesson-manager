using LessonManager.Models.Enums;
using LessonManager.Services;
using LessonManager.ViewModels;
using LessonManager.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace LessonManager.WPF;

public partial class SubjectDetailsPage : Page
{
    private readonly SubjectDetailsViewModel _viewModel;

    public SubjectDetailsPage(SubjectView subject)
    {
        InitializeComponent();
        var dataService = App.ServiceProvider.GetRequiredService<IDataService>();
        _viewModel = new SubjectDetailsViewModel(dataService, subject);
        DataContext = _viewModel;
        Loaded += async (s, e) => await _viewModel.LoadLessonsAsync();

        _viewModel.SubjectDeleted += (s, e) =>
        {
            NavigationService?.GoBack();
        };
    }

    private void ViewLessonButton_Click(object sender, RoutedEventArgs e)
    {
        if (LessonsList.SelectedItem is LessonView selectedLesson)
        {
            NavigationService.Navigate(new LessonDetailsPage(selectedLesson));
        }
    }

    private void Back_Click(object sender, RoutedEventArgs e) => NavigationService.GoBack();
}

public static class EnumHelper
{
    public static Array KnowledgeAreas => Enum.GetValues(typeof(KnowledgeArea));
}