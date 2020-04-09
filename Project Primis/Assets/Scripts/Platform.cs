using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void isLit(bool isLit)
    {
        transform.gameObject.layer = 0; //set collision layer to 0 so it can collide with everything but an object in the "invisible" layer

        

        
    }
}
