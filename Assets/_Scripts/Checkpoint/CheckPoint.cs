using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class CheckPoint : MonoBehaviour
{
    private PlayerCheckpoint playerCheckpoint;
    Animator animator;

    private void Awake()
    {
        playerCheckpoint = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<PlayerCheckpoint>();
    }
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            animator.SetBool("isCheckPoint", true);
            playerCheckpoint.UpdateCheckpoint(transform.position);
            Debug.Log("Saved checkpoint: " + transform.position);
        }

        
    }
}
