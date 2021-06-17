using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class SquaresCreator : MonoBehaviour
{
    public GameObject squarePrefab;
    public Board board;

    const float boardSize = 8;

    void Start()
    {
        AreaCreator();
    }

    void AreaCreator()
    {
        float widthArea = transform.lossyScale.x;
        float heightArea = transform.lossyScale.y;
        float widthSquare = widthArea / boardSize / widthArea;
        float heightSquare = heightArea / boardSize / heightArea;
        float z = 0f;

        for (int j = 0; j < boardSize; j++)
        {
            float y = -heightSquare * boardSize / 2 + heightSquare / 2 + heightSquare * j;
            for (int i = 0; i < boardSize; i++)
            {
                float x = -widthSquare * boardSize / 2 + widthSquare / 2 + widthSquare * i;
                Vector3 positionSc = new Vector3(x, y, z);
                board.squares[i, j] = CreateSquare(positionSc);
                SquareScript squareScript = board.squares[i, j].transform.gameObject.GetComponent<SquareScript>();
                squareScript.SetBoardPosition(i, j);
            }
        }
    }

    GameObject CreateSquare(Vector3 position)
    {
        GameObject squereGO;
        squereGO = Instantiate(squarePrefab, this.transform);
        var result = squereGO;

        // изменяем размер дочернего объекта в соответствии с размером родителя
        float width = transform.lossyScale.x;
        float height = transform.lossyScale.y;
        squereGO.transform.localScale = new Vector3(width / boardSize / width, height / boardSize / height, 1f);
        squereGO.transform.localPosition = position;
        return result;
    }
    //
    // void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireCube(transform.position, transform.lossyScale);
    //
    //     var size = SquareSize();
    //     var offsetX = -(transform.lossyScale.x - size) / 2;
    //     var offsetZ = -(transform.lossyScale.z - size) / 2;
    //     for (int i = 0; i < boardSize; i++)
    //     {
    //         for (int j = 0; j < boardSize; j++)
    //         {
    //             Vector3 positionSc = new Vector3(offsetX + i * size, 0, offsetZ + j * size);
    //             DrawSquareGizmo(positionSc);
    //         }
    //     }
    // }
    //
    // private void DrawSquareGizmo(Vector3 position)
    // {
    //     Gizmos.color = Color.green;
    //     var size = new Vector3(SquareSize(), SquareSize(), SquareSize());
    //     Gizmos.DrawWireCube(position, size);
    // }
    //
    // private float SquareSize()
    // {
    //     return transform.lossyScale.x / boardSize;
    // }
}
