using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputManager : MonoBehaviour
{
    //used for singeleton
    public static InputManager IM;

    public float xboxCont;
    public float ps4Cont;

    public float sens;

    //put buttons here
    public KeyCode jump;
    public KeyCode spawnFlare;
    public KeyCode dash;
    

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


    private void Update()
    {
        string[] names = Input.GetJoystickNames();
        for (int x = 0; x < names.Length; x++)
        {
            print(names[x].Length);
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
                ps4Cont = 0;
                xboxCont = 0;
            }
        }


        if (xboxCont == 1)
        {
            Xboxsetup();

            
        }
        else if (ps4Cont== 1)
        {
            //do something
        }
        else if(xboxCont ==0 && ps4Cont ==0)
        {
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
    
}
