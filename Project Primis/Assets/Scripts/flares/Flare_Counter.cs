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
        if (Input.GetKeyUp (InputManager.IM.spawnFlare) && !isthrowing && flares > 0)
        {
            isthrowing = true;
            StartCoroutine(thrown());
            isthrowing = false;
        }
    }

    //added a delay so it worked as a projectile
    IEnumerator thrown()
    {
        yield return new WaitForSeconds(0.1f);
        flares--;
    }
}
