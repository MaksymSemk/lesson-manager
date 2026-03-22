using LessonManager.Views;

namespace LessonManager.ViewModels;

public class LessonDetailsViewModel : ViewModelBase
{
    public LessonView Lesson { get; }

    public LessonDetailsViewModel(LessonView lesson)
    {
        Lesson = lesson;
    }
}