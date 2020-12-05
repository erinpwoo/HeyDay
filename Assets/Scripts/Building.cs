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

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        timerPosition = transform.Find("Timer position");
        SpawnOrder("No rush");

    }

    private void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("No rush"))
        {
            if (requestedPackageType == "No rush")
            {
                player.points += 10;
            }
        } else if (collision.gameObject.CompareTag("Standard"))
        {
            if (requestedPackageType == "Standard")
            {
                player.points += 15;
            }
        }
    }

    public void SpawnOrder(string packageType)
    {
        GameObject timer = Instantiate(timerPrefab, timerPosition);
        if (packageType == "No rush")
        {
            timer.GetComponent<Bar>().time = 60;
        } else if (packageType == "Standard")
        {
            timer.GetComponent<Bar>().time = 50;
        }
        requestedPackageType = packageType;
        timer.GetComponent<Bar>().AnimateBar();
    }
}
   