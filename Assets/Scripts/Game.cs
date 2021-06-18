using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;


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

    private void Start()
    {
        // board.ResetToPositionCheckers(positionOfCheckers);
        // GetFibonacci();
        // GetRange();
        // GetFilter();
    }

    public void PutCheckersOnPosition()
    {
        board.ResetToPositionCheckers(positionOfCheckers);
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
            checkerBoardPosition = null;
        }
    }

    void TryMove(BoardPosition toBoardPosition)
    {
        var possibleMovesOrChops = GetPossibleChopsOrMoves(checkerBoardPosition);
        if (CheckMove(toBoardPosition, possibleMovesOrChops))
        {
            TryChopEnemy(toBoardPosition);
            board.MoveChecker(selectedChecker, toBoardPosition);
            ChangePlayer();
            playerMoveEvent.Invoke(playerSideColor);
        }
    }

    void TryChopEnemy(BoardPosition chopBoardPosition)
    {
        var possibleChops = PossibleChops(checkerBoardPosition);
        if (CheckMove(chopBoardPosition, possibleChops))
        {
            var checkerPosition = GetCheckerBoardPosition(selectedChecker);
            var enemyChecker = GetEnemyChecker(checkerBoardPosition, chopBoardPosition);
            board.DeleteChecler(enemyChecker);
        }
    }

    Checker GetEnemyChecker(BoardPosition checkerBoardPosition, BoardPosition chopBoardPosition)
    {
        int enemyX;
        int enemyY;
        if (chopBoardPosition.x > checkerBoardPosition.x)
        {
            enemyX = chopBoardPosition.x - 1;
        }
        else
        {
            enemyX = checkerBoardPosition.x - 1;
        }
        if (chopBoardPosition.y > checkerBoardPosition.y)
        {
            enemyY = chopBoardPosition.y - 1;
        }
        else
        {
            enemyY = checkerBoardPosition.y - 1;
        }
        var enemyBoardPosition = new BoardPosition(enemyX, enemyY);
        return GetCheckerAt(enemyBoardPosition);
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
        var possibleMovesOrChops = GetPossibleChopsOrMoves(boardPosition);
        HighlightSquares(possibleMovesOrChops);

        board.HighlightChecker(selectedChecker);
        isCheckerSelected = true;
        checkerBoardPosition = boardPosition;
    }

    List<BoardPosition> GetPossibleChopsOrMoves(BoardPosition boardPosition)
    {
        var possibleChops = PossibleChops(boardPosition);
        // Debug.Log(possibleChops.Count);
        if (possibleChops.Count > 0)
        {
            // Debug.Log("есть рубить");
            return possibleChops;
        }
        else
        {
            // Debug.Log("есть ходить");
            var possibleMoves = PossibleMoves(boardPosition);
            return possibleMoves;
        }
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
        // enemiesPosition = possibleMoves.GetEnemiesWithPositionOfChop(); // вытащить
        return possibleMoves.Call(boardPosition, playerSideColor);
    }

    List<BoardPosition> PossibleChops(BoardPosition boardPosition)
    {
        PossibleChops possibleChops = new PossibleChops(board);
        return possibleChops.Call(boardPosition, playerSideColor);
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

    BoardPosition GetCheckerBoardPosition(Checker checker)
    {
        return board.GetCheckerBoardPosition(checker);
    }
}
