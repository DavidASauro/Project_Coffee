using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerMovement player;
    public GameObject UserInterface;

    private void Awake()
    {
        Instance = this;  
    }

    // Start is called before the first frame update
    void Start()
    {
        
      
    }

    // Update is called once per frame
    void Update()
    {
       checkPlayerHealth();
    }


    void checkPlayerHealth()
    {
        if (player.currentHealth <= 0)
        {
            Time.timeScale = 0;
            UserInterface.SetActive(true);
            resetPlayer();
        }
    }

    void resetPlayer()
    {
        player.currentHealth = player.maxHealth;
        player.isDead = false;
        

    }

}


