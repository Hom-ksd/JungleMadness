using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDector : MonoBehaviour
{
    public List<Collider2D> detectedCollisions = new List<Collider2D>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        detectedCollisions.Add(collision);
        collision.gameObject.GetComponent<PlayerController>().targetNPC = gameObject.GetComponentInParent<NPC>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        detectedCollisions.Remove(collision);
        collision.gameObject.GetComponent<PlayerController>().targetNPC = null;

    }
}
