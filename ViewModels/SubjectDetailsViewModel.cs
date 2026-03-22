using System.Collections.ObjectModel;
using LessonManager.Services;
using LessonManager.Views;

namespace LessonManager.ViewModels;

public class SubjectDetailsViewModel : ViewModelBase
{
    private readonly IDataService _dataService;
    public SubjectView Subject { get; }

    private LessonView? _selectedLesson;
    public LessonView? SelectedLesson
    {
        get => _selectedLesson;
        set { _selectedLesson = value; OnPropertyChanged(); }
    }
    public ObservableCollection<LessonView> Lessons { get; } = new();

    public SubjectDetailsViewModel(IDataService dataService, SubjectView subject)
    {
        _dataService = dataService;
        Subject = subject;
        LoadLessons();
    }

    private void LoadLessons()
    {
        _dataService.LoadLessonsForSubject(Subject);
        foreach (var lesson in Subject.Lessons)
        {
            Lessons.Add(lesson);
        }
    }
}