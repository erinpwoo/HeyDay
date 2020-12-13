using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    
    // Start is called before the first frame update
    void Start()
    {
        // initializing level values
        GameObject[] buildingObjs = GameObject.FindGameObjectsWithTag("Buildings");
        availPackageTypes = new string[] { "No rush", "Standard" };
        for (int i = 0; i < buildingObjs.Length; i++)
        {
            buildings.Add(buildingObjs[i].GetComponent<Building>());
        }

        minimapWindow = GameObject.FindGameObjectWithTag("Minimap window");
        startButton = GameObject.FindGameObjectWithTag("Start button");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        minimapWindow.SetActive(false);
        startButton.SetActive(true);
        timer = GameObject.FindGameObjectWithTag("Level timer");
        startTime = Time.time;
        failLevelText = GameObject.FindGameObjectWithTag("Fail level");
        failLevelText.SetActive(false);
        isGameRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
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

            float timeLeft = 120 - (Time.time - startTime);
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
            }
            
        }
        player.points = 0;
        failLevelText.SetActive(true);
        startButton.SetActive(true);
    }

    public void PassedToNextLevel ()
    {

    }
}
