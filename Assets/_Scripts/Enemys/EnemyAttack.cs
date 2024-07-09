using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] protected float timeStartATK;
    [SerializeField] protected float timEndATK1;
    [SerializeField] private GameObject attack1;
    [SerializeField] protected float timeNextATK2;
    [SerializeField] private GameObject attack2;
    [SerializeField] protected float timeEndATK;
    [SerializeField] protected float timeResetAction;
    private EnemyManager enemyManager;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        enemyManager = GetComponent<EnemyManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enemyManager.moveSpeed = 0;
            ResetAction();
            Invoke("StartATK", timeStartATK);
            animator.SetBool("isSeePlayer", true);
            Invoke("ResetAction", timeResetAction);
        }
    }

    public void ResetAction()
    {
        animator.SetBool("isSeePlayer", false);
        enemyManager.moveSpeed = 2f;
    }

    private void StartATK()
    {
        attack1.SetActive(true);
        Invoke("ATK1", timEndATK1);
    }

    private void ATK1()
    {
        attack1.SetActive(false);
        Invoke("ATK2", timeNextATK2);
    }

    private void ATK2()
    {
        attack2.SetActive(true);
        Invoke("EndATK", timeEndATK);
    }

    private void EndATK()
    {
        attack2.SetActive(false);
    }

 
}
