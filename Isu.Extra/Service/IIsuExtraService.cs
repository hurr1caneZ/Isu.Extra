using Isu.Entities;
using Isu.Extra.Entities;

namespace Isu.Extra.Service;

public interface IIsuExtraService
{
    public Ognp AddNewOgnp(Ognp ognp);
    public Ognp EnrollToOgnp(Student student, Streams stream, ExtraGroup extraGroup, OgnpGroup ognpGroup, Ognp ognp);
    public List<Streams> GetOgnpStreams(Ognp ognp);
    public List<Student> GetStudentsList(OgnpGroup ognpGroup);
    public List<Student> GetUnEnrolledStudents(ExtraGroup extraGroup);
}