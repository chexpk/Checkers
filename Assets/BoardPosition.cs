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
}
