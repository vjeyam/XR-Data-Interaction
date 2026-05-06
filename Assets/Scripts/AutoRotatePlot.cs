using UnityEngine;

public class AutoRotatePlot : MonoBehaviour
{
    public float rotationSpeed = 12f;
    public bool rotateOnlyWhenNotGrabbed = true;

    private Vector3 lockedPosition;

    private void Start()
    {
        lockedPosition = transform.position;
    }

    private void Update()
    {
        // Keep plot locked to same height/position
        transform.position = lockedPosition;

        // Tight local spin
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f, Space.Self);
    }
}