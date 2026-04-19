using LessonManager.ViewModels;
using LessonManager.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;

namespace LessonManager.WPF;

public partial class SubjectsListPage : Page
{
    private readonly SubjectsListViewModel _viewModel;

    public SubjectsListPage()
    {
        InitializeComponent();
        _viewModel = App.ServiceProvider.GetRequiredService<SubjectsListViewModel>();
        DataContext = _viewModel;
        Loaded += async (s, e) => await _viewModel.LoadSubjectsAsync();
    }

    private void ViewButton_Click(object sender, RoutedEventArgs e)
    {
        if (SubjectsList.SelectedItem is SubjectView selectedSubject)
        {
            NavigationService.Navigate(new SubjectDetailsPage(selectedSubject));
        }
    }
}