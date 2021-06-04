using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbitRotation : MonoBehaviour
{
    public Camera cam;
    public Transform targetObject;
    public float orbitSpeed = -100f;
    private bool isRotate = false;
    private float angle = 0;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("ChangePlayer", 2f);
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

        }
        OffRotate();
    }

    void OffRotate()
    {
        if (angle >= 180)
        {
            isRotate = false;
        }
    }

    void RotateAround180()
    {
        Rotate();
        OffRotate();
    }

    void ChangePlayer()
    {
        isRotate = true;
    }
}
