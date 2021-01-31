using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartMenu() {
        SoundManager.StartMenu();
        SceneManager.LoadScene(0);
    }

    public void PlayGame() 
    {
        SoundManager.PlayRoaming();
        SceneManager.LoadScene(1);
    }

    public void QuitGame() 
    {
        Debug.Log("Quit Selected.");
        Application.Quit();
    }

   
}
