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
            SpawnPlot();
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

    private void SpawnPlot()
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

        GameObject spawnedPlot = Instantiate(
            plotPrefab,
            plotSpawnPoint.position,
            plotSpawnPoint.rotation
        );

        spawnedPlot.name = "Spawned 3D Plot";
        spawnedPlot.transform.localScale = Vector3.one * 0.8f;

        Debug.Log("Plot spawned at: " + plotSpawnPoint.position);
    }
}