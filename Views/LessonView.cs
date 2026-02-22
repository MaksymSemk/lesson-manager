using LessonManager.Models;

namespace LessonManager.Views;

public class LessonView
{
    public Lesson BaseLesson { get; }
    public TimeSpan Duration => BaseLesson.EndTime - BaseLesson.StartTime;

    public LessonView(Lesson lesson)
    {
        BaseLesson = lesson;
    }
}