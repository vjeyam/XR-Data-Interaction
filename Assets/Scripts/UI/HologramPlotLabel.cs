using UnityEngine;
using TMPro;

public class HologramPlotLabel : MonoBehaviour
{
    [Header("Label")]
    public string title = "Breast Cancer Wisconsin";
    public string subtitle = "radius_mean / texture_mean / area_mean";

    [Header("Placement")]
    public Vector3 worldOffset = new Vector3(0f, 0.75f, 0f);
    public float textScale = 0.04f;

    private TextMeshPro textMesh;
    private Transform labelTransform;
    private Transform cameraTransform;

    private void Start()
    {
        cameraTransform = Camera.main != null ? Camera.main.transform : null;
        CreateLabel();
    }

    private void LateUpdate()
    {
        if (labelTransform == null) return;

        // Follow plot position, but do not inherit plot rotation
        labelTransform.position = transform.position + worldOffset;

        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }

        if (cameraTransform == null) return;

        // Face camera while staying upright
        Vector3 direction = labelTransform.position - cameraTransform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.001f)
        {
            labelTransform.rotation = Quaternion.LookRotation(direction);
        }
    }

    private void CreateLabel()
    {
        GameObject labelObject = new GameObject("World Plot Title Label");

        // Important: no parent, so it does NOT rotate with the plot
        labelObject.transform.SetParent(null);

        labelTransform = labelObject.transform;
        labelTransform.position = transform.position + worldOffset;
        labelTransform.localScale = Vector3.one * textScale;

        textMesh = labelObject.AddComponent<TextMeshPro>();
        textMesh.text = title + "\n" + subtitle;
        textMesh.alignment = TextAlignmentOptions.Center;
        textMesh.fontSize = 6f;
        textMesh.color = Color.cyan;
    }

    public void SetLabel(string newTitle, string newSubtitle)
    {
        title = newTitle;
        subtitle = newSubtitle;

        if (textMesh != null)
        {
            textMesh.text = title + "\n" + subtitle;
        }
    }

    private void OnDestroy()
    {
        if (labelTransform != null)
        {
            Destroy(labelTransform.gameObject);
        }
    }
}