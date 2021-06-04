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

    // private int checkerX;
    // private int checkerY;
    private BoardPosition checkerBoardPosition;

    public UnityEventGame playerMoveEvent;

    // BoardPosition boardPosition;
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
            if (to.x == possiblePosition.x && to.y == possiblePosition.y)
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
        var result = AllMoves(boardPosition);
        var enemy = GetNearEnemy(result);
        var possibleChop = PossibleChop(boardPosition, enemy);
        result = MovesWithoutCheckers(result);
        result = MovesForvad(boardPosition, result);
        result.AddRange(possibleChop);
        return result;
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

    List<BoardPosition> GetNearEnemy(List<BoardPosition> moves)
    {
        List<BoardPosition> result = new List<BoardPosition>();
        foreach (BoardPosition enemy in moves)
        {
            if (HasCheckerAt(enemy))
            {
                if (!IsCheckerIsSameColorOfPlayer(enemy))
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
                    result.Add(move);
                }
            }

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
}
