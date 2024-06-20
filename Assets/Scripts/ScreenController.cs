using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenController : MonoBehaviour
{
    private static ScreenController _instance;
    public static ScreenController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ScreenController>();
            }
            return _instance;
        }
    }

    [Header("REFERENCED")]
    [SerializeField] private Toggle toggleFullScreen;

    [Header("INFORMATION")]
    [SerializeField] private bool fullScreen;

    public void ChangeFullScreen(bool ToggleBool)
    {
        fullScreen = ToggleBool;
        Screen.fullScreen = fullScreen;
    }

    public bool GetFullScreen()
    {
        return fullScreen;
    }

    public void SetFullScreen(bool full)
    {
        ChangeFullScreen(full);
        toggleFullScreen.isOn = fullScreen;
    }
}
