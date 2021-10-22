using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public GameObject MainMenuUI;
    public GameObject SettingsUI;
    public GameObject CreditsUI;


    public void LoadPlay()
    {
        AudioManager.current.Play("Open");
        SceneManager.LoadSceneAsync("Play");
    }
    public void GoToMenu()
    {
        AudioManager.current.Play("Open");
        SceneManager.LoadSceneAsync("Main Menu");
    }
    public void Restart()
    {
        AudioManager.current.Play("Open");
        SceneManager.LoadScene("Play");
    }

    public void LoadSettings()
    {
        AudioManager.current.Play("Open");
        SettingsUI.SetActive(true);
    }
    public void QuitSettings()
    {
        AudioManager.current.Play("Close");
        SettingsUI.SetActive(false);
    }

    public void LoadCredits()
    {
        AudioManager.current.Play("Open");
        CreditsUI.SetActive(true);
    }

    public void QuitCredits()
    {
        AudioManager.current.Play("Close");
        CreditsUI.SetActive(false);
    }

    public void QuitGame()
    {
        AudioManager.current.Play("Close");
        Application.Quit();
    }

}
