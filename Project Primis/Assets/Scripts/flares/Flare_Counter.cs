using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Flare_Counter : MonoBehaviour
{
    public int flares;
    public bool isthrowing;
    public Text FlareDisplay;

    void Update()
    {
        FlareDisplay.text = flares.ToString();
        if (Input.GetKeyDown (InputManager.IM.spawnFlare) && !isthrowing && flares > 0)
        {
            isthrowing = true;
            flares--;
            isthrowing = false;
        }
    }
}
