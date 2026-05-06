using UnityEngine;

public class DatasetDropZone : MonoBehaviour
{
    public GameObject plotPrefab;
    public Transform plotSpawnPoint;

    private bool hasSpawned = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasSpawned) return;

        if (other.CompareTag("Dataset"))
        {
            Instantiate(plotPrefab, plotSpawnPoint.position, plotSpawnPoint.rotation);
            hasSpawned = true;
            Debug.Log("Dataset placed. Plot spawned.");
        }
    }
}