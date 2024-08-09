using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFinalHealth : MonoBehaviour
{
    int maxHP = 100;
    [HideInInspector] public int curHP;
    [HideInInspector] public bool isDeading;
    HealthBarBoss bossHealth;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        bossHealth = GameObject.FindGameObjectWithTag("BossHealth").GetComponent<HealthBarBoss>();
        curHP = maxHP;
        bossHealth.UpdateBar(maxHP, curHP);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PlayerATK")
        {
            curHP -= 10;
            bossHealth.UpdateBar(maxHP, curHP);
        }
    }

    private void Update()
    {
        if(curHP <= 0)
        {
            curHP = 0;
            animator.SetBool("isDeath", true);
            OnDead();
        }
    }

    private void OnDead()
    {
        isDeading = true;
        animator.SetBool("isDeath", true);
        StartCoroutine(WaitForAnimationAndStopGame());
    }

    private IEnumerator WaitForAnimationAndStopGame()
    {
        yield return new WaitForSeconds(0.01f);
        animator.SetBool("isDeath", false);

        float animationLength = GetAnimationLength("Die");
        yield return new WaitForSeconds(animationLength);
        Destroy(gameObject);
    }

    private float GetAnimationLength(string animationName)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName(animationName))
        {
            return stateInfo.length;
        }
        return 0f;
    }
}
