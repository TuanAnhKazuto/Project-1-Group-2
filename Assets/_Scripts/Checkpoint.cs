using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    PlayerCheckpoint checkpoint;
    Animator animator;
    public AudioSource checkpointSound;

    private void Awake()
    {
        checkpoint = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<PlayerCheckpoint>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            checkpointSound.Play();
            animator.SetBool("isCheckPoint", true);
            checkpoint.UpDateCheckpoint(transform.position);

        }
    }
}
