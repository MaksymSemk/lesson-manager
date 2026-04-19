using System.Windows.Input;
using LessonManager.Models.Enums;
using LessonManager.Services;
using LessonManager.Views;

namespace LessonManager.ViewModels;

public class LessonDetailsViewModel : ViewModelBase
{
    private readonly IDataService _dataService;
    private DateTime _date;
    private TimeOnly _startTime;
    private TimeOnly _endTime;
    private string _topic;
    private LessonType _type;

    public LessonView Lesson { get; }

    public DateTime Date
    {
        get => _date;
        set { _date = value; OnPropertyChanged(); }
    }

    public TimeOnly StartTime
    {
        get => _startTime;
        set { _startTime = value; OnPropertyChanged(); }
    }

    public TimeOnly EndTime
    {
        get => _endTime;
        set { _endTime = value; OnPropertyChanged(); }
    }

    public string Topic
    {
        get => _topic;
        set { _topic = value; OnPropertyChanged(); }
    }

    public LessonType Type
    {
        get => _type;
        set { _type = value; OnPropertyChanged(); }
    }

    public ICommand SaveChangesCommand { get; }
    public ICommand DeleteLessonCommand { get; }

    public event EventHandler<EventArgs>? LessonDeleted;

    public LessonDetailsViewModel(IDataService dataService, LessonView lesson)
    {
        _dataService = dataService;
        Lesson = lesson;

        Date = Lesson.BaseLesson.Date;
        StartTime = Lesson.BaseLesson.StartTime;
        EndTime = Lesson.BaseLesson.EndTime;
        Topic = Lesson.BaseLesson.Topic;
        Type = Lesson.BaseLesson.Type;

        SaveChangesCommand = new AsyncRelayCommand(_ => SaveChangesAsync());
        DeleteLessonCommand = new AsyncRelayCommand(_ => DeleteLessonAsync());
    }

    public async Task SaveChangesAsync()
    {
        try
        {
            IsLoading = true;
            ErrorMessage = string.Empty;

            Lesson.BaseLesson.Date = Date;
            Lesson.BaseLesson.StartTime = StartTime;
            Lesson.BaseLesson.EndTime = EndTime;
            Lesson.BaseLesson.Topic = Topic;
            Lesson.BaseLesson.Type = Type;

            await _dataService.UpdateLessonAsync(Lesson.BaseLesson);
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error saving changes: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }

    public async Task DeleteLessonAsync()
    {
        try
        {
            IsLoading = true;
            ErrorMessage = string.Empty;

            await _dataService.DeleteLessonAsync(Lesson.BaseLesson.Id);
            LessonDeleted?.Invoke(this, EventArgs.Empty);
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
}