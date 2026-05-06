using UnityEngine;

public class SimplePointPlot : MonoBehaviour
{
    public int pointCount = 60;
    public float plotSize = 0.8f;
    public float pointScale = 0.035f;

    private void Start()
    {
        GeneratePlot();
    }

    private void GeneratePlot()
    {
        CreateAxis("X Axis", new Vector3(1f, 0f, 0f), Color.red);
        CreateAxis("Y Axis", new Vector3(0f, 1f, 0f), Color.green);
        CreateAxis("Z Axis", new Vector3(0f, 0f, 1f), Color.blue);

        for (int i = 0; i < pointCount; i++)
        {
            Vector3 position = new Vector3(
                Random.Range(0f, plotSize),
                Random.Range(0f, plotSize),
                Random.Range(0f, plotSize)
            );

            GameObject point = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            point.name = "Data Point";
            point.transform.SetParent(transform);
            point.transform.localPosition = position;
            point.transform.localScale = Vector3.one * pointScale;
        }
    }

    private void CreateAxis(string name, Vector3 direction, Color color)
    {
        GameObject axis = GameObject.CreatePrimitive(PrimitiveType.Cube);
        axis.name = name;
        axis.transform.SetParent(transform);

        axis.transform.localPosition = direction * 0.4f;

        if (direction.x != 0)
            axis.transform.localScale = new Vector3(0.8f, 0.01f, 0.01f);
        else if (direction.y != 0)
            axis.transform.localScale = new Vector3(0.01f, 0.8f, 0.01f);
        else
            axis.transform.localScale = new Vector3(0.01f, 0.01f, 0.8f);

        axis.GetComponent<Renderer>().material.color = color;
    }
}