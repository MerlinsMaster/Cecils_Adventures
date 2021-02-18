using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxMovement : MonoBehaviour
{
    public Vector2 parallaxEffectMultiplier;
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    private void Awake()
    {
        cameraTransform = Camera.main.transform;                // Cache the camera's transform
        lastCameraPosition = cameraTransform.position;          // Set the initial state of lastCameraPosition
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;                                                          // Get the change in position of the object since the last frame
        transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier.x, deltaMovement.y * parallaxEffectMultiplier.y);  // Multiply this value by the parallaxEffectMultiplier that has been set in the Inspector
        lastCameraPosition = cameraTransform.position;                                                                                  // Reset lastCameraPosition to the current camera position
    }
}
