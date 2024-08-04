using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [HideInInspector] public Animator animator;

    //move
    [HideInInspector] public float moveSpeed = 2;
    [SerializeField] private float leftPosition;
    [SerializeField] private float rightPosition;
    private int moveDirection = 1;

    //attack
    public float timeStopAtk;

    //take hit
    private float maxLife = 5;
    [HideInInspector] public float curLife;
    [SerializeField] private GameObject enemyCollider;
    private Rigidbody2D rg;

    //set damage
    public int damage; // Ensure this is public

    private void Start()
    {
        curLife = maxLife;
        animator = GetComponent<Animator>();
        rg = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
        TakeAttack();
    }

    private void Move()
    {
        transform.Translate(Vector2.right * moveSpeed * moveDirection * Time.deltaTime);

        Vector2 scale = transform.localScale;
        if (transform.position.x <= leftPosition)
        {
            moveDirection = 1;
            scale.x = 1;
        }
        else if (transform.position.x >= rightPosition)
        {
            moveDirection = -1;
            scale.x = -1;
        }
        transform.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
                Attack();
        }
    }

    //Attack
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

    //TakeAttack
    private void TakeAttack()
    {
        if (curLife <= -1)
        {
            enemyCollider.SetActive(false);
            rg.gravityScale = 0;
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        do
        {
            yield return new WaitForSeconds(0.3f);
        } while (!animator.GetCurrentAnimatorStateInfo(0).IsName("TakeHit"));
        Destroy(gameObject);
    }
}
