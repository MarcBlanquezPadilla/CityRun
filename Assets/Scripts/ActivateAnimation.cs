using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAnimation : MonoBehaviour
{
    private Animator _anim;
    private ActivateByProximityBehaviour _act;

    [Header("PROPERTIES")]
    [SerializeField] string trigger;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _act = GetComponent<ActivateByProximityBehaviour>();
    }

    public void Activate()
    {
        _anim.SetTrigger(trigger);
    }

}
