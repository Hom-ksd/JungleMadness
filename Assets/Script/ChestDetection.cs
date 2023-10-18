using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestDetection : MonoBehaviour
{
    public GameObject buttonPopup;

    public List<Collider2D> detectedCollisions = new List<Collider2D>();

    // Start is called before the first frame update    

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        detectedCollisions.Add(collision);
        collision.gameObject.GetComponent<PlayerController>().targetChest = gameObject.GetComponentInParent<Chest>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        detectedCollisions.Remove(collision);
        collision.gameObject.GetComponent<PlayerController>().targetChest = null;
    }
}
