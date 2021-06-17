using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public string playerSideColor  = "white";
    public Board board;
    public UnityEventGame playerMoveEvent;
    public bool isCheckerSelected = false;

    private BoardPosition checkerBoardPosition;
    private Checker selectedChecker;
    private Dictionary<Checker, BoardPosition> enemiesPosition;

    private void Start()
    {
        board.ResetToPositionCheckers(positionOfCheckers);
        // GetFibonacci();
        // GetRange();
        // GetFilter();
    }

    void GetRange()
    {
        var range = Enumerable.Range(1, 10);
        Display(range);

        // MAP item -> item*
        // var doubleRange = range.Select((item) =>
        // {
        //     return item * 2;
        // });

        var lol = 13;
        range.Select((item, index) =>
        {
            return item * lol;
        });


        var doubleRange = range.Select(item => item * 2);

        // (item, index) =>
        // {
        //     return item * index;
        // }

        // (item, index) => item * index;
        // item => item * 2;
        Display(doubleRange);
    }

    void GetFilter()
    {
        var quad = Enumerable.Range(1, 100).Where(x => x * x > 1000);
        var array = Enumerable.Range(1, 100);
        array.Any(x => x > 0);

        Display(quad);
    }

    void Display(IEnumerable collection)
    {
        foreach (var item in collection)
        {
            Debug.Log(item);
        }
    }

    void GetFibonacci()
    {
        int i = 0;
        Fibonacci fibonacci = new Fibonacci();
        foreach (var iteme in fibonacci)
        {
            i++;
            Debug.Log(iteme);
            if (i > 9) return;
        }
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
        enemiesPosition = possibleMoves.GetEnemiesWithPositionOfChop(); // вытащить
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
