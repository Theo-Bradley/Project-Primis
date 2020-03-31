using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToSpawn : MonoBehaviour
{

    public GameObject SpawnObject;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            var objRef = Instantiate(SpawnObject);
            var tempPos = Camera.main.ScreenToWorldPoint(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono);
            objRef.transform.position = new Vector3(tempPos.x, tempPos.y, 0);
        }
        
    }
}
