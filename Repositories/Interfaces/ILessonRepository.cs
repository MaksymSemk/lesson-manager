using LessonManager.Models;

namespace LessonManager.Repositories.Interfaces;

public interface ILessonRepository
{
    Task<IEnumerable<Lesson>> GetBySubjectIdAsync(int subjectId);
    Task<Lesson> GetByIdAsync(int id);
    Task<Lesson> AddAsync(Lesson lesson);
    Task<Lesson> UpdateAsync(Lesson lesson);
    Task DeleteAsync(int id);
    Task DeleteBySubjectIdAsync(int subjectId);
}