using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCursor : MonoBehaviour
{
    private void Update()
    {
        float x = Input.GetAxis("xboxXR");
        float y = Input.GetAxis("xboxYR");
        Vector2 dir = new Vector2(x, y);

        if(x>InputManager.IM.contdeadzone || x < -InputManager.IM.contdeadzone || y> InputManager.IM.contdeadzone || y< -InputManager.IM.contdeadzone)
        {
            transform.Translate(dir * InputManager.IM.contsens);
        }
        
    }
}
