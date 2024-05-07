using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using System.IO;

public class selectText : MonoBehaviour
{
    public string selectTxt;
    public TMP_Text textField;


    public void selectTexter(string selectTxt)
    {

        textField.text = selectTxt;

    }
    
}
