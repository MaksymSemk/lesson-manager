using LessonManager.Models;
using LessonManager.Repositories.Interfaces;
using LessonManager.Services;
using System.Collections.Generic;
using System.Linq;

namespace LessonManager.Repositories;

public class MockSubjectRepository : ISubjectRepository
{
    public IEnumerable<Subject> GetAll() => MockStorage.Subjects;

    public Subject GetById(int id) => MockStorage.Subjects.FirstOrDefault(s => s.Id == id);
}