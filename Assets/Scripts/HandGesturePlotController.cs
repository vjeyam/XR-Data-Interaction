using UnityEngine;

public class HandGesturePlotController : MonoBehaviour
{
    [Header("Hand References")]
    public Transform leftHand;
    public Transform rightHand;
    public Transform headCamera;

    [Header("Detection")]
    public float gestureCooldown = 1.0f;
    public float twoHandNearDistance = 0.45f;
    public float expandContractThreshold = 0.08f;

    private float lastGestureTime = -10f;
    private float previousHandDistance = -1f;

    private HologramPlotController currentPlot;

    private void Update()
    {
        if (currentPlot == null)
        {
            currentPlot = FindObjectOfType<HologramPlotController>();
            if (currentPlot == null) return;
        }

        DetectTwoHandScaleGesture();
        DetectPalmPauseGesture();
    }

    private void DetectTwoHandScaleGesture()
    {
        if (leftHand == null || rightHand == null) return;

        float currentDistance = Vector3.Distance(leftHand.position, rightHand.position);

        if (previousHandDistance < 0f)
        {
            previousHandDistance = currentDistance;
            return;
        }

        float delta = currentDistance - previousHandDistance;

        if (Mathf.Abs(delta) > 0.002f)
        {
            currentPlot.ScaleByGesture(delta);
        }

        previousHandDistance = currentDistance;
    }

    private void DetectPalmPauseGesture()
    {
        if (rightHand == null || headCamera == null) return;

        if (Time.time - lastGestureTime < gestureCooldown) return;

        float distanceToHead = Vector3.Distance(rightHand.position, headCamera.position);

        // Simple placeholder: right hand held near headset toggles pause.
        // We can refine this into actual palm-facing detection once we identify the palm transform.
        if (distanceToHead < 0.35f)
        {
            currentPlot.ToggleRotation();
            Debug.Log("Gesture: right hand near head = toggle rotation");

            lastGestureTime = Time.time;
        }
    }
}