using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialLevel : MonoBehaviour
{
    public GameObject minimapWindow;
    public Player player;
    public GameObject minimapCam;
    public GameObject pauseMenu;
    public bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        minimapWindow = GameObject.FindGameObjectWithTag("Minimap window");
        minimapWindow.SetActive(true);
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.hasStarted)
        {
            player.hasStarted = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
                isPaused = false;
                AudioListener.pause = false;
            }
            else
            {
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
                isPaused = true;
                AudioListener.pause = true;
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    public void ResumeGame()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            isPaused = false;
            AudioListener.pause = false;
        }

    }

    public void NavigateToMainMenu()
    {
        SceneManager.LoadScene("Main menu");
    }
}
