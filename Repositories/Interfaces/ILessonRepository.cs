using LessonManager.Models;

namespace LessonManager.Repositories.Interfaces;

public interface ILessonRepository
{
    IEnumerable<Lesson> GetBySubjectId(int subjectId);
}