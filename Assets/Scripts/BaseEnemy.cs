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

    // Update is called once per frame
    void Update()
    {
        if (currentHealth == 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {   
        
        if (collision.gameObject.CompareTag("projectile"))
        {
            currentHealth -=  collision.gameObject.GetComponent<ProjectileMovement>().dmg;
            healthBar.adjustHealthBar(currentHealth);

        }
        
    }
}
