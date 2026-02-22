using System;
using System.Collections.Generic;
using LessonManager.Models;
using LessonManager.Models.Enums;

namespace LessonManager.Services;

internal static class MockStorage
{
    public static List<Subject> Subjects = new();
    public static List<Lesson> Lessons = new();

    static MockStorage()
    {
        Subjects.Add(new Subject(1, "Програмування C#", 5.0, KnowledgeArea.Programming));
        Subjects.Add(new Subject(2, "Вища математика", 4.0, KnowledgeArea.Mathematics));
        Subjects.Add(new Subject(3, "Теорія алгоритмів", 3.0, KnowledgeArea.Engineering));

        for (int i = 1; i <= 10; i++)
        {
            Lessons.Add(new Lesson(
                i,
                1,
                DateTime.Now.AddDays(i),
                new TimeOnly(10, 0),
                new TimeOnly(11, 20),
                $"Тема занять №{i}",
                LessonType.Lecture));
        }

        Lessons.Add(new Lesson(
            11,
            2,
            DateTime.Now.AddDays(1),
            new TimeOnly(12, 0),
            new TimeOnly(13, 20),
            "Матриці",
            LessonType.Seminar));

        Lessons.Add(new Lesson(
            12,
            2,
            DateTime.Now.AddDays(2),
            new TimeOnly(14, 0),
            new TimeOnly(15, 20),
            "Інтеграли",
            LessonType.Laboratory));
    }
}