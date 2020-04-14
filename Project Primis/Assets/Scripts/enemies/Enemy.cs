using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float LerpTime;
    private float timeStartedLerp;
    [Space(10)]
    public List<Transform> Waypoints;
    public enum EndTypeEnum
    {
        Loop,
        Reverse,
        Stop
    }
    [Space(10)]
    public EndTypeEnum EndType;
    public bool LookTowards= false;

    private bool shouldLerp = false;
    private int i = 0;
    private Vector3 startVector;


    private bool reverse = false;

    void Start()
    {
        StartLerp();
    }

    void StartLerp()
    {
        timeStartedLerp = Time.time;
        shouldLerp = true;
        startVector = transform.position;
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, Waypoints[i].position) <= 0.01) //if reached waypoint
        {
            shouldLerp = false;

            switch (EndType)
            {
                case EndTypeEnum.Loop:
                    if (i == Waypoints.Count - 1)
                        i = 0;
                    else
                        i++;
                    break;
                case EndTypeEnum.Reverse:
                    if (i == Waypoints.Count - 1 && !reverse)
                        reverse = true;
                    if (i == 0 && reverse)
                        reverse = false;

                    if (!reverse)
                        i++;
                    if (reverse)
                        i--;
                    break;
                case EndTypeEnum.Stop:
                    if (i != Waypoints.Count - 1)
                        i++;
                    break;
            }
            StartLerp();
        }

        if (shouldLerp)
        {
            transform.position = Lerp(startVector, Waypoints[i].position, timeStartedLerp, LerpTime);
            if (LookTowards)
            {
                transform.right = Waypoints[i].position - transform.position;
            }
                
        }
    }

    private Vector3 Lerp (Vector3 start, Vector3 end, float timeStartedLerp, float LerpTime = 1)
    {
        float timeSinceStarted = Time.time - timeStartedLerp;
        float percentComplete = timeSinceStarted / LerpTime;

        var result = Vector3.Lerp(start, end, percentComplete);
        return result;
    }
}
