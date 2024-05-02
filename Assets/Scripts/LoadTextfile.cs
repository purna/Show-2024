using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using System.IO;


public class LoadTextfile : MonoBehaviour
{
    // Start is called before the first frame update
   public TMP_Text textField;

    public TMP_Text log;

    //File path of the text file to load
    public string filePath;

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
}