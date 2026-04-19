using System;
using System.Collections.Generic;
using System.Text;

using LessonManager.Models.Enums;

namespace LessonManager.Models;

public class Subject
{
    public int Id { get; set; }
    public string Title { get; set; }
    public double EctsCredits { get; set; }
    public KnowledgeArea Area { get; set; }

    [System.Text.Json.Serialization.JsonConstructor]
    public Subject()
    {
    }

    public Subject(int id, string title, double ectsCredits, KnowledgeArea area)
    {
        Id = id;
        Title = title;
        EctsCredits = ectsCredits;
        Area = area;
    }
}