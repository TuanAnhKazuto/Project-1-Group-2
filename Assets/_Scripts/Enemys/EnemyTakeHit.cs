using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeHit : MonoBehaviour
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
            enemyBhv.moveSpeed = 0.1f;
            Invoke("SetBoolFalse", 0.4f);
            enemyBhv.curLife -= 1;
            Debug.Log(enemyBhv.curLife);
        }
    }


    private void SetBoolFalse()
    {
        enemyBhv.moveSpeed = 2;
        enemyBhv.animator.SetBool("takeHit", false);
    }

    
}
