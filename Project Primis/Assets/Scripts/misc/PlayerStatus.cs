using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStatus : MonoBehaviour
{
    public Oxygen_Slider oBar;
    public CanvasGroup img;

    [Space]

    public float maxoxygen;
    public float currentoxygen;

    [Space]

    //how long it takes to fade into darkness
    public float fadetime;


    void Start()
    {
        currentoxygen = maxoxygen;
        oBar.SetMaxOxygen(maxoxygen);

        img.alpha = 0f;  //clear at start
    }

 
    void Update()
    {
        oBar.SetOxygen(currentoxygen);  //always set oxygen on the bar
       
        if(currentoxygen <= 0)
        {
            StartCoroutine(Fade(0, 1, fadetime));
        }
    }


    //couroutine for fade 
    IEnumerator Fade(float v_start, float v_end, float duration)
    {
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            img.alpha= Mathf.Lerp(v_start, v_end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        img.alpha = v_end;
    }


    void Die()
    {
        
    }
}
