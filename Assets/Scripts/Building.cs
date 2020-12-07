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
    public Material lightsOnMaterial;
    public Material lightsOffMaterial;
    public List<int> windowIndices;
    public AudioSource doorbell;
    public GameObject arrowPrefab;
    private GameObject arrow;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        timerPosition = transform.parent.Find("Timer position");
        isTimerRunning = false;
        for (int i = 0; i < GetComponent<MeshRenderer>().materials.Length; i++)
        {
            if (GetComponent<MeshRenderer>().materials[i].name == (lightsOffMaterial.name + " (Instance)"))
            {
                windowIndices.Add(i);
            }
        }
        doorbell = GetComponent<AudioSource>();
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (requestedPackageType == null)
        {
            return;
        }
        if (collision.gameObject.CompareTag("No-rush"))
        {
            if (requestedPackageType == "No rush")
            {
                player.points += 10;
                timer.GetComponent<Bar>().CancelBarTimer();
                Destroy(timer);
                isTimerRunning = false;
                requestedPackageType = null;
                PlayDoorbell();
                Destroy(arrow);
            }
        }
        else if (collision.gameObject.CompareTag("Standard"))
        {
            if (requestedPackageType == "Standard")
            {
                player.points += 15;
                timer.GetComponent<Bar>().CancelBarTimer();
                Destroy(timer);
                isTimerRunning = false;
                requestedPackageType = null;
                PlayDoorbell();
                Destroy(arrow);
            }
        }
        player.UpdatePointsUI();
    }

    public void SpawnOrder(string packageType)
    {
        isTimerRunning = true;
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
        arrow = Instantiate(arrowPrefab, timerPosition.position, arrowPrefab.transform.rotation);
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

    public void PlayDoorbell()
    {
        doorbell.Play();
    }
}
   