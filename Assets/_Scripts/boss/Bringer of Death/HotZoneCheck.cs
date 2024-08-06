using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class HotZoneCheck : MonoBehaviour
{
    private BossBehaviour bossParent;
    private bool inRange;
    private Animator animator;

    private void Awake()
    {
        bossParent = GetComponentInParent<BossBehaviour>();
        animator = GetComponentInParent<Animator>();
    }
    private void Update()
    {
        if(inRange && !animator.GetCurrentAnimatorStateInfo(0).IsName("AttackNormal"))
        {
            bossParent.Flip();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            inRange = false;
            gameObject.SetActive(false);
            bossParent.triggerArea.SetActive(true);
            bossParent.inRange = false;
            bossParent.SelectTarget();
        }
    }
}
