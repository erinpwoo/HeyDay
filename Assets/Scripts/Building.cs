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

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        timerPosition = transform.parent.Find("Timer position");
        SpawnOrder("No rush");

    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("No-rush"))
        {
            if (requestedPackageType == "No rush")
            {
                player.points += 10;
                timer.GetComponent<Bar>().CancelBarTimer();
                Destroy(timer);
            }
        }
        else if (collision.gameObject.CompareTag("Standard"))
        {
            if (requestedPackageType == "Standard")
            {
                player.points += 15;
                timer.GetComponent<Bar>().CancelBarTimer();
            }
        }
        player.UpdatePointsUI();
    }

    public void SpawnOrder(string packageType)
    {
        timer = Instantiate(timerPrefab, timerPosition);
        if (packageType == "No rush")
        {
            timer.GetComponent<Bar>().time = 60;
        } else if (packageType == "Standard")
        {
            timer.GetComponent<Bar>().time = 50;
        }
        requestedPackageType = packageType;
        timer.GetComponent<Bar>().AnimateBar(timer, this);
    }

    public void DecrementPoints()
    {
        if (requestedPackageType == "No rush")
        {
            player.points -= 10;
        } else if (requestedPackageType == "Standard")
        {
            player.points -= 15;
        }
        player.UpdatePointsUI();
        requestedPackageType = null;
    }
}
   