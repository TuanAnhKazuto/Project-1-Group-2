using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeHit : MonoBehaviour
{
    EnemyBehaviour enemyBhv;

    private void Start()
    {
        enemyBhv = GetComponentInParent<EnemyBehaviour>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerATK")
        {
            enemyBhv.animator.SetBool("takeHit", true);
            Invoke("SetBoolFalse", 0.2f);
            enemyBhv.curLife -= 1;
        }
    }

    private void SetBoolFalse()
    {
        enemyBhv.animator.SetBool("takeHit", false);
    }

    
}
