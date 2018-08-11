using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiderPassageLerp : MonoBehaviour {

    private Vector3 startPos;
    [SerializeField]
    private Transform endPos;
    [SerializeField]
    private float speed = 1.0f;
    private static float startTime, totalDistance;
    private static float waitTime;

    IEnumerator Start()
    {
        startTime = Time.time;
        totalDistance = Vector3.Distance(startPos, endPos.position);
        yield return null;
    }

    private void Update()
    {
        waitTime += Time.deltaTime;
        startPos = transform.position;
        float currentDuration = (Time.time - startTime) * speed;
        float journeyFraction = currentDuration / totalDistance;
        if (waitTime >= 2f)
        {
            transform.position = Vector3.Lerp(startPos, endPos.position, journeyFraction);
        }        
    }

    public static void NewStartTime()
    {
        startTime = Time.time;
        waitTime = 0f;
    }

}
