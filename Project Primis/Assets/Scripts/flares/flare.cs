using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Flare : MonoBehaviour
{
    private Light2D flarelight;
   
    private void Start()
    {
        flarelight = GetComponentInParent<Light2D>();
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {   
        //if collided with trigger that is a platform then turn on platform
        if(collision.gameObject.CompareTag("platform"))
        {
            Platform plat = collision.GetComponent<Platform>();

            if (plat != null)
            {
                plat.Lit();
            }
        }
    }
}
