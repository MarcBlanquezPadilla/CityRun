using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{
    [Header("PROPERTIES")]
    [SerializeField] private float defaultFrontSpeed;
    [SerializeField] private float defaultLateralSpeed;
    [SerializeField] private float defaultJumpSpeed;

    [Header("INFORMATION")]
    [SerializeField] private float alpha;
    [SerializeField] private float currentFrontSpeed;
    [SerializeField] private float currentLateralSpeed;
    [SerializeField] private float currentJumpSpeed;


    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponentInParent<Rigidbody>();
        SetDefaultVelocity();
        SetDefaultJumpVelocity();
    }

    public void MoveVelocity(Vector3 direction)
    {
        direction = new Vector3 (direction.x * currentLateralSpeed * Time.fixedDeltaTime, _rb.velocity.y, direction.z * currentFrontSpeed * Time.fixedDeltaTime);
        _rb.velocity = direction;
    }

    public void MoveVelocityToView(float horizontalInput, float verticalInput)
    {
        Vector3 direction = transform.forward * verticalInput + transform.right * horizontalInput;
        _rb.velocity = new Vector3 (direction.x * currentLateralSpeed * Time.fixedDeltaTime, _rb.velocity.y, direction.z * currentFrontSpeed * Time.fixedDeltaTime);
    }

    public void Jump()
    {
        _rb.velocity = new Vector3(_rb.velocity.x, currentJumpSpeed * Time.fixedDeltaTime, _rb.velocity.z);
    }

    public void RotateAngle(float degrees, char axis)
    {
        if (axis == 'x')
        {
            transform.Rotate(degrees, 0, 0);
        }
        else if (axis == 'y')
        {
            transform.Rotate(0, degrees, 0);
        }
        else if (axis == 'z')
        {
            transform.Rotate(0, 0, degrees);
        }
    }

    public void SetVelocity(float s)
    {
        currentFrontSpeed = s;
    }

    public void SetDefaultVelocity()
    {
        currentFrontSpeed = defaultFrontSpeed;
        currentLateralSpeed = defaultLateralSpeed;
    }

    public void SetDefaultJumpVelocity()
    {
        currentJumpSpeed = defaultJumpSpeed;
    }

    public float GetFrontVelocity()
    {
        return defaultFrontSpeed;
    }

    public void ChangeCurrentLateralSpeed(float s)
    {
        currentLateralSpeed = s;
    }

    public void SetDefaultLateralSpeed()
    {
        currentLateralSpeed = defaultLateralSpeed;
    }

    public void ChangeDefaultVelocity(float velocity)
    {
        defaultFrontSpeed = velocity;
        currentFrontSpeed = defaultFrontSpeed;
    }
    public void ChangeDefaultLateralVelocity(float velocity)
    {
        defaultLateralSpeed = velocity;
        currentLateralSpeed = defaultFrontSpeed;
    }
}