using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFlare : MonoBehaviour
{
    #region Line Renderer
    [Range(1, 32)]
    public int resolution = 4;
    private LineRenderer line;
    #endregion
    [Space(10)]
    #region Prediction Formula
    public Vector2 velocity;
    public float yLimit;
    private float g;
    public Transform launchTarget;
    public float strengthMultiplier = 1;
    #endregion
    [Space(10)]
    #region Linecast
    [Range(1, 32)]
    public int linecastResolution = 4;
    public LayerMask canHit;
    #endregion
    [Space(10)]
    #region Flare Spawning
    public GameObject flarePrefab;
    private bool pingPong;
    public KeyCode SpawnKey;
    #endregion

    private void Start()
    {
        g = Mathf.Abs(Physics2D.gravity.y);
        line = gameObject.GetComponent<LineRenderer>();
        pingPong = true;
    }

    private void Update()
    {
        yLimit = -Mathf.Abs(yLimit);
        velocity = (launchTarget.position - transform.position) * strengthMultiplier;
        StartCoroutine(RenderArc());
        if (Input.GetKeyDown(SpawnKey))
        {
            if (pingPong)
            {
                var prefabRef = Instantiate(flarePrefab);
                prefabRef.transform.position = transform.position;
                prefabRef.GetComponent<Rigidbody2D>().velocity = velocity;
            }
        }
        if (Input.GetKeyUp(SpawnKey))
            pingPong = true;
    }

    private IEnumerator RenderArc()
    {
        line.positionCount = resolution + 1;
        line.SetPositions(CalculateLineArray());
        yield return null;
    }

    private Vector3[] CalculateLineArray()
    {
        Vector3[] lineArray = new Vector3[resolution + 1];

        var lowestTimeValue = MaxTimeX() / resolution;

        for (int i = 0; i < lineArray.Length; i++)
        {
            var t = lowestTimeValue * i;
            lineArray[i] = CalculateLinePoint(t);
        }

        return lineArray;
    }

    private Vector2 HitPosition()
    {
        var lowestTimeValue = MaxTimeY() / linecastResolution;

        for (int i = 0; i < linecastResolution + 1; i++)
        {
            var t = lowestTimeValue * i;
            var tt = lowestTimeValue * (i + 1);

            var hit = Physics2D.Linecast(CalculateLinePoint(t), CalculateLinePoint(tt), canHit);

            if (hit)
                return hit.point;
        }

        return CalculateLinePoint(MaxTimeY());
    }

    private Vector3 CalculateLinePoint(float t)
    {
        float x = velocity.x * t;
        float y = (velocity.y * t) - (g * Mathf.Pow(t, 2) / 2);
        return new Vector3(x + transform.position.x, y + transform.position.y);
    }

    private float MaxTimeY()
    {
        var v = velocity.y;
        var vv = v * v;

        var t = (v + Mathf.Sqrt(vv + 2 * g * (transform.position.y - yLimit))) / g;
        return t;
    }

    private float MaxTimeX()
    {
        var x = velocity.x;
        if (x == 0)
        {
            velocity.x = 000.1f;
            x = velocity.x;
        }

        var t = (HitPosition().x - transform.position.x) / x;
        return t;
    }
}
/*used code from https://www.youtube.com/watch?v=ITsynQy5APg&t=33s
source code at https://github.com/Biebras/Unity-2D-Trajectory-Prediction/blob/master/TrajectoryController.cs*/
