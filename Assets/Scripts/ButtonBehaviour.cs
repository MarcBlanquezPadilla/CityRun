using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    public void OnHover()
    {
        SoundManager.Instance.PlayAudio("ButtonHover");
    }

    public void OnClick()
    {
        SoundManager.Instance.PlayAudio("ButtonClick");
    }
}
