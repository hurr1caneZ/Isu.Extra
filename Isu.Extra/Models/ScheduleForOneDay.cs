using Isu.Extra.Entities;

namespace Isu.Extra.Models;

public class ScheduleForOneDay
{
    private Dictionary<LessonNumber, Lesson> _lessonsList;
    public ScheduleForOneDay()
    {
        _lessonsList = new Dictionary<LessonNumber, Lesson>();
    }

    public IReadOnlyDictionary<LessonNumber, Lesson> LessonsList { get => _lessonsList; }

    public void AddLesson(Lesson lesson, LessonNumber lessonNumber)
    {
        if (lesson is null)
            throw new ArgumentException("Incorrect lesson arguments");
        _lessonsList.Add(lessonNumber, lesson);
    }
}