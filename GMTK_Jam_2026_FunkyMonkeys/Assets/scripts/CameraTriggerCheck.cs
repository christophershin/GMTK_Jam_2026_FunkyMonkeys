using System;
using UnityEngine;

public class CameraTriggerCheck : MonoBehaviour
{
    public Transform yPosition;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("CameraTrigger"))
        {
            yPosition = other.transform;
        }
    }
}
