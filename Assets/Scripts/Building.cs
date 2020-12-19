using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    public string requestedPackageType;
    public Player player;
    public GameObject timerPrefab;
    public Transform timerPosition;
    public GameObject timer;
    public bool isTimerRunning;
    public AudioSource doorbell;
    public GameObject arrowPrefab;
    public GameObject arrow;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        timerPosition = transform.parent.Find("Timer position");
        isTimerRunning = false;
        doorbell = GetComponent<AudioSource>();
    }

    private void Update()
    {
        
    }

    public void CheckPackageCollision(string packageType)
    {
        if (packageType == "No-rush")
        {
            player.points += 10;
            timer.GetComponent<Bar>().CancelBarTimer();
            Destroy(timer);
            isTimerRunning = false;
            requestedPackageType = null;
            PlayDoorbell();
            Destroy(arrow);
        }
        else if (packageType == "Standard")
        {
            player.points += 15;
            timer.GetComponent<Bar>().CancelBarTimer();
            Destroy(timer);
            isTimerRunning = false;
            requestedPackageType = null;
            PlayDoorbell();
            Destroy(arrow);
        }
        else if (packageType == "2-day")
        {
            player.points += 20;
            timer.GetComponent<Bar>().CancelBarTimer();
            Destroy(timer);
            isTimerRunning = false;
            requestedPackageType = null;
            PlayDoorbell();
            Destroy(arrow);
        } else if (packageType == "Same day")
        {
            player.points += 30;
            timer.GetComponent<Bar>().CancelBarTimer();
            Destroy(timer);
            isTimerRunning = false;
            requestedPackageType = null;
            PlayDoorbell();
            Destroy(arrow);
        }
        player.UpdatePointsUI();
    }

    public void SpawnOrder(string packageType)
    {
        isTimerRunning = true;
        timer = Instantiate(timerPrefab, timerPosition);
        if (packageType == "No-rush")
        {
            timer.GetComponent<Bar>().time = 60;
        }
        else if (packageType == "Standard")
        {
            timer.GetComponent<Bar>().time = 50;
        }
        else if (packageType == "2-day")
        {
            timer.GetComponent<Bar>().time = 40;
        }
        else if (packageType == "Same day")
        {
            timer.GetComponent<Bar>().time = 40;
        }
        requestedPackageType = packageType;
        timer.GetComponent<Bar>().AnimateBar(timer, this);
        arrow = Instantiate(arrowPrefab, timerPosition.position, arrowPrefab.transform.rotation);
    }

    public void DecrementPoints()
    {

        if (requestedPackageType == "No-rush" && player.points >= 10)
        {
            player.points -= 10;
        } else if (requestedPackageType == "Standard" && player.points >= 15)
        {
            player.points -= 15;
        }
        else if (requestedPackageType == "2-day" && player.points >= 20)
        {
            player.points -= 20;
        }
        else if (requestedPackageType == "Same day" && player.points >= 30)
        {
            player.points -= 30;
        }
        player.UpdatePointsUI();
        requestedPackageType = null;
        player.MakePointsRed();
    }

    public void PlayDoorbell()
    {
        doorbell.Play();
    }
}
   