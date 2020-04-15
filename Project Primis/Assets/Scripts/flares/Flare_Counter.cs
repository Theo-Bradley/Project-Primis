using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Flare_Counter : MonoBehaviour
{
    public int flares;
    public bool isthrowing;
    private KeyCode SpawnKey;
    public Text FlareDisplay;
    // Start is called before the first frame update

    void Start()
    {
      SpawnKey = GameObject.Find("GameManager").GetComponent<ClickToSpawn>().SpawnKey;

    }



    // Update is called once per frame
    void Update()
    {
        FlareDisplay.text = flares.ToString();
        if (Input.GetKeyDown (SpawnKey) && !isthrowing && flares > 0)
            {
            isthrowing = true;
            flares--;
            isthrowing = false;
            }
    }
}
