using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerCollisionEvent : MonoBehaviour
{
    public UnityEvent<GameObject> Event;

    private void OnCollisionEnter(Collision collision)
    {
        Event.Invoke(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Event.Invoke(gameObject);
    }
}
