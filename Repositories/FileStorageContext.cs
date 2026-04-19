using LessonManager.Models;
using LessonManager.Models.Enums;
using System.IO;
using System.Text.Json;

namespace LessonManager.Repositories;

public class FileStorageContext
{
    private static readonly string DataDirectory = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "LessonManager");

    private static readonly string SubjectsFilePath = Path.Combine(DataDirectory, "subjects.json");
    private static readonly string LessonsFilePath = Path.Combine(DataDirectory, "lessons.json");

    public List<Subject> Subjects { get; set; } = new();
    public List<Lesson> Lessons { get; set; } = new();

    public FileStorageContext()
    {
        InitializeStorage();
    }

    private void InitializeStorage()
    {
        if (!Directory.Exists(DataDirectory))
            Directory.CreateDirectory(DataDirectory);

        if (!File.Exists(SubjectsFilePath) || !File.Exists(LessonsFilePath))
            InitializeDefaultData();
        else
            LoadFromFiles();
    }

    private void InitializeDefaultData()
    {
        Subjects = new()
        {
            new Subject(1, "Програмування C#", 5.0, KnowledgeArea.Programming),
            new Subject(2, "Вища математика", 4.0, KnowledgeArea.Mathematics),
            new Subject(3, "Теорія алгоритмів", 3.0, KnowledgeArea.Engineering)
        };

        Lessons = new();
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

        SaveToFiles();
    }

    private void LoadFromFiles()
    {
        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        if (File.Exists(SubjectsFilePath))
        {
            var json = File.ReadAllText(SubjectsFilePath);
            Subjects = JsonSerializer.Deserialize<List<Subject>>(json, options) ?? new();
        }

        if (File.Exists(LessonsFilePath))
        {
            var json = File.ReadAllText(LessonsFilePath);
            Lessons = JsonSerializer.Deserialize<List<Lesson>>(json, options) ?? new();
        }
    }

    public void SaveToFiles()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        File.WriteAllText(SubjectsFilePath, JsonSerializer.Serialize(Subjects, options));
        File.WriteAllText(LessonsFilePath, JsonSerializer.Serialize(Lessons, options));
    }
}
