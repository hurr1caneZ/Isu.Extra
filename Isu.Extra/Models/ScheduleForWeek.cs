using Isu.Extra.Entities;

namespace Isu.Extra.Models;

public class ScheduleForWeek
{
    private Dictionary<DayOfWeek, ScheduleForOneDay> _scheduleForWeek;
    public ScheduleForWeek()
    {
        _scheduleForWeek = new Dictionary<DayOfWeek, ScheduleForOneDay>();
    }

    public IReadOnlyDictionary<DayOfWeek, ScheduleForOneDay> ScheduleForTheWeek => _scheduleForWeek;

    public void AddLesson(Lesson lesson, DayOfWeek dayOfWeek, LessonNumber lessonNumber)
    {
        if (!_scheduleForWeek.ContainsKey(dayOfWeek))
            _scheduleForWeek.Add(dayOfWeek, new ScheduleForOneDay());
        _scheduleForWeek[dayOfWeek].AddLesson(lesson, lessonNumber);
    }
}