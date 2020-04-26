using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class camerafollow : MonoBehaviour
{
	//veery simple for now
	public Transform target;
    private float velocity;
    public float defaultSize;
    public float maxSize;
    public float zoomSpeed;
    
	public Vector3 offset;

    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y, offset.z);
        var size = Camera.main.orthographicSize;
        /*
        size = (target.GetComponent<Rigidbody2D>().velocity.x) / target.GetComponent<Movement>().speed * maxSize;
        if (size > defaultSize)
            Camera.main.orthographicSize = size;
        else
            Camera.main.orthographicSize = size;
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, defaultSize, maxSize);
        */
        var vel = Mathf.Abs(target.GetComponent<Rigidbody2D>().velocity.x) / target.GetComponent<Movement>().speed - 0.5; //get normalized x velocity
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize + ((float)vel * zoomSpeed), defaultSize, maxSize);
    }
}


