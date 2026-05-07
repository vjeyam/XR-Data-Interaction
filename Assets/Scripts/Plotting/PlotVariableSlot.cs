using UnityEngine;

public class PlotVariableSlot : MonoBehaviour
{
    public enum AxisTarget
    {
        X,
        Y
    }

    public AxisTarget axisTarget;

    private void OnTriggerEnter(Collider other)
    {
        DatasetVariableToken variable = other.GetComponentInParent<DatasetVariableToken>();

        if (variable == null) return;

        CSVPointPlot plot = FindObjectOfType<CSVPointPlot>();

        if (plot == null)
        {
            Debug.LogWarning("No CSVPointPlot found.");
            return;
        }

        PlotVariableSlotPanel panel = FindObjectOfType<PlotVariableSlotPanel>();

        if (axisTarget == AxisTarget.X)
        {
            plot.SetXColumn(variable.columnName);

            if (panel != null)
            {
                panel.SetXLabel(variable.columnName);
            }

            Debug.Log("X axis set to: " + variable.columnName);
        }
        else if (axisTarget == AxisTarget.Y)
        {
            plot.SetYColumn(variable.columnName);

            if (panel != null)
            {
                panel.SetYLabel(variable.columnName);
            }

            Debug.Log("Y axis set to: " + variable.columnName);
        }
    }
}