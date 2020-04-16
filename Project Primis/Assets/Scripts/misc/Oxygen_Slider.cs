using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Oxygen_Slider : MonoBehaviour
{ 
    public Slider slider;
    public Image fill;

    [Space]

    public Gradient air;
    

    public void SetMaxOxygen(float Oxygen)
    {
        slider.maxValue = Oxygen;
        slider.value = Oxygen;
        fill.color = air.Evaluate(1f); //start at first color in gradient
    }

    
    public void SetOxygen (float Oxygen)
    {
        slider.value = Oxygen;
        fill.color = air.Evaluate(slider.normalizedValue); //to make the fill color equal to gradient
    }
}
