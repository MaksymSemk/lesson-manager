using LessonManager.Models;

namespace LessonManager.Views;

public class SubjectView
{
    public Subject BaseSubject { get; }
    public List<LessonView> Lessons { get; set; } = new();

    public TimeSpan TotalDuration =>
        TimeSpan.FromTicks(Lessons.Sum(l => l.Duration.Ticks));

    public SubjectView(Subject subject)
    {
        BaseSubject = subject;
    }
}