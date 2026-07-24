using System;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;
    public float speed;
    
    public float minClamp, maxClamp;
    
    [SerializeField] private CameraTriggerCheck cameraTriggerCheck;

    void Update()
    {
        Transform yPos = cameraTriggerCheck.yPosition;    
        
        //if (player == null) return;
        //float clampedY = y; //Mathf.Clamp(target.position.y, minClamp, maxClamp);
        Vector3 targetPosition = new Vector3(target.position.x, yPos.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
