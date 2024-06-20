using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingLampBehaviour : MonoBehaviour
{
    [SerializeField] private List<AudioSource> audios;

    public void PlayLight1()
    {
        audios[0].Play();
    }
    public void PlayLight2()
    {
        audios[1].Play();
    }
    public void PlayKnock()
    {
        audios[2].Play();
    }

}
