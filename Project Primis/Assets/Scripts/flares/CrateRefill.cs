using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateRefill : MonoBehaviour
{
    public int flaresAvailable;
    public GameObject arm;
    public GameObject flares;
    private void Update()
    {
        if(flaresAvailable == 0)
        {
            Destroy(flares);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Refill();
        }
    }
    public void Refill()
    {
        Flare_Counter fc = arm.GetComponent<Flare_Counter>();
        int flaresgiven = fc.maxflares - fc.flares;

        if(flaresAvailable < flaresgiven)
        {
            fc.flares += flaresAvailable;
            flaresAvailable = 0;
        }
        else
        {
            fc.flares += flaresgiven;
            flaresAvailable -= flaresgiven;
        }

        
    }
}
