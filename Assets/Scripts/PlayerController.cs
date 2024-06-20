using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Timeline;

public class PlayerController : MonoBehaviour
{
    public enum Anims
    {
        RUN_FORWARD,
        RUN_FORWARD_RIGHT,
        RUN_FORWARD_LEFT,
        JUMP,
        LAND,
        SLIDE,
        END
    };

    private MovementBehaviour _mb;
    private Rigidbody _rb;
    private CapsuleCollider _collider;
    private Animator _anim;
    private DestroyBehaviour _destroy;
    private StaminaBehaviour _stamina;

    [Header("PROPERTIES")]
    [SerializeField] private float rollingColliderHeigh;
    [SerializeField] private Vector3 rollingColliderCenter;
    [SerializeField] private int staminaToJump;
    [SerializeField] private int staminaToRoll;

    [Header("INFORMATION")]
    [SerializeField] private float horizontalInput;
    [SerializeField] private float verticalInput;
    [SerializeField] private bool grounded;
    [SerializeField] private bool jumping;
    [SerializeField] private bool rolling;
    [SerializeField] private float defaultColliderHeight;
    [SerializeField] private Vector3 defaultColliderCenter;

    private void Awake()
    {
        _mb = GetComponent<MovementBehaviour>();
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();
        _anim = GetComponent<Animator>();
        _destroy = GetComponent<DestroyBehaviour>();
        _stamina = GetComponent<StaminaBehaviour>();

        defaultColliderCenter = _collider.center;
        defaultColliderHeight = _collider.height;
        rollingColliderCenter = new Vector3(_collider.center.x, rollingColliderCenter.y, _collider.center.z);
    }

    private void OnEnable()
    {
        verticalInput = 1;
        jumping = false;
        rolling = false;
    }

    private void Update()
    {
        if (GameManager.ended)
        {
            SetAnimation(Anims.END);
        }
        else
        {
            if (grounded && !rolling)
            {
                if (horizontalInput<0)
                {
                    SetAnimation(Anims.RUN_FORWARD_LEFT);
                }
                else if (horizontalInput>0)
                {
                    SetAnimation(Anims.RUN_FORWARD_RIGHT);
                }
                else
                {
                    SetAnimation(Anims.RUN_FORWARD);
                }
            }
            else if (grounded && rolling)
            {
                SetAnimation(Anims.SLIDE);
            }
            else if (_rb.velocity.y != 0)
            {
                SetAnimation(Anims.JUMP);
            }
        }

        //INPUT
        MyInput();
    }

    private void FixedUpdate()
    {
        _mb.MoveVelocityToView(horizontalInput, verticalInput);
    }

    private void MyInput()
    {
        if (GameManager.started && !GameManager.Paused && !GameManager.ended)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");

            if (Input.GetKeyDown(KeyCode.Space) && grounded && _stamina.HaveNeededStamina(staminaToJump) && rolling == false && jumping == false)
            {
                SoundManager.Instance.StopAudio("Run");
                SoundManager.Instance.PlayAudio("Jump");
                _mb.Jump();
                _stamina.WasteStamina(staminaToJump);
                jumping = true;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) && grounded && _stamina.HaveNeededStamina(staminaToRoll) && jumping == false && rolling == false)
            {
                _stamina.WasteStamina(staminaToRoll);
                rolling = true;
            }
        }
    }

    public void Land()
    {
        SoundManager.Instance.PlayAudio("Run");
        SetAnimation(Anims.LAND);
        jumping = false;
    }

    public void UnLand()
    {
        SoundManager.Instance.StopAudio("Run");
    }

    public void Slide()
    {
        SoundManager.Instance.StopAudio("Run");
        SoundManager.Instance.PlayAudio("Slide");
        _rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;
        _collider.height = rollingColliderHeigh;
        _collider.center = rollingColliderCenter;
    }

    public void StopSlide()
    {
        SoundManager.Instance.PlayAudio("Run");
        rolling = false;
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
        _collider.height = defaultColliderHeight;
        _collider.center = defaultColliderCenter;
    }

    public void OnDie()
    {
        if (jumping == true)
        {
            jumping = false;
        }
        if (rolling == true)
        {
            StopSlide();
        }
        SoundManager.Instance.StopAudio("Run");
        SoundManager.Instance.PlayAudio("Dead");
    }

    public void SetGrounded(bool boolean)
    {
        grounded = boolean;
    }

    public void SetAnimation(Anims anim)
    {
        _anim.SetInteger("State", (int)anim);
    }

    public float GetPlayerVelocity()
    {
        return _mb.GetFrontVelocity();
    }

    public float GetRBVelocityZ()
    {
        return _rb.velocity.z;
    }

    public bool GetJumping()
    {
        return jumping;
    }

    public bool GetRolling()
    {
        return rolling;
    }
}
