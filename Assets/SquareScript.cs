using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareScript : MonoBehaviour
{
    public Material highlightMaterial;
    public Material nullMaterial;
    int x = 1;
    int y = 1;

    string xLine;
    string yLine;

    string boardPosition;

    public void SetBoardPosition(int x, int y)
    {
        this.x = x;
        this.y = y;

        // ConverToChessBoardPosition();
    }

    public BoardPosition GetBoardPosition ()
    {
        // Debug.Log(boardPosition);
        return new BoardPosition(x, y);
    }

    public Vector3 GetPosition()
    {
        Vector3 result = new Vector3(this.transform.position.x, this.transform.position.y + 0.15f, this.transform.position.z);

        return result;
    }

    public void Highlight()
    {
        GetComponent<Renderer>().material = highlightMaterial;
    }

    public void UnHighlight()
    {
        GetComponent<Renderer>().material = nullMaterial;
    }

    void ConverToChessBoardPosition()
    {
        y = y + 1;
        yLine = y.ToString();
        xLine = ConverXLine(x);

        boardPosition = xLine + yLine;
    }

    string ConverXLine (int x)
    {
        string result;

        switch (x)
        {
            case 0:
                result = "a";
                break;
            case 1:
                result = "b";
                break;
            case 2:
                result = "c";
                break;
            case 3:
                result = "d";
                break;
            case 4:
                result = "e";
                break;
            case 5:
                result = "f";
                break;
            case 6:
                result = "g";
                break;
            case 7:
                result = "h";
                break;
            default:
                result = "null";
                break;
        }
        return result;
    }
}
