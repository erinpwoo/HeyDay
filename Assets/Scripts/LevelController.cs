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
    }

    // Update is called once per frame
    void Update()
    {
        if (player.hasStarted)
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
        }
        
    }

    public void StartGame()
    {
        minimapWindow.SetActive(true);
        player.hasStarted = true;
        startButton.SetActive(false);
    }
}
