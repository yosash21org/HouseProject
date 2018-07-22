using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHead : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(1))
        {
            MouseLockCamera();
        }
    }

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    public float speedH = 2.0f;
    public float speedV = 2.0f;
    public float max;
    public float min;

    private void MouseLockCamera()
    {
        var my = Input.GetAxis("Mouse Y");

        var transform = GetComponent<Transform>();
        transform.Rotate(new Vector3(my * 3, 0, 0));

        //transform.Rotate(my, 0, 0, Space.Self);
        //transform.eulerAngles = new Vector3(Mathf.Clamp(transform.eulerAngles.x, -90, 90),  transform.eulerAngles.y, transform.eulerAngles.z);


        //transform.eulerAngles = new Vector3( Mathf.Clamp(transform.eulerAngles.y, min, max), 0,0);

        //if (Input.GetMouseButtonDown(1))
        //{
        //    yaw = Camera.main.transform.eulerAngles.y;
        //    pitch = Camera.main.transform.eulerAngles.x;
        //}

        //yaw += speedH * Input.GetAxis("Mouse X");
        //pitch -= speedV * Input.GetAxis("Mouse Y");

        //Camera.main.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }

}
