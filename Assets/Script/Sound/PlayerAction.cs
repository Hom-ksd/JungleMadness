using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    AudioSource audioSource;
    public PlayerController controller;
    public AudioClip movingclip;
    public AudioClip jumpclip;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.IsMoving && !audioSource.isPlaying && controller.touchingdirection.IsGrounded)
        {
            audioSource.clip = movingclip;
            audioSource.loop = true;
            audioSource.Play();
        }
        else if((!controller.IsMoving && audioSource.isPlaying) || !controller.touchingdirection.IsGrounded)
        {
            audioSource.Stop();
        }
    }
}
