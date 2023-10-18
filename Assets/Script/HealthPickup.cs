using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthRestore = 20;
    public Vector3 rotateSpin = new Vector3 (0, 180, 0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        
        if (damageable != null)
        {
            bool Healed = damageable.Heal(healthRestore);
            if(Healed)
                Destroy(gameObject);
        }

    }

    private void Update()
    {
        transform.eulerAngles += rotateSpin * Time.deltaTime;  
    }
}
