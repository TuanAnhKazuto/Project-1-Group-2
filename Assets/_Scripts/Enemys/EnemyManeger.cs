using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float moveSpeed = 2f;
    [SerializeField] private float leftPosition;
    [SerializeField] private float rightPosition;
    private int moveDiretion = 1;
    Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isSeePlayer", false);
    }

    private void Update()
    {
        EnemyMove();
    }

    public void EnemyMove()
    {
        transform.Translate(Vector2.right * moveSpeed * moveDiretion * Time.deltaTime);
        Vector2 scale = transform.localScale;
        if (transform.position.x <= leftPosition)
        {
            scale.x = 1;
            moveDiretion = 1;
        }
        if (transform.position.x >= rightPosition)
        {
            scale.x = -1;
            moveDiretion = -1;
        }
        transform.localScale = scale;
    }

}
