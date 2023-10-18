using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using static Skeleton;

public class Boss_1 : MonoBehaviour
{
    public GameObject playerTarget;
    public DetectionZone attackZone;
    public List<GameObject> summonPoints;
    public GameObject summonPrefab;
    public GameObject parent;
    
    Rigidbody rb;
    Damageable damageable;
    Animator animator;
    SpriteRenderer spriteRenderer;
    AudioSource audioSource;

    public GameObject dropParent;

    public AudioClip attackClip;
    public AudioClip hitClip;
    public AudioClip deathClip;
    public AudioClip skillClip;

    private int checkpoint = 0;

    public bool canSummon
    {
        get
        {
            return animator.GetBool(AnimationString.canSummon);
        }
        set
        {
            animator.SetBool(AnimationString.canSummon, value);
        }
    }

    public enum FacingAbleDirection { Right, Left};

    private FacingAbleDirection _FacingDirection;
    public Vector2 FacingDirectionVector = Vector2.right;
    public FacingAbleDirection FacingDirection
    {
        get { return _FacingDirection; }
        set {
            if (_FacingDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if (value == FacingAbleDirection.Right)
                {
                    FacingDirectionVector = Vector2.right;
                }
                else if (value == FacingAbleDirection.Left)
                {
                    FacingDirectionVector = Vector2.left;
                }
            }
            _FacingDirection = value;
        }
    }

    private bool _hasTarget = false;

    public bool hasTarget
    {
        get { return _hasTarget; }
        set
        {
            _hasTarget = value;
            animator.SetBool(AnimationString.hasTarget, value);
        }
    }

    public float AttackCooldown
    {
        get
        {
            return animator.GetFloat(AnimationString.attackCooldown);
        }
        set
        {
            animator.SetFloat(AnimationString.attackCooldown, Mathf.Max(value, 0f));
        }
    }

    public float SummonCooldown
    {
        get
        {
            return animator.GetFloat(AnimationString.summonCoolDown);
        }
        set
        {
            animator.SetFloat(AnimationString.summonCoolDown, Mathf.Max(value, 0f));
        }
    }

    public float SkillCoolDown
    {
        get
        {
            return animator.GetFloat(AnimationString.skillCoolDown);
        }
        set
        {
            animator.SetFloat(AnimationString.skillCoolDown, Mathf.Max(value, 0));
        }
    }

    private void FlipDirection()
    {
        if (FacingDirection == FacingAbleDirection.Right)
        {
            FacingDirection = FacingAbleDirection.Left;
        }
        else if (FacingDirection == FacingAbleDirection.Left)
        {
            FacingDirection = FacingAbleDirection.Right;
        }
        else
        {
            Debug.LogError("Current walkable direction is not set to legal value of right or left");
        }
    }

    public bool useSkill
    {
        get
        {
            return animator.GetBool(AnimationString.skill);
        }
        set
        {
            animator.SetBool(AnimationString.skill, value);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
        rb = GetComponent<Rigidbody>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTarget.transform.position.x < gameObject.transform.position.x && FacingDirection == FacingAbleDirection.Right)
        {
            FlipDirection();
        }
        else if (playerTarget.transform.position.x >= gameObject.transform.position.x && FacingDirection == FacingAbleDirection.Left)
        {
            FlipDirection();
        }

        hasTarget = attackZone.detectedColliders.Count > 0;

        if(AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }

        if(SummonCooldown > 0)
        {
            SummonCooldown -= Time.deltaTime;
        }
        else
        {
            canSummon = true;
        }

        if (SkillCoolDown > 0)
        {
            SkillCoolDown -= Time.deltaTime;
        }
        else
        {
            useSkill = true;
        }

        if(damageable.Health <= damageable.MaxHealth / 5 && checkpoint == 0)
        {
            canSummon = true;
            SummonCooldown = 0;
            checkpoint += 1;
        }

        if(damageable.Health <= 300)
        {
            useSkill = true;
        }
    }

    private void FixedUpdate()
    {
        
    }

    public void summonMinnions()
    {
        foreach (GameObject summonPoint in summonPoints)
        {
            Vector2 spawnPosition = summonPoint.transform.position;
            GameObject summonObject = Instantiate(summonPrefab,parent.transform);
            summonObject.transform.localPosition = spawnPosition;
            summon S = summonObject.GetComponent<summon>();
            S.playerTarget = playerTarget;
            S.GetComponent<RandomDrop>().parent = dropParent;
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

    public void skillSound()
    {
        audioSource.PlayOneShot(skillClip);
    }
}
