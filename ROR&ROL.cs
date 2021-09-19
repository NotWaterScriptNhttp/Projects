//Srry m8's but its named after bitShifts then i realized its ROR & ROL
using System;

public enum BitShiftType { 
    Right,
    Left
}

public static string BitShift(BitShiftType type, int shiftpower, string bytes)
{
    string Shifted = "";
    switch (type)
    {
        case BitShiftType.Left:
            {
                string tmp = bytes.Substring(0, shiftpower);
                Shifted = bytes.Substring(shiftpower);
                Shifted += tmp;
            }
            break;
        case BitShiftType.Right:
            {
                string tmp = bytes.Substring(0, bytes.Length - shiftpower);
                Shifted = bytes.Substring(tmp.Length);
                Shifted += tmp;
            }
            break;
    }
    return Shifted;
}
