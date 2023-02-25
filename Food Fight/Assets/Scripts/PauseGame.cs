/*
 * Jacob Zydorowicz
 * PauseGame.cs
 * Food Fight
 * Basic pause menu script
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1;
    }
    [SerializeField] GameObject pauseMenu;
    public static bool gameIsPaused = false;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
                ResumeTheGame();
            else
                PauseTheGame();
        }
    }

    public void ResumeTheGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
    }

    private void PauseTheGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        gameIsPaused = true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}
