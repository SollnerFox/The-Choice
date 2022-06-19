using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public DayNightManager dayManager;

    public AudioSource menuAudio;
    public AudioSource gameAudio;

    public AudioClip[] dayMusic;

    private void Start()
    {
        gameAudio.Play();
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        
    }
    public void ResumeGame()
    {
        if (pauseMenuUI.activeInHierarchy)
        { pauseMenuUI.SetActive(false); }
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        menuAudio.Pause();
        gameAudio.Play();
    }
    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        menuAudio.Play();
        gameAudio.Pause();
    }
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    public void ExitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}
