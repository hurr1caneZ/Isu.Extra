using Isu.Entities;
using Isu.Extra.Models;
using Isu.Models;
using Group = Isu.Entities.Group;

namespace Isu.Extra.Entities;

public class ExtraGroup : Group
{
    private List<Student> _unEnrolledStudents;
    private List<Student> _enrolledStudentsToOgnp;
    public ExtraGroup(GroupName groupName)
        : base(groupName)
    {
        GroupName = groupName;
        ScheduleForWeek = new ScheduleForWeek();
        _unEnrolledStudents = new List<Student>();
        _enrolledStudentsToOgnp = new List<Student>();
    }

    public IReadOnlyList<Student> EnrolledStudentsToOgnp => _enrolledStudentsToOgnp;
    public IReadOnlyList<Student> UnEnrolledStudents => _unEnrolledStudents;
    public ScheduleForWeek ScheduleForWeek { get; internal set; }

    public void Enroll(Student student)
    {
        if (student is null)
            throw new ArgumentException("Incorrect student");
        Student foundedStudent = _unEnrolledStudents.Find(x => x.Id == student.Id) ??
                                 throw new ArgumentException("Student is not found");
        _unEnrolledStudents.Remove(foundedStudent);
        _enrolledStudentsToOgnp.Add(student);
    }

    public void UnEnroll(Student student)
    {
        if (student is null)
            throw new ArgumentException("Incorrect student");
        var foundedStudent = _enrolledStudentsToOgnp.Find(x => x.Id == student.Id) ??
                             throw new ArgumentException("Student is not found");
        _enrolledStudentsToOgnp.Remove(foundedStudent);
        _unEnrolledStudents.Add(student);
    }

    public void AddLesson(Lesson lesson, DayOfWeek dayOfWeek, LessonNumber lessonNumber)
    {
        if (lesson is null)
            throw new ArgumentException("Incorrect lesson");
        ScheduleForWeek.AddLesson(lesson, dayOfWeek, lessonNumber);
    }

    public new void AddStudentToGroup(Student student)
    {
        ((List<Student>)Students).Add(student);
        _unEnrolledStudents.Add(student);
    }
}