using System;
using UnityEngine;


[CreateAssetMenu(fileName = "PositionOfCheckers", menuName = "PositionOfCheckers", order = 0)]
// [System.Serializable]
public class PositionOfCheckers : ScriptableObject
{
    // [System.Serializable]
    // public struct Col
    // {
    //     [SerializeField] public int[] row;
    // }
    //
    // public Col[] rows;

    
    public int[] row1 = new int[8];
    public int[] row2 = new int[8];
    public int[] row3 = new int[8];
    public int[] row4 = new int[8];
    public int[] row5 = new int[8];
    public int[] row6 = new int[8];
    public int[] row7 = new int[8];
    public int[] row8 = new int[8];
}
