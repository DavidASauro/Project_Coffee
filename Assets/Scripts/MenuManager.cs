using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour

{
    public GameObject interfaceThing;
    public void startGame()
    {
        SceneManager.LoadScene("UnderWorld01");
    }
    public void playAgain()
    {
        SceneManager.LoadScene("UnderWorld01");

        if (interfaceThing != null)
        {
            Time.timeScale = 1;
            interfaceThing.SetActive(false);
        }
        else
        {
            Debug.Log("No GameObject");
        }
    }
    public void quitGamne()
    {
        Application.Quit();
    }
}
