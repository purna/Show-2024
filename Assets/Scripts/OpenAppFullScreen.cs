using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using TMPro;
using Debug = UnityEngine.Debug;

public class OpenAppFullScreen : MonoBehaviour
{
    public string batchFilePath;
    public string parameter;

    public TMP_Text log;

    public void OpenBatchwithParamter()
    {


#if UNITY_EDITOR
        string appPath = System.IO.Path.Combine(Application.dataPath, batchFilePath);
#else
        string appPath = System.IO.Path.Combine(Application.dataPath, "..", batchFilePath);
#endif
        //


        appPath = System.IO.Path.GetFullPath(appPath);
        //appPath = appPath.Replace("\\", "/");


        Debug.Log(appPath);

        log.text = appPath;

        //Start the batch process
        Process process = new Process();
        process.StartInfo.FileName = appPath;
        process.StartInfo.Arguments = parameter;
        process.Start();


        //process.StartInfo.Arguments ="-fullscreen";
        //process.StartInfo.Arguments = $"\"{appPath}\"  {parameter}";


        /*
         
        //Specify the path to the batch file
        process.StartInfo.FileName = "cmd.exe"; // Run prompt command
        process.StartInfo.Arguments = $"/c \"{batchFilePath}\" {parameter}"; //Pass the batchFilePath & parameter

        */
        //Hide the command prompt window

        //process.StartInfo.CreateNoWindow = true;
        //process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

        //Start the process


    }

    public void OpenBatchwithParamterDebug(string index)
    {
        Debug.LogWarning("reference called " + index);
    }
}
