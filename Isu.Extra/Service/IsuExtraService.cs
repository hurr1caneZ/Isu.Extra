using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;
using Isu.Models;
using Isu.Services;
using Group = Isu.Entities.Group;

namespace Isu.Extra.Service;

public class IsuExtraService : IsuService
{
    private int _id;
    private List<Ognp> _ognpList;
    private List<ExtraGroup> _groups;
    private List<Student> _students;
    public IsuExtraService()
    {
        _ognpList = new List<Ognp>();
        _groups = new List<ExtraGroup>();
        _students = new List<Student>();
        _id = 100_000;
    }

    public Ognp AddNewOgnp(Ognp ognp)
    {
        if (ognp is null)
            throw new ArgumentException("Incorrect ognpCourse");
        _ognpList.Add(ognp);
        return ognp;
    }

    public OgnpGroup AddNewOgnpGroup(Streams stream, OgnpGroup ognpGroup)
    {
        if (ognpGroup is null)
            throw new ArgumentException("incorrect ognpGroup");
        stream.AddGroupToStream(ognpGroup);
        return ognpGroup;
    }

    public Streams AddNewStream(Ognp ognp, Streams stream)
    {
        if (ognp is null)
            throw new ArgumentException("incorrect ognp");
        ognp.AddStream(stream);
        return stream;
    }

    public new ExtraGroup AddGroup(GroupName groupName)
    {
        var newGroup = new ExtraGroup(groupName);
        if (_groups.Contains(newGroup))
            throw new ArgumentException("newGroup didn't contains");
        _groups.Add(newGroup);
        return newGroup;
    }

    public Student AddStudent(ExtraGroup extraGroup, string studentName)
    {
        var newStudent = new Student(studentName, ++_id, extraGroup.GroupName);
        if (extraGroup.Students.Contains(newStudent))
            throw new ArgumentException("newStudent didn't contains");
        extraGroup.AddStudentToGroup(newStudent);
        return newStudent;
    }

    public Ognp EnrollToOgnp(Student student, Streams stream, ExtraGroup extraGroup, OgnpGroup ognpGroup, Ognp ognp)
    {
        if (student is null || ognpGroup is null)
            throw new ArgumentException("Incorrect   student");
        if (student.GroupName.GroupNamy.StartsWith(ognp.Name))
            throw new ArgumentException("Student couldn't enroll to his MF");
        if (CheckSchedule(extraGroup, ognpGroup))
            throw new IsuExtraException();
        ognp.AddStudentToOgnpGroup(stream, ognpGroup, student);
        extraGroup.Enroll(student);
        return ognp;
    }

    public Ognp CancelEnroll(Ognp ognp, ExtraGroup extraGroup, OgnpGroup ognpGroup, Streams stream, Student student)
    {
        if (ognp is null || extraGroup is null || ognpGroup is null || stream is null || student is null)
            throw new ArgumentException("Incorrect cancel enroll arguments");
        if (!CheckSchedule(extraGroup, ognpGroup))
        {
            ognp.RemoveStudentFromOgnpGroup(stream, ognpGroup, student);
        }

        extraGroup.UnEnroll(student);
        return ognp;
    }

    public bool CheckSchedule(ExtraGroup extraGroup, OgnpGroup ognpGroup)
    {
        foreach (DayOfWeek dayOfWeek in Enum.GetValues(typeof(DayOfWeek)))
        {
            bool isOgnpContainsDayKey = ognpGroup.ScheduleForWeek.ScheduleForTheWeek
                .ContainsKey(dayOfWeek);
            bool isExtraContainsDayKey = extraGroup.ScheduleForWeek.ScheduleForTheWeek
                .ContainsKey(dayOfWeek);
            if (isOgnpContainsDayKey && isExtraContainsDayKey)
            {
                foreach (LessonNumber lessonNumber in Enum.GetValues(typeof(LessonNumber)))
                {
                    bool isOgnpContainsLessonKey = ognpGroup.ScheduleForWeek.ScheduleForTheWeek[dayOfWeek].LessonsList
                        .ContainsKey(lessonNumber);
                    bool isExtraContainsLessonKey = extraGroup.ScheduleForWeek.ScheduleForTheWeek[dayOfWeek].LessonsList
                        .ContainsKey(lessonNumber);
                    if (isOgnpContainsLessonKey && isExtraContainsLessonKey)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    public List<Streams> GetOgnpStreams(Ognp ognp)
    {
        return (List<Streams>)ognp.OgnpStreams;
    }

    public List<Student> GetStudentsList(OgnpGroup ognpGroup)
    {
        return (List<Student>)ognpGroup.OgnpStudents;
    }

    public List<Student> GetUnEnrolledStudents(ExtraGroup extraGroup)
    {
        return (List<Student>)extraGroup.UnEnrolledStudents;
    }

    public Lesson AddLessonToGroup(ExtraGroup extraGroup, Lesson lesson)
    {
        if (extraGroup is null || lesson is null)
            throw new ArgumentException("extraGroup or lesson is null");
        extraGroup.AddLesson(lesson, lesson.DayOfWeek, lesson.LessonNumber);
        return lesson;
    }
}