using System;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public float walkStopRate = 10f;
    public float maxSpeed = 100f;
    public float walkAcceleration = 5f;

    Rigidbody2D rb;
    AudioSource audioSource;
    public DetectionZone attackZone;
    public DetectionZone cliffDetectionZone;
    public AudioClip attackClip;
    public AudioClip hitClip;
    public AudioClip deathClip;
    Animator animator;

    public enum WalkableDirection { Right, Left}

    Damageable damageable;
    TouchingDirection touchingDirection;

    private WalkableDirection _walkDirection;
    public Vector2 WalkDirectionVector = Vector2.right;
    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set {
            if (_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                
                if (value == WalkableDirection.Right)
                {
                    WalkDirectionVector = Vector2.right;
                }
                else if(value == WalkableDirection.Left)
                {
                    WalkDirectionVector = Vector2.left;
                }
            }
            _walkDirection = value;
        }
    }

    public float attackCooldown
    {
        get
        {
            return animator.GetFloat(AnimationString.attackCooldown);
        }
        set
        {
           animator.SetFloat(AnimationString.attackCooldown,Mathf.Max(value,0f));
        }
    }
    
    private bool _hasTarget = false;

    public bool hasTarget { 
        get { return _hasTarget; } 
        private set {
            _hasTarget = value;
            animator.SetBool(AnimationString.hasTarget, value);
        } 
    }

    public bool canMove
    {
        get
        {
            return animator.GetBool(AnimationString.canMove);
        }
    }


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirection = GetComponent<TouchingDirection>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()   
    {
        hasTarget = attackZone.detectedColliders.Count > 0;
        if(attackCooldown > 0)
            attackCooldown -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (touchingDirection.IsGrounded && touchingDirection.IsOnWall)
        {
            FlipDirection();
        }
        if(!damageable.LockVelocity)
        {
            if (canMove)
            {
                float xVelocity = Mathf.Clamp(rb.velocity.x + (walkAcceleration * WalkDirectionVector.x * Time.fixedDeltaTime), -maxSpeed, maxSpeed);

                rb.velocity = new Vector2(xVelocity, rb.velocity.y);
            }
            else
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
        }
    }

    private void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
        else
        {
            Debug.LogError("Current walkable direction is not set to legal value of right or left");
        }
    }

    public void onHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    public void noCliffDetected()
    {
        if (touchingDirection.IsGrounded)
        {
            FlipDirection();
        }
    }

    public void attackSound()
    {
        audioSource.PlayOneShot(attackClip);
    }

    public void hitSound()
    {
        audioSource.PlayOneShot(hitClip);
    }

    public void deathSound()
    {
        audioSource.PlayOneShot(deathClip);
    }
}
