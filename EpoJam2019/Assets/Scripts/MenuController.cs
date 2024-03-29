﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    //Menus
    [SerializeField] private GameObject canvasMainMenu = null;
    [SerializeField] private GameObject canvasSettings = null;
    [SerializeField] private GameObject canvasCredits = null;

    //Checkers
    private bool mainMenu;

    void Start()
    {
        MainMenu();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (mainMenu)
                CloseGame();
            else
                MainMenu();
        }
    }

    /// <summary>
    /// Function that starts the game
    /// Used by the Start button
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Fuction that close the game
    /// Go to desktop
    /// Used by the Exit Button
    /// </summary>
    public void CloseGame()
    {
        Application.Quit();
    }

    /// <summary>
    ///function thats set the canvas of the Settings the visible canvas 
    /// </summary>
    public void Settings()
    {
        mainMenu = false;

        canvasMainMenu.SetActive(false);
        canvasCredits.SetActive(false);
        canvasSettings.SetActive(true);
    }

    /// <summary>
    ///function thats set the canvas of the Credits the visible canvas 
    /// </summary>
    public void Credits()
    {
        mainMenu = false;

        canvasMainMenu.SetActive(false);
        canvasCredits.SetActive(true);
        canvasSettings.SetActive(false);
    }

    /// <summary>
    ///function thats set the canvas of the Main Menu the visible canvas 
    /// </summary>
    public void MainMenu()
    {
        mainMenu = true;

        canvasMainMenu.SetActive(true);
        canvasCredits.SetActive(false);
        canvasSettings.SetActive(false);
    }
}
