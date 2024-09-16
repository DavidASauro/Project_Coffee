using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelManager : MonoBehaviour
{
    public void LoadLevelByName(string lvlName)
    {
        SceneManager.LoadScene(lvlName);
    }

    public void LoadLevelByIndex(int lvlIndex)
    {
        SceneManager.LoadScene(lvlIndex);
    }

    public void ReloadCurrentLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene != null)
        {
            SceneManager.LoadScene(scene.name);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting Game...");
    }

}
