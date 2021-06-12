using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareScript : MonoBehaviour
{
    public Material highlightMaterial;
    public Material nullMaterial;
    int x = 1;
    int y = 1;

    public void SetBoardPosition(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public BoardPosition GetBoardPosition ()
    {
        return new BoardPosition(x, y);
    }

    public Vector3 GetPosition()
    {
        return new Vector3(transform.position.x, transform.position.y + 0.15f, transform.position.z);
    }

    public void Highlight()
    {
        Debug.Log("HISHS");
        GetComponent<Renderer>().material = highlightMaterial;
    }

    public void UnHighlight()
    {
        GetComponent<Renderer>().material = nullMaterial;
    }
}
