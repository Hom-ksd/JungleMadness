using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

public class TouchingDirection : MonoBehaviour
{
    // Start is called before the first frame update
    public ContactFilter2D contactFilter;
    CapsuleCollider2D capsuleCollider2D;
    Animator animator;

    RaycastHit2D[] groundHits =  new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];

    public float groundDistance = 0.05f;
    public float wallDistance = 0.2f;
    public float ceilingDistance = 0.05f;


    [SerializeField] private bool _IsGrounded = true;

    public bool IsGrounded { 
        get 
        {
            return _IsGrounded;
        } 
        private set
        {
            _IsGrounded = value;
            animator.SetBool(AnimationString.isGrounded, value);
        }
    }
    [SerializeField] private bool _IsOnWall = true;

    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left; 

    public bool IsOnWall
    {
        get
        {
            return _IsOnWall;
        }
        private set
        {
            _IsOnWall = value;
            animator.SetBool(AnimationString.IsOnWall, value);
        }
    }
    [SerializeField] private bool _IsOnCeiling = true;

    public bool IsOnCeiling
    {
        get
        {
            return _IsOnCeiling;
        }
        private set
        {
            _IsOnCeiling = value;
            animator.SetBool(AnimationString.IsOnCeiling, value);
        }
    }

    private void Awake()
    {
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        IsGrounded = capsuleCollider2D.Cast(Vector2.down, contactFilter, groundHits, groundDistance) > 0;
        IsOnWall = capsuleCollider2D.Cast(wallCheckDirection, contactFilter, wallHits, wallDistance) > 0;
        IsOnCeiling = capsuleCollider2D.Cast(Vector2.up, contactFilter, ceilingHits, ceilingDistance) > 0;
    }
}
