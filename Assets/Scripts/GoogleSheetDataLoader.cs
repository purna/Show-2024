using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class GoogleSheetDataLoader : MonoBehaviour
{
    public string googleSheetsURL = "https://docs.google.com/spreadsheets/d/e/2PACX-1vQy3jQxng4KXs00w7bfcEm7-K3_eA7Ej1XnuIYgMLHp492xzRC59V0bWjNbfLbVYjvrWIHoz5eReIxH/pub?output=csv";
    private List<string[]> rowData = new List<string[]>();

    public bool liveLink = true;

    void Start()
    {
        if (liveLink == true)
        {
            // Load CSV data from Google Sheets
            Debug.Log("CSV Sheets");
            //StartCoroutine(LoadCSVFromGoogleSheets());

        }
        else
        {
            // Load CSV data from the art.csv file in the Assets folder
            Debug.Log("CSV Assets");
            //LoadCSVFromAssets();
        }
    }


    public IEnumerator StartLoading()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(googleSheetsURL))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string[] lines = www.downloadHandler.text.Split('\n');

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
                Debug.Log("Data loaded from Google Sheets.");
            }
            else
            {
                Debug.LogError("Failed to load data from Google Sheets. Error: " + www.error);
            }
        }
    }

    /*
    public IEnumerator LoadCSVMenuFromGoogleSheets()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(googleSheetsURL))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string[] lines = www.downloadHandler.text.Split('\n');

                // Parse CSV data
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] columns = lines[i].Split(',');
                    if (columns.Length >= 4)
                    {
                        //fourthColumnData.Add(columns[3]); // Adding the fourth column data to the list
                    }
                }

                Debug.Log("CSV data loaded successfully.");
            }
            else
            {
                Debug.LogError("Failed to load CSV from Google Sheets. Error: " + www.error);
            }
        }

        //int childCount = containerRect.transform.childCount;
        //Debug.Log("children" + childCount);

        
        for (int i = 0; i < childCount; i++)
        {
            Debug.Log("Data menu" + fourthColumnData[i]);
            StartCoroutine(LoadMenuImage(fourthColumnData[i], i));
        }
       
    }
    */

    /*
    void LoadCSVFromAssets()
    {
        TextAsset csvFile = Resources.Load<TextAsset>("art");
        if (csvFile != null)
        {
            string[] lines = csvFile.text.Split('\n');

            // Parse CSV data
            for (int i = 0; i < lines.Length; i++)
            {
                string[] columns = lines[i].Split(',');
                if (columns.Length >= 4)
                {
                    string buttonName = columns[0];
                    if (!buttonData.ContainsKey(buttonName))
                    {
                        buttonData[buttonName] = new List<string>();
                    }
                    buttonData[buttonName].AddRange(columns);
                }
            }

            // Debug prints

            Debug.Log("Button data loaded:" + buttonData.GetType());
            Debug.Log("Number of buttons: " + buttonData.Count);
            foreach (KeyValuePair<string, List<string>> pair in buttonData)
            {
                Debug.Log("Button name: " + pair.Key);
            }
        }
        else
        {
            Debug.LogError("Failed to load art.csv from the Assets folder.");
        }
    }
    */

    /*
    IEnumerator LoadCSVFromGoogleSheets()
    {

        using (UnityWebRequest www = UnityWebRequest.Get(googleSheetsURL))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string[] lines = www.downloadHandler.text.Split('\n');

                // Parse CSV data
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] columns = lines[i].Split(',');
                    if (columns.Length >= 4)
                    {
                        string buttonName = columns[0];
                        if (!buttonData.ContainsKey(buttonName))
                        {
                            buttonData[buttonName] = new List<string>();
                        }
                        buttonData[buttonName].AddRange(columns);


                    }
                }

                // Debug prints

                Debug.Log("Button data loaded:" + buttonData.GetType());
                Debug.Log("Number of buttons: " + buttonData.Count);
                foreach (KeyValuePair<string, List<string>> pair in buttonData)
                {
                    Debug.Log("Button name: " + pair.Key);
                }
            }
            else
            {
                Debug.LogError("Failed to load CSV from Google Sheets. Error: " + www.error);
            }
        }
    }
    */

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
