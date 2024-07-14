using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealht : MonoBehaviour
{
    int maxHP = 100;
    int curHP;
    private float safeTime = 0.7f;
    private float _safeTimeCoolDown;

    public HealhtBar healhtBar;

    private void Start()
    {
        curHP = maxHP;
        healhtBar.UpdateBar(maxHP, curHP);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "EnemyATK")
        {
            EnemyBehaviour enemyBhv = other.GetComponentInParent<EnemyBehaviour>();
            if (enemyBhv != null)
            {
                TakeDameEnemy(enemyBhv.damage);
            }
        }
    }

    private void TakeDameEnemy(int damage)
    {
        if(_safeTimeCoolDown <= 0)
        {
            curHP -= damage;
            healhtBar.UpdateBar(maxHP, curHP);
            _safeTimeCoolDown = safeTime;
        }
    }

    private void Update()
    {
        _safeTimeCoolDown -= Time.deltaTime;
        if (curHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}