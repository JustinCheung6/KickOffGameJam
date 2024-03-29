﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Manages Pause Menu, Game Pausing
public class PauseMenuManager : MenuOptions
{
    //Singleton
    private static PauseMenuManager instance = null;
    public static PauseMenuManager I
    {
        get
        {
            if(instance == null)
                instance = FindObjectOfType<PauseMenuManager>();

            return instance;
        }
    }

    //Properties
    private bool gamePaused = false;

    [Header("Object References")]
    [SerializeField] private GameObject pauseMenu = null;

    #region External Script Values (Getters/Setters)
    public bool GamePaused
    {
        get => gamePaused;
    }
    #endregion

    private void Awake()
    {
        //Setup singleton
        if (instance == null)
            instance = this;
        else if (instance != this)
            Debug.LogError($"Found multiple instances of PauseMenuManager:\nCurrent instance: {instance.gameObject.name},\nThis instance: {gameObject.name}");
    }
    void Update()
    {
        if (Input.GetButtonDown("PauseUnpause"))
        {
            gamePaused = !gamePaused;
        }

        if (gamePaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
        
    }
    private void PauseGame()
    {
        Time.timeScale = 0f;
        if (pauseMenu != null)
            pauseMenu.SetActive(true);
        gamePaused = true;
    }
    private void ResumeGame()
    {
        Time.timeScale = 1f;
        if(pauseMenu != null)
            pauseMenu.SetActive(false);
        gamePaused = false;
    }



    public void ResumeGame_Btn()
    {
        ResumeGame();
    }
}
