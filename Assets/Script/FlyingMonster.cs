using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMonster : MonoBehaviour
{
    public float flightSpeed = 100f;
    public float waypointReachedDistance = 0.1f;

    public DetectionZone AttackDetectionZone;
    public List<Transform> waypoints;
    
    private Transform nextWaypoint;
    int waypointnum = 0;

    Animator animator;
    Rigidbody2D rb;
    Damageable damageable;
    AudioSource audioSource;

    public AudioClip deathClip;
    public AudioClip attackClip;
    public AudioClip hitClip;

    private bool _hasTarget = false;

    public bool hasTarget
    {
        get
        {
            return _hasTarget;
        }
        set
        {
            _hasTarget = value;
            animator.SetBool(AnimationString.hasTarget, value);
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
            animator.SetFloat(AnimationString.attackCooldown, value);
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationString.canMove);
        }
    }




    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        damageable = GetComponent<Damageable>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        nextWaypoint = waypoints[waypointnum];
    }

    // Update is called once per frame
    void Update()
    {
        hasTarget = AttackDetectionZone.detectedColliders.Count > 0;
    }

    private void FixedUpdate()
    {
        if(attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }

        if (damageable.isAlive)
        {
            if (CanMove)
            {
                Flight();
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }
        else
        {
            rb.gravityScale = 3f;
        }
    }

    private void Flight()
    {
        Vector2 directionToWaypoint = (nextWaypoint.position - transform.position).normalized;

        float distance = Vector2.Distance(transform.position, nextWaypoint.position);

        rb.velocity = directionToWaypoint * flightSpeed;
        UpdateDirection();

        if (distance <= waypointReachedDistance)
        {
            waypointnum++;

            if(waypointnum >= waypoints.Count)
            {
                waypointnum = 0;
            }

            nextWaypoint = waypoints[waypointnum];
        }
    }

    private void UpdateDirection()
    {
        if(transform.localScale.x > 0f)
        {
            if(rb.velocity.x < 0f)
            {
                transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);

            }
        }

        if(transform.localScale.x < 0f)
        {
            if (rb.velocity.x > 0f)
            {
                transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
    }

    public void attackSound()
    {
        audioSource.PlayOneShot(attackClip);
    }

    public void deathSound()
    {
        audioSource.PlayOneShot(deathClip);
    }

    public void hitSound()
    {
        audioSource.PlayOneShot(hitClip);
    }
}
