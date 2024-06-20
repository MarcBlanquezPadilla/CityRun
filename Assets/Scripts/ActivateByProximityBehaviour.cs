using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActivateByProximityBehaviour : MonoBehaviour
{
    [Header("REFERENCED")]
    [SerializeField] private string objName;

    [Header("PROPERTIES")]
    [SerializeField] private float distanceToActivate;
    [SerializeField] private bool xAxis;
    [SerializeField] private bool yAxis;
    [SerializeField] private bool zAxis;
    [SerializeField] private bool allAxis;

    [Header("INFORMATION")]
    [SerializeField] private Transform Obj;
    [SerializeField] private float currentDistance;
    [SerializeField] private bool activated;

    public UnityEvent Activate;

    private void Awake()
    {
        Obj = GameObject.Find(objName).transform;
        activated = false;
        currentDistance = 0;
    }

    private void Update()
    {
        if (xAxis)
        {
            currentDistance = Mathf.Abs(transform.position.x - Obj.position.x);
        }

        else if (yAxis)
        {
            currentDistance = Mathf.Abs(transform.position.y - Obj.position.y);
        }

        else if (zAxis)
        {
            currentDistance = Mathf.Abs(transform.position.z - Obj.position.z);
        }
        
        else if (allAxis)
        {
            currentDistance = Vector3.Distance(transform.position, Obj.position);         
        }

        if (currentDistance < distanceToActivate && !activated)
        {
            activated = true;
            Activate.Invoke();
        }
    }

    public float GetCurrentDistance()
    {
        return currentDistance;
    }
}
