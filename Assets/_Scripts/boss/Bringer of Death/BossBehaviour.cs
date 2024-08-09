using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    #region Public Variables
    public float attackDistance; //Minimum distance for attack
    public float moveSpeed;
    public float timer; //Timer for cooldown between attacks
    public Transform leftLimit;
    public Transform rightLimit;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange; //Check if Player is in range
    public GameObject hotZone;
    public GameObject triggerArea;
    public GameObject bossSkill;
    #endregion

    #region Private Variables
    private Animator anim;
    private TheGhost player;
    private float distance; //Store the distance b/w enemy and player
    private bool attackMode;
    private bool cooling; //Check if Enemy is cooling after attack
    private float intTimer;
    private bool isUesSkill;
    private BossFinalHealth bossHealth;
    #endregion

    void Awake()
    {
        SelectTarget();
        intTimer = timer; //Store the inital value of timer
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<TheGhost>();
        bossHealth = GetComponent<BossFinalHealth>();
    }

    private void Start()
    {
        if (bossHealth.isDeading) return;
        InvokeRepeating("UseSkill", 1f, 3f);
    }

    void Update()
    {
        if (!attackMode || !anim.GetCurrentAnimatorStateInfo(0).IsName("BossThunderAttack") || !bossHealth.isDeading)
        {
            Move();
        }

        if(bossHealth.isDeading)
        {
            transform.position = new Vector2(0,0);
        }

        if (!InsideOfLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("AttackNormal"))
        {
            SelectTarget();
        }

        if (inRange && !bossHealth.isDeading)
        {
            EnemyLogic();
        }
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if (distance > attackDistance)
        {
            StopAttack();
        }
        else if (attackDistance >= distance && cooling == false)
        {
            Attack();
        }

        if (cooling)
        {
            Cooldown();
            anim.SetBool("Attack", false);
        }
    }

    void Move()
    {
        anim.SetBool("isWalk", true);

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("AttackNormal"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        timer = intTimer; //Reset Timer when Player enter Attack Range
        attackMode = true; //To check if Enemy can still attack or not

        anim.SetBool("isWalk", false);
        anim.SetBool("Attack", true);
    }

    void Cooldown()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }

    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("Attack", false);
    }

    public void TriggerCooling()
    {
        cooling = true;
    }

    public void UseSkill()
    {
        anim.SetBool("UseSkill", true);
        GameObject skill = Instantiate(bossSkill, new Vector3(player.transform.position.x, player.transform.position.y + 2.47f, player.transform.position.z), Quaternion.identity);
        Invoke("StopUseSkill", 0.3f);
    }

    private void StopUseSkill()
    {
        anim.SetBool("UseSkill", false);
    }

    private bool InsideOfLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    public void SelectTarget()
    {
        float distanceToLeft = Vector3.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector3.Distance(transform.position, rightLimit.position);

        if (distanceToLeft > distanceToRight)
        {
            target = leftLimit;
        }
        else
        {
            target = rightLimit;
        }

        //Ternary Operator
        //target = distanceToLeft > distanceToRight ? leftLimit : rightLimit;

        Flip();
    }

    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > target.position.x)
        {
            rotation.y = 0;
        }
        else
        {
            rotation.y = 180;
        }

        //Ternary Operator
        //rotation.y = (currentTarget.position.x < transform.position.x) ? rotation.y = 180f : rotation.y = 0f;

        transform.eulerAngles = rotation;
    }
}