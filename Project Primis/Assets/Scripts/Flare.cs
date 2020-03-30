using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flare : MonoBehaviour
{

    Light FlareLight;
    public float speed;
    public float offset = 0.3f;

    void Start()
    {
        FlareLight = transform.gameObject.AddComponent<Light>();
        FlareLight.type = LightType.Point;
        FlareLight.intensity = 1;
        FlareLight.areaSize = Vector2.one * 5; //Editor Only
        FlareLight.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - offset);
    }

    void Update()
    {
        FlareLight.color = Color.red;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Platform"))
            col.gameObject.GetComponent<Platform>().isLit(true);
    }
}
