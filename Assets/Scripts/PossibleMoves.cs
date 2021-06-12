using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PossibleMoves
{
    private Board board;
    Dictionary<Checker, BoardPosition> enemiesPosition = new Dictionary<Checker, BoardPosition>();

    public PossibleMoves(Board board)
    {
        this.board = board;
    }

    public List<BoardPosition> Call(BoardPosition boardPosition, string playerSideColor)
    {
        var result = AllMoves(boardPosition);
        var enemy = GetNearEnemy(result, playerSideColor);
        var possibleChop = PossibleChop(boardPosition, enemy);
        result = MovesWithoutCheckers(result);
        result = MovesForvad(boardPosition, result);
        result.AddRange(possibleChop);
        return result;
    }

    public Dictionary<Checker, BoardPosition> GetEnemiesWithPositionOfChop()
    {
        return enemiesPosition;
    }

    List<BoardPosition> AllMoves(BoardPosition boardPosition)
    {
        List<BoardPosition> result = new List<BoardPosition>();
        if (boardPosition.x + 1 < 8 && boardPosition.y + 1 < 8)
        {
            result.Add(new BoardPosition(boardPosition.x + 1, boardPosition.y + 1));
        }

        if (boardPosition.x - 1 > -1 && boardPosition.y - 1 > -1)
        {
            result.Add(new BoardPosition(boardPosition.x - 1, boardPosition.y - 1));
        }

        if (boardPosition.x + 1 < 8 && boardPosition.y - 1 > -1)
        {
            result.Add(new BoardPosition(boardPosition.x + 1, boardPosition.y - 1));
        }

        if (boardPosition.x - 1 > -1 && boardPosition.y + 1 < 8)
        {
            result.Add(new BoardPosition(boardPosition.x - 1, boardPosition.y + 1));
        }

        return result;
    }

    List<BoardPosition> GetNearEnemy(List<BoardPosition> moves, string playerSideColor)
    {
        List<BoardPosition> result = new List<BoardPosition>();
        foreach (BoardPosition enemy in moves)
        {
            if (HasCheckerAt(enemy))
            {
                if (!IsCheckerIsSameColorOfPlayer(enemy, playerSideColor))
                {
                    result.Add(enemy);
                }
            }
        }
        return result;
    }

    List<BoardPosition> PossibleChop(BoardPosition boardPosition, List<BoardPosition> enemies)
    {
        List<BoardPosition> result = new List<BoardPosition>();

        foreach (BoardPosition enemy in enemies)
        {
            var moves = AllMoves(enemy);
            var movesWithoutCheckersNearEnemy = MovesWithoutCheckers(moves);

            foreach (BoardPosition move in movesWithoutCheckersNearEnemy)
            {
                if (IsOnLineToChop(boardPosition, enemy, move))
                {
                    SaveEnemyWithPositionOfChop(enemy, move);
                    result.Add(move);
                }
            }

        }
        return result;
    }

    Dictionary<Checker, BoardPosition> SaveEnemyWithPositionOfChop(BoardPosition enemyPosition, BoardPosition chopPosition)
    {
        enemiesPosition.Add(board.GetCheckerAt(enemyPosition),chopPosition);
        return enemiesPosition;
    }

    List<BoardPosition> MovesWithoutCheckers(List<BoardPosition> moves)
    {
        List<BoardPosition> result = new List<BoardPosition>();

        foreach (BoardPosition move in moves)
        {
            if (!HasCheckerAt(move))
            {
                result.Add(move);
            }
        }
        return result;
    }

    List<BoardPosition> MovesForvad(BoardPosition boardPosition, List<BoardPosition> moves)
    {
        List<BoardPosition> result = new List<BoardPosition>();

        foreach (BoardPosition move in moves)
        {
            if (CheckIsForwardMove(boardPosition, move))
            {
                result.Add(move);
            }
        }
        return result;
    }

    bool HasCheckerAt(BoardPosition boardPosition)
    {
        return board.GetCheckerAt(boardPosition) != null;
    }

    bool IsCheckerIsSameColorOfPlayer(BoardPosition boardPosition, string playerSideColor)
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

    bool IsOnLineToChop(BoardPosition boardPosition, BoardPosition enemyBoardPosition, BoardPosition moveBoardPosition)
    {

        if (enemyBoardPosition.x < boardPosition.x && moveBoardPosition.x < enemyBoardPosition.x && boardPosition.y != moveBoardPosition.y)
        {
            return true;
        } else if (boardPosition.x < enemyBoardPosition.x && enemyBoardPosition.x < moveBoardPosition.x && boardPosition.y != moveBoardPosition.y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    string GetCheckerColor(BoardPosition boardPosition)
    {
        return board.GetCheckerAt(boardPosition).GetColor();
    }

    bool CheckIsForwardMove(BoardPosition boardPosition, BoardPosition toBoardPosition)
    {
        bool result = false;
        if (GetCheckerColor(boardPosition) == "white")
        {
            if (toBoardPosition.y > boardPosition.y)
            {
                result = true;
            }
            else
            {
                result = false;
            }
        }
        if (GetCheckerColor(boardPosition) == "black")
        {
            if (toBoardPosition.y < boardPosition.y)
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
}
