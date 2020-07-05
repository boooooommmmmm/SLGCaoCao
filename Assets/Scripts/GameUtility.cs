using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework.Core;

public static class GameUtility
{
    static public int _converAttrToInt(string attr)
    {
        switch (attr)
        {
            case "S":
                return 1;
            case "A":
                return 2;
            case "B":
                return 3;
            case "C":
                return 4;
            default:
                return 0;
        }
    }

    static public string _converIntToAttr(int attr)
    {
        switch (attr)
        {
            case 1:
                return "S";
            case 2:
                return "A";
            case 3:
                return "B";
            case 4:
                return "C";
            default:
                return "";
        }
    }
}
