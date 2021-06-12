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
        CreateCheckers(positionOfCheckers.checkers);
    }

    void CreateCheckers(CheckersSerializer[] allCheckersOnBoard)
    {
        foreach (var checker in allCheckersOnBoard)
        {
            var color = checker.color.ToString();
            if (color == "white")
            {
                var boardPosition = new BoardPosition(checker.x, checker.y);
                CreateWhiteChecker(boardPosition);
            }
            if (color == "black")
            {
                var boardPosition = new BoardPosition(checker.x, checker.y);
                CreateBlackChecker(boardPosition);
            }
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
