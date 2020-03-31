using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToSpawn : MonoBehaviour
{

    public GameObject SpawnObject;
    public Vector2 TurnMax;
    public KeyCode SpawnKey;

    void Update()
    {
        if (Input.GetKeyDown(SpawnKey))
        {
            var objRef = Instantiate(SpawnObject);
            var tempPos = Camera.main.ScreenToWorldPoint(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono);
            objRef.transform.position = new Vector3(tempPos.x, tempPos.y, 0);
            objRef.GetComponent<Rigidbody2D>().AddTorque(Random.Range(TurnMax.x, TurnMax.y));
        }
        
    }
}
