using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public LoadLevelManager SceneLoader;
    public GameObject UserInterface;
    private Player player;

    List<int> SceneIndexs = new List<int> {1,2,3,4,5};
    List<int> SceneIndexsTwo = new List<int>{3,4,5};


    private void Awake()
    {
        Instance = this;
       
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
      
    }

    // Update is called once per frame
    void Update()
    {
      
        if (player.isChangingLevel == true)
        {
            SceneLoader.LoadLevelByIndex(SceneIndexsTwo[UnityEngine.Random.Range(0,SceneIndexsTwo.Count)]);
        }
      
    }
    
}



