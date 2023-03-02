using Isu.Extra.Entities;

namespace Isu.Extra.Models;

public class Lesson
{
    public Lesson(string name, LessonNumber lessonNumber, string teacherName, DayOfWeek dayOfWeek)
    {
        if (name is null || teacherName is null)
            throw new ArgumentException("Incorrect lesson arguments");
        Name = name;
        LessonNumber = lessonNumber;
        TeacherName = teacherName;
        DayOfWeek = dayOfWeek;
    }

    public string Name { get; internal set; }
    public LessonNumber LessonNumber { get; internal set; }
    public DayOfWeek DayOfWeek { get; internal set; }
    public string TeacherName { get; internal set; }
}
