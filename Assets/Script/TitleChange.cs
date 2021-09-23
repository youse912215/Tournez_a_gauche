using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class TitleChange : MonoBehaviour
{
    public string txt;

    // Start is called before the first frame update
    [DllImport("user32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Unicode)]
    private static extern IntPtr FindWindow(string className, string windowName);

    [DllImport("user32.dll", EntryPoint = "SetWindowText", CharSet = CharSet.Unicode)]
    private static extern bool SetWindowText(IntPtr hwnd, string txt);

    private void Start()
    {
        var hwnd = FindWindow(null, Application.productName);
        SetWindowText(hwnd, txt);
    }
}