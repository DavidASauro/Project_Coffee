using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public float speed = 10f;
    public float dmg = 1f;
    public Rigidbody2D body;
    public CircleCollider2D circleCollider;


    // Start is called before the first frame update
    void Start()
    {
     
        body.velocity = transform.right * speed;
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
