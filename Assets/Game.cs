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
    public bool isGameRun = false;
    public string playerSideColor  = "white";
    public Board board;
    public bool isCheckerSelected = false;

    private int checkerX;
    private int checkerY;

    public UnityEventGame playerMoveEvent;

    // BoardPosition boardPosition;
    public void OnSquareClick(int x, int y)
    {
        //тест
        TestCreatBoardPosition();

        board.UnHighlightAllCheckers();
        board.UnHighlightAllSquares();

        if (HasCheckerAt(x, y) && IsCheckerIsSameColorOfPlayer(x, y))
        {
            SelectChecker(x, y);
        }
        else
        {
            if (isCheckerSelected)
            {
                TryMove(x, y);
            }
            isCheckerSelected = false;
        }
    }

    void TryMove(int toX, int toY)
    {
        var possibleMoves = PossibleMoves(checkerX, checkerY);
        if (CheckMove(toX, toY, possibleMoves))
        {
            board.MoveChecker(checkerX, checkerY, toX, toY);
            ChangePlayer();
            playerMoveEvent.Invoke(playerSideColor);
        }
    }

    bool CheckMove(int toX, int toY, List<Vector2Int> possibleMoves)
    {
        Vector2Int to = new Vector2Int(toX, toY);
        foreach (Vector2Int possiblePosition in possibleMoves)
        {
            if (to == possiblePosition)
            {
                return true;
            }
        }

        return false;
    }

    void SelectChecker(int x, int y)
    {
        var possibleMoves = PossibleMoves(x, y);
        HighlightSquares(possibleMoves);
        board.HighlightChecker(x, y);
        isCheckerSelected = true;
        checkerX = x;
        checkerY = y;
    }

    void HighlightSquares(List<Vector2Int> squares)
    {
        foreach (Vector2Int square in squares)
        {
            board.HighlightSquare(square.x, square.y);
        }
    }

    List<Vector2Int> PossibleMoves(int x, int y)
    {
        var result = AllMoves(x, y);
        var enemy = GetNearEnemy(result);
        // var nearEnemy = AllMovesNearEnemy(enemy);
        var possibleChop = PossibleChop(x, y, enemy);
        result = MovesWithoutCheckers(result);
        result = MovesForvad(x, y, result);
        result.AddRange(possibleChop);
        return result;
    }

    List<Vector2Int> AllMoves(int x, int y)
    {
        List<Vector2Int> result = new List<Vector2Int>();
        if (x + 1 < 8 && y + 1 < 8)
        {
            result.Add(new Vector2Int(x + 1, y + 1));
        }

        if (x - 1 > -1 && y - 1 > -1)
        {
            result.Add(new Vector2Int(x - 1, y - 1));
        }

        if (x + 1 < 8 && y - 1 > -1)
        {
            result.Add(new Vector2Int(x + 1, y - 1));
        }

        if (x - 1 > -1 && y + 1 < 8)
        {
            result.Add(new Vector2Int(x - 1, y + 1));
        }

        return result;
    }

    List<Vector2Int> MovesWithoutCheckers(List<Vector2Int> moves)
    {
        List<Vector2Int> result = new List<Vector2Int>();

        foreach (Vector2Int move in moves)
        {
            if (!HasCheckerAt(move.x, move.y))
            {
                result.Add(move);
            }
        }
        return result;
    }

    List<Vector2Int> MovesForvad(int x, int y, List<Vector2Int> moves)
    {
        List<Vector2Int> result = new List<Vector2Int>();

        foreach (Vector2Int move in moves)
        {
            if (CheckIsForwardMove(x, y, move.y))
            {
                result.Add(move);
            }
        }
        return result;
    }

    bool CheckIsForwardMove(int x, int y, int toY)
    {
        bool result = false;
        if (GetCheckerColor(x, y) == "white")
        {
            if (toY > y)
            {
                result = true;
            }
            else
            {
                result = false;
            }
        }
        if (GetCheckerColor(x, y) == "black")
        {
            if (toY < y)
            {
                result = true;
            }
            else
            {
                result = false;
            }
        }

        return result;
    }

    bool HasCheckerAt(int x, int y)
    {
        return board.HasCheckerAt(x, y);
    }

    bool IsCheckerIsSameColorOfPlayer(int x, int y)
    {
        bool result;
        if (GetCheckerColor(x, y) == playerSideColor)
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

    string GetCheckerColor(int x, int y)
    {
        return board.GetCheckerColor(x, y);
    }

    List<Vector2Int> GetNearEnemy(List<Vector2Int> moves)
    {
        List<Vector2Int> result = new List<Vector2Int>();
        foreach (Vector2Int enemy in moves)
        {
            if (HasCheckerAt(enemy.x, enemy.y))
            {
                if (!IsCheckerIsSameColorOfPlayer(enemy.x, enemy.y))
                {
                    result.Add(enemy);
                }
            }
        }
        return result;
    }

    List<Vector2Int> AllMovesNearEnemy(List<Vector2Int> enemies)
    {
        List<Vector2Int> result = new List<Vector2Int>();
        foreach (Vector2Int move in enemies)
        {
            result.AddRange(AllMoves(move.x, move.y));
        }
        return result;
    }

    List<Vector2Int> PossibleChop(int x, int y, List<Vector2Int> enemies)
    {
        List<Vector2Int> result = new List<Vector2Int>();

        foreach (Vector2Int enemy in enemies)
        {
            var moves = AllMoves(enemy.x, enemy.y);
            var movesWithoutCheckersNearEnemy = MovesWithoutCheckers(moves);

            foreach (Vector2Int move in movesWithoutCheckersNearEnemy)
            {
                if (IsOnLineToChop(x, y, enemy.x, enemy.y, move.x, move.y))
                {
                    result.Add(move);
                }
            }

        }
        return result;
    }

    bool IsOnLineToChop(int x, int y, int enemyX, int enemyY, int moveX, int moveY)
    {
        if (enemyX < x && moveX < enemyX && y != moveY)
        {
            return true;
        } else if (x < enemyX && enemyX < moveX && y != moveY)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void TestCreatBoardPosition()
    {
        BoardPosition test = new BoardPosition(1, 2);
    }
}
