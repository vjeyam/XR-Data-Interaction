using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramPlotController : MonoBehaviour
{
    [Header("Rotation")]
    public bool autoRotate = true;
    public float rotationSpeed = 12f;
    public bool lockHeight = true;

    [Header("Scale")]
    public float minScale = 0.4f;
    public float maxScale = 1.6f;
    public float scaleStep = 0.15f;

    [Header("Explode")]
    public float explodeMultiplier = 1.5f;

    private float lockedY;
    private bool axesVisible = true;
    private bool exploded = false;

    private readonly List<Transform> dataPoints = new List<Transform>();
    private readonly List<Vector3> originalPointPositions = new List<Vector3>();

    private void Start()
    {
        lockedY = transform.position.y;
        StartCoroutine(CachePointsAfterGeneration());
    }

    private IEnumerator CachePointsAfterGeneration()
    {
        yield return null;

        dataPoints.Clear();
        originalPointPositions.Clear();

        foreach (Transform child in GetComponentsInChildren<Transform>())
        {
            if (child.name.Contains("Data Point"))
            {
                dataPoints.Add(child);
                originalPointPositions.Add(child.localPosition);
            }
        }
    }

    private void Update()
    {
        if (lockHeight)
        {
            Vector3 pos = transform.position;
            pos.y = lockedY;
            transform.position = pos;
        }

        if (autoRotate)
        {
            transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f, Space.Self);
        }
    }

    public void ToggleRotation()
    {
        autoRotate = !autoRotate;
        Debug.Log("Auto rotate: " + autoRotate);
    }

    public void ScaleUp()
    {
        float nextScale = Mathf.Min(transform.localScale.x + scaleStep, maxScale);
        transform.localScale = Vector3.one * nextScale;
        Debug.Log("Plot scale up: " + nextScale);
    }

    public void ScaleDown()
    {
        float nextScale = Mathf.Max(transform.localScale.x - scaleStep, minScale);
        transform.localScale = Vector3.one * nextScale;
        Debug.Log("Plot scale down: " + nextScale);
    }

    public void ToggleAxes()
    {
        axesVisible = !axesVisible;

        foreach (Transform child in GetComponentsInChildren<Transform>(true))
        {
            if (child.name.Contains("Axis"))
            {
                child.gameObject.SetActive(axesVisible);
            }
        }

        Debug.Log("Axes visible: " + axesVisible);
    }

    public void ToggleExplode()
    {
        exploded = !exploded;

        for (int i = 0; i < dataPoints.Count; i++)
        {
            if (dataPoints[i] == null) continue;

            Vector3 original = originalPointPositions[i];
            dataPoints[i].localPosition = exploded
                ? original * explodeMultiplier
                : original;
        }

        Debug.Log("Exploded: " + exploded);
    }

    public void ResetPlot()
    {
        autoRotate = true;
        axesVisible = true;
        exploded = false;

        for (int i = 0; i < dataPoints.Count; i++)
        {
            if (dataPoints[i] != null)
            {
                dataPoints[i].localPosition = originalPointPositions[i];
            }
        }

        foreach (Transform child in GetComponentsInChildren<Transform>(true))
        {
            if (child.name.Contains("Axis"))
            {
                child.gameObject.SetActive(true);
            }
        }

        transform.localScale = Vector3.one;
        Debug.Log("Plot reset.");
    }
}