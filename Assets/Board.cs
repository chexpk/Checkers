using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

[System.Serializable]
public class UnityEvent : UnityEvent<BoardPosition, BoardPosition>
{
}

public class Board : MonoBehaviour
{
    public GameObject[,] checkers = new GameObject[8, 8];
    public GameObject[,] squares = new GameObject[8, 8];
    // public UnityEvent checkerMoveEvent = new UnityEvent();
    public Checker checkerScript;

    public UIContent logOfMoves;
    // public UnityEvent checkerMoveEventLog = new UnityEvent();

    public UnityEvent checkerMoveEvent;

    private void Start()
    {
        // if (checkerMoveEvent == null)
        //     checkerMoveEvent = new UnityEvent();
        // Invoke("TestMove", 1f);
        // Invoke("TestMove2", 4f);
    }

    public bool HasCheckerAt(int x, int y)
    {
        return checkers[x, y] != null;
    }

    public void HighlightChecker(BoardPosition boardPosition)
    {
        checkers[boardPosition.x, boardPosition.y].GetComponent<Checker>().Highlight();
    }

    public void UnHighlightAllCheckers()
    {
        for (int y = 0; y < checkers.GetLength(0); y++)
        {
            for (int x = 0; x < checkers.GetLength(1); x++)
            {
                if (checkers[x, y] != null)
                {
                    checkers[x, y].GetComponent<Checker>().UnHighlight();
                }
            }
        }
    }

    public void UnHighlightAllSquares()
    {
        for (int y = 0; y < squares.GetLength(0); y++)
        {
            for (int x = 0; x < squares.GetLength(1); x++)
            {
                squares[x, y].GetComponent<SquareScript>().UnHighlight();
            }
        }
    }

    public void HighlightSquare(BoardPosition boardPosition)
    {
        squares[boardPosition.x, boardPosition.y].GetComponent<SquareScript>().Highlight();
    }

    public void MoveChecker(BoardPosition fromBoardPosition, BoardPosition toBoardPosition)
    {
        var checker = checkers[fromBoardPosition.x, fromBoardPosition.y];

        if (checker == null) return;
        if (checkers[toBoardPosition.x, toBoardPosition.y] != null) return;

        var square = squares[toBoardPosition.x, toBoardPosition.y].GetComponent<SquareScript>();
        var position = square.GetPosition();
        Checker checkerScript = checker.GetComponent<Checker>();
        checkerScript.MoveWithAnimatedTo(position.x, position.z);
        checkers[fromBoardPosition.x, fromBoardPosition.y] = null;
        checkers[toBoardPosition.x, toBoardPosition.y] = checker;
        checkerMoveEvent.Invoke(fromBoardPosition, toBoardPosition);
    }


    public string GetCheckerColor(BoardPosition boardPosition)
    {
        return checkers[boardPosition.x, boardPosition.y].GetComponent<Checker>().GetColor();
    }

    void TestMove()
    {
        // MoveChecker(2, 2, 3, 3);
    }

    void TestMove2()
    {
        // MoveChecker(3, 3, 4, 4);
        // MoveChecker(4, 4, 5, 5);
        // HighlightSquare(7, 7);
    }

}
