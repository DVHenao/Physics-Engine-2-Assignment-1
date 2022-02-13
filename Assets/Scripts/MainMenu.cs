using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// very self explantory

public class MainMenu : MonoBehaviour
{
 
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }


    public void QuitGame()
    {
        Debug.Log("Quit game button pressed");
        Application.Quit();

    }
}
