using LessonManager.Repositories.Interfaces;
using LessonManager.Views;
using System.Collections.Generic;
using System.Linq;

namespace LessonManager.Services;

public class DataService : IDataService
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly ILessonRepository _lessonRepository;

    public DataService(ISubjectRepository subjectRepository, ILessonRepository lessonRepository)
    {
        _subjectRepository = subjectRepository;
        _lessonRepository = lessonRepository;
    }

    public List<SubjectView> GetAllSubjects()
    {
        return _subjectRepository.GetAll()
            .Select(s => new SubjectView(s))
            .ToList();
    }

    public void LoadLessonsForSubject(SubjectView subjectView)
    {
        var lessons = _lessonRepository.GetBySubjectId(subjectView.BaseSubject.Id)
            .Select(l => new LessonView(l))
            .ToList();

        subjectView.Lessons = lessons;
    }
}