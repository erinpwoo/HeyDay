using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialLevel : MonoBehaviour
{
    public GameObject minimapWindow;
    public Player player;
    public GameObject minimapCam;
    public GameObject pauseMenu;
    public bool isPaused;
    int currTextIndex = 0;
    public GameObject instructionPanel;
    int maxTextIndex;
    public GameObject buildingExample;
    public Button contToLevel1;
    public string[] tutorialText;

    // Start is called before the first frame update
    void Start()
    {
        tutorialText = new string[] {
            "Today is your first day as a delivery person for the mega e-commerce company, Zaminon®. Lucky you, today is the busiest shopping day of the year due to Zaminon’s yearly sales event, HeyDay! As you navigate through the chaos of your first day, you are tasked with navigating the vehicle throughout the neighborhood while launching the correct type of packages at their specified locations in a timely manner . Driving this massive vehicle is no easy task, and neither is getting these packages delivered at their correct locations.",
            "To launch packages, right click to spawn one of the package types above the vehicle. Continuing to right click will allow you to cycle through your package inventory to find the correct package type. Give it a try!",
            "Here, you have two package types: \n\n\t* Standard (red) - 15 points - 60 sec.\n\t* No-rush (purple) - 10 points - 50 sec.\n\nEach package type will have its own point value that depends on the delivery speed. The less time you have to deliver the package type, the more points that package is worth. If you fail to make a delivery on time, the point value of that missed package will be deducted from your score.",
            "Now let's try launching a package! To grab a package, left click on the spawned package. Hold, drag, and release the mouse to throw it in the direction of where you want to launch it. Once you throw a package, left click to re-spawn a new package from your inventory.",
            "A house requesting a package will appear as a location on your minimap in the bottom-right corner of your screen. You will also see an arrow spawn above the house, indicating the location on the scene. The bar on the minimap will be colored based on the requested package type and will display the amount of time you have left to deliver a package.",
            "Let's try navigating towards the house. To navigate the vehicle:\n\tW/S: accelerate forwards/backwards \n\tA/D: Steer left/right\n\n Hitting street objects will result in point deductions, so watch where you drive! The larger the object you hit, the larger the point deductions. Tipping over the truck will result in an automatic fail.",
            "In order to make a successful delivery, the delivery needs to be delivered in an area within the perimeter of the house. Upon a successful delivery, you should hear a doorbell and the arrow above the house should disappear.",
            "For each level, you have 2 minutes to complete as many successful deliveries as possible. In order to move onto the next level, you must exceed the point threshold as specified in the beginning of the level. As you progress through the levels, you will unlock newer package types with greater point values as well as new maps and environments. That's all you need to know for this tutorial, goodluck!"
        };
        minimapWindow = GameObject.FindGameObjectWithTag("Minimap window");
        minimapWindow.SetActive(true);
        pauseMenu.SetActive(false);
        isPaused = false;
        instructionPanel = GameObject.FindGameObjectWithTag("Instruction panel");
        instructionPanel.GetComponentInChildren<Text>().text = tutorialText[currTextIndex];
        maxTextIndex = tutorialText.Length - 1;
        buildingExample.transform.Find("Package area").GetComponent<MeshRenderer>().enabled = false;
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

    public void NavigateToLevel1()
    {

    }

    public void ClickNext()
    {
        currTextIndex++;
        if (currTextIndex > maxTextIndex)
        {
            SceneManager.LoadScene("Level 1");
        } else
        {
            instructionPanel.GetComponentInChildren<Text>().text = tutorialText[currTextIndex];
        }
        
        if (currTextIndex == 4)
        {
            buildingExample.transform.Find("Package area").GetComponent<MeshRenderer>().enabled = true;
            buildingExample.GetComponent<Building>().SpawnOrder("Standard");
        }
        if (currTextIndex == maxTextIndex)
        {
            contToLevel1.GetComponentInChildren<Text>().text = "Start!";
        }
    }
}
