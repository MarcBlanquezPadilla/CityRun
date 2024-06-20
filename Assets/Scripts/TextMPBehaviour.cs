using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextMPBehaviour : MonoBehaviour
{
    private TextMeshProUGUI text;

    [Header("PROPERTIES")]
    [SerializeField] private Color changeColor;


    [Header("INFORMATION")]
    [SerializeField] private Color baseColor;
    [SerializeField] private string baseText;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        baseText = text.text;
        baseColor = text.color;
    }

    public void UpdateText(string s)
    {
        text.text = s;
    }

    public void UpdateText(int s)
    {
        text.text = s.ToString();
    }

    public void ChangeColor()
    {
        text.color = changeColor;
    }

    public void BaseColor()
    {
        text.color = baseColor;
    }
}
