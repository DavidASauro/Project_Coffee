using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    internal PlayerMovement player;

    void Update()

    {
        player.direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        //Getting input for Jump and jumping
        if (Input.GetButtonDown("Jump") && player.onGround)
        {
            player.jumped = true;
        }

        //cheking for attack button press
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Shoot");
            //wtv the fuck
        }

        //Getting the input for left or right movement and using appropriate function
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            player.moveLeft(player.direction.x);


        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            player.moveRight(player.direction.x);
        }
    }

     void FixedUpdate()
    {
        
        if (player.jumped)
        {
               player.jump();
        }
    }

}
