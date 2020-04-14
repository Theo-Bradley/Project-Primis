using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class flare : MonoBehaviour
{
    private Light2D flarelight;
    private void Start()
    {
        flarelight = GetComponentInParent<Light2D>();
    }

    public void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {       
        if(collision.gameObject.CompareTag("platform"))
        {
            platform plat = collision.GetComponent<platform>();

            if (plat != null)
            {
                plat.Lit();
            }
        }
    }

}
