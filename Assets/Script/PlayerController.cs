using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;


[RequireComponent(typeof(Rigidbody2D),typeof(Animator))]

public class PlayerController : MonoBehaviour
{
    public float walkspeed = 200f;
    public float jumpimpulse = 10f;
    Vector2 moveInput;

    Rigidbody2D rb;
    Animator animator;
    AudioSource audioSource;
    public AudioClip attackClip;
    public AudioClip jumpClip;
    public AudioClip magicClip;
    public AudioClip hitClip;

    [SerializeField] private bool _isMoving = false;
    [SerializeField] private float _roll_Speed = 300f;
    [SerializeField] private float _roll_cd = 2f;
    [SerializeField] private float _magic_cd = 2f;
    private float _roll_cooldown = 0;
    private float _magic_cooldown = 0;

    Damageable damageable;
    public TouchingDirection touchingdirection;
    ProjectileManager projectileManager;

    public Chest targetChest;
    public NPC targetNPC;

    public bool isRoll
    {
        get
        {
            return animator.GetBool(AnimationString.isRoll); ;
        }
        private set
        {
            animator.SetBool(AnimationString.isRoll, value);
        }
    }

    public bool IsMoving { 
        get { 
            return _isMoving; 
        } 
        private set { 
            _isMoving = value;
            animator.SetBool(AnimationString.isMoving, value);
        } 
    }

    public bool canMove
    {
        get { return animator.GetBool(AnimationString.canMove); }
    }

    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationString.IsAlive);
        }
    }

    public bool isAttack { get { return animator.GetBool(AnimationString.isAttack); } set { animator.SetBool(AnimationString.isAttack, value); } }
    private bool _isMagic = false;
    public bool isMagic { get { return _isMagic; } set { _isMagic = value; } }

    private bool _IsFacingRight = true;
    public bool IsFacingRight { get { return _IsFacingRight; } private set {
            if(_IsFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _IsFacingRight = value;
        } 
    }



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingdirection = GetComponent<TouchingDirection>();
        projectileManager = GetComponentInChildren<ProjectileManager>();
        damageable = GetComponent<Damageable>();
        audioSource = GetComponentInChildren<AudioSource>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public float CurrentWalkspeed
    {
        get
        {
            if (canMove)
            {
                _roll_cooldown -= Time.fixedDeltaTime;
                if (_isMoving && !isRoll && !touchingdirection.IsOnWall)
                {
                    return moveInput.x * walkspeed * Time.fixedDeltaTime;
                }
                else if (isRoll && !touchingdirection.IsOnWall)
                {
                    if (moveInput.x == 0)
                    {
                        if (IsFacingRight)
                        {
                            return _roll_Speed * Time.fixedDeltaTime;
                        }
                        else
                        {
                            return -_roll_Speed * Time.fixedDeltaTime;
                        }
                    }
                    else
                    {
                        return moveInput.x * _roll_Speed * Time.fixedDeltaTime;
                    }
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
            
        }
    }


    private void FixedUpdate()
    {
        _roll_cooldown -= Time.fixedDeltaTime;
        _magic_cooldown -= Time.fixedDeltaTime;
        if(!damageable.LockVelocity)
                rb.velocity = new Vector2(CurrentWalkspeed, rb.velocity.y);
        
        animator.SetFloat(AnimationString.yVelocity, rb.velocity.y);
    }
    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
           // Face the right
           IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }
    public void onMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (IsAlive && Time.timeScale == 1)
        {
            IsMoving = moveInput != Vector2.zero;
            SetFacingDirection(moveInput);
        }
        else
        {
            IsMoving = false;
        }
    }

    public void onRoll(InputAction.CallbackContext context)
    {
        if (context.started && _roll_cooldown <= 0f && !isAttack && !isMagic && Time.timeScale == 1)
        {
            isRoll = true;
            damageable.isInvincible = true;
        }
    }

    public void stopRoll()
    {
        isRoll = false;
        _roll_cooldown = _roll_cd;
        damageable.isInvincible = false;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && touchingdirection.IsGrounded && !isAttack && !isRoll && Time.timeScale == 1)
        {
            animator.SetTrigger(AnimationString.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpimpulse);
        }
    }

    public void Usemagic(InputAction.CallbackContext context)
    {
        if (context.started && _magic_cooldown <= 0f && !isAttack && touchingdirection.IsGrounded && Time.timeScale == 1)
        {
            projectileManager.Shotmagic(_IsFacingRight);
            animator.SetTrigger(AnimationString.magicTrigger);
            isMagic = true;
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started && !isRoll && Time.timeScale == 1)
        {
            animator.SetTrigger(AnimationString.attackTrigger);
            isAttack = true;
            //Debug.Log("Attack!");

        }
    }

    public void OpenChest(InputAction.CallbackContext context)
    {
        if(context.started && Time.timeScale == 1)
        {
            if(targetChest != null)
            {
                targetChest.OpenChest();
            }

            if(targetNPC != null && targetNPC.isEnd == true)
            {
                targetNPC.victory();
            }
        }
    }

    public void onHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
        audioSource.PlayOneShot(hitClip);
        isAttack = false;
        isRoll = false;
        isMagic = false;    
    }

    public void StopMagic()
    {
        _magic_cooldown = _magic_cd;
        isMagic = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Chest")
        {
            targetChest = collision.gameObject.GetComponent<Chest>();
        }
    }

    public void attackSound()
    {
        audioSource.loop = false;
        audioSource.clip = attackClip;
        audioSource.Play();
    }

    public void jumpSound()
    {
        audioSource.PlayOneShot(jumpClip);
    }

    public void magicSound()
    {
        audioSource.PlayOneShot(magicClip);
    }
}
