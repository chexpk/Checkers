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
    public GameObject checker;
    // public UnityEvent checkerMoveEvent = new UnityEvent();
    public Checker checkerScript;

    public UIContent logOfMoves;
    // public UnityEvent checkerMoveEventLog = new UnityEvent();

    public UnityEvent checkerMoveEvent;

    // WIP
    public Checker GetCheckerAt(BoardPosition position)
    {
        var cheackerGO = checkers[position.x, position.y];
        if (cheackerGO == null) return null;

        return cheackerGO.GetComponent<Checker>();
    }

    // WIP
    public void MoveChecker(Checker checker, BoardPosition toBoardPosition)
    {
        if (checker == null) return;
        if (checkers[toBoardPosition.x, toBoardPosition.y] != null) return;

        // Физ координаты
        var square = squares[toBoardPosition.x, toBoardPosition.y].GetComponent<SquareScript>();
        var position = square.GetPosition();
        checker.MoveWithAnimatedTo(position.x, position.z);

        // Коорд на доске
        var fromBoardPosition = checker.GetBoardPosition();
        checkers[toBoardPosition.x, toBoardPosition.y] = checkers[fromBoardPosition.x, fromBoardPosition.y];
        checkers[fromBoardPosition.x, fromBoardPosition.y] = null;
        checker.SetBoardPosition(toBoardPosition.x, toBoardPosition.y); // why x,y

        checkerMoveEvent.Invoke(fromBoardPosition, toBoardPosition);
    }

    // deprecated
    public bool HasCheckerAt(int x, int y)
    {
        return checkers[x, y] != null;
    }

    // deprecated
    public string GetCheckerColor(BoardPosition boardPosition)
    {
        return checkers[boardPosition.x, boardPosition.y].GetComponent<Checker>().GetColor();
    }

    public void CreateWhiteChecker(BoardPosition boardPosition)
    {
        var col = boardPosition.x;
        var row = boardPosition.y;
        var position = GetSquarePosition(col, row);
        checkers[col, row] = CreateChecker(position, "white");
        var checker = checkers[col, row].GetComponent<Checker>();
        checker.SetBoardPosition(col, row);
    }

     public void CreateBlackChecker(BoardPosition boardPosition)
    {
        var col = boardPosition.x;
        var row = boardPosition.y;
        var position = GetSquarePosition(col, row);
        checkers[col, row] = CreateChecker(position, "black");
        var checker = checkers[col, row].GetComponent<Checker>();
        checker.SetBoardPosition(col, row);
    }

    Vector3 GetSquarePosition(int x, int y)
    {
        SquareScript squareScript = squares[x, y].transform.gameObject.GetComponent<SquareScript>();
        return squareScript.GetPosition();
    }

    GameObject CreateChecker(Vector3 position, string color)
    {
        var result = Instantiate(checker, position, Quaternion.identity);
        result.GetComponent<Checker>().SetColor(color);
        return result;
    }

    // deprecated
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

    public void HighlightChecker(BoardPosition boardPosition)
    {
        checkers[boardPosition.x, boardPosition.y].GetComponent<Checker>().Highlight();
    }

    public void HighlightSquare(BoardPosition boardPosition)
    {
        squares[boardPosition.x, boardPosition.y].GetComponent<SquareScript>().Highlight();
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

    public void ResetToPositionCheckers(PositionOfCheckers positionOfCheckers)
    {
        var checkersCreator = new CheckersCreator(positionOfCheckers, this);
        checkersCreator.Call();
    }
}
