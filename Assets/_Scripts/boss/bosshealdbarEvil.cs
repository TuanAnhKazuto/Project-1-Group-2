using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Slider healthBar; // Thanh máu
    public float maxHealth = 400f; // Máu tối đa
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0; // Đảm bảo không âm
        healthBar.value = currentHealth;
        Debug.Log("Damage taken: " + amount + ", Current Health: " + currentHealth);
        if (currentHealth <= 0)
        {
            DestroyObject(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerATK"))
        {
            TakeDamage(4); 
        }
    }

}
