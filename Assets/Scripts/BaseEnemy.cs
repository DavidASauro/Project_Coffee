using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public float health = 10f;
    

    // Start is called before the first frame update
    void Start()
    {
      
    
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {   
        
        if (collision.gameObject.CompareTag("projectile"))
        {
            
            health -= collision.gameObject.GetComponent<ProjectileMovement>().dmg;
        }
    }
}
