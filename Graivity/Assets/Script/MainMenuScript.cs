using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void SettingsButton()
    {
        Debug.Log("SettingsButton");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
