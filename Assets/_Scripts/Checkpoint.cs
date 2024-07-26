using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    PlayerCheckpoint checkpoint;

    private void Awake()
    {
        checkpoint = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<PlayerCheckpoint>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            checkpoint.UpDateCheckpoint(transform.position);
        }
    }
}
