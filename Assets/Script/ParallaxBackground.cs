using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public Transform cameraTransform;
    public float parallaxEffect = 0.5f;

    private Vector3 lastCameraPosition;

    void Start()
    {
        lastCameraPosition = cameraTransform.position;
    }

    void Update()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxEffect, deltaMovement.y * parallaxEffect, 0);
        lastCameraPosition = cameraTransform.position;

        Debug.Log($"📷 Kamera se pohybuje: {cameraTransform.position}, Pozadí: {transform.position}");
    }
}
