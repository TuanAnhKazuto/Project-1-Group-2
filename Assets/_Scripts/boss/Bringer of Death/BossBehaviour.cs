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
            
        }
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
