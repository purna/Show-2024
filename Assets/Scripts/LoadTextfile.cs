using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using System.IO;


public class LoadTextfile : MonoBehaviour
{
    public TMP_Text textField;

    public TMP_Text log;

    //File path of the text file to load
    public string filePath;

    // Drag and drop the CSV file here in the Inspector
    public TextAsset csvFile; 

    public List<string[]> rowData = new List<string[]>();

    // Start is called before the first frame update
    private void Start()
    {

#if UNITY_EDITOR
        string appPath = System.IO.Path.Combine(Application.dataPath, filePath);
#else
        string appPath = System.IO.Path.Combine(Application.dataPath, "..", filePath);
#endif


        //Load the text file
        string text = LoadTextFromFile(appPath);
           
        //Assign the loaded text to the componant
        textField.text = text;

    }


    string LoadTextFromFile(string path)
    {
        //Check if the file exsists
        if (File.Exists(path))
        {
            //Read the file
            log.text = System.IO.Path.GetFullPath(path);
            return File.ReadAllText(path);
        } 
        else
        {
            log.text = "Not found";
            return null;
        }
    }


    public IEnumerator StartLoading()
    {
        if (csvFile != null)
        {
            string fileContent = csvFile.text;
            string[] lines = fileContent.Split('\n');

            // Parse CSV data
            for (int i = 0; i < lines.Length; i++)
            {
                string[] columns = lines[i].Split(',');
                if (columns.Length > 0)
                {
                    rowData.Add(columns);
                }
            }

            // Debug prints
            Debug.Log("Data loaded from local CSV file.");
        }
        else
        {
            Debug.LogError("CSV file is not assigned.");
        }

        yield return null;
    }


    // Method to get row data by index
    public string[] GetRowData(int rowIndex)
        {
            if (rowIndex >= 0 && rowIndex < rowData.Count)
            {
                return rowData[rowIndex];
            }
            else
            {
                Debug.LogWarning("Row with index " + rowIndex + " not found.");
                return null;
            }
        }

}
