using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCursor : MonoBehaviour
{
    private void Update()
    {
        float x = InputManager.IM.RX;
        float y = InputManager.IM.RY;
        Vector2 dir = new Vector2(x, y);

        if(x>InputManager.IM.contdeadzone || x < -InputManager.IM.contdeadzone || y> InputManager.IM.contdeadzone || y< -InputManager.IM.contdeadzone)
        {
            transform.Translate(dir * InputManager.IM.contsens);
        }
        
    }
}
