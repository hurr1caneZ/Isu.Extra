namespace Isu.Extra.Exceptions;

public class IsuExtraException : Exception
{
    public IsuExtraException()
        : base("Расписание пересекается")
    { }
}