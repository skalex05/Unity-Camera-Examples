using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashBrosCamera : MonoBehaviour
{
    public Transform[] targets;

    public float zoomFactor;
    [Range(0,1)]public float positionSmoothing;
    public float endGameOffset;

    public Vector2 lowerLeftBound;
    public Vector2 upperRightBound;
    int activeTargets;
    public Transform t;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    Vector3 desiredPosition;

    // Update is called once per frame
    void Update()
    {
        lowerLeftBound = Vector2.zero;
        upperRightBound = Vector2.zero;
        activeTargets = 0;
        
        foreach (Transform target in targets)
        {
            if (target == null)
            {
                continue;
            }
            t = target;
            activeTargets++;
            if (target.position.x < lowerLeftBound.x)
            {
                lowerLeftBound.x = target.position.x;
            }
            else if (target.position.x > upperRightBound.x)
            {
                upperRightBound.x = target.position.x;
            }
            if (target.position.y < lowerLeftBound.y)
            {
                lowerLeftBound.y = target.position.y;
            }
            else if (target.position.y > upperRightBound.y)
            {
                upperRightBound.y = target.position.y;
            }
        }
        if (activeTargets == 1)
        {
            desiredPosition = t.position + new Vector3(0,0,endGameOffset);
        }
        else if (activeTargets == 0)
        {
            desiredPosition = new Vector3(0,0,endGameOffset);
        }
        else
        {
            desiredPosition = new Vector3(lowerLeftBound.x + (upperRightBound.x - lowerLeftBound.x) * 0.5f, lowerLeftBound.y + (upperRightBound.y - lowerLeftBound.y) * 0.5f, -zoomFactor * (upperRightBound - lowerLeftBound).magnitude);
        }
        transform.position = Vector3.Lerp(transform.position, desiredPosition, positionSmoothing);
    }
}
