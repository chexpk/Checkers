﻿using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class SquaresCreator : MonoBehaviour
{
    public GameObject squarePrefab;
    public GameObject checker;

    public Board board;

    const float boardSize = 8;

    void Start()
    {
        AreaCreator();
        // CreateCheckers();
    }

    GameObject CreateSquere(Vector3 position)
    {
        GameObject squereGO;
        squereGO = Instantiate(squarePrefab, this.transform);
        var result = squereGO;

        //изменяем размер дочернего объекта в соответствии с размером родителя
        float width = transform.lossyScale.x;
        float height = transform.lossyScale.y;
        squereGO.transform.localScale = new Vector3(width / boardSize / width, height / boardSize / height, 1f);

        squereGO.transform.localPosition = position;

        return result;
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
                board.squares[i, j] = CreateSquere(positionSc);

                SquareScript squareScript = board.squares[i, j].transform.gameObject.GetComponent<SquareScript>();
                squareScript.SetBoardPosition(i, j);
            }
        }
    }

    void CreateCheckers ()
    {
        CreateWhiteCheckers();
        CreateBlackCheckers();
    }

    void CreateWhiteCheckers()
    {
        for (int row = 0; row < 3; row++)
        {
            bool isFirstExist = row % 2 == 0;
            CreateCheckerRow(isFirstExist, row, "white");
        }
    }

    void CreateBlackCheckers()
    {
        for (int row = 5; row < 8; row++)
        {
            bool isFirstExist = row % 2 == 0;
            CreateCheckerRow(isFirstExist, row, "black");
        }
    }

    void CreateCheckerRow(bool isFirstExist, int row, string color)
    {
        bool shouldPlace = isFirstExist;

        for (int col = 0; col < 8; col++)
        {
            if (shouldPlace)
            {
                var position = GetSquarePosition(col, row);
                board.checkers[col, row] = CreateChecker(position, color);
                var checker = board.checkers[col, row].GetComponent<Checker>();
                var boardPosition = new BoardPosition(col, row);
                checker.SetBoardPosition(boardPosition);
                shouldPlace = false;
            }
            else
            {
                shouldPlace = true;
            }
        }
    }

    Vector3 GetSquarePosition(int x, int y)
    {
        SquareScript squareScript = board.squares[x, y].transform.gameObject.GetComponent<SquareScript>();
        return squareScript.GetPosition();
    }

    GameObject CreateChecker(Vector3 position, string color)
    {
        var result = Instantiate(checker, position, Quaternion.identity);
        result.GetComponent<Checker>().SetColor(color);
        return result;
    }
}
