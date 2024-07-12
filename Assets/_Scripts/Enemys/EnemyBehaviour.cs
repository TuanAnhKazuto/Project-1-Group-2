using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [HideInInspector] public float moveSpeed = 2;
    [SerializeField] private float leftPosition;
    [SerializeField] private float rightPosition;
    private int moveDiretion = 1;

    public float timeStopAtk;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector2.right * moveSpeed * moveDiretion * Time.deltaTime);

        Vector2 scale = transform.localScale;
        if(transform.position.x <= leftPosition)
        {
            moveDiretion = 1;
            scale.x = 1;
        }
        else if(transform.position.x >= rightPosition)
        {
            moveDiretion = -1;
            scale.x = -1;
        }
        transform.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Attack();
        }
    }

    private void Attack()
    {
        moveSpeed = 0;
        animator.SetBool("attack", true);
        Invoke("StopAttack", timeStopAtk);
    }

    private void StopAttack()
    {
        moveSpeed = 2;
        animator.SetBool("attack", false);
    }
}
