using LessonManager.Models;
using LessonManager.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LessonManager.Repositories;

public class SubjectRepository : ISubjectRepository
{
    private readonly FileStorageContext _context;

    public SubjectRepository()
    {
        _context = new FileStorageContext();
    }

    public Task<IEnumerable<Subject>> GetAllAsync()
    {
        return Task.FromResult(_context.Subjects.AsEnumerable());
    }

    public Task<Subject> GetByIdAsync(int id)
    {
        var subject = _context.Subjects.FirstOrDefault(s => s.Id == id);
        return Task.FromResult(subject);
    }

    public Task<Subject> AddAsync(Subject subject)
    {
        var maxId = _context.Subjects.Max(s => (int?)s.Id) ?? 0;
        subject.Id = maxId + 1;
        _context.Subjects.Add(subject);
        _context.SaveToFiles();
        return Task.FromResult(subject);
    }

    public Task<Subject> UpdateAsync(Subject subject)
    {
        var existing = _context.Subjects.FirstOrDefault(s => s.Id == subject.Id);
        if (existing != null)
        {
            existing.Title = subject.Title;
            existing.EctsCredits = subject.EctsCredits;
            existing.Area = subject.Area;
            _context.SaveToFiles();
        }
        return Task.FromResult(existing ?? subject);
    }

    public Task DeleteAsync(int id)
    {
        var subject = _context.Subjects.FirstOrDefault(s => s.Id == id);
        if (subject != null)
        {
            _context.Subjects.Remove(subject);
            _context.SaveToFiles();
        }
        return Task.CompletedTask;
    }
}
