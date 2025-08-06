using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class Extensions
{
    public static int CustomRound(this float value, float minFracToCeil)
    {
        if(value % 1 < minFracToCeil % 1)
            return (int)value;
        return (int)value + 1;
    }
}
