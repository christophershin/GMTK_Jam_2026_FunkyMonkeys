using System;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;
    public float speed;
    
    public float minClamp, maxClamp;

    void Update()
    {
        //if (player == null) return;
        float clampedY = Mathf.Clamp(target.position.y, minClamp, maxClamp);
        Vector3 targetPosition = new Vector3(target.position.x, clampedY, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
