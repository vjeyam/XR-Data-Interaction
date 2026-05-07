using UnityEngine;

public class DatasetToken : MonoBehaviour
{
    [Header("Dataset Info")]
    public string displayName = "Breast Cancer Wisconsin";
    public string datasetResourcePath = "Datasets/breast_cancer_wisconsin";

    [Header("Default Plot Columns")]
    public string xColumn = "radius_mean";
    public string yColumn = "texture_mean";
    public string zColumn = "area_mean";
    public string colorColumn = "diagnosis";
}