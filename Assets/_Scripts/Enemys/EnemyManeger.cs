using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyManeger : MonoBehaviour
{
    private float moveSpeed = 2f;
    [SerializeField] private float leftPosition;
    [SerializeField] private float rightPosition;
    private int moveDiretion = 1;
    [SerializeField] protected float timeStartATK;
    [SerializeField] protected float timEndATK1;
    [SerializeField] private GameObject attack1;
    [SerializeField] protected float timeNextATK2;
    [SerializeField] private GameObject attack2;
    [SerializeField] protected float timeEndATK;
    [SerializeField] protected float timeResetSeePlayer;
    Animator animator;

    private void Update()
    {
        EnemyMove();
    }

    public void EnemyMove()
    {
        transform.Translate(Vector2.right *  moveSpeed * moveDiretion * Time.deltaTime);
        Vector2 scale = transform.localScale;
        if(transform.position.x <= leftPosition)
        {
            scale.x = 1;
            moveDiretion = 1;
        }
        if(transform.position.x >= rightPosition)
        {
            scale.x = -1;
            moveDiretion = -1;
        }
        transform.localScale = scale;

    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isSeePlayer", false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            moveSpeed = 0;
            Invoke("StartATK", timeStartATK);
            animator.SetBool("isSeePlayer", true);
            Invoke("ResetSeePlayer", timeResetSeePlayer);
        }
    }

    private void ResetSeePlayer()
    {
        animator.SetBool("isSeePlayer", false);
        moveSpeed = 2f;
    }

    private void StartATK()
    {
        attack1.SetActive(true);
        Invoke("ATK1",  timEndATK1);
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
