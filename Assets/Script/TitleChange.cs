using System;
using System.Runtime.InteropServices;
using UnityEngine;
using static Call.ConstantValue;

public class TitleChange : MonoBehaviour
{
    private void Start()
    {
        var hwnd = GetWindow(null, Application.productName); //�E�B���h�E�擾
        SetTitle(hwnd, title); //�^�C�g���Z�b�g
    }

    //DLL�֐��̌Ăяo��
    [DllImport("user32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Unicode)]
    private static extern IntPtr GetWindow(string className, string windowName);

    [DllImport("user32.dll", EntryPoint = "SetWindowText", CharSet = CharSet.Unicode)]
    private static extern bool SetTitle(IntPtr hwnd, string txt);
}