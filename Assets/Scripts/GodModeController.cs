using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GodModeController : MonoBehaviour
{
    private static GodModeController _instance;
    public static GodModeController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GodModeController>();
            }
            return _instance;
        }
    }

    [Header("REFERENCED")]
    [SerializeField] private Toggle toggleGodMode;
    [SerializeField] private GameObject godModeObj;

    [Header("INFORMATION")]
    [SerializeField] private bool godMode;

    public void ChangeGodMode(bool ToggleBool)
    {
        godMode = ToggleBool;
        godModeObj.SetActive(godMode);
    }

    public bool GetGodMode()
    {
        return godMode;
    }

    public void SetGodMode(bool god)
    {
        ChangeGodMode(god);
        toggleGodMode.isOn = godMode;
    }
}
