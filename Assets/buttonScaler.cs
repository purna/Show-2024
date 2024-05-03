using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonScaler : MonoBehaviour
{
    [SerializeField] RectTransform containerRect;
    [SerializeField] float maxScale = 3f;
    [SerializeField] float middleScale = 2f;
    [SerializeField] float sideScale = 1.5f;

    private RectTransform[] buttons;

    void Start()
    {
      
        SetButtonList();
        
    }

    void SetButtonList()
    {
        int childCount = containerRect.childCount;
        buttons = new RectTransform[childCount];
        int halfCount = buttons.Length / 2;


        for (int i = 0; i < childCount; i++)
        {
            buttons[i] = containerRect.GetChild(i).GetComponent<RectTransform>();

            // Add click event to each button
            Button button = buttons[i].GetComponent<Button>();
            if (button != null)
            {
                int index = i; // Capture the current index in the loop
                button.onClick.AddListener(() => OnButtonClick(index));
            }
        }

        SetButtonSizes(halfCount);
    }

    void SetButtonSizes(int clickedIndex)
    {
        // Calculate the adjusted indices for scaling
        int childCount = containerRect.childCount;


            int prev2Index = (clickedIndex - 2 + buttons.Length) % buttons.Length;
            int prev1Index = (clickedIndex - 1 + buttons.Length) % buttons.Length;
            int next1Index = (clickedIndex + 1) % buttons.Length;
            int next2Index = (clickedIndex + 2) % buttons.Length;

        if (childCount % 2 == 0) // Even number of buttons
        {
            // if even number of buttons
        }


            // Display which button was clicked
            Debug.Log("Clicked Button: " + clickedIndex);

        // Set the scale for each button
        for (int i = 0; i < buttons.Length; i++)
        {
            float scaleFactor = 1f; // Default scale factor

            if (i == prev2Index)
            {
                scaleFactor = sideScale;
                // Apply scale
                buttons[i].localScale = new Vector3(scaleFactor / 2f, scaleFactor, 1f);
            }
            else if (i == prev1Index)
            {
                scaleFactor = middleScale;
                // Apply scale
                buttons[i].localScale = new Vector3(scaleFactor / 1.5f, scaleFactor, 1f);
            }
            else if (i == clickedIndex)
            {
                scaleFactor = maxScale;
                // Apply scale
                buttons[i].localScale = new Vector3(scaleFactor , scaleFactor, 1f);
            }
                
            else if (i == next1Index)
            {
                scaleFactor = middleScale;
                // Apply scale
                buttons[i].localScale = new Vector3(scaleFactor / 1.2f, scaleFactor, 1f);
            }
                
            else if (i == next2Index)
            {
                scaleFactor = sideScale;
                // Apply scale
                buttons[i].localScale = new Vector3(scaleFactor / 2f, scaleFactor, 1f);
            }
            else
            {
                scaleFactor = sideScale;
                // Apply scale
                buttons[i].localScale = new Vector3(scaleFactor / 3f, scaleFactor / 1.5f, 1f);
            }
               

           
        }
    }

    public void OnButtonClick(int clickedIndex)
    {
        SetButtonSizes(clickedIndex);
    }
}
