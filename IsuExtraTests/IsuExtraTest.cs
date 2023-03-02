using System.Runtime.CompilerServices;
using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Models;
using Isu.Extra.Service;
using Isu.Models;
using Isu.Services;
using Xunit;

namespace Isu.Extra.Test;

public class IsuExtraTest
{
    private IsuExtraService isu = new ();

    [Fact]
    public void AddingLessonCheck()
    {
        ExtraGroup extraGroup = isu.AddGroup(new GroupName("M32121"));
        Lesson lesson = isu.AddLessonToGroup(extraGroup, new Lesson("Math", LessonNumber.Fifth, "Mayatin", DayOfWeek.Friday));
        Assert.Contains(lesson, extraGroup.ScheduleForWeek.ScheduleForTheWeek[DayOfWeek.Friday].LessonsList.Values);
    }

    [Fact]
    public void AddingStudentToOgnpGroupCheck()
    {
        ExtraGroup extraGroup = isu.AddGroup(new GroupName("M32121"));
        Student student = isu.AddStudent(extraGroup, "Данек");
        Ognp ognp = isu.AddNewOgnp(new Ognp(MF.KTU));
        Streams stream = isu.AddNewStream(ognp, new Streams("pok", 1));
        OgnpGroup ognpGroup = isu.AddNewOgnpGroup(stream, new OgnpGroup("Asdm!23"));
        isu.EnrollToOgnp(student, stream, extraGroup, ognpGroup, ognp);
        Assert.Contains(student, ognpGroup.OgnpStudents);
    }

    [Fact]
    public void CancelEnrollCheck()
    {
        Ognp ognp = isu.AddNewOgnp(new Ognp(MF.KTU));
        ExtraGroup extraGroup = isu.AddGroup(new GroupName("V32123"));
        Student student = isu.AddStudent(extraGroup, "Name");
        Streams stream = isu.AddNewStream(ognp, new Streams("name", 4));
        OgnpGroup ognpGroup = isu.AddNewOgnpGroup(stream, new OgnpGroup("name"));
        isu.EnrollToOgnp(student, stream, extraGroup, ognpGroup, ognp);
        isu.CancelEnroll(ognp, extraGroup, ognpGroup, stream, student);
        Assert.DoesNotContain(student, ognpGroup.OgnpStudents);
    }

    [Fact]
    public void GetStreamsCheck()
    {
        Ognp ognp = isu.AddNewOgnp(new Ognp(MF.KTU));
        Streams stream1 = isu.AddNewStream(ognp, new Streams("name", 4));
        Streams stream2 = isu.AddNewStream(ognp, new Streams("name", 5));
        List<Streams> streamsCount = isu.GetOgnpStreams(ognp);
        Assert.True(streamsCount.Count == 2);
    }

    [Fact]
    public void GetStudentsInOgnpGroupCheck()
    {
        ExtraGroup extraGroup = isu.AddGroup(new GroupName("M32121"));
        Student student1 = isu.AddStudent(extraGroup, "Данек1");
        Student student2 = isu.AddStudent(extraGroup, "Данек2");
        Student student3 = isu.AddStudent(extraGroup, "Данек3");
        Student student4 = isu.AddStudent(extraGroup, "Данек4");
        Ognp ognp = isu.AddNewOgnp(new Ognp(MF.KTU));
        Streams stream = isu.AddNewStream(ognp, new Streams("pok", 1));
        OgnpGroup ognpGroup = isu.AddNewOgnpGroup(stream, new OgnpGroup("Asdm!23"));
        isu.EnrollToOgnp(student1, stream, extraGroup, ognpGroup, ognp);
        isu.EnrollToOgnp(student2, stream, extraGroup, ognpGroup, ognp);
        isu.EnrollToOgnp(student3, stream, extraGroup, ognpGroup, ognp);
        isu.EnrollToOgnp(student4, stream, extraGroup, ognpGroup, ognp);
        isu.GetStudentsList(ognpGroup);
        Assert.True(ognpGroup.OgnpStudents.Count == 4);
    }

    [Fact]
    public void GetUnEnrolledStudentsCheck()
    {
        ExtraGroup extraGroup = isu.AddGroup(new GroupName("M32121"));
        Student student1 = isu.AddStudent(extraGroup, "Данек1");
        Student student2 = isu.AddStudent(extraGroup, "Данек2");
        Student student3 = isu.AddStudent(extraGroup, "Данек3");
        Student student4 = isu.AddStudent(extraGroup, "Данек3");
        Ognp ognp = isu.AddNewOgnp(new Ognp(MF.KTU));
        Streams stream = isu.AddNewStream(ognp, new Streams("pok", 1));
        OgnpGroup ognpGroup = isu.AddNewOgnpGroup(stream, new OgnpGroup("Asdm!23"));
        isu.EnrollToOgnp(student1, stream, extraGroup, ognpGroup, ognp);
        isu.EnrollToOgnp(student2, stream, extraGroup, ognpGroup, ognp);
        isu.GetUnEnrolledStudents(extraGroup);
        Assert.True(extraGroup.UnEnrolledStudents.Count == 2);
    }
}