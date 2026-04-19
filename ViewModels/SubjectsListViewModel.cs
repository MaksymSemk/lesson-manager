using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using LessonManager.Models;
using LessonManager.Services;
using LessonManager.Views;
using System.Linq;

namespace LessonManager.ViewModels;

public enum SubjectSortOption
{
    Name,
    Credits
}

public class SubjectsListViewModel : ViewModelBase
{
    private readonly IDataService _dataService;
    private SubjectView? _selectedSubject;
    private string _subjectNameFilter = string.Empty;
    private SubjectSortOption _subjectSortOption = SubjectSortOption.Name;
    private List<SubjectView> _allSubjects = new();

    public SubjectView? SelectedSubject
    {
        get => _selectedSubject;
        set { _selectedSubject = value; OnPropertyChanged(); }
    }

    public string SubjectNameFilter
    {
        get => _subjectNameFilter;
        set
        {
            if (_subjectNameFilter != value)
            {
                _subjectNameFilter = value;
                OnPropertyChanged();
                ApplySubjectFiltersAndSort();
            }
        }
    }

    public SubjectSortOption SubjectSortOption
    {
        get => _subjectSortOption;
        set
        {
            if (_subjectSortOption != value)
            {
                _subjectSortOption = value;
                OnPropertyChanged();
                ApplySubjectFiltersAndSort();
            }
        }
    }

    public ObservableCollection<SubjectView> Subjects { get; } = new();

    public ICommand LoadSubjectsCommand { get; }
    public ICommand DeleteSubjectCommand { get; }
    public ICommand AddSubjectCommand { get; }
    public ICommand SortByNameCommand { get; }
    public ICommand SortByCreditsCommand { get; }
    public ICommand ClearSubjectFiltersCommand { get; }

    public SubjectsListViewModel(IDataService dataService)
    {
        _dataService = dataService;
        
        LoadSubjectsCommand = new AsyncRelayCommand(_ => LoadSubjectsAsync());
        DeleteSubjectCommand = new AsyncRelayCommand(
            _ => DeleteSubjectAsync(),
            _ => SelectedSubject != null);
        AddSubjectCommand = new AsyncRelayCommand(_ => AddSubjectAsync());
        SortByNameCommand = new RelayCommand(_ => { SubjectSortOption = SubjectSortOption.Name; });
        SortByCreditsCommand = new RelayCommand(_ => { SubjectSortOption = SubjectSortOption.Credits; });
        ClearSubjectFiltersCommand = new RelayCommand(_ => ClearSubjectFilters());
    }

    public async Task LoadSubjectsAsync()
    {
        try
        {
            IsLoading = true;
            ErrorMessage = string.Empty;
            Subjects.Clear();
            _allSubjects.Clear();
            var subjects = await _dataService.GetAllSubjectsAsync();
            foreach (var s in subjects)
                _allSubjects.Add(s);
            ApplySubjectFiltersAndSort();
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error loading subjects: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }

    private void ApplySubjectFiltersAndSort()
    {
        var filtered = _allSubjects.AsEnumerable();
        if (!string.IsNullOrWhiteSpace(SubjectNameFilter))
            filtered = filtered.Where(s => s.BaseSubject.Title.Contains(SubjectNameFilter, System.StringComparison.OrdinalIgnoreCase));
        switch (SubjectSortOption)
        {
            case SubjectSortOption.Name:
                filtered = filtered.OrderBy(s => s.BaseSubject.Title);
                break;
            case SubjectSortOption.Credits:
                filtered = filtered.OrderByDescending(s => s.BaseSubject.EctsCredits);
                break;
        }
        Subjects.Clear();
        foreach (var s in filtered)
            Subjects.Add(s);
    }

    private void ClearSubjectFilters()
    {
        SubjectNameFilter = string.Empty;
        SubjectSortOption = SubjectSortOption.Name;
        ApplySubjectFiltersAndSort();
    }

    public async Task DeleteSubjectAsync()
    {
        if (SelectedSubject == null)
            return;

        var result = MessageBox.Show(
            $"Ви впевнені, що хочете видалити предмет '{SelectedSubject.BaseSubject.Title}'?\n\nВсі заняття також будуть видалені.",
            "Підтвердження видалення",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

        if (result != MessageBoxResult.Yes)
            return;

        try
        {
            IsLoading = true;
            ErrorMessage = string.Empty;
            
            await _dataService.DeleteSubjectAsync(SelectedSubject.BaseSubject.Id);
            Subjects.Remove(SelectedSubject);
            SelectedSubject = null;
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error deleting subject: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }

    public async Task AddSubjectAsync()
    {
        try
        {
            IsLoading = true;
            ErrorMessage = string.Empty;
            
            var newSubject = new Subject(0, "Новий предмет", 0, Models.Enums.KnowledgeArea.Programming);
            var added = await _dataService.AddSubjectAsync(newSubject);
            Subjects.Add(added);
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error adding subject: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }
}