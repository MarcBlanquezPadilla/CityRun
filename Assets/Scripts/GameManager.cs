using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private PreferencesController _pc;

    public static bool started;
    public static bool Paused;
    public static bool ended;

    [Header("REFERENCED")]
    [SerializeField] private GameObject preferencesController;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject endPoint;
    [SerializeField] private GameObject endParticles1;
    [SerializeField] private GameObject endParticles2;

    [Header("INFORMATION")]
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private float progress;
    [SerializeField] private float mapDistance;

    [Header("EVENTS")]
    public UnityEvent<int> UpdateProgress;
    public UnityEvent OneStarWin;
    public UnityEvent TwoStarWin;
    public UnityEvent ThreeStarWin;

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                DontDestroyOnLoad(_instance);
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _pc = preferencesController.GetComponent<PreferencesController>();
        startPosition = player.transform.position;
        mapDistance = Mathf.Abs(startPosition.z - endPoint.transform.position.z);
        progress = 0;
        started = false;
        ended = false;
        SoundManager.Instance.StopAllAudios();
        Pause();
    }

    private void Update()
    {
        if (!started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SoundManager.Instance.PlayMusic("InGameMusic");
                SoundManager.Instance.PlayAudio("Run");
                started = true;
                Resume();
            }
        }
        else
        {
            progress = (Mathf.Abs(player.transform.position.z - startPosition.z) / mapDistance) * 100;
            UpdateProgress.Invoke((int)progress);
        
            if (Input.GetKeyDown(KeyCode.Escape) && !ended)
            {
                if (CanvasManager.Instance.GetActiveCanvasName() == "Hud")
                {
                    CanvasManager.Instance.ActivateCanvas("Pause");
                    Pause();
                }
                else if(CanvasManager.Instance.GetActiveCanvasName() == "Pause")
                {
                    CanvasManager.Instance.ActivateCanvas("Hud");
                    Resume();
                }
                else if (CanvasManager.Instance.GetActiveCanvasName() == "Options")
                {
                    _pc.SavePrefs();
                    CanvasManager.Instance.ActivatePreviousCanvas();
                }
                else if (CanvasManager.Instance.GetActiveCanvasName() == "Instructions")
                {
                    CanvasManager.Instance.ActivatePreviousCanvas();
                    if (CanvasManager.Instance.GetActiveCanvasName() == "Hud")
                    {
                        Resume();
                    }
                }
                else 
                {
                    CanvasManager.Instance.ActivatePreviousCanvas();
                }

            }
        }
        
    }

    public void Pause()
    {
        Time.timeScale = 0;
        Paused = true;
        SoundManager.Instance.PauseAllAudios();
    }
    public void Resume()
    {
        Time.timeScale = 1;
        Paused = false;
        SoundManager.Instance.ResumeAllAudios();
    }

    public void End(bool playerAlive)
    {
        ended = true;
        SoundManager.Instance.StopAllAudios();
        if (!playerAlive || ScoreManager.Instance.GetStars() == 0)
        {
            CanvasManager.Instance.ActivateCanvas("Lose");
            SoundManager.Instance.PlayAudio("Lose");
        }
        else
        {
            CanvasManager.Instance.ActivateCanvas("Win");
            if (ScoreManager.Instance.GetStars() >= 1)
            {
                OneStarWin.Invoke();
            }
            if (ScoreManager.Instance.GetStars() >= 2)
            {
                TwoStarWin.Invoke();
            }
            if (ScoreManager.Instance.GetStars() == 3)
            {
                ThreeStarWin.Invoke();
            }
            SoundManager.Instance.PlayAudio("Win");
            endParticles1.SetActive(true);
            endParticles2.SetActive(true);
        }
    }
}
