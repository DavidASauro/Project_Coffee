using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{

    public enum Scene
    {
        GameScene,
        LoadingScene,
        MainMenu,
        SampleScene
    }

    public static void LoadMainGame()
    {
        SceneManager.LoadScene("SampleScene");
    }



}
