using Isu.Entities;
using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class Ognp
{
    private List<Streams> _ognpStreamsList;
    public Ognp(MF name)
    {
        _ognpStreamsList = new List<Streams>();
        Name = MFLetters.GetMF(name);
    }

    public char Name { get; internal set; }
    public IReadOnlyCollection<Streams> OgnpStreams { get => _ognpStreamsList; }

    public void AddStream(Streams stream)
    {
        if (stream is null)
            throw new ArgumentException("Incorrect Stream");
        _ognpStreamsList.Add(stream);
    }

    public void AddGroupToStream(OgnpGroup ognpGroup, Streams stream)
    {
        if (ognpGroup is null || stream is null)
            throw new ArgumentException("Incorrect ognpGroup or Stream");
        stream.AddGroupToStream(ognpGroup);
    }

    public void AddStudentToOgnpGroup(Streams stream, OgnpGroup ognpGroup, Student student)
    {
        if (ognpGroup is null)
            throw new ArgumentException("Incorrect ExtraGroup");
        stream.AddStudentToGroup(student, ognpGroup);
    }

    public void RemoveStudentFromOgnpGroup(Streams stream, OgnpGroup ognpGroup, Student student)
    {
        if (stream is null || ognpGroup is null || student is null)
            throw new ArgumentException("Incorrect arguments");
        stream.RemoveStudentFromOgnpGroup(ognpGroup, student);
    }
}