using LessonManager.Models;
using LessonManager.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LessonManager.Repositories;

public class LessonRepository : ILessonRepository
{
    private readonly FileStorageContext _context;

    public LessonRepository()
    {
        _context = new FileStorageContext();
    }

    public Task<IEnumerable<Lesson>> GetBySubjectIdAsync(int subjectId)
    {
        return Task.FromResult(_context.Lessons.Where(l => l.SubjectId == subjectId).AsEnumerable());
    }

    public Task<Lesson> GetByIdAsync(int id)
    {
        var lesson = _context.Lessons.FirstOrDefault(l => l.Id == id);
        return Task.FromResult(lesson);
    }

    public Task<Lesson> AddAsync(Lesson lesson)
    {
        var maxId = _context.Lessons.Max(l => (int?)l.Id) ?? 0;
        lesson.Id = maxId + 1;
        _context.Lessons.Add(lesson);
        _context.SaveToFiles();
        return Task.FromResult(lesson);
    }

    public Task<Lesson> UpdateAsync(Lesson lesson)
    {
        var existing = _context.Lessons.FirstOrDefault(l => l.Id == lesson.Id);
        if (existing != null)
        {
            existing.SubjectId = lesson.SubjectId;
            existing.Date = lesson.Date;
            existing.StartTime = lesson.StartTime;
            existing.EndTime = lesson.EndTime;
            existing.Topic = lesson.Topic;
            existing.Type = lesson.Type;
            _context.SaveToFiles();
        }
        return Task.FromResult(existing ?? lesson);
    }

    public Task DeleteAsync(int id)
    {
        var lesson = _context.Lessons.FirstOrDefault(l => l.Id == id);
        if (lesson != null)
        {
            _context.Lessons.Remove(lesson);
            _context.SaveToFiles();
        }
        return Task.CompletedTask;
    }

    public Task DeleteBySubjectIdAsync(int subjectId)
    {
        var lessonsToDelete = _context.Lessons.Where(l => l.SubjectId == subjectId).ToList();
        foreach (var lesson in lessonsToDelete)
        {
            _context.Lessons.Remove(lesson);
        }
        if (lessonsToDelete.Any())
            _context.SaveToFiles();
        return Task.CompletedTask;
    }
}
