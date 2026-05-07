using System.Collections.Generic;
using UnityEngine;

public static class CSVLoader
{
    public static List<Dictionary<string, string>> LoadCSV(string resourcePath)
    {
        TextAsset csvFile = Resources.Load<TextAsset>(resourcePath);

        if (csvFile == null)
        {
            Debug.LogError("CSV not found at Resources/" + resourcePath);
            return new List<Dictionary<string, string>>();
        }

        string[] lines = csvFile.text.Split('\n');

        if (lines.Length < 2)
        {
            Debug.LogError("CSV has no data rows.");
            return new List<Dictionary<string, string>>();
        }

        string[] headers = ParseLine(lines[0]);
        Debug.Log("CSV headers: " + string.Join(" | ", headers));

        List<Dictionary<string, string>> rows = new List<Dictionary<string, string>>();

        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            string[] values = ParseLine(lines[i]);

            Dictionary<string, string> row = new Dictionary<string, string>();

            for (int j = 0; j < headers.Length && j < values.Length; j++)
            {
                string header = CleanHeader(headers[j]);
                string value = values[j].Trim();

                if (string.IsNullOrWhiteSpace(header)) continue;
                if (header.StartsWith("Unnamed")) continue;

                row[header] = value;
            }

            rows.Add(row);
        }

        Debug.Log("Loaded CSV rows: " + rows.Count);
        return rows;
    }

    private static string CleanHeader(string header)
    {
        return header
            .Replace("\uFEFF", "")
            .Replace("\"", "")
            .Trim();
    }

    private static string[] ParseLine(string line)
    {
        return line.Replace("\r", "").Split(',');
    }
}