using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

using LessonManager.Models.Enums;

namespace LessonManager.Models;

public class Lesson
{
    public int Id { get; set; }
    public int SubjectId { get; set; }
    public DateTime Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public string Topic { get; set; }
    public LessonType Type { get; set; }

    [JsonConstructor]
    public Lesson()
    {
    }

    public Lesson(int id, int subjectId, DateTime date, TimeOnly startTime, TimeOnly endTime, string topic, LessonType type)
    {
        Id = id;
        SubjectId = subjectId;
        Date = date;
        StartTime = startTime;
        EndTime = endTime;
        Topic = topic;
        Type = type;
    }
}