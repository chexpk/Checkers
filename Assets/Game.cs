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
    private Checker selectedChecker;
    Dictionary<Checker, BoardPosition> enemiesPosition;

    private void Start()
    {
        board.ResetToPositionCheckers(positionOfCheckers);
    }

    public void OnSquareClick(BoardPosition boardPosition)
    {
        board.UnHighlightAllCheckers();
        board.UnHighlightAllSquares();

        if (GetCheckerAt(boardPosition) != null && IsCheckerIsSameColorOfPlayer(GetCheckerAt(boardPosition)))
        {
            selectedChecker = GetCheckerAt(boardPosition);
            SelectChecker(boardPosition);
        }
        else
        {
            if (isCheckerSelected)
            {
                TryMove(boardPosition);
            }
            isCheckerSelected = false;
            selectedChecker = null;
        }
    }

    void TryMove(BoardPosition toBoardPosition)
    {
        var possibleMoves = PossibleMoves(checkerBoardPosition);
        if (CheckMove(toBoardPosition, possibleMoves))
        {
            board.MoveChecker(selectedChecker, toBoardPosition);
            TryChopEnemy(toBoardPosition);
            ChangePlayer();
            playerMoveEvent.Invoke(playerSideColor);
        }
    }

    void TryChopEnemy(BoardPosition chopBoardPosition)
    {
        // chop enemy
        foreach (var enemy in enemiesPosition)
        {
            if (enemy.Value == chopBoardPosition)
            {
                board.DeleteChecler(enemy.Key);
            }
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
        board.HighlightChecker(selectedChecker);
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
        enemiesPosition = possibleMoves.GetEnemiesWithPositionOfChop(); // вытвщить
        return possibleMoves.Call(boardPosition, playerSideColor);
    }

    bool IsCheckerIsSameColorOfPlayer(Checker checker)
    {
        bool result;
        if (GetCheckerColor(checker) == playerSideColor)
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

    string GetCheckerColor(Checker checker)
    {
        return checker.GetColor();
    }

    Checker GetCheckerAt (BoardPosition boardPosition)
    {
        return board.GetCheckerAt(boardPosition);
    }
}
