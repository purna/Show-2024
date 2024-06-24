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

    public Text textButtonIndex;

    public int reference;

    public GameObject fadein;

    [SerializeField] GameObject containerRect;

    public OpenAppFullScreen openAppFullScreen;

    public bool liveLink = true;


    bool hasExecuted = false; // Flag to track if the action has been executed
    int currentindex = 0;
    int lastCurrentIndex = -1; // Store the last currentindex



    [SerializeField] Button[] buttons;
    private RectTransform[] scrollButtons;


    private List<string> fourthColumnData = new List<string>();

    public List<string> FourthColumnData
    {
        get { return fourthColumnData; }
    }

    private List<string[]> rowData = new List<string[]>();

    private Dictionary<string, List<string>> buttonData = new Dictionary<string, List<string>>();

    GoogleSheetDataLoader dataLoader;

    LoadTextfile dataLoaderLocal;


    void Start()
    {
        if (liveLink == true)
        {
            // Load CSV data from Google Sheets
            Debug.Log("Google Sheets");
            dataLoader = GetComponent<GoogleSheetDataLoader>();
            StartCoroutine(dataLoader.StartLoading());

        }
        else
        {
            // Load CSV data from the art.csv file in the Assets folder
            Debug.Log("CSV Assets");
            dataLoaderLocal = GetComponent<LoadTextfile>();
            StartCoroutine(dataLoaderLocal.StartLoading());
        }



        // Set buttons to

        GameObject container = containerRect; 
        int childCount = container.transform.childCount;

        Button[] buttons = new Button[childCount];

        for (int i = 0; i < childCount; i++)
        {
            Transform child = container.transform.GetChild(i);
            Button buttonComponent = child.GetComponent<Button>();

            if (buttonComponent != null)
            {
                buttons[i] = buttonComponent;
                Debug.LogWarning("Child added " + i + " or array.");
            }
            else
            {
                Debug.LogWarning("Child object at index " + i + " does not have a Button component.");
            }
        }


        // Add listeners to the buttons
        foreach (Button button in buttons)
        {

           // button.onClick.AddListener(() => OnButtonClicked(button.name));
        }

       

        

    }

    private void Update()
    {
        GameObject container = containerRect;
        int childCount = container.transform.childCount;
        //Button[] buttons = new Button[childCount];

        if (textHeaderPlaceholder.text != "LOADING")
        {
            fadein.SetActive(true); // Set GameObject active
        }


        GameObject[] buttons = new GameObject[childCount];

        for (int i = 0; i < childCount; i++)
        {
            Transform child = container.transform.GetChild(i);

            // Store the child GameObject directly
            buttons[i] = child.gameObject;
      
            Button buttonComponent = child.GetComponent<Button>();


            // Access the QuadCell script attached to the child
            QuadCell quadCellScript = child.GetComponent<QuadCell>();

            //Debug.Log("Children in collection =  "+ childCount);


            if (buttonComponent != null)
            {
                // buttons[i] = buttonComponent;

                // Get the x and y positions of the child
                float xPos = child.position.x;
                float yPos = child.position.y;

                // Check if x and y are zero
                if (xPos <= 1f && yPos <= 1f && xPos >= -1f && yPos >= -1f)
                {
                    // Set reference to 1 if both x and y are zero
                    quadCellScript.reference = 1;

                    reference = quadCellScript.index;

                    currentindex = quadCellScript.index;

                     //Debug.LogWarning("Request hasExecuted : " + hasExecuted);
                     //Debug.LogWarning("Request currentindex : " + currentindex);
                     //Debug.LogWarning("Request lastCurrentIndex : " + lastCurrentIndex);

                    if (hasExecuted == false && currentindex == lastCurrentIndex)
                        {
                            hasExecuted = true; // Set flag to true to indicate that the action has been executed

                            Debug.LogWarning("Request reference : " + reference);
                            Debug.LogWarning("Request hasExecuted : " + hasExecuted);


                            callData(reference);
                        }

                    //Debug.Log("Child " + i + " - X: " + xPos + ", Y: " + yPos);
                }
                else
                {
                    // Set reference to 0 if either x or y are non-zero
                    quadCellScript.reference = 0;

                }
            }
        }

        // Reset hasExecuted if currentindex is different from the last currentindex
        if (currentindex != lastCurrentIndex)
        {
            hasExecuted = false;
            lastCurrentIndex = currentindex; // Update lastCurrentIndex
            //Debug.LogWarning("Request hasExecuted : " + hasExecuted);
            //Debug.LogWarning("Request currentindex : " + currentindex);
            //Debug.LogWarning("Request lastCurrentIndex : " + lastCurrentIndex);
        }
    }


    void callData(int reference)
    {

        LoadData(reference);

    }
    


    void LoadData(int rowIndex)
    {

        // Retrieve row data by row index
        //int rowIndex = 4; // 0-based index for the 5th row
        string[] data;

        if (liveLink == true)
        {
            // Load CSV data from Google Sheets

           data = dataLoader.GetRowData(rowIndex);

        }
        else
        {
            // Load CSV data from the art.csv file in the Assets folder
            data = dataLoaderLocal.GetRowData(rowIndex);
           
        }

        if (data != null)
        {

            //Debug.LogError("Request Header : " + data[1]);
            textHeaderPlaceholder.text = data[1];
            //Debug.LogError("Request Description : " + data[4]);
            textDescriptionPlaceholder.text = data[4];

            //Refence number
            textButtonIndex.text = data[1];


            if (liveLink == true)
            {
                // Load CSV data from Google Sheets
                Debug.Log("Google Sheets");
                //StartCoroutine(LoadImage(data[3]));

                //StartCoroutine(LoadRawImage(data[3]));

            }
            else
            {
                // Load CSV data from the art.csv file in the Assets folder
                Debug.Log("CSV Assets");
                //StartCoroutine(LoadImageLocal(data[3]));

                //StartCoroutine(LoadRawImageLocal(data[3]));
            }


            

            string link = data[0];
            clickButton.GetComponent<Button>().onClick.RemoveAllListeners();
            clickButton.GetComponent<Button>().onClick.AddListener(() => OnPlaceholderButtonClicked(link));

        }              
        
    }


    void LoadButtonData(string buttonName)
    {
       
        if (buttonData.ContainsKey(buttonName))
        {
            List<string> data = buttonData[buttonName];
            if (data.Count >= 5)
            {
                //Debug.LogError("Request Header : " + data[1]);
                textHeaderPlaceholder.text = data[1];
                //Debug.LogError("Request Description : " + data[4]);
                textDescriptionPlaceholder.text = data[4];

                //Button Refernce
                textButtonIndex.text = data[1];

                if (liveLink == true)
                {
                    // Load CSV data from Google Sheets
                    Debug.Log("Google Sheets");
                    //StartCoroutine(LoadImage(data[3]));

                    //StartCoroutine(LoadRawImage(data[3]));

                }
                else
                {
                    // Load CSV data from the art.csv file in the Assets folder
                    Debug.Log("CSV Assets");
                    //StartCoroutine(LoadImageLocal(data[3]));

                    //StartCoroutine(LoadRawImageLocal(data[3]));
                }

                

                if (data.Count >= 5)
                {

                    string link = data[0];
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


    void OnButtonClicked(string buttonName)
    {
        Debug.Log("Button clicked: " + buttonName);

        LoadButtonData(buttonName);
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

    IEnumerator LoadRawImageLocal(string filePath)
    {
        // Check if the file exists at the given path
        if (File.Exists(filePath))
        {
            // Read the file data
            byte[] fileData = File.ReadAllBytes(filePath);

            // Create a new Texture2D and load the image data
            Texture2D texture = new Texture2D(2, 2);
            if (texture.LoadImage(fileData))
            {
                Debug.Log("Success: Loaded image from " + filePath);

                // Set the texture to the rawImagePlaceholder
                rawImagePlaceholder.texture = texture;
            }
            else
            {
                Debug.LogError("Failed to load image from file data.");
            }
        }
        else
        {
            Debug.LogError("File not found: " + filePath);
        }

        yield return null;
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

    IEnumerator LoadImageLocal(string filePath)
    {
        // Check if the file exists at the given path
        if (File.Exists(filePath))
        {
            // Read the file data
            byte[] fileData = File.ReadAllBytes(filePath);

            // Create a new Texture2D and load the image data
            Texture2D texture = new Texture2D(2, 2);
            if (texture.LoadImage(fileData))
            {
                Debug.Log("Success: Loaded image from " + filePath);

                // Create a sprite from the loaded texture
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

                // Set the sprite to the imagePlaceholder
                imagePlaceholder.sprite = sprite;
            }
            else
            {
                Debug.LogError("Failed to load image from file data.");
            }
        }
        else
        {
            Debug.LogError("File not found: " + filePath);
        }

        yield return null;
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


                //if containerRect is a RectTransform
                //Transform child = containerRect.GetChild(i);

                GameObject container = containerRect; // Assuming containerRect is the GameObject
                Transform child = container.transform.GetChild(i);


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

    IEnumerator LoadMenuImageLocal(string filePath, int i)
    {
        // Check if the file exists at the given path
        if (File.Exists(filePath))
        {
            // Read the file data
            byte[] fileData = File.ReadAllBytes(filePath);

            // Create a new Texture2D and load the image data
            Texture2D texture = new Texture2D(2, 2);
            if (texture.LoadImage(fileData))
            {
                Debug.Log("Success: Loaded image from " + filePath);

                // Create a sprite from the loaded texture
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

                // Assuming containerRect is the GameObject
                Transform child = containerRect.transform.GetChild(i);

                // Get the Image component of the child object
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
                Debug.LogError("Failed to load image from file data.");
            }
        }
        else
        {
            Debug.LogError("File not found: " + filePath);
        }

        yield return null;
    }



    public void OnPlaceholderButtonClicked(string link)
    {
        Debug.Log("Link: " + link);
        // Show a message, open a link, or perform any other action based on the provided link variable
        openAppFullScreen.OpenBatchwithParamterDebug(link); // Pass the link string or any parameter here

    }
}

