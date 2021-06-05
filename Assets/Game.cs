using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.WSA;

[System.Serializable]
public class UnityEventGame : UnityEvent<string>
{
}

public class Game : MonoBehaviour
{
    public PositionOfCheckers positionOfCheckers;

    public bool isGameRun = false;
    public string playerSideColor  = "white";
    public Board board;
    public UnityEventGame playerMoveEvent;

    public bool isCheckerSelected = false;
    private BoardPosition checkerBoardPosition;

    private void Start()
    {
        board.ResetToPositionCheckers(positionOfCheckers);
    }

    public void OnSquareClick(BoardPosition boardPosition)
    {
        board.UnHighlightAllCheckers();
        board.UnHighlightAllSquares();

        if (HasCheckerAt(boardPosition) && IsCheckerIsSameColorOfPlayer(boardPosition))
        {
            SelectChecker(boardPosition);
        }
        else
        {
            if (isCheckerSelected)
            {
                TryMove(boardPosition);
            }
            isCheckerSelected = false;
        }
    }

    void TryMove(BoardPosition toBoardPosition)
    {
        var possibleMoves = PossibleMoves(checkerBoardPosition);
        if (CheckMove(toBoardPosition, possibleMoves))
        {
            board.MoveChecker(checkerBoardPosition, toBoardPosition);
            ChangePlayer();
            playerMoveEvent.Invoke(playerSideColor);
        }
    }

    bool CheckMove(BoardPosition toBoardPosition, List<BoardPosition> possibleMoves)
    {
        BoardPosition to = new BoardPosition(toBoardPosition.x, toBoardPosition.y);
        foreach (BoardPosition possiblePosition in possibleMoves)
        {
            if (to == possiblePosition)
            {
                return true;
            }
        }

        return false;
    }

    void SelectChecker(BoardPosition boardPosition)
    {
        var possibleMoves = PossibleMoves(boardPosition);
        HighlightSquares(possibleMoves);
        board.HighlightChecker(boardPosition);
        isCheckerSelected = true;
        checkerBoardPosition = boardPosition;
    }

    void HighlightSquares(List<BoardPosition> squares)
    {
        foreach (BoardPosition square in squares)
        {
            board.HighlightSquare(square);
        }
    }

    List<BoardPosition> PossibleMoves(BoardPosition boardPosition)
    {
        PossibleMoves possibleMoves = new PossibleMoves(board);
        return possibleMoves.Call(boardPosition, playerSideColor);
    }

    bool HasCheckerAt(BoardPosition boardPosition)
    {
        return board.HasCheckerAt(boardPosition.x, boardPosition.y);
    }

    bool IsCheckerIsSameColorOfPlayer(BoardPosition boardPosition)
    {
        bool result;
        if (GetCheckerColor(boardPosition) == playerSideColor)
        {
            result = true;
        }
        else
        {
            result = false;
        }
        return result;
    }

    void ChangePlayer()
    {
        if (playerSideColor == "white")
        {
            playerSideColor = "black";
            return;
        }
        if (playerSideColor == "black")
        {
            playerSideColor = "white";
        }
    }

    string GetCheckerColor(BoardPosition boardPosition)
    {
        return board.GetCheckerColor(boardPosition);
    }
}
