using UnityEngine;

public class DatasetDropZone : MonoBehaviour
{
    public GameObject plotPrefab;
    public Transform plotSpawnPoint;

    private bool hasSpawned = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("DropZone touched by: " + other.name);

        if (hasSpawned) return;

        if (IsDatasetObject(other.transform))
        {
            Debug.Log("Dataset detected. Spawning plot.");
            SpawnPlot(other);
            hasSpawned = true;
        }
    }

    private bool IsDatasetObject(Transform hitTransform)
    {
        Transform current = hitTransform;

        while (current != null)
        {
            if (current.CompareTag("Dataset"))
                return true;

            current = current.parent;
        }

        return false;
    }

    private void SpawnPlot(Collider datasetCollider)
    {
        if (plotPrefab == null)
        {
            Debug.LogError("Plot prefab missing on DatasetDropZone.");
            return;
        }

        if (plotSpawnPoint == null)
        {
            Debug.LogError("Plot spawn point missing on DatasetDropZone.");
            return;
        }

        DatasetToken token = datasetCollider.GetComponentInParent<DatasetToken>();

        GameObject spawnedPlot = Instantiate(
            plotPrefab,
            plotSpawnPoint.position,
            plotSpawnPoint.rotation
        );

        spawnedPlot.name = "Spawned 3D Plot";

        CSVPointPlot csvPlot = spawnedPlot.GetComponent<CSVPointPlot>();

        if (token != null && csvPlot != null)
        {
            Debug.Log("TOKEN FOUND");
            Debug.Log("Dataset: " + token.displayName);
            Debug.Log("CSV Path: " + token.datasetResourcePath);
            Debug.Log("Columns: " + token.xColumn + ", " + token.yColumn + ", " + token.zColumn);
            Debug.Log("Color Column: " + token.colorColumn);

            // Generate plot from selected dataset cube metadata
            csvPlot.GeneratePlotFromToken(token);

            // Update floating plot title label
            HologramPlotLabel label = spawnedPlot.GetComponent<HologramPlotLabel>();

            if (label != null)
            {
                label.SetLabel(
                    token.displayName,
                    token.xColumn + " / " + token.yColumn + " / " + token.zColumn
                );
            }

            // Make X/Y variable slot panel follow this spawned plot
            PlotVariableSlotPanel slotPanel = FindObjectOfType<PlotVariableSlotPanel>();

            if (slotPanel != null)
            {
                slotPanel.targetPlot = spawnedPlot.transform;
                slotPanel.SetLabels(csvPlot.xColumn, csvPlot.yColumn);
            }
            else
            {
                Debug.LogWarning("PlotVariableSlotPanel not found in scene.");
            }
        }
        else
        {
            if (token == null)
            {
                Debug.LogWarning("DatasetToken missing on dataset object or parent.");
            }

            if (csvPlot == null)
            {
                Debug.LogWarning("CSVPointPlot missing on spawned plot prefab.");
            }
        }
    }

    public void ResetDropZone()
    {
        hasSpawned = false;
        Debug.Log("Drop zone reset.");
    }
}