using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserInterfaceScript : MonoBehaviour
{
    public GameObject interfaceThing;
    public void playAgain()
    {
        SceneManager.LoadScene("SampleScene");
        if (interfaceThing != null)
        {
            interfaceThing.SetActive(false);
        }
        else
        {
            Debug.Log("No GameObject");
        }   
    }


}
