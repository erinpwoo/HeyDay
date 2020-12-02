using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Package : MonoBehaviour
{
    public int pointValue;
    public GameObject destination;
    public Player player;
    public float timeLimit;
    public Text pointsText;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        pointsText = GameObject.FindGameObjectWithTag("Points UI").GetComponent<Text>();
        destination = GameObject.FindGameObjectWithTag("House");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == destination)
        {
            player.points += pointValue;
            pointsText.text = "Points: " + player.points;
        }
    }
}
