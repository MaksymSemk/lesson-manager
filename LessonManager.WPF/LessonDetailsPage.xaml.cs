using LessonManager.Models.Enums;
using LessonManager.Services;
using LessonManager.ViewModels;
using LessonManager.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace LessonManager.WPF;

public partial class LessonDetailsPage : Page
{
    public LessonDetailsPage(LessonView lessonView)
    {
        InitializeComponent();
        var dataService = App.ServiceProvider.GetRequiredService<IDataService>();
        var viewModel = new LessonDetailsViewModel(dataService, lessonView);
        DataContext = viewModel;

        viewModel.LessonDeleted += (s, e) =>
        {
            NavigationService?.GoBack();
        };
    }

    private void Back_Click(object sender, RoutedEventArgs e)
    {
        NavigationService.GoBack();
    }
}

public static class EnumHelperLesson
{
    public static Array LessonTypes => Enum.GetValues(typeof(LessonType));
}