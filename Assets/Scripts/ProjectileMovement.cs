using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public Rigidbody2D body;
    public CircleCollider2D circleCollider;
    public float projectileSpeed;


    // Start is called before the first frame update
    void Start()
    {
     
       body.velocity = transform.right * projectileSpeed;
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        Destroy(gameObject);
    }
}
