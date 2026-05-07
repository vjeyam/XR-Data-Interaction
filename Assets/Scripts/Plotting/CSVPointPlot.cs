using System.Collections.Generic;
using UnityEngine;

public class CSVPointPlot : MonoBehaviour
{
    [Header("CSV")]
    public string datasetResourcePath = "Datasets/breast_cancer_wisconsin";

    [Header("Column Mapping")]
    public string xColumn = "radius_mean";
    public string yColumn = "texture_mean";
    public string zColumn = "area_mean";
    public string colorColumn = "diagnosis";

    [Header("Plot Settings")]
    public float plotSize = 1.0f;
    public float pointScale = 0.035f;

    private List<Dictionary<string, string>> rows;

    private bool hasGenerated = false;

    private void Start()
    {
        if (!hasGenerated)
        {
            GeneratePlot();
        }
    }

    public void GeneratePlotFromToken(DatasetToken token)
    {
        datasetResourcePath = token.datasetResourcePath;
        xColumn = token.xColumn;
        yColumn = token.yColumn;
        zColumn = token.zColumn;
        colorColumn = token.colorColumn;

        GeneratePlot();
    }

    private void GeneratePlot()
    {
        hasGenerated = true;
        rows = CSVLoader.LoadCSV(datasetResourcePath);

        if (rows == null || rows.Count == 0)
        {
            Debug.LogError("No CSV rows loaded.");
            return;
        }

        float minX = GetMin(xColumn);
        float maxX = GetMax(xColumn);

        float minY = GetMin(yColumn);
        float maxY = GetMax(yColumn);

        float minZ = GetMin(zColumn);
        float maxZ = GetMax(zColumn);

        Debug.Log("X column: " + xColumn + " min=" + minX + " max=" + maxX);
        Debug.Log("Y column: " + yColumn + " min=" + minY + " max=" + maxY);
        Debug.Log("Z column: " + zColumn + " min=" + minZ + " max=" + maxZ);

        CreateAxis("X Axis", Vector3.right, Color.red);
        CreateAxis("Y Axis", Vector3.up, Color.green);
        CreateAxis("Z Axis", Vector3.forward, Color.blue);

        int createdCount = 0;
        int skippedCount = 0;

        foreach (Dictionary<string, string> row in rows)
        {
            if (!TryGetFloat(row, xColumn, out float rawX))
            {
                skippedCount++;
                continue;
            }

            if (!TryGetFloat(row, yColumn, out float rawY))
            {
                skippedCount++;
                continue;
            }

            if (!TryGetFloat(row, zColumn, out float rawZ))
            {
                skippedCount++;
                continue;
            }

            float x = Normalize(rawX, minX, maxX) * plotSize;
            float y = Normalize(rawY, minY, maxY) * plotSize;
            float z = Normalize(rawZ, minZ, maxZ) * plotSize;

            GameObject point = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            point.name = "Data Point";
            point.transform.SetParent(transform);
            point.transform.localPosition = new Vector3(x, y, z);
            point.transform.localScale = Vector3.one * pointScale;

            Renderer renderer = point.GetComponent<Renderer>();
            renderer.material.color = GetDiagnosisColor(row);

            Collider pointCollider = point.GetComponent<Collider>();
            if (pointCollider != null)
            {
                Destroy(pointCollider);
            }

            createdCount++;

            if (createdCount <= 5)
            {
                Debug.Log("Created point " + createdCount + " at " + point.transform.localPosition);
            }
        }

        Debug.Log("Total created CSV points: " + createdCount);
        Debug.Log("Total skipped CSV rows: " + skippedCount);
        Debug.Log("Final child count on plot: " + transform.childCount);
    }

    private Color GetDiagnosisColor(Dictionary<string, string> row)
    {
        if (!row.ContainsKey(colorColumn)) return Color.white;

        string diagnosis = row[colorColumn].Trim();

        if (diagnosis == "M")
        {
            return new Color(1f, 0.25f, 0.25f);
        }

        if (diagnosis == "B")
        {
            return new Color(0.25f, 0.6f, 1f);
        }

        return Color.white;
    }

    private float GetMin(string column)
    {
        float min = float.MaxValue;

        foreach (Dictionary<string, string> row in rows)
        {
            if (TryGetFloat(row, column, out float value))
            {
                min = Mathf.Min(min, value);
            }
        }

        return min;
    }

    private float GetMax(string column)
    {
        float max = float.MinValue;

        foreach (Dictionary<string, string> row in rows)
        {
            if (TryGetFloat(row, column, out float value))
            {
                max = Mathf.Max(max, value);
            }
        }

        return max;
    }

    private bool TryGetFloat(Dictionary<string, string> row, string column, out float value)
    {
        value = 0f;

        if (!row.ContainsKey(column))
        {
            Debug.LogWarning("Missing column: " + column);
            return false;
        }

        return float.TryParse(row[column], out value);
    }

    private float Normalize(float value, float min, float max)
    {
        if (Mathf.Approximately(min, max)) return 0f;
        return (value - min) / (max - min);
    }

    private void CreateAxis(string name, Vector3 direction, Color color)
    {
        GameObject axis = GameObject.CreatePrimitive(PrimitiveType.Cube);
        axis.name = name;
        axis.transform.SetParent(transform);

        axis.transform.localPosition = direction * (plotSize / 2f);

        if (direction.x != 0)
            axis.transform.localScale = new Vector3(plotSize, 0.01f, 0.01f);
        else if (direction.y != 0)
            axis.transform.localScale = new Vector3(0.01f, plotSize, 0.01f);
        else
            axis.transform.localScale = new Vector3(0.01f, 0.01f, plotSize);

        axis.GetComponent<Renderer>().material.color = color;

        Collider axisCollider = axis.GetComponent<Collider>();
        if (axisCollider != null)
        {
            Destroy(axisCollider);
        }
    }
}