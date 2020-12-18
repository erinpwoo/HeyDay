using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public List<Building> buildings;
    public float duration = 15f;
    public string[] availPackageTypes;
    float currCounter = 0;
    public GameObject minimapWindow;
    public Player player;
    public GameObject startButton;
    public GameObject timer;
    public GameObject failLevelText;
    private float startTime;
    public bool isGameRunning;
    public int pointThreshold;
    public Vector3 playerStartPosition;
    public GameObject pauseMenu;
    public bool isPaused;
    public GameObject nextLevelButton;
    public GameObject nextLevelText;
    public GameObject whatsNew;
    Scene currentScene;
    
    // Start is called before the first frame update
    void Start()
    {
        // initializing level values
        GameObject[] buildingObjs = GameObject.FindGameObjectsWithTag("Buildings");
        for (int i = 0; i < buildingObjs.Length; i++)
        {
            buildings.Add(buildingObjs[i].GetComponent<Building>());
        }

        minimapWindow = GameObject.FindGameObjectWithTag("Minimap window");
        startButton = GameObject.FindGameObjectWithTag("Start button");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        minimapWindow.SetActive(false);
        startButton.SetActive(true);
        if (whatsNew != null)
            whatsNew.SetActive(true);
        timer = GameObject.FindGameObjectWithTag("Level timer");
        startTime = Time.time;
        failLevelText = GameObject.FindGameObjectWithTag("Fail level");
        failLevelText.SetActive(false);
        isGameRunning = false;
        pauseMenu.SetActive(false);
        isPaused = false;
        availPackageTypes = new string[player.availablePackages.Length];
        for (int i = 0; i < player.availablePackages.Length; i++)
        {
            availPackageTypes[i] = player.availablePackages[i].tag;
        }
        nextLevelButton.SetActive(false);
        nextLevelText.SetActive(false);
        currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "Level 1")
        {
            whatsNew.GetComponentInChildren<Text>().text = "Welcome to the first round! In order to move on to level 2, you make at least 80 points worth of deliveries.\n\n Good luck!";
            pointThreshold = 80;

        }
        else if (currentScene.name == "Level 2")
        {
            pointThreshold = 150;
            whatsNew.GetComponentInChildren<Text>().text = "Welcome to round 2! To move to round 3, you'll need to make at least 150 pts. In this round, we'll be introducing:\n\nTwo-day shipping packages (BLUE):\nTime limit = 40 sec.\nPoint value: 20 pts";
        }
        else if (currentScene.name == "Level 3")
        {
            pointThreshold = 200;
            whatsNew.GetComponentInChildren<Text>().text = "Welcome to round 3! To move to round 4, you'll need to make at least 200 pts. We have more buildings to deliver to this round, so you better get on it!";

        }
        else if (currentScene.name == "Level 4")
        {
            pointThreshold = 250;
        }
    }

    // Update is called once per frame
    void Update()
    {
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
        if (player.hasStarted && isGameRunning)
        {
            currCounter -= Time.deltaTime;
            if (currCounter <= 0)
            {
                // spawning random order at random building every x seconds
                Building newOrder = buildings[Random.Range(0, buildings.Count - 1)];
                if (!newOrder.isTimerRunning)
                {
                    newOrder.SpawnOrder(availPackageTypes[Random.Range(0, availPackageTypes.Length)]);
                    currCounter = duration;
                }
            }

            float timeLeft = 119 - (Time.time - startTime);
            float minutes = Mathf.Floor(timeLeft / 60);
            float seconds = Mathf.RoundToInt(timeLeft % 60);
            if (seconds < 10)
            {
                timer.GetComponent<Text>().text = "Time left: " + minutes + ":0" + Mathf.RoundToInt(seconds);
            } else
            {
                timer.GetComponent<Text>().text = "Time left: " + minutes + ":" + Mathf.RoundToInt(seconds);
            }
            
            if (timeLeft <= 0)
            {
                timer.GetComponent<Text>().text = "Time left: 0:00";
                if (player.points >= pointThreshold)
                {
                    PassedToNextLevel();
                } else
                {
                    GameOver();
                }
            }
        }
    }

    public void StartGame()
    {
        if (!isGameRunning)
        {
            player.transform.position = playerStartPosition;
            player.transform.rotation = new Quaternion(0, 0, 0, 1);
            minimapWindow.SetActive(true);
            player.hasStarted = true;
            isGameRunning = true;
            failLevelText.SetActive(false);
            startButton.SetActive(false);
            if (whatsNew != null)
                whatsNew.SetActive(false);
            startTime = Time.time;
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        
    }

    public void GameOver()
    {
        isGameRunning = false;
        player.hasStarted = false;
        minimapWindow.SetActive(false);
        for (int i = 0; i < buildings.Count; i++)
        {
            if (buildings[i].isTimerRunning)
            {
                buildings[i].timer.GetComponent<Bar>().CancelBarTimer();
                buildings[i].isTimerRunning = false;
                buildings[i].requestedPackageType = null;
                Destroy(buildings[i].arrow);
                Destroy(buildings[i].timer);
            }
            
        }
        player.points = 0;
        failLevelText.SetActive(true);
        startButton.SetActive(true);
    }

    public void PassedToNextLevel ()
    {
        isGameRunning = false;
        player.hasStarted = false;
        minimapWindow.SetActive(false);
        for (int i = 0; i < buildings.Count; i++)
        {
            if (buildings[i].isTimerRunning)
            {
                buildings[i].timer.GetComponent<Bar>().CancelBarTimer();
                buildings[i].isTimerRunning = false;
                buildings[i].requestedPackageType = null;
                Destroy(buildings[i].arrow);
                Destroy(buildings[i].timer);
            }

        }
        player.points = 0;
        nextLevelText.SetActive(true);
        nextLevelButton.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToNextLevel()
    {
        if (currentScene.name == "Level 1")
        {
            SceneManager.LoadScene("Level 2");
        } else if (currentScene.name == "Level 2")
        {
            SceneManager.LoadScene("Level 3");
        }
        else if (currentScene.name == "Level 3")
        {
            SceneManager.LoadScene("Level 4");
        }
        else if (currentScene.name == "Level 4")
        {
            SceneManager.LoadScene("Level 5");
        }
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
