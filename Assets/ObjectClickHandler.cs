using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.IO;


public class ObjectClickHandler : MonoBehaviour
{
    public Text textHeaderPlaceholder;
    public Text textDescriptionPlaceholder;
    public Image imagePlaceholder;
    public RawImage rawImagePlaceholder;
    public Button clickButton;

    [SerializeField] RectTransform containerRect;

    public bool liveLink = true;


    public Button[] buttons;
    private RectTransform[] scollButtons;

    public string googleSheetsURL = "https://docs.google.com/spreadsheets/d/e/2PACX-1vQy3jQxng4KXs00w7bfcEm7-K3_eA7Ej1XnuIYgMLHp492xzRC59V0bWjNbfLbVYjvrWIHoz5eReIxH/pub?output=csv";

    private List<string> fourthColumnData = new List<string>();

    public List<string> FourthColumnData
    {
        get { return fourthColumnData; }
    }


    //public string googleSheetsURL;

    private Dictionary<string, List<string>> buttonData = new Dictionary<string, List<string>>();

    void Start()
    {
        
        StartCoroutine(LoadCSVMenuFromGoogleSheets());


        // Add listeners to the buttons
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => OnButtonClicked(button.name));
        }

        if (liveLink == true)
        {
            // Load CSV data from Google Sheets
            Debug.Log("CSV Sheets");
            StartCoroutine(LoadCSVFromGoogleSheets());

        } else
        {
            // Load CSV data from the art.csv file in the Assets folder
            Debug.Log("CSV Assets");
            LoadCSVFromAssets();
        }


        

    }

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

    void OnButtonClicked(string buttonName)
    {
        Debug.Log("Button clicked: " + buttonName);

        if (buttonData.ContainsKey(buttonName))
        {
            List<string> data = buttonData[buttonName];
            if (data.Count >= 5)
            {
                //Debug.LogError("Request Header : " + data[1]);
                textHeaderPlaceholder.text = data[1];
                //Debug.LogError("Request Description : " + data[4]);
                textDescriptionPlaceholder.text = data[4];
                

                StartCoroutine(LoadImage(data[3]));

                StartCoroutine(LoadRawImage(data[3]));

                if (data.Count >= 5)
                {
                    
                    string link = data[1];
                    clickButton.GetComponent<Button>().onClick.RemoveAllListeners();
                    clickButton.GetComponent<Button>().onClick.AddListener(() => OnPlaceholderButtonClicked(link));
                   
                }
            }
            else
            {
                Debug.LogError("Insufficient data for button: " + buttonName);
            }
        }
        else
        {
            Debug.LogError("Button data not found for button: " + buttonName);
        }
    }


    IEnumerator LoadRawImage(string imageURL)
    {
        using (WWW www = new WWW(imageURL))
        {
            yield return www;

            if (string.IsNullOrEmpty(www.error))
            {
                Texture2D texture = www.texture;
                rawImagePlaceholder.texture = texture;
            }
            else
            {
                Debug.LogError("Failed to load image from URL: " + imageURL + ", Error: " + www.error);
            }
        }
    }


    IEnumerator LoadImage(string imageURL)
    {
        
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageURL))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Success: " + www.result);

                Texture2D texture = DownloadHandlerTexture.GetContent(www);
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                imagePlaceholder.sprite = sprite;
            }
            else
            {
                Debug.Log("Failed to load image. Error: " + www.error);
            }
        }
        
    }


    IEnumerator LoadMenuImage(string imageURL, int i)
    {
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageURL))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Success: " + www.result);

                Texture2D texture = DownloadHandlerTexture.GetContent(www);
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

                Transform child = containerRect.GetChild(i);
                Image imageComponent = child.GetComponent<Image>();
                if (imageComponent != null)
                {
                    imageComponent.sprite = sprite;
                }
                else
                {
                    Debug.LogError("Child object does not have an Image component.");
                }
            }
            else
            {
                Debug.Log("Failed to load image. Error: " + www.error);
            }
        }
    }






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
                        fourthColumnData.Add(columns[3]); // Adding the fourth column data to the list
                    }
                }

                Debug.Log("CSV data loaded successfully.");
            }
            else
            {
                Debug.LogError("Failed to load CSV from Google Sheets. Error: " + www.error);
            }
        }

        int childCount = containerRect.childCount;
        Debug.Log("children" + childCount);

        /*
        for (int i = 0; i < childCount; i++)
        {
            Debug.Log("Data menu" + fourthColumnData[i]);
            StartCoroutine(LoadMenuImage(fourthColumnData[i], i));
        }
        */
    }


    public void OnPlaceholderButtonClicked(string link)
    {
        Debug.Log("Link: " + link);
        // Show a message, open a link, or perform any other action based on the provided link variable
    }
}

