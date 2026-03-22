using LessonManager.Models;

namespace LessonManager.Repositories.Interfaces;

public interface ISubjectRepository
{
    IEnumerable<Subject> GetAll();
    Subject GetById(int id);
}