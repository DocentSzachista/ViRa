using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class keyboard_follow_camera : MonoBehaviour
{
    public Transform cameraTransform; // Reference to the VR camera transform
    public Vector3 offset = new Vector3(0f, 1f, 2f);
    public float smooth = 0.5f;
    void Update()
    {
        // Check if the camera transform is assigned
        if (cameraTransform != null)
        {
            // Set the position of this object to match the position of the VR camera
            Quaternion headRotation = cameraTransform.transform.rotation;
            //transform.position = cameraTransform.position + offset;
            // Vector3 resultingPosition = cameraTransform.position + Vector3.Scale(cameraTransform.position, transform.position);
            //transform.position = resultingPosition;
            transform.rotation = headRotation;
            transform.position = Vector3.Lerp(
            transform.position, cameraTransform.position,
            Time.deltaTime * smooth);
        }
        else
        {
            Debug.LogWarning("Camera transform not assigned! Assign the VR camera transform.");
        }
    }
}
