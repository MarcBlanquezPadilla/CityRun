using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GroundChecker : MonoBehaviour
{
    public UnityEvent<bool> SetGrounded;
    public UnityEvent TriggerEnter;
    public UnityEvent TriggerExit;

    [Header("INFORMATION")]
    [SerializeField] private bool firstTime;

    private void Awake()
    {
        SetGrounded.Invoke(true);
        firstTime = true;
    }

    private void OnTriggerStay(Collider other)
    {
        SetGrounded.Invoke(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (firstTime)
        {
            firstTime = false;
        }
        else
        {
            TriggerEnter.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        SetGrounded.Invoke(false);
        TriggerExit.Invoke();
    }
}
