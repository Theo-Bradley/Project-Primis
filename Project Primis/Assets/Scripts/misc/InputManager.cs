using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputManager : MonoBehaviour
{
    //used for singeleton
    public static InputManager IM;

    #region system
    [Header("system")]
    public float updateRate = 3; //interval of when input type is checked
    [HideInInspector]
    public float xboxCont;
    [HideInInspector]
    public float ps4Cont;
    #endregion

    #region usingbools
    [Header("Using Bools")]
    public bool usingXB;
    public bool usingPS;
    public bool usingKBM;
    public bool usingcontroller;
    #endregion

    #region currentbinds
    [Header("current Binds")]
    //dont change this
    public KeyCode jump;
    public KeyCode spawnFlare;
    public KeyCode dash;
    #endregion

    #region controller
    [Header("Controller")]
    //controller values
    public float contsens;
    public float contdeadzone;

    //right analog
    [HideInInspector]
    public float RX;
    [HideInInspector]
    public float RY;

    //triggers
    [HideInInspector]
    public float LT;
    [HideInInspector]
    public float RT;

    [HideInInspector]
    public bool rtPressed;
    [HideInInspector]
    public bool ltPressed;

    #endregion


    //access with InputManager.IM.(keyname)


    private void Start()
    {
        //invokes method for optimization purposes
        InvokeRepeating("Inputchecker", 0, updateRate);
    }


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


    private void Update()
    {
        usingcontroller = usingXB || usingPS;

        //easy to use axises, RX and RY are the two axises of the right anlog stick (left stick = Vertical and Horizontal)
        if (usingXB)
        {
            RX = Input.GetAxis("xboxXR");
            RY = Input.GetAxis("xboxYR");
            //triggers
            LT = Input.GetAxis("xboxLT");
            RT = Input.GetAxis("xboxRT");
        }
        if (usingPS)
        {
            RX = Input.GetAxis("psXR");
            RY = Input.GetAxis("psYR");
            //triggers
            LT = Input.GetAxis("psLT");
            RT = Input.GetAxis("psRT");
        }
        //trigger presses
        if(RT > 0)
        {
            rtPressed = true;
        }
        else
        {
            rtPressed = false;
        }
        if (LT > 0)
        {
            ltPressed = true;
        }
        else
        {
            ltPressed = false;
        }
    }


    private void Inputchecker()
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
            if (names[x].Length == 0)
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

        }
        else if (ps4Cont == 1)
        {
            usingXB = false;
            usingPS = true;
            usingKBM = false;

        }
        else if (xboxCont == 0 && ps4Cont == 0)
        {
            usingXB = false;
            usingPS = false;
            usingKBM = true;
        }

        Binds();
    }


    void Binds()
    {
        //binds go here
        if (usingXB)
        {
            jump = KeyCode.Joystick1Button0;
            dash = KeyCode.Joystick1Button5;
            spawnFlare = KeyCode.Joystick1Button2;
        }
        if (usingKBM)
        {
            jump = KeyCode.Space;
            dash = KeyCode.Mouse0;
            spawnFlare = KeyCode.Mouse1;
        }
        if (usingPS)
        {
            //whatever
        }
    }
}
