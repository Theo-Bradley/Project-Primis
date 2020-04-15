using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class Platform : MonoBehaviour
{

    private Light2D spritelight;
    private BoxCollider2D coll;
    public float litIntensity;
    public float unlitIntensity;

    private void Start()
    {
        spritelight = GetComponent<Light2D>();
        coll = GetComponent<BoxCollider2D>();
        spritelight.intensity = unlitIntensity;
            
    }

    //activates the collider and makes it more visible
    public void Lit()
    {
        coll.isTrigger = false;
        spritelight.intensity = litIntensity;
    }

    
}
