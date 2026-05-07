using UnityEngine;

public class PlotVariableSelectionZone : MonoBehaviour
{
    private CSVPointPlot currentPlot;

    private string selectedX = "";
    private string selectedY = "";

    private float cooldown = 0.75f;
    private float lastSelectTime = -10f;

    private void OnTriggerEnter(Collider other)
    {
        if (Time.time - lastSelectTime < cooldown) return;

        DatasetVariableToken variable = other.GetComponentInParent<DatasetVariableToken>();

        if (variable == null) return;

        currentPlot = FindObjectOfType<CSVPointPlot>();

        if (currentPlot == null)
        {
            Debug.LogWarning("No CSVPointPlot found for variable selection.");
            return;
        }

        SelectVariable(variable.columnName);
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

        // After X and Y are both set, replace Y by default.
        selectedY = columnName;
        currentPlot.SetYColumn(columnName);
        Debug.Log("Replaced Y variable: " + columnName);
    }

    public void ResetSelection()
    {
        selectedX = "";
        selectedY = "";
    }
}