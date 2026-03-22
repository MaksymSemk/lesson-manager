using System.Collections.ObjectModel;
using LessonManager.Services;
using LessonManager.Views;

namespace LessonManager.ViewModels;

public class SubjectsListViewModel : ViewModelBase
{
    private readonly IDataService _dataService;
    private SubjectView? _selectedSubject;
    public SubjectView? SelectedSubject
    {
        get => _selectedSubject;
        set { _selectedSubject = value; OnPropertyChanged(); }
    }

    public ObservableCollection<SubjectView> Subjects { get; } = new();

    public SubjectsListViewModel(IDataService dataService)
    {
        _dataService = dataService;
        LoadSubjects();
    }

    private void LoadSubjects()
    {
        var subjects = _dataService.GetAllSubjects();
        foreach (var s in subjects) Subjects.Add(s);
    }
}