using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;
using System.IO;

public class butInfo : MonoBehaviour
{
    public Text textPlaceholder;
    public Image imagePlaceholder;
    public Button button;
    public string textURL;
    public string imageURL;
    public string buttonURL;

    public string csvFileName;

    private string[][] data; // 2D array to hold CSV data


    void Start()
    {
        // Add listener to the button
        button.onClick.AddListener(OpenButtonLink);

        // Load CSV data
        //LoadCSV();
    }

    public void OnObjectClicked()
    {
        StartCoroutine(LoadTextAndImage());

        // Example: Accessing CSV data and assigning to placeholders
        int rowIndex = Random.Range(1, data.Length); // Skip header row (if any)
        textPlaceholder.text = data[rowIndex][0]; // Assuming the first column contains text data
        // Assuming the second column contains image file paths
        StartCoroutine(LoadImage(data[rowIndex][1]));

    }

    IEnumerator LoadTextAndImage()
    {
        // Load text
        using (UnityWebRequest www = UnityWebRequest.Get(textURL))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                textPlaceholder.text = www.downloadHandler.text;
            }
            else
            {
                Debug.Log(www.error);
            }
        }

        // Load image
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageURL))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(www);
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                imagePlaceholder.sprite = sprite;
            }
            else
            {
                Debug.Log(www.error);
            }
        }
    }

    void OpenButtonLink()
    {
        Application.OpenURL(buttonURL);

        // Example: Open a hardcoded link
        //Application.OpenURL("https://www.example.com");
    }



    void LoadCSV()
    {
        // Load CSV file from Resources folder
        TextAsset csvFile = Resources.Load<TextAsset>(csvFileName);

        if (csvFile != null)
        {
            string[] lines = csvFile.text.Split('\n');
            data = new string[lines.Length][];

            // Parse CSV data
            for (int i = 0; i < lines.Length; i++)
            {
                data[i] = lines[i].Split(',');
            }
        }
        else
        {
            Debug.LogError("CSV file not found: " + csvFileName);
        }
    }

    

    IEnumerator LoadImage(string imageURL)
    {
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageURL))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(www);
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                imagePlaceholder.sprite = sprite;
            }
            else
            {
                Debug.Log(www.error);
            }
        }
    }

   
}

