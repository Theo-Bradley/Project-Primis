using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputManager : MonoBehaviour
{
    //used for singeleton
    public static InputManager IM;

    [HideInInspector]
    public float xboxCont;
    [HideInInspector]
    public float ps4Cont;
    
    [Header("Using Bools")]
    public bool usingXB;
    public bool usingPS;
    public bool usingKBM;

    [Header("current Binds")]
    //dont change this
    public KeyCode jump;
    public KeyCode spawnFlare;
    public KeyCode dash;

    [Header("Controller")]
    //controller values
    public float contsens;
    public float contdeadzone;

    //access with InputManager.IM.(keyname)


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


    //in fixed update for optimization
    private void FixedUpdate()
    {
        //check if ps4 or xbox controllers are connected
        string[] names = Input.GetJoystickNames();
        for (int x = 0; x < names.Length; x++)
        {
            if (names[x].Length == 19)
            {
                print("PS4 CONTROLLER IS CONNECTED");
                ps4Cont = 1;
                xboxCont = 0;
            }
            if (names[x].Length == 33)
            {
                print("XBOX ONE CONTROLLER IS CONNECTED");
                //set a controller bool to true
                ps4Cont = 0;
                xboxCont = 1;

            }
            if(names[x].Length == 0)
            {
                print("No Controllers connected");
                ps4Cont = 0;
                xboxCont = 0;
            }
        }

        //if connected
        if (xboxCont == 1)
        {
            usingXB = true;
            usingPS = false;
            usingKBM = false;
            Xboxsetup();
        }
        else if (ps4Cont== 1)
        {
            usingXB = false;
            usingPS = true;
            usingKBM = false;
            PsSetup();
        }
        else if(xboxCont ==0 && ps4Cont ==0)
        {
            usingXB = false;
            usingPS = false;
            usingKBM = true;
            Keyboardsetup();
        }
    }

    void Xboxsetup()
    {
        jump = KeyCode.Joystick1Button0;
        dash = KeyCode.Joystick1Button5;
        spawnFlare = KeyCode.Joystick1Button2;
    }

    void Keyboardsetup()
    {
        jump = KeyCode.Space;
        dash = KeyCode.Mouse0;
        spawnFlare = KeyCode.Mouse1;
    }
    void PsSetup()
    {
        //whatever ps4 keys
    }
}
