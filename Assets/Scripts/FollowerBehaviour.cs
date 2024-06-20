using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerBehaviour : MonoBehaviour
{
    public enum Anims
    {
        RUN_FORWARD,
        RUN_FORWARD_RIGHT,
        RUN_FORWARD_LEFT,
        JUMP,
        LAND,
        SLIDE
    };

    private MovementBehaviour _mb;
    private Rigidbody _rb;
    private Animator _anim;
    private PlayerController _player;
    private CapsuleCollider _collider;

    [Header("PROPERTIES")]
    [SerializeField] private float buffSpeed;
    [SerializeField] private float maxDistanceWithPlayer;
    [SerializeField] private float minDistanceWithPlayer;
    [SerializeField] private float rollingColliderHeigh;
    [SerializeField] private Vector3 rollingColliderCenter;

    [Header("REFERENCED")]
    [SerializeField] private GameObject skinnedBody;
    [SerializeField] private Transform leftChecker;
    [SerializeField] private Transform rightChecker;
    [SerializeField] private LayerMask followerLayer;

    [Header("INFORMATION")]
    [SerializeField] private Vector3 dir;
    [SerializeField] private bool grounded;
    [SerializeField] private float delay;
    [SerializeField] private GameObject player;
    [SerializeField] private Material pantsMat;
    [SerializeField] private Material shirtMat;
    [SerializeField] private bool startedDelay;
    [SerializeField] private float defaultColliderHeight;
    [SerializeField] private Vector3 defaultColliderCenter;
    [SerializeField] private bool sliding;
    [SerializeField] private Vector3 defaultRbVelocity;

    private void Awake()
    {
        player = GameObject.Find("Player");

        _mb = GetComponent<MovementBehaviour>();
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _player = player.GetComponent<PlayerController>();
        _collider = GetComponent<CapsuleCollider>();


        pantsMat = skinnedBody.GetComponent<SkinnedMeshRenderer>().materials[0];
        shirtMat = skinnedBody.GetComponent<SkinnedMeshRenderer>().materials[4];

        _mb.ChangeDefaultVelocity(_player.GetPlayerVelocity());
        defaultColliderCenter = _collider.center;
        defaultColliderHeight = _collider.height;
        defaultRbVelocity = new Vector3(0, 0, 0);
        rollingColliderCenter = new Vector3(_collider.center.x, rollingColliderCenter.y, _collider.center.z);
    }

    private void OnEnable()
    {
        delay = 0;
        startedDelay = false;
        _rb.velocity = defaultRbVelocity;
        SoundManager.Instance.PlayAudio("TakeFollower");
    }

    private void Update()
    {
        MyInput();

        Collider[] leftCollisions = Physics.OverlapSphere(leftChecker.position, 0.01f, followerLayer);
        Collider[] rightCollisions = Physics.OverlapSphere(rightChecker.position, 0.01f, followerLayer);

        //DIRECTION
        dir = player.transform.position - transform.position;
        dir.Normalize();

        if (!grounded)
        {
            dir.x = 0;
            dir.z = 1;
        }
        else if (leftCollisions.Length != 0 && dir.x != 0)
        {
            dir.x = leftCollisions[0].gameObject.GetComponent<FollowerBehaviour>().GetDirX();
            dir.z = 1;
        }
        else if (rightCollisions.Length != 0 && dir.x > 0)
        {
            dir.x = rightCollisions[0].gameObject.GetComponent<FollowerBehaviour>().GetDirX();
            dir.z = 1;
        }
        else
        {
            dir.z = 1;
        }

        if (Mathf.Abs(player.transform.position.z - transform.position.z) > maxDistanceWithPlayer && grounded)
        {
            _mb.SetVelocity(_mb.GetFrontVelocity()+buffSpeed);
        }
        else if (Mathf.Abs(player.transform.position.z - transform.position.z) < minDistanceWithPlayer && grounded)
        {
            _mb.SetVelocity(_mb.GetFrontVelocity()-buffSpeed);
        }
        else
        {
            _mb.SetDefaultVelocity();
        }

        //ANIMS
        if (grounded)
        {
            
            if (sliding)
            {
                SetAnimation(Anims.SLIDE);
            }
            else
            {
                SetAnimation(Anims.RUN_FORWARD);
            }
        }
        else
        {
            if (!grounded)
            {
                SetAnimation(Anims.JUMP);
            }
        }

        //JUMP TIMER
        if (delay < 0)
        {
            if (grounded && _player.GetJumping())
            {
                Jump();
            }
            else if (grounded && _player.GetRolling())
            {
                sliding = true;
            }
            delay = 0;
            startedDelay = false;
        }
        else if (delay > 0)
        {
            delay -= Time.deltaTime;
            if (delay == 0)
            {
                delay -= 1;
            }
        }
    }

    private void FixedUpdate()
    {
        _mb.MoveVelocity(dir);
    }

    private void MyInput()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.LeftShift) && (_player.GetJumping() || _player.GetRolling()) && !startedDelay) 
        {
            CalculateDelay();
            startedDelay = true;
        }
    }

    public void CalculateDelay()
    {
        delay = Vector3.Distance(player.transform.position, transform.position) / _player.GetRBVelocityZ();
    }

    private void Jump()
    {
        _collider.enabled = false;
        _mb.Jump();
        _anim.SetInteger("State", (int)Anims.JUMP);
    }

    public void Slide()
    {
        _rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX;
        _collider.height = rollingColliderHeigh;
        _collider.center = rollingColliderCenter;
    }

    public void StopSlide()
    {
        sliding = false;
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
        _collider.height = defaultColliderHeight;
        _collider.center = defaultColliderCenter;
    }

    public void Land()
    {
        SetAnimation(Anims.LAND);
        _collider.enabled = true;
    }

    public void OnDie()
    {
        if (player.activeInHierarchy && !GameManager.ended)
        {
            SoundManager.Instance.PlayAudio("Dead");
        }
        if (sliding)
        {
            StopSlide();
        }
        FollowersManager.Instance.LessActiveFollowers();
    }

    public float GetDirX()
    {
        return dir.x;
    }

    public void SetPlayer(GameObject GOplayer)
    {
        player = GOplayer;
    }

    public void SetGrounded(bool boolean)
    {
        grounded = boolean;
    }

    public void SetAnimation(Anims anim)
    {
        _anim.SetInteger("State", (int)anim);
    }

    public void SetPantsColor(Color c)
    {
        pantsMat.color = c;
    }

    public void SetShirtColor(Color c)
    {
        shirtMat.color = c;
    }
}
