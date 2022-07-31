using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsMenu;
    public GameObject  optionsFirstButton, optionsClosedButton;
  
    public void PlayGame()
   {
       SceneManager.LoadScene(1);
   }

    public void QuitGmae ()
    {
        Debug.Log("you quit (:");
        Application.Quit();
    }

    public void OpenOptions()
    {
        optionsMenu.SetActive(true);
        
        EventSystem.current.SetSelectedGameObject(null);
        
        EventSystem.current.SetSelectedGameObject(optionsFirstButton);
    }
    public void ClosedOptions()
    {
        optionsMenu.SetActive(false);
        
        EventSystem.current.SetSelectedGameObject(null);
        
        EventSystem.current.SetSelectedGameObject(optionsClosedButton);
    }
}
