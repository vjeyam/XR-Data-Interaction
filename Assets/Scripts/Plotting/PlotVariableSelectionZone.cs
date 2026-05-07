using UnityEngine;

public class PlotVariableSelectionZone : MonoBehaviour
{
    private CSVPointPlot currentPlot;

    private string selectedX = "";
    private string selectedY = "";
    private string lastSelectedColumn = "";

    public float cooldown = 1.25f;
    private float lastSelectTime = -10f;

    private void OnTriggerEnter(Collider other)
    {
        if (Time.time - lastSelectTime < cooldown) return;

        DatasetVariableToken variable = other.GetComponentInParent<DatasetVariableToken>();
        if (variable == null) return;

        if (variable.columnName == lastSelectedColumn) return;

        currentPlot = FindObjectOfType<CSVPointPlot>();

        if (currentPlot == null)
        {
            Debug.LogWarning("No CSVPointPlot found for variable selection.");
            return;
        }

        SelectVariable(variable.columnName);
        lastSelectedColumn = variable.columnName;
        lastSelectTime = Time.time;
    }

    private void SelectVariable(string columnName)
    {
        if (string.IsNullOrEmpty(selectedX))
        {
            selectedX = columnName;
            currentPlot.SetXColumn(columnName);
            Debug.Log("Selected X variable: " + columnName);
            return;
        }

        if (string.IsNullOrEmpty(selectedY))
        {
            selectedY = columnName;
            currentPlot.SetYColumn(columnName);
            Debug.Log("Selected Y variable: " + columnName);
            return;
        }

        selectedY = columnName;
        currentPlot.SetYColumn(columnName);
        Debug.Log("Replaced Y variable: " + columnName);
    }

    public void ResetSelection()
    {
        selectedX = "";
        selectedY = "";
        lastSelectedColumn = "";
    }
}