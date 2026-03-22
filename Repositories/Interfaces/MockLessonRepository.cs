using LessonManager.Models;
using LessonManager.Repositories.Interfaces;
using LessonManager.Services;
using System.Collections.Generic;
using System.Linq;

namespace LessonManager.Repositories;

public class MockLessonRepository : ILessonRepository
{
    public IEnumerable<Lesson> GetBySubjectId(int subjectId)
        => MockStorage.Lessons.Where(l => l.SubjectId == subjectId);
}