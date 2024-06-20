using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<MovementBehaviour>(out MovementBehaviour _move))
        {
            _move.ChangeDefaultLateralVelocity(0);
            _move.ChangeDefaultVelocity(0);
        }
    }
}
