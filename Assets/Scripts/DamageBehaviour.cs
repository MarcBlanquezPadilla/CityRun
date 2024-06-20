using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBehaviour : MonoBehaviour
{
    [Header("PROPERTIES")]
    [SerializeField] private int damage;

    private void OnCollisionEnter(Collision collision)
    {
        Hurt(collision.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Hurt(other.gameObject);
    }

    private void OnParticleCollision(GameObject other)
    {
        Hurt(other);
    }

    public void Hurt(GameObject g)
    {
        if (g.TryGetComponent<HealthBehaviour>(out HealthBehaviour _health))
        {
            _health.Hurt(damage);
        }
    }
}
