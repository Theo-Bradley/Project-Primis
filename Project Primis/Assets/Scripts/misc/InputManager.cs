using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    //used for singeleton
    public static InputManager IM;

    //put buttons here
    public KeyCode jump;
    public KeyCode spawnFlare;
    public KeyCode dash;
    

    //access with InputMnager.IM.(keyname)


    void Awake()
    {
        //Singleton pattern
        if (IM == null)
        {
            DontDestroyOnLoad(gameObject);
            IM = this;
        }
        else if (IM != this)
        {
            Destroy(gameObject);
        }
    }
}