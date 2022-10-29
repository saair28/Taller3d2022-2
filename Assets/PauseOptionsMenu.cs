using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseOptionsMenu : MonoBehaviour
{
    public static PauseOptionsMenu instance;
    GameObject player;
    public GameObject pauseUI, pauseButtons, audioSliders, gameSliders;
    public bool isPaused = false;

    void Awake()
    {
        instance = this;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if(isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PressingEscape();
        }
    }

    public void PressingEscape()
    {
        if (audioSliders.activeSelf)
        {
            gameSliders.SetActive(false);
            audioSliders.SetActive(false);
            pauseButtons.SetActive(true);
            return;
        }

        if (!pauseUI.activeSelf)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    public void PauseGame()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
        player.GetComponent<PlayerShoot>().enabled = false;
    }

    public void ResumeGame()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
        player.GetComponent<PlayerShoot>().enabled = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
