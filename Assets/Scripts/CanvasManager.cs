using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

[Serializable]
public class CanvasInfo
{
    public string name;
    public GameObject canvasObj;
    public CanvasGroup canvasGroup;
}

public class CanvasManager : MonoBehaviour
{    
    [Header("PROPERTIES")]
    [SerializeField] private String firstCanvas;
    [SerializeField] private float appearingSpeedMultiplier;
    [SerializeField] private float disappearingSpeedMultiplier;

    [Header("REFERENCED")]
    [SerializeField] private List<CanvasInfo> canvasList;

    [Header("INFORMATION")]
    [SerializeField] private String activeCanvas;
    [SerializeField] private String previousCanvas;
    [SerializeField] private CanvasGroup fadingInCanvasGroup;
    [SerializeField] private CanvasGroup fadingOutCanvasGroup;
    [SerializeField] private bool fadingIn;
    [SerializeField] private bool fadingOut;
    [SerializeField] private float timerIn;
    [SerializeField] private float timerOut;

    private static CanvasManager _instance;
    public static CanvasManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<CanvasManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        for (int i = 0; i < canvasList.Count; i++)
        {
            canvasList[i].canvasGroup = canvasList[i].canvasObj.GetComponent<CanvasGroup>();

            if (canvasList[i].name ==  firstCanvas)
            {
                canvasList[i].canvasObj.SetActive(true);
                activeCanvas = canvasList[i].name;
            }
            else
            {
                canvasList[i].canvasObj.SetActive(false);
            }
        }
    }

    private void Update()
    {
        timerIn += Time.unscaledDeltaTime * appearingSpeedMultiplier;
        timerOut -= Time.unscaledDeltaTime * disappearingSpeedMultiplier;

        if (fadingIn)
        {
            if (timerIn >= 1)
            {
                timerIn = 1;
                fadingIn = false;
            } 
            else fadingInCanvasGroup.alpha = Mathf.Lerp(0, 1, timerIn);
        }
        if (fadingOut)
        {
            if (timerOut <= 0)
            {
                timerOut = 0;
                fadingOut = false;
                fadingOutCanvasGroup.gameObject.SetActive(false);
            }
            else fadingOutCanvasGroup.alpha = Mathf.Lerp(0, 1, timerOut);
        }
    }

    public void ActivateCanvas(string s)
    {
        for (int i = 0; i < canvasList.Count; i++)
        {
            if (canvasList[i].name ==  s)
            {
                FadeInCanvas(canvasList[i].canvasGroup);
                activeCanvas = canvasList[i].canvasObj.name;
                canvasList[i].canvasObj.SetActive(true);
            }
            else
            {
                if (canvasList[i].canvasObj.activeInHierarchy == true)
                {
                    FadeOutCanvas(canvasList[i].canvasGroup);
                    previousCanvas = canvasList[i].name;
                }
            }
        }
    }

    public void FadeInCanvas(CanvasGroup cg)
    {
        fadingInCanvasGroup = cg;
        fadingInCanvasGroup.alpha = 0;
        timerIn = 0;
        fadingIn = true;
    }

    public void FadeOutCanvas(CanvasGroup cg)
    {
        fadingOutCanvasGroup = cg;
        fadingOutCanvasGroup.alpha = 1;
        timerOut = 1;
        fadingOut = true;
    }

    public void ActivatePreviousCanvas()
    {
        ActivateCanvas(previousCanvas);
    }

    public string GetActiveCanvasName()
    {
        return activeCanvas;
    }

    public string GetPreviousCanvasName()
    {
        return previousCanvas;
    }
}
