using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingVehicle : MonoBehaviour
{
    private MovementBehaviour _mb;
    private DestroyBehaviour _destroy;

    [Header("PROPERTIES")]
    [SerializeField] private float maxDistanceMove;

    [Header("INFORMATION")]
    [SerializeField] private Vector3 dir;
    [SerializeField] private float distanceMoved;
    [SerializeField] private float startPosZ;
    [SerializeField] private bool activated;
    [SerializeField] private GameObject player;
    [SerializeField] private AudioSource carSound;

    private void Awake()
    {
        player = GameObject.Find("Player");
        carSound = GetComponent<AudioSource>();

        _mb = GetComponent<MovementBehaviour>();
        _destroy = GetComponent<DestroyBehaviour>();

        startPosZ = transform.position.z;
        dir = new Vector3 (0,0,-1);
        dir.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        distanceMoved = startPosZ - transform.position.z;
    }

    private void FixedUpdate()
    {
        if (activated)
        {
            _mb.MoveVelocity(dir);
            if (distanceMoved > maxDistanceMove)
            {
                _destroy.Destroy();
            }
        }
    }

    public void Activate()
    {
        carSound.Play();
        activated = true;
    }
}
