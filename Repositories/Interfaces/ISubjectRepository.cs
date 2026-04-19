using LessonManager.Models;

namespace LessonManager.Repositories.Interfaces;

public interface ISubjectRepository
{
    Task<IEnumerable<Subject>> GetAllAsync();
    Task<Subject> GetByIdAsync(int id);
    Task<Subject> AddAsync(Subject subject);
    Task<Subject> UpdateAsync(Subject subject);
    Task DeleteAsync(int id);
}