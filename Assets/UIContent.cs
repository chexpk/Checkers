using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIContent : MonoBehaviour
{
    [SerializeField] Text logsWindow;
    [SerializeField] Text moveNow;
    private string FullLog = "Move";
    private string NewMove;
    private int numberOfMoves = 0;
    public Board board;
    public Game game;

    // Start is called before the first frame update
    void Start()
    {
        logsWindow.text = FullLog;
        board.checkerMoveEvent.AddListener(RecordMove);
        game.playerMoveEvent.AddListener(UpdateWhoMoveNowBar);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RecordMove(BoardPosition fromBoardPosition, BoardPosition toBoardPosition)
    {
        numberOfMoves++;
        NewMove = fromBoardPosition.x.ToString() + ":" + fromBoardPosition.y.ToString() + " -- " + toBoardPosition.x.ToString() + ":" + toBoardPosition.y.ToString();
        FullLog =  FullLog + "\n" + numberOfMoves.ToString() +")" + " " + NewMove;
        logsWindow.text = FullLog;
    }

    void UpdateWhoMoveNowBar(string color)
    {
        moveNow.text = "Move " + color + " side";
    }
}
