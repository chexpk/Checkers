using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Camera camera;
    public Game game;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetBoardPosition();
        }
    }

    void GetBoardPosition()
    {
        RaycastHit hit;

        if (RaycastMousePosition(out hit))
        {
            SquareScript squareScript = hit.transform.gameObject.GetComponent<SquareScript>();
            var boardPosition = squareScript.GetBoardPosition();
            // int x = (int) boardPosition.x;
            // int y = (int) boardPosition.y;
            game.OnSquareClick(boardPosition);
        }
    }

    bool RaycastMousePosition(out RaycastHit hit)
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hit);
    }
}
