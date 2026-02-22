using System;
using System.Collections.Generic;
using System.Text;

using LessonManager.Models.Enums;

namespace LessonManager.Models;

public class Subject
{
    public int Id { get; }
    public string Title { get; set; }
    public double EctsCredits { get; set; }
    public KnowledgeArea Area { get; set; }

    public Subject(int id, string title, double ectsCredits, KnowledgeArea area)
    {
        Id = id;
        Title = title;
        EctsCredits = ectsCredits;
        Area = area;
    }
}