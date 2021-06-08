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
    public GameObject checkerPref;
    // public UnityEvent checkerMoveEvent = new UnityEvent();
    public Checker checkerScript;

    public UIContent logOfMoves;
    // public UnityEvent checkerMoveEventLog = new UnityEvent();

    public UnityEvent checkerMoveEvent;

    public Checker GetCheckerAt(BoardPosition position)
    {
        var cheackerGO = checkers[position.x, position.y];
        if (cheackerGO == null) return null;

        return cheackerGO.GetComponent<Checker>();
    }

    public void MoveChecker(Checker checker, BoardPosition toBoardPosition)
    {
        if (checker == null) return;
        if (checkers[toBoardPosition.x, toBoardPosition.y] != null) return;

        // Физ координаты
        var square = squares[toBoardPosition.x, toBoardPosition.y].GetComponent<SquareScript>();
        var position = square.GetPosition();
        checker.MoveWithAnimatedTo(position.x, position.z);

        // Коорд на доске
        //TODO - выделить в метод? (часть используется в удалении)
        var fromBoardPosition = checker.GetBoardPosition();
        checkers[toBoardPosition.x, toBoardPosition.y] = checkers[fromBoardPosition.x, fromBoardPosition.y];
        checkers[fromBoardPosition.x, fromBoardPosition.y] = null;
        checker.SetBoardPosition(toBoardPosition);

        checkerMoveEvent.Invoke(fromBoardPosition, toBoardPosition);
    }

    public void CreateWhiteChecker(BoardPosition boardPosition)
    {
        var col = boardPosition.x;
        var row = boardPosition.y;
        var position = GetSquarePosition(col, row);
        checkers[col, row] = CreateChecker(position, "white");
        var checker = checkers[col, row].GetComponent<Checker>();
        checker.SetBoardPosition(boardPosition);
    }

     public void CreateBlackChecker(BoardPosition boardPosition)
    {
        var col = boardPosition.x;
        var row = boardPosition.y;
        var position = GetSquarePosition(col, row);
        checkers[col, row] = CreateChecker(position, "black");
        var checker = checkers[col, row].GetComponent<Checker>();
        checker.SetBoardPosition(boardPosition);
    }

    Vector3 GetSquarePosition(int x, int y)
    {
        SquareScript squareScript = squares[x, y].transform.gameObject.GetComponent<SquareScript>();
        return squareScript.GetPosition();
    }

    GameObject CreateChecker(Vector3 position, string color)
    {
        var result = Instantiate(checkerPref, position, Quaternion.identity);
        result.GetComponent<Checker>().SetColor(color);
        return result;
    }

    public void HighlightChecker(Checker checker)
    {
        checker.Highlight();
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

    public void DeleteChecler(Checker checker)
    {
        var deletedPosition = checker.GetBoardPosition();
        checkers[deletedPosition.x, deletedPosition.y] = null;
        checker.Delete();
    }
}
