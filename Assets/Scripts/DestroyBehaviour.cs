using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBehaviour : MonoBehaviour
{
    [Header("REFERENCED")]
    [SerializeField] private GameObject fx;
    [SerializeField] private Transform particlesSpawn;

    public void Destroy()
    {
        if (fx!=null)
        {
            if (particlesSpawn != null)
            {
                Instantiate(fx, particlesSpawn.position, Quaternion.identity);
            }
            else
            {
                Instantiate(fx, transform.position, Quaternion.identity);
            }
        }
        Destroy(gameObject);
    }

    public void Disable()
    {
        if (fx != null)
        {
            if (particlesSpawn != null)
            {
                Instantiate(fx, particlesSpawn.position, Quaternion.identity);
            }
            else
            {
                Instantiate(fx, transform.position, Quaternion.identity);
            }
        }
        gameObject.SetActive(false);
    }
}
