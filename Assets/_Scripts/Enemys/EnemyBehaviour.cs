using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform rayCast;
    public LayerMask rayCastMask;
    public float rayCastLength;
    public float attackDistance;
    public float moveSpeed;
    public float timer;


    private RaycastHit2D hit;
    private GameObject target;
    private Animator animator;
    private float distance;
    private bool atkMode;
    private bool inRange;
    private bool cooling;
    private float intTimer;

    void Awake()
    {
        intTimer = timer;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (inRange)
        {
            hit = Physics2D.Raycast(rayCast.position, Vector2.left, rayCastLength, rayCastMask);
            RayCastDebugger();
        }

        //
        if(hit.collider != null)
        {
            EnemyLogic();
        }
        else if(hit.collider == null)
        {
            inRange = false;
        }

        if(inRange == false)
        {
            animator.SetBool("attack", false);
            StopAttack();
        }
    }

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.transform.tag == "Player")
        {
            target = other.gameObject;
            inRange = true;
        }
    }

    public void RayCastDebugger()
    {
        if (distance > attackDistance)
        {
            UnityEngine.Debug.DrawRay(rayCast.position, Vector2.left * rayCastLength, Color.red);
        }
        else if(distance < attackDistance)
        {
            UnityEngine.Debug.DrawRay(rayCast.position, Vector2.left * rayCastLength, Color.green);
        }
    }

    public void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);

        if(distance > attackDistance)
        {
            Move();
            StopAttack();
        }
        else if(attackDistance >= distance && cooling == false)
        {
            Attack();
        }

        if (cooling)
        {
            animator.SetBool("attack", false);
        }
    }

    private void Move()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            Vector2 targetPosition = new Vector2(target.transform.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed *  Time.deltaTime);
        }
    }
    private void Attack()
    {
        timer = intTimer;
        atkMode = true;

        animator.SetBool("attack", true);
    }
    private void StopAttack()
    {
        cooling = false;
        atkMode = false;
        animator.SetBool("attack", false);
    }
    
}