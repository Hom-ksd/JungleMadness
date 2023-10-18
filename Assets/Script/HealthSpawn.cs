using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthSpawn : MonoBehaviour
{
    public GameObject targetPosition;

    public float flyingSpeed = 50f;
    public float flyingdistance = 2f;
    Vector2 flyingDirection;

    Rigidbody2D rb;
    Collider2D col;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = rb.GetComponent<Collider2D>();
    }

    private void Start()
    {
        flyingDirection = new Vector2(targetPosition.transform.position.x - transform.position.x, targetPosition.transform.position.y - transform.position.y).normalized;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        flyingdistance -= Time.fixedDeltaTime;
        if(flyingdistance >= 0)
        {
            col.enabled = false;
            rb.velocity = flyingDirection * flyingSpeed * Time.fixedDeltaTime;
        }
        else
        {
            col.enabled = true;
            rb.velocity = Vector2.zero;
        }
    }
}
