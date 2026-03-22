using LessonManager.ViewModels;
using LessonManager.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace LessonManager.WPF;

public partial class SubjectsListPage : Page
{
    public SubjectsListPage()
    {
        InitializeComponent();
        DataContext = App.ServiceProvider.GetRequiredService<SubjectsListViewModel>();
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