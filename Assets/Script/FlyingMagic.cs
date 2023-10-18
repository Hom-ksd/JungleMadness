using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMagic : MonoBehaviour
{
    public float Flyingspeed = 300f;
    public float FlyingDistance = 10f;
    public int attackDamage;
    public Vector2 knockback;

    Rigidbody2D rb;
    Animator animator;
    AudioSource audioSource;
    public AudioClip hitClip;

    private float _FlyingDirection = 1;
    private Vector3 startposition;

    public float FlyingDirection
    {
        get { return _FlyingDirection; }
        set { 
            _FlyingDirection = value;
        }
    }

    public bool isHit
    {
        get
        {
            return animator.GetBool("isHit");
        }
        set {
            animator.SetBool("isHit", value);
        }
    }
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        FlyingDirection = transform.localScale.x * 2;
        startposition = transform.position;
        rb.velocity = new Vector2(FlyingDirection * Flyingspeed * Time.fixedDeltaTime, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (isHit)
        {
            rb.velocity = Vector2.zero;
        }
            
        if (Mathf.Abs(startposition.x - transform.position.x) > FlyingDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isHit = true;
            return;
        }
        isHit = true;
        Damageable damageable = collision.GetComponent<Damageable>();

        Vector2 deliveredKnockback = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

        bool gothit = damageable.Hit(attackDamage, deliveredKnockback);
        if (gothit)
            Debug.Log(collision.name + "hit for " + attackDamage);

    }

    public void onHit()
    {
        Destroy(gameObject);
    }

    public void playSound()
    {
        audioSource.PlayOneShot(hitClip);
    }
}
