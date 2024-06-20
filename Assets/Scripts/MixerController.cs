using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixerController : MonoBehaviour
{
    private static MixerController _instance;
    public static MixerController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MixerController>();
            }
            return _instance;
        }
    }

    [Header("REFERENCED")]
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider fxSlider;

    [Header("INFORMATION")]
    [SerializeField] private float master;
    [SerializeField] private float music;
    [SerializeField] private float fx;

    public void SliderMasterVolume(float sliderValue)
    {
        master = sliderValue;
        mixer.SetFloat("MasterVolume", GetMixerVolume(master));
    }

    public void SliderMusicVolume(float sliderValue)
    {
        music = sliderValue;
        mixer.SetFloat("MusicVolume", GetMixerVolume(music));
    }

    public void SliderFxVolume(float sliderValue)
    {
        fx = sliderValue;
        mixer.SetFloat("FxVolume", GetMixerVolume(fx));
    }

    public float GetMasterVolume()
    {
        return master;
    }

    public float GetMusicVolume()
    {
        return music;
    }

    public float GetFxVolume()
    {
        return fx;
    }

    public void SetMasterVolume(float masterVolume)
    {

        master = masterVolume;
        masterSlider.value = master;
        mixer.SetFloat("MasterVolume", GetMixerVolume(master));
    }

    public void SetMusicVolume(float musicVolume)
    {
        music = musicVolume;
        musicSlider.value = music;
        mixer.SetFloat("MusicVolume", GetMixerVolume(music));
    }

    public void SetFxVolume(float fxVolume)
    {
        fx = fxVolume;
        fxSlider.value = fx;
        mixer.SetFloat("FxVolume", GetMixerVolume(fx));
    }

    public float GetMixerVolume(float num)
    {
        if (num != 0)
        {
            return Mathf.Log10(num) * 20;
        }
        else
        {
            return -80;
        }
    }
}
