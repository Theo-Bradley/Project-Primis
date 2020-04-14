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
    private void Awake()
    {
        StartCoroutine(flicker());
    }

    IEnumerator flicker()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            //flarelight.intensity = Random.Range(0.8f, 1.5f);
            flarelight.pointLightOuterRadius = Random.Range(4.85f, 5);
        }
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
