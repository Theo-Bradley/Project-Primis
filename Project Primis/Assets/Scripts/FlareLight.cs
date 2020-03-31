using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareLight : MonoBehaviour
{

    Light FlareLightRef;
    public Vector3 offset;
    public float intensity = 2;

    void Start()
    {
        FlareLightRef = transform.gameObject.AddComponent<Light>();
        FlareLightRef.type = LightType.Point;
        FlareLightRef.intensity = intensity;
        FlareLightRef.areaSize = Vector2.one * 5; //Editor Only
        FlareLightRef.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z) + offset;
    }

    void Update()
    {
        FlareLightRef.color = Color.red;
    }
}
