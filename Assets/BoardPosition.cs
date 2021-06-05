using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPosition
{
    public int x;
    public int y;

    public BoardPosition (int x, int y)
    {
       this.x = x;
       this.y = y;
    }

    public static bool operator == (BoardPosition c1, BoardPosition c2)
    {
        return c1.x == c2.x && c1.y == c2.y;
    }

    public static bool operator != (BoardPosition c1, BoardPosition c2)
    {
        return c1.x != c2.x || c1.y != c2.y;
    }

    public string ToChessPosition()
    {
        var yLine = (y + 1).ToString();
        var xLine = ConvertXLine(x);
        return xLine + yLine;
    }

    public override string ToString()
    {
        return $"{base.ToString()} - x: {x}, y: {y}";
    }

    string ConvertXLine (int x)
    {
        string result;

        switch (x)
        {
            case 0:
                result = "a";
                break;
            case 1:
                result = "b";
                break;
            case 2:
                result = "c";
                break;
            case 3:
                result = "d";
                break;
            case 4:
                result = "e";
                break;
            case 5:
                result = "f";
                break;
            case 6:
                result = "g";
                break;
            case 7:
                result = "h";
                break;
            default:
                result = "null";
                break;
        }
        return result;
    }
}
