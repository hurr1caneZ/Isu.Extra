using Isu.Entities;

namespace Isu.Extra.Entities;

public class Streams
{
    private List<OgnpGroup> _extraGroupsList;
    public Streams(string name, int num)
    {
        if (name is null)
            throw new ArgumentException("Incorrect name for stream");
        Name = name;
        Num = num;
        _extraGroupsList = new List<OgnpGroup>();
    }

    public IReadOnlyList<OgnpGroup> Stream { get => _extraGroupsList; }

    public string Name { get; set; }
    public int Num { get; internal set; }
    public void AddGroupToStream(OgnpGroup ognpGroup)
    {
        if (ognpGroup is null)
            throw new ArgumentException("Incorrect extraGroup arguments");
        _extraGroupsList.Add(ognpGroup);
    }

    public void AddStudentToGroup(Student student, OgnpGroup ognpGroup)
    {
        if (student is null || ognpGroup is null)
            throw new ArgumentException("Incorrect student or ognpGroup");
        ognpGroup.AddStudentToOgnpGroup(student);
    }

    public OgnpGroup GetOgnpGroup(OgnpGroup ognpGroup)
    {
        if (ognpGroup is null)
            throw new ArgumentException("Incorrect ognpGroup");
        return _extraGroupsList.Find(x => x.Name == ognpGroup.Name)
                           ?? throw new ArgumentException("ognpGroup is not found");
    }

    public void RemoveStudentFromOgnpGroup(OgnpGroup ognpGroup, Student student)
    {
        if (ognpGroup is null)
            throw new ArgumentException("Incorrect ognpGroup");
        OgnpGroup foundedGroup = _extraGroupsList.Find(x => x.Name == ognpGroup.Name)
                                 ?? throw new ArgumentException("Group is not found");
        foundedGroup.RemoveStudentFromOgnpGroup(student);
    }
}