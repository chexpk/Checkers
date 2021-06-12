using System;
using UnityEngine;

[Serializable]
public class CheckersSerializer
{
    public enum Color
    {
        black,
        white
    }

    public int x;
    public int y;
    public Color color;
}
