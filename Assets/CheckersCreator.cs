using UnityEngine;

public class CheckersCreator
{
    private PositionOfCheckers positionOfCheckers;
    private Board board;

    public CheckersCreator(PositionOfCheckers positionOfCheckers, Board board)
    {
        this.positionOfCheckers = positionOfCheckers;
        this.board = board;
    }

    public void Call()
    {
        CreateRow(positionOfCheckers.row1, 0);
        CreateRow(positionOfCheckers.row2, 1);
        CreateRow(positionOfCheckers.row3, 2);
        CreateRow(positionOfCheckers.row4, 3);
        CreateRow(positionOfCheckers.row5, 4);
        CreateRow(positionOfCheckers.row6, 5);
        CreateRow(positionOfCheckers.row7, 6);
        CreateRow(positionOfCheckers.row8, 7);
    }

    void CreateRow(int[] row, int y)
    {
        for (int i = 0; i < row.Length; i++)
        {
            TryCreateChecker(i, y, row);
        }
    }

    void TryCreateChecker(int x, int y, int[] row)
    {
        int checkerCode = row[x];
        BoardPosition boardPosition = new BoardPosition(x, y);
        if (checkerCode == 0) return;
        if (checkerCode == 1)
        {
            CreateWhiteChecker(boardPosition);
        }
        if (checkerCode == 2)
        {
            CreateBlackChecker(boardPosition);
        }
    }

    void CreateWhiteChecker(BoardPosition boardPosition)
    {
        board.CreateWhiteChecker(boardPosition);
    }

    void CreateBlackChecker(BoardPosition boardPosition)
    {
        board.CreateBlackChecker(boardPosition);
    }
}
