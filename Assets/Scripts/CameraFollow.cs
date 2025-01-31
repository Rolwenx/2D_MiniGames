using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player; 
    [SerializeField] private float smoothSpeed = 5f; 

    private Vector3 offset; // Keeps the camera at a fixed distance

    void Start()
    {
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        if (player == null)
        {
            Debug.Log("Player not found");
            return;
        } 

        Vector3 targetPosition = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }

}
