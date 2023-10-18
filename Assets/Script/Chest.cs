using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;

    public AudioClip openSound;

    public Canvas canvas;
    public ChestDetection detection;

    public GameObject PickupHealthPrefab;
    public GameObject PickupHealthSpawnPosition;

    public GameObject parent;
    public bool isOpen { 
        get { return animator.GetBool(AnimationString.isOpen); }
        set { 
            animator.SetBool(AnimationString.isOpen, value); 
        } 
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(detection.detectedCollisions.Count > 0)
        {
            canvas.enabled = true;
        }
        else
        {
            canvas.enabled = false;
        }
    }

    public void OpenChest()
    {
        if (!isOpen)
        {
            audioSource.PlayOneShot(openSound);
            isOpen = true;
            GameObject Healthpickup = Instantiate(PickupHealthPrefab,parent.transform);
            Healthpickup.transform.position = PickupHealthSpawnPosition.transform.position;
        }
    }
}
