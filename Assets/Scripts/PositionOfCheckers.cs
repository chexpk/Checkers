using UnityEngine;

[CreateAssetMenu(fileName = "PositionOfCheckers", menuName = "PositionOfCheckers", order = 0)]
public class PositionOfCheckers : ScriptableObject
{
    public CheckersSerializer[] checkers;
}
