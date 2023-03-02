using System.Collections;
using Isu.Entities;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class OgnpGroup
{
    private List<Student> _ognpStudents;
    public OgnpGroup(string name)
    {
        if (name is null)
            throw new ArgumentException("Incorrect ognpGroupName");
        Name = name;
        _ognpStudents = new List<Student>();
        ScheduleForWeek = new ScheduleForWeek();
    }

    public ScheduleForWeek ScheduleForWeek { get; set; }
    public IReadOnlyList<Student> OgnpStudents { get => _ognpStudents; }
    public string Name { get; set; }

    public void AddStudentToOgnpGroup(Student student)
    {
        if (student is null)
            throw new ArgumentException("Incorrect student");
        _ognpStudents.Add(student);
    }

    public void RemoveStudentFromOgnpGroup(Student student)
    {
        if (student is null)
            throw new ArgumentException("Incorrect student");
        var foundedStudent = _ognpStudents.Find(x => x.Name == student.Name);
        if (foundedStudent is null)
            throw new ArgumentException("Student is not found");
        _ognpStudents.Remove(foundedStudent);
    }
}