using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform PlayerController;  // The object the camera should follow
    public float smoothSpeed = 0.125f;  // How quickly the camera follows the target
    public Vector3 offset;  // The offset from the target's position
    void Start()
    {
        offset = new Vector3(10f, 0f, 0f); // offset camera
    }
    void LateUpdate()
    {
        if (PlayerController == null)
            return;

        Vector3 desiredPosition = PlayerController.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(PlayerController);
    }
}
