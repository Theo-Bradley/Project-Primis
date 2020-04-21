using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCursor : MonoBehaviour
{
    public float distance = 3;
    public Transform center;
    private void Update()
    {
        float x = InputManager.IM.RX;
        float y = InputManager.IM.RY;
        Vector2 dir = new Vector2(x, y);

        if(x > InputManager.IM.contdeadzone || x < -InputManager.IM.contdeadzone || y > InputManager.IM.contdeadzone || y < -InputManager.IM.contdeadzone)
        {
            transform.Translate(dir * InputManager.IM.contsens);
        }

        //clamps the distance so it doenst effect rotation speed
        float dst = Vector3.Distance(center.position, transform.position);
        if (dst >distance)
        {
            Vector3 vect = center.position - transform.position;
            vect = vect.normalized;
            vect *= (dst - distance);
            transform.position += vect;
        }
    }
}
