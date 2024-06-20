using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyBehaviour : MonoBehaviour
{
    private ParticleSystem _particle;
    private DestroyBehaviour _destroy;

    [Header("INFORMATION")]
    [SerializeField] private float timer;

    private void Awake()
    {
        _particle = GetComponent<ParticleSystem>();
        _destroy = GetComponent<DestroyBehaviour>();
        timer = _particle.main.duration;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            _destroy.Destroy();
        }
    }
}
