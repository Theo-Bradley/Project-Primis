using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToSpawn : MonoBehaviour
{
    private Flare_Counter fc;

    public GameObject SpawnObject;
    public Vector2 TurnMax;

    private void Start()
    {
        fc = GetComponent<Flare_Counter>();
    }
    void Update()
    {
        if (Input.GetKeyDown(InputManager.IM.spawnFlare) && fc.flares >0)
        {
            var objRef = Instantiate(SpawnObject);
            var tempPos = Camera.main.ScreenToWorldPoint(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono);
            objRef.transform.position = new Vector3(tempPos.x, tempPos.y, 0);
            objRef.GetComponent<Rigidbody2D>().AddTorque(Random.Range(TurnMax.x, TurnMax.y));
        }
    }
}
