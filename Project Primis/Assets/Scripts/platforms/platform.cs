using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class platform : MonoBehaviour
{

    private Light2D spritelight;
    private BoxCollider2D coll;

    private void Start()
    {
        spritelight = GetComponent<Light2D>();
        coll = GetComponent<BoxCollider2D>();
    }
    public void Lit()
    {
        coll.isTrigger = false;
        spritelight.intensity = 1;
    }

    
}
