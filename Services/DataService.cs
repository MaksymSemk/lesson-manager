using LessonManager.Models;
using LessonManager.Views;

namespace LessonManager.Services;

public class DataService
{
    public List<SubjectView> GetAllSubjects()
    {
        return MockStorage.Subjects.Select(s => new SubjectView(s)).ToList();
    }

    public void LoadLessonsForSubject(SubjectView subjectView)
    {
        var filteredLessons = MockStorage.Lessons
            .Where(l => l.SubjectId == subjectView.BaseSubject.Id)
            .Select(l => new LessonView(l))
            .ToList();

        subjectView.Lessons = filteredLessons;
    }
}