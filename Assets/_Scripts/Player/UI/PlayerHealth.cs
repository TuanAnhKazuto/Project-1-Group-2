using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    int maxLife = 3;
    int curLife;
    int maxHP = 100;
    int curHP;
    private float safeTime = 0.7f;
    private float _safeTimeCoolDown;

    public HealthBar healhtBar;
    TheGhost player;
    PlayerCheckpoint respawn;

    private void Start()
    {
        curLife = maxLife;
        curHP = maxHP;
        healhtBar.UpdateBar(maxHP, curHP);
        player = GetComponentInParent<TheGhost>();
        respawn = GetComponentInParent<PlayerCheckpoint>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "EnemyATK")
        {
            EnemyBehaviour enemyBhv = other.GetComponentInParent<EnemyBehaviour>();
            if (enemyBhv != null)
            {
                TakeDame(enemyBhv.damage);
            }
        }

        if(other.gameObject.tag == "Trap")
        {
            MovingTrap trap = other.GetComponent<MovingTrap>();
            if(trap != null)
            {
                TakeDame(15);
            }
        }
    }

    public void TakeDame(int damage)
    {
        if (player.isDashing) return;

        if(_safeTimeCoolDown <= 0)
        {
            curHP -= damage;
            healhtBar.UpdateBar(maxHP, curHP);
            _safeTimeCoolDown = safeTime;
        }

        if(curHP <= 0)
        {
            SubLife();
        }
    }

    private void SubLife()
    {
        
        curLife -= 1;
        if (curLife > 0)
        {
            Debug.Log(curLife);
            curHP = maxHP;
            healhtBar.UpdateBar(maxHP, curHP);
            healhtBar.UpdateLife(maxLife, curLife);
            respawn.Respawn();
        }
        else
        {
            OnDead();
        }
    }

    private void OnDead()
    {
        Time.timeScale = 0;
        Destroy(gameObject);
    }

    private void Update()
    {
        _safeTimeCoolDown -= Time.deltaTime;
    }
}