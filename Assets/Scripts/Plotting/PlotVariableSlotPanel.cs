using UnityEngine;
using TMPro;

public class PlotVariableSlotPanel : MonoBehaviour
{
    [Header("Plot Follow")]
    public Transform targetPlot;
    public Vector3 worldOffset = new Vector3(0f, -0.25f, -0.55f);

    [Header("Slot Placement")]
    public Transform xSlot;
    public Transform ySlot;
    public Vector3 xLocalOffset = new Vector3(-0.25f, 0f, 0f);
    public Vector3 yLocalOffset = new Vector3(0.25f, 0f, 0f);

    [Header("Text")]
    public TextMeshPro xText;
    public TextMeshPro yText;

    private Transform cameraTransform;

    private void Start()
    {
        cameraTransform = Camera.main != null ? Camera.main.transform : null;
    }

    private void LateUpdate()
    {
        if (targetPlot == null) return;

        transform.position = targetPlot.position + worldOffset;

        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }

        if (cameraTransform != null)
        {
            Vector3 direction = transform.position - cameraTransform.position;
            direction.y = 0f;

            if (direction.sqrMagnitude > 0.001f)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }

        if (xSlot != null)
        {
            xSlot.localPosition = xLocalOffset;
        }

        if (ySlot != null)
        {
            ySlot.localPosition = yLocalOffset;
        }
    }

    public void SetXLabel(string columnName)
    {
        if (xText != null)
        {
            xText.text = "X\n" + columnName;
        }
    }

    public void SetYLabel(string columnName)
    {
        if (yText != null)
        {
            yText.text = "Y\n" + columnName;
        }
    }

    public void SetLabels(string xColumn, string yColumn)
    {
        SetXLabel(xColumn);
        SetYLabel(yColumn);
    }
}