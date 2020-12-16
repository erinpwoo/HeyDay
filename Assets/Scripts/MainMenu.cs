using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartLevel0()
    {
        SceneManager.LoadScene("Level 0");
    }
    public void StartLevel1()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void DisplayCredits()
    {

    }
}
