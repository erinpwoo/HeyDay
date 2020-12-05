using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Main game and player logic
public class Player : MonoBehaviour
{
    public int points;
    public int currPackageIndex;
    private GameObject currPackage;
    public GameObject[] availablePackages;
    public GameObject spawnLocation;
    public Transform[] buildings;
    public Text pointsUI;
    
    // Start is called before the first frame update
    void Start()
    {
        currPackageIndex = 0;
        spawnLocation = GameObject.FindGameObjectWithTag("Package spawn");
        currPackage = availablePackages[currPackageIndex];
        buildings = GameObject.FindGameObjectWithTag("Buildings").GetComponentsInChildren<Transform>();
        pointsUI = GameObject.FindGameObjectWithTag("Points UI").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currPackageIndex == 0)
            {
                currPackageIndex = availablePackages.Length - 1;
            } else
            {
                currPackageIndex--;
            }
            currPackage = Instantiate(availablePackages[currPackageIndex], spawnLocation.transform.position, spawnLocation.transform.rotation);
            
        } else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currPackageIndex == availablePackages.Length - 1)
            {
                currPackageIndex = 0;
            }
            else
            {
                currPackageIndex++;
            }
            currPackage = Instantiate(availablePackages[currPackageIndex], spawnLocation.transform.position, spawnLocation.transform.rotation);
        }
    }

    public void UpdatePointsUI()
    {
        pointsUI.text = "Points: " + points;
    }

}
