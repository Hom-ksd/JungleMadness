using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public int attackDamage;
    public Vector2 knockback = Vector2.zero;
    public GameObject ObjToRemove;

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        Damageable selfDamage = ObjToRemove.GetComponent<Damageable>();
        if (damageable != null)
        {
            Vector2 deliveredKnockback = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);

            bool gothit = damageable.Hit(attackDamage, deliveredKnockback);
            if (gothit)
                selfDamage.Health = -1;
                Debug.Log(collision.name + "hit for " + attackDamage);
        }
    }
}