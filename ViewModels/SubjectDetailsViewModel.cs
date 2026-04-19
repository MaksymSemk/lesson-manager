using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using LessonManager.Models;
using LessonManager.Models.Enums;
using LessonManager.Services;
using LessonManager.Views;
using System.ComponentModel;
using System.Linq;

namespace LessonManager.ViewModels;

public enum LessonSortOption
{
    Name,
    Date,
    Duration
}

public class SubjectDetailsViewModel : ViewModelBase
{
    private readonly IDataService _dataService;
    private string _title;
    private double _ectsCredits;
    private KnowledgeArea _area;
    private LessonView? _selectedLesson;
    private AsyncRelayCommand? _deleteLessonCommand;
    private string _lessonNameFilter = string.Empty;
    private DateTime? _lessonDateFilter;
    private List<LessonView> _allLessons = new();

    public SubjectView Subject { get; }

    public string LessonNameFilter
    {
        get => _lessonNameFilter;
        set
        {
            if (_lessonNameFilter != value)
            {
                _lessonNameFilter = value;
                OnPropertyChanged();
                ApplyLessonFilters();
            }
        }
    }

    public DateTime? LessonDateFilter
    {
        get => _lessonDateFilter;
        set
        {
            if (_lessonDateFilter != value)
            {
                _lessonDateFilter = value;
                OnPropertyChanged();
                ApplyLessonFilters();
            }
        }
    }

    public string Title
    {
        get => _title;
        set { _title = value; OnPropertyChanged(); }
    }

    public double EctsCredits
    {
        get => _ectsCredits;
        set { _ectsCredits = value; OnPropertyChanged(); }
    }

    public KnowledgeArea Area
    {
        get => _area;
        set { _area = value; OnPropertyChanged(); }
    }

    public LessonView? SelectedLesson
    {
        get => _selectedLesson;
        set
        {
            _selectedLesson = value;
            OnPropertyChanged();
            _deleteLessonCommand?.RaiseCanExecuteChanged();
        }
    }

    public ObservableCollection<LessonView> Lessons { get; } = new();

    public ICommand LoadLessonsCommand { get; }
    public ICommand AddLessonCommand { get; }
    public ICommand DeleteLessonCommand => _deleteLessonCommand!;
    public ICommand SaveChangesCommand { get; }
    public ICommand DeleteSubjectCommand { get; }
    public ICommand ClearLessonFiltersCommand { get; }

    public event EventHandler<EventArgs>? SubjectDeleted;

    public SubjectDetailsViewModel(IDataService dataService, SubjectView subject)
    {
        _dataService = dataService;
        Subject = subject;
        
        Title = Subject.BaseSubject.Title;
        EctsCredits = Subject.BaseSubject.EctsCredits;
        Area = Subject.BaseSubject.Area;

        LoadLessonsCommand = new AsyncRelayCommand(_ => LoadLessonsAsync());
        AddLessonCommand = new AsyncRelayCommand(_ => AddLessonAsync());
        _deleteLessonCommand = new AsyncRelayCommand(
            _ => DeleteLessonAsync(),
            _ => SelectedLesson != null);
        SaveChangesCommand = new AsyncRelayCommand(_ => SaveChangesAsync());
        DeleteSubjectCommand = new AsyncRelayCommand(_ => DeleteSubjectAsync());
        ClearLessonFiltersCommand = new RelayCommand(_ => ClearLessonFilters());
    }

    private void ClearLessonFilters()
    {
        LessonNameFilter = string.Empty;
        LessonDateFilter = null;
        ApplyLessonFilters();
    }

    public async Task LoadLessonsAsync()
    {
        try
        {
            IsLoading = true;
            ErrorMessage = string.Empty;
            Lessons.Clear();
            _allLessons.Clear();
            var lessons = await _dataService.GetLessonsForSubjectAsync(Subject.BaseSubject.Id);
            foreach (var lesson in lessons)
            {
                _allLessons.Add(lesson);
            }
            ApplyLessonFilters();
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error loading lessons: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }

    private LessonSortOption _lessonSortOption = LessonSortOption.Date;
    public LessonSortOption LessonSortOption
    {
        get => _lessonSortOption;
        set
        {
            if (_lessonSortOption != value)
            {
                _lessonSortOption = value;
                OnPropertyChanged();
                ApplyLessonFilters();
            }
        }
    }

    private void ApplyLessonFilters()
    {
        var filtered = _allLessons.AsEnumerable();
        if (!string.IsNullOrWhiteSpace(LessonNameFilter))
            filtered = filtered.Where(l => l.BaseLesson.Topic.Contains(LessonNameFilter, System.StringComparison.OrdinalIgnoreCase));
        if (LessonDateFilter.HasValue)
            filtered = filtered.Where(l => l.BaseLesson.Date.Date == LessonDateFilter.Value.Date);
        // Sorting
        switch (LessonSortOption)
        {
            case LessonSortOption.Name:
                filtered = filtered.OrderBy(l => l.BaseLesson.Topic);
                break;
            case LessonSortOption.Date:
                filtered = filtered.OrderBy(l => l.BaseLesson.Date);
                break;
            case LessonSortOption.Duration:
                filtered = filtered.OrderByDescending(l => l.Duration);
                break;
        }
        Lessons.Clear();
        foreach (var lesson in filtered)
            Lessons.Add(lesson);
    }

    public async Task AddLessonAsync()
    {
        try
        {
            IsLoading = true;
            ErrorMessage = string.Empty;
            
            var newLesson = new Lesson(
                0,
                Subject.BaseSubject.Id,
                DateTime.Now,
                new TimeOnly(10, 0),
                new TimeOnly(11, 30),
                "New Lesson",
                LessonType.Lecture);

            var added = await _dataService.AddLessonAsync(newLesson);
            Lessons.Add(added);
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error adding lesson: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }

    public async Task DeleteLessonAsync()
    {
        if (SelectedLesson == null)
            return;

        var result = MessageBox.Show(
            $"Ви впевнені, що хочете видалити заняття '{SelectedLesson.BaseLesson.Topic}'?",
            "Підтвердження видалення",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

        if (result != MessageBoxResult.Yes)
            return;

        try
        {
            IsLoading = true;
            ErrorMessage = string.Empty;
            
            await _dataService.DeleteLessonAsync(SelectedLesson.BaseLesson.Id);
            Lessons.Remove(SelectedLesson);
            SelectedLesson = null;
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error deleting lesson: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }

    public async Task SaveChangesAsync()
    {
        try
        {
            IsLoading = true;
            ErrorMessage = string.Empty;

            Subject.BaseSubject.Title = Title;
            Subject.BaseSubject.EctsCredits = EctsCredits;
            Subject.BaseSubject.Area = Area;

            await _dataService.UpdateSubjectAsync(Subject.BaseSubject);
            MessageBox.Show("Зміни збережено успішно!", "Збереження", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Помилка збереження: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }

    public async Task DeleteSubjectAsync()
    {
        var result = MessageBox.Show(
            $"Ви впевнені, що хочете видалити предмет '{Title}'?\n\nВсі заняття також будуть видалені.",
            "Підтвердження видалення",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning);

        if (result != MessageBoxResult.Yes)
            return;

        try
        {
            IsLoading = true;
            ErrorMessage = string.Empty;
            await _dataService.DeleteSubjectAsync(Subject.BaseSubject.Id);
            SubjectDeleted?.Invoke(this, EventArgs.Empty);
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Помилка видалення: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }
}