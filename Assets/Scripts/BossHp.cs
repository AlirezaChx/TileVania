using System.Collections;
using UnityEngine;

public class BossHp : MonoBehaviour
{
    public HealthBar healthBar;
    public int currentHealth;
    public int maxHealth = 200;
    public Animator animator;
    public GameObject deathEffect;
    public float deathDelay = 1.5f;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "bullet")
        {
            TakeDamage(10);
            Debug.Log("bullet hit the boss: " + currentHealth);

            Destroy(other.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Boss took damage. Current Health: " + currentHealth);
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Boss has died.");
        
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }
    
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
    
        Collider2D bossCollider = GetComponent<Collider2D>();
        if (bossCollider != null)
        {
            bossCollider.enabled = false;
        }
    
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }
    
        Destroy(gameObject, deathDelay);
    }
}