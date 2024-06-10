using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public Rigidbody2D body;
    public CircleCollider2D circleCollider;
    public RangedWeapon RangeWeapon;


    // Start is called before the first frame update
    void Start()
    {
     
       body.velocity = transform.right * RangeWeapon.projectileSpeed;
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("enemy"))
        {
            BaseEnemy enemy = collision.collider.GetComponent<BaseEnemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(RangeWeapon.damage);
                Destroy(gameObject);
            }
        }
        Destroy(gameObject);
    }
}
