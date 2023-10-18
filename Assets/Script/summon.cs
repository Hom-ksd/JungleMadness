using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class summon : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject playerTarget;
    
    public float flyingSpeed = 1.0f;
    public AudioClip deathClip;

    AudioSource audioSource;
    Rigidbody2D rb;
    Vector2 targetVector;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = rb.GetComponent<AudioSource>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        targetVector = new Vector2(playerTarget.transform.position.x - transform.position.x, playerTarget.transform.position.y - transform.position.y).normalized;
        rb.velocity = targetVector * flyingSpeed;
    }

    void deathSound()
    {
        audioSource.PlayOneShot(deathClip);
    }
}
