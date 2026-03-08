using LessonManager.Views;
using System.Collections.Generic;

namespace LessonManager.Services;

public interface IDataService
{
    List<SubjectView> GetAllSubjects();

    void LoadLessonsForSubject(SubjectView subjectView);
}