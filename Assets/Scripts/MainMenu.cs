using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject tutorialBtn;
    public GameObject startBtn;
    public GameObject playByLevelBtn;
    public GameObject[] lvlBtns;
    public GameObject backBtn;

    private void Start()
    {
        ShowMain();  
    }

    public void StartLevel0()
    {
        SceneManager.LoadScene("Level 0");
    }
    public void StartLevel1()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void StartLevel2()
    {
        SceneManager.LoadScene("Level 2");
    }
    public void StartLevel3()
    {
        SceneManager.LoadScene("Level 3");
    }
    public void StartLevel4()
    {
        SceneManager.LoadScene("Level 4");
    }
    public void DisplayCredits()
    {

    }
    public void PlayByLevel()
    {
        tutorialBtn.SetActive(false);
        startBtn.SetActive(false);
        playByLevelBtn.SetActive(false);
        backBtn.SetActive(true);
        for (int i = 0; i < lvlBtns.Length; i++)
        {
            lvlBtns[i].SetActive(true);
        }

    }

    public void ShowMain()
    {
        tutorialBtn.SetActive(true);
        startBtn.SetActive(true);
        playByLevelBtn.SetActive(true);
        backBtn.SetActive(false);
        for (int i = 0; i < lvlBtns.Length; i++)
        {
            lvlBtns[i].SetActive(false);
        }
    }
}
