using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbitRotation : MonoBehaviour
{
    public Camera cam;
    public Transform targetObject;
    public Game game;
    public float orbitSpeed = 100f;
    private bool isRotate = false;
    private float angle = 0;


    // Start is called before the first frame update
    void Start()
    {
        game.playerMoveEvent.AddListener(ChangePlayer);
    }

    // Update is called once per frame
    void Update()
    {
        RotateAround180();
    }

    void lalal()
    {
        while (angle < 180)
        {
            angle +=  orbitSpeed * Time.deltaTime;
            cam.transform.RotateAround(targetObject.transform.position, Vector3.up, orbitSpeed * Time.deltaTime);
            Debug.Log(angle);
        }
    }

    void Rotate()
    {
        if (isRotate)
        {
            angle =  Mathf.Clamp((angle + orbitSpeed * Time.deltaTime), 0, 180);
            cam.transform.RotateAround(targetObject.transform.position, Vector3.up, Mathf.Clamp((orbitSpeed * Time.deltaTime), 0, 180));
            // angle = Mathf.Clamp((orbitSpeed * Time.deltaTime), 0, 180);
        }
        OffRotate();
    }

    void OffRotate()
    {
        if (angle >= 180 || angle <= -180)
        {
            isRotate = false;
            angle = 0;
        }
    }

    void RotateAround180()
    {
        Rotate();
        OffRotate();
    }

    void ChangePlayer(string color)
    {
        isRotate = true;
        // if (orbitSpeed > 0)
        // {
        //     orbitSpeed *= -1;
        // }
        // orbitSpeed *= -1;
        Debug.Log("кружим");
    }
}
