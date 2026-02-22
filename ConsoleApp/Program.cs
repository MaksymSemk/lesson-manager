using System.Text;
using LessonManager.Services;
using LessonManager.Views;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

DataService service = new DataService();
bool isRunning = true;

while (isRunning)
{
    Console.Clear();
    Console.WriteLine("=== Менеджер занять: Головне меню ===");

    var subjects = service.GetAllSubjects();
    Console.WriteLine("\nСписок доступних предметів:");
    for (int i = 0; i < subjects.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {subjects[i].BaseSubject.Title} ({subjects[i].BaseSubject.Area})");
    }

    Console.Write("\nОберіть номер предмета для деталей: ");

    if (int.TryParse(Console.ReadLine(), out int choice))
    {
        if (choice == 0)
        {
            isRunning = false;
            continue;
        }

        if (choice > 0 && choice <= subjects.Count)
        {
            ShowSubjectDetails(subjects[choice - 1], service);
        }
        else
        {
            Console.WriteLine("Невірний вибір. Натисніть будь-яку клавішу...");
            Console.ReadKey();
        }
    }
}

void ShowSubjectDetails(SubjectView subjectView, DataService dataService)
{
    Console.Clear();
    Console.WriteLine($"--- Деталі предмета: {subjectView.BaseSubject.Title} ---");
    Console.WriteLine($"Кредити ECTS: {subjectView.BaseSubject.EctsCredits}");
    Console.WriteLine($"Сфера знань: {subjectView.BaseSubject.Area}");

    dataService.LoadLessonsForSubject(subjectView);

    Console.WriteLine($"\nСписок занять ({subjectView.Lessons.Count}):");
    foreach (var lessonView in subjectView.Lessons)
    {
        var lesson = lessonView.BaseLesson;
        Console.WriteLine($"- [{lesson.Date.ToShortDateString()}] {lesson.StartTime}-{lesson.EndTime} " +
                          $"| {lesson.Type}: {lesson.Topic} (Тривалість: {lessonView.Duration:hh\\:mm})");
    }

    Console.WriteLine($"\nЗагальна тривалість усіх занять: {subjectView.TotalDuration:hh\\:mm}");
    Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися до списку предметів... ");
    Console.ReadKey();
}