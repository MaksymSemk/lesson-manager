using LessonManager.Models;
using LessonManager.Repositories.Interfaces;
using LessonManager.Views;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LessonManager.Services;

public interface IDataService
{
    Task<List<SubjectView>> GetAllSubjectsAsync();
    Task<SubjectView> GetSubjectAsync(int id);
    Task<SubjectView> AddSubjectAsync(Subject subject);
    Task<SubjectView> UpdateSubjectAsync(Subject subject);
    Task DeleteSubjectAsync(int id);

    Task<List<LessonView>> GetLessonsForSubjectAsync(int subjectId);
    Task<LessonView> GetLessonAsync(int id);
    Task<LessonView> AddLessonAsync(Lesson lesson);
    Task<LessonView> UpdateLessonAsync(Lesson lesson);
    Task DeleteLessonAsync(int id);
}

public class DataService : IDataService
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly ILessonRepository _lessonRepository;

    public DataService(ISubjectRepository subjectRepository, ILessonRepository lessonRepository)
    {
        _subjectRepository = subjectRepository;
        _lessonRepository = lessonRepository;
    }

    public async Task<List<SubjectView>> GetAllSubjectsAsync()
    {
        var subjects = await _subjectRepository.GetAllAsync();
        return subjects.Select(s => new SubjectView(s)).ToList();
    }

    public async Task<SubjectView> GetSubjectAsync(int id)
    {
        var subject = await _subjectRepository.GetByIdAsync(id);
        return subject != null ? new SubjectView(subject) : null;
    }

    public async Task<SubjectView> AddSubjectAsync(Subject subject)
    {
        var added = await _subjectRepository.AddAsync(subject);
        return new SubjectView(added);
    }

    public async Task<SubjectView> UpdateSubjectAsync(Subject subject)
    {
        var updated = await _subjectRepository.UpdateAsync(subject);
        return new SubjectView(updated);
    }

    public async Task DeleteSubjectAsync(int id)
    {
        await _lessonRepository.DeleteBySubjectIdAsync(id);
        await _subjectRepository.DeleteAsync(id);
    }

    public async Task<List<LessonView>> GetLessonsForSubjectAsync(int subjectId)
    {
        var lessons = await _lessonRepository.GetBySubjectIdAsync(subjectId);
        return lessons.Select(l => new LessonView(l)).ToList();
    }

    public async Task<LessonView> GetLessonAsync(int id)
    {
        var lesson = await _lessonRepository.GetByIdAsync(id);
        return lesson != null ? new LessonView(lesson) : null;
    }

    public async Task<LessonView> AddLessonAsync(Lesson lesson)
    {
        var added = await _lessonRepository.AddAsync(lesson);
        return new LessonView(added);
    }

    public async Task<LessonView> UpdateLessonAsync(Lesson lesson)
    {
        var updated = await _lessonRepository.UpdateAsync(lesson);
        return new LessonView(updated);
    }

    public Task DeleteLessonAsync(int id)
    {
        return _lessonRepository.DeleteAsync(id);
    }
}