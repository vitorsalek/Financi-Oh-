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

    public void LoadSettings()
    {
        AudioManager.current.Play("Open");
        //MainMenuUI.SetActive(false);
        SettingsUI.SetActive(true);
    }
    public void QuitSettings()
    {
        AudioManager.current.Play("Close");
        SettingsUI.SetActive(false);
        //MainMenuUI.SetActive(true);
    }

    public void LoadCredits()
    {
        AudioManager.current.Play("Open");
        //MainMenuUI.SetActive(false);
        CreditsUI.SetActive(true);
    }

    public void QuitCredits()
    {
        AudioManager.current.Play("Close");
        CreditsUI.SetActive(false);
        //MainMenuUI.SetActive(true);
    }



}
