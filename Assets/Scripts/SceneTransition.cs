using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    private ScenesManager _scene;

    [Header("REFERENCED")]
    [SerializeField] private Animator _anim;

    [Header("INFORMATION")]
    [SerializeField] private Canvas canvas;

    private void Awake()
    {
        _scene = GetComponentInParent<ScenesManager>();
        canvas = GetComponentInChildren<Canvas>();
        Invoke("TriggerStart", 0.5f);
    }

    private void TriggerStart()
    {
        _anim.SetTrigger("Start");
    }

    public void StartTransitionEvent()
    {
        canvas.enabled = false;
    }

    public void PlayEndTransition()
    {
        canvas.enabled = true;
        _anim.SetTrigger("End");
    }

    public void EndTransitionEvent()
    {
        Time.timeScale = 1;
        _scene.LoadNextScene();
    }
}
