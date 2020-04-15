using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public float maxoxygen;
    public float currentoxygen;

    public Oxygen_Slider oBar;
   

    void Start()
    {
        currentoxygen = maxoxygen;
        oBar.SetMaxOxygen(maxoxygen);
    }

 
    void Update()
    {
        oBar.SetOxygen(currentoxygen);
    }

    void Die()
    {

    }
}
