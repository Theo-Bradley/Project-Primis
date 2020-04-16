using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Flare : MonoBehaviour
{
    private Light2D flarelight;
    
    public float minIntensity; //minimum intensity that the light will lerp to
    public float maxIntensity; //vice versa
    public float lerpTime; //time in seconds for the interpolation to complete

    private float timeStartedLerp;
    private bool flipFlop = false; //increase/decrease intensity flipFlop
    private int frameDelay = 0; //delay in number of FixedUpdate calls

    private void Start()
    {
        flarelight = GetComponentInParent<Light2D>();
        timeStartedLerp = Time.time;

    }
    void OnTriggerEnter2D(Collider2D collision)
    {   
        //if collided with trigger that is a platform then turn on platform
        if(collision.gameObject.CompareTag("platform"))
        {
            Platform plat = collision.GetComponent<Platform>();

            if (plat != null)
                plat.Lit();
        }
    }

    void FixedUpdate()
    {
        frameDelay++; //increment FixedUpdate call count
        if (flarelight.intensity <= minIntensity && frameDelay == 3) //if the intensity can't lerp furhter and the delay has expired
        {
            flipFlop = false; //set flip flop to increase
            timeStartedLerp = Time.time;
        }
            
        else if (flarelight.intensity >= maxIntensity && frameDelay == 3) //if the light intensity cannot increase further and the delay has expired
        {
            flipFlop = true; //set flip flop to decrease
            timeStartedLerp = Time.time;
        }

        if (!flipFlop) //if set to false (increase)
            flarelight.intensity = Lerp(minIntensity, maxIntensity, timeStartedLerp, lerpTime); //interpolate from 0% to 100x
        else if (flipFlop) //if set to true (decrease)
            flarelight.intensity = Lerp(maxIntensity, minIntensity, timeStartedLerp, lerpTime); //interpolate from 100% to 0%

        if (frameDelay == 3) //if delay reaches max ammount then overflow
            frameDelay = 0;
    }

    private float Lerp(float start, float end, float timeStartedLerp, float lerpTime = 1)
    {
        float timeSinceStarted = Time.time - timeStartedLerp;
        float percentComplete = timeSinceStarted / lerpTime;

        var result = Mathf.Lerp(start, end, percentComplete);
        return result;
    }
}
