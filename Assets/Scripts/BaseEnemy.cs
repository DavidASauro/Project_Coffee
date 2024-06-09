using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public float health = 10f;
    public float currentHealth;
    public HealthBar healthBar;
    

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
        healthBar.SetMaxHealthBar(health);
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        healthBar.adjustHealthBar(currentHealth);
        if (currentHealth <= 0) {
            Die();
        }

    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
