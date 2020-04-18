using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    public float maxDistance;
    private Vector2 mousePos;
    public Transform vc;
    void Update()
    {
        if (InputManager.IM.usingXB)
        {
            vc.gameObject.SetActive(true);
            transform.position = vc.position;
        }
        else
        {
            vc.gameObject.SetActive(false);
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono);
        }
        transform.localPosition = Vector2.ClampMagnitude(transform.localPosition, maxDistance);
    } //(Vector3.Scale(new Vector3(1, 1, 0), 
}
