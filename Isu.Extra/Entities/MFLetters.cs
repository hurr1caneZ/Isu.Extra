using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class MFLetters
{
    public static char GetMF(MF mf)
    {
        switch (mf)
        {
            case MF.TINT:
                return 'M';
            case MF.KTU:
                return 'K';
            case MF.NOZH:
                return 'N';
            case MF.FTMI:
                return 'U';
            case MF.FTMF:
                return 'F';
            default:
                throw new ArgumentException("error");
        }
    }
}