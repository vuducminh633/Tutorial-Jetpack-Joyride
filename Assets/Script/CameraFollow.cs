using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform targetPos;
    public float smoothness;
    private float distanceToTarget;


    private void Start()
    {
        distanceToTarget = transform.position.x - targetPos.transform.position.x;
    }
    private void Update()
    {
        float camPos = Mathf.Lerp(transform.position.x, targetPos.position.x + distanceToTarget, smoothness);
        
        

        transform.position = new Vector3 (camPos, 0, -10);    
    }
}
