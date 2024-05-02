using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class BringWindowtoFront : MonoBehaviour
{

    public string processName;


    // Start is called before the first frame update
    void Start()
    {
       //Get the current Process
        Process currentProcess= Process.GetCurrentProcess();

        //Bring the mani window of the current process to the front
        SetForegroundWindow(currentProcess.MainWindowHandle);
        
        //BringWindowToFront();
    }

    public void BringWindowToFront()
    {
        Process[] processes = Process.GetProcessesByName(processName);

        if (processes.Length > 0)
        {
            IntPtr mainwindowHandle = processes[0].MainWindowHandle;
            SetForegroundWindow(mainwindowHandle);

        }
        else
        {
            Debug.LogWarning("Provcess not found");



        }
    }


    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern bool SetForegroundWindow(IntPtr hWnd);
}
