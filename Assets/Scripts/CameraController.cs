using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraController : MonoBehaviour
{
    //MovementCamera
    [SerializeField] private float yAxisWorld = 0f;
    [SerializeField] private float pitch = 0f;
    [SerializeField] private float distToTarget = 8f;
    [SerializeField] private float maxZoomOut = 50f;
    [SerializeField] private float maxZoomIn = 4f;
    [SerializeField] private Transform player;

    void Start()
    {
      
    }

    void Update()
    {
        MovementCamera();
    }

    void MovementCamera()
    {

        if (Input.GetMouseButton(1))
        {
            yAxisWorld += Input.GetAxis("Mouse X") * 5f;
            pitch -= Input.GetAxis("Mouse Y");
            transform.rotation = Quaternion.Euler(pitch, yAxisWorld, 0f);
        }

        if (distToTarget - Input.GetAxis("Mouse ScrollWheel") * 2f <= maxZoomOut && distToTarget - Input.GetAxis("Mouse ScrollWheel") * 2f >= maxZoomIn)
        {
            distToTarget -= Input.GetAxis("Mouse ScrollWheel") * 2f;
            transform.position = player.transform.position - transform.forward * distToTarget;
        }
        else
        {
            transform.position = player.transform.position - transform.forward * distToTarget;
        }
    }
}
