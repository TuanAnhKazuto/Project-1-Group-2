using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    #region Public
    public Transform rayCast;
    public LayerMask rayCastMask;
    public float rayCastLenght;
    public float attackDistace;
    public float moveSpeed;
    public float timer;
    #endregion

    #region Private
    private RaycastHit2D hit;
    private GameObject target;
    private Animator animator;
    private float distance;
    private bool attackMode;
    private bool inRange;
    private bool cooling;
    private float intTimer;
    #endregion

    private void Awake()
    {
        intTimer = timer;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(inRange)
        {
            hit = Physics2D.Raycast(rayCast.position, Vector2.left, rayCastLenght, rayCastMask);
            RayCastDebugger();
        }

        //when Player is detected
        if(hit.collider != null)
        {
            EnemyLogic();
        }
        else if(hit.collider == null)
        {
            inRange = false;
        }

        if (!inRange)
        {
            animator.SetBool("isWalk", false);
            StopAttack();
        }
        
    }

    private void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);

        if(distance > attackDistace)
        {
            Move();
            StopAttack();
        }
        else if(attackDistace >= distance && !cooling)
        {
            Attack();
        }

        if (cooling)
        {
            animator.SetBool("Attack", false);
        }
    }

    void Move()
    {
        animator.SetBool("isWalk", true);

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("AttackNormal"))
        {
            Vector2  targetPosition = new Vector2(target.transform.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        timer = intTimer;
        attackMode = true;

        animator.SetBool("isWalk", false);
        animator.SetBool("Attack", true);
    }
    private void StopAttack()
    {
        cooling = false;
        attackMode  = false;
        animator.SetBool("Attack", false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            target = other.gameObject;
            inRange = true;
        }
    }

    void RayCastDebugger()
    {
        if(distance > attackDistace)
        {
            Debug.DrawRay(rayCast.position, Vector2.left * rayCastLenght, Color.red);
        }
        else if(attackDistace > distance)
        {
            Debug.DrawRay(rayCast.position, Vector2.left * rayCastLenght, Color.green);
        }
    }
}
