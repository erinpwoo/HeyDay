using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    public string requestedPackageType;
    public Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

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

    public void SpawnOrder()
    {

    }
}
   