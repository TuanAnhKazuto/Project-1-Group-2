using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    int maxLife = 3;
    int curLife;
    int maxHP = 100;
    [HideInInspector] public int curHP;
    private float safeTime = 0.7f;
    private float _safeTimeCoolDown;
    [HideInInspector] public bool isDeading = false;

    public HealthBar healhtBar;
    TheGhost player;
    PlayerCheckpoint respawn;
    Animator animator;
    Rigidbody2D rb;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private AudioSource gameOverSound;
    [SerializeField] private AudioSource mainSound;

    private void Start()
    {
        gameOverPanel.SetActive(false);
        curLife = maxLife;
        curHP = maxHP;
        healhtBar.UpdateBar(maxHP, curHP);
        player = GetComponentInParent<TheGhost>();
        respawn = GetComponentInParent<PlayerCheckpoint>();
        animator = GetComponentInParent<Animator>();
        rb = GetComponentInParent<Rigidbody2D>();
        rb.simulated = true;
        isDeading = false;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "EnemyATK")
        {
            EnemyBehaviour enemyBhv = other.GetComponentInParent<EnemyBehaviour>();
            if (enemyBhv != null)
            {
                TakeDamage(enemyBhv.damage);
            }
        }

        if(other.gameObject.tag == "Trap")
        {
            MovingTrap trap = other.GetComponent<MovingTrap>();
            if(trap != null)
            {
                TakeDamage(100);
            }
        }

        if(other.gameObject.tag == "BossATK")
        {
            TakeDamage(35);
        }
    }

    public void TakeDamage(int damage)
    {
        if(isDeading) return;
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

    public void HealingInBar(int healing)
    {
        curHP += healing;
        healhtBar.UpdateBar(maxHP, curHP);
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
        isDeading = true;
        rb.simulated = false;
        animator.SetBool("isDead", true);
        StartCoroutine(WaitForAnimationAndStopGame("Dead"));
    }

    private IEnumerator WaitForAnimationAndStopGame(string animationName)
    {
        yield return new WaitForSeconds(0.01f);
        animator.SetBool("isDead", false);

        // Lấy thời lượng của animation
        float animationLength = GetAnimationLength(animationName);
        yield return new WaitForSeconds(animationLength);
        gameOverSound.Play();
        mainSound.Stop();
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
        
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

    

    private void Update()
    {
        _safeTimeCoolDown -= Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.X))
        {
            OnDead();
        }
    }
}