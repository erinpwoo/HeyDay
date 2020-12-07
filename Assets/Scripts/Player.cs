using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Main game and player logic
public class Player : MonoBehaviour
{
    public int points;
    public int currPackageIndex;
    public GameObject currPackage;
    public GameObject[] availablePackages;
    public GameObject spawnLocation;
    public Transform[] buildings;
    public Text pointsUI;
    public bool hasStarted;
    
    // Start is called before the first frame update
    void Start()
    {
        currPackageIndex = 0;
        spawnLocation = GameObject.FindGameObjectWithTag("Package spawn");
        currPackage = availablePackages[currPackageIndex];
        pointsUI = GameObject.FindGameObjectWithTag("Points UI").GetComponent<Text>();
        hasStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStarted)
        {
            if (Input.GetMouseButtonDown(1))
            {
                SwitchPackage();
            }
        }
    }

    public void UpdatePointsUI()
    {
        pointsUI.text = "Points: " + points;
    }

    public void SwitchPackage()
    {
        if (currPackageIndex == availablePackages.Length - 1)
        {
            currPackageIndex = 0;
        }
        else
        {
            currPackageIndex++;
        }
        if (currPackage != null)
        {
            Destroy(currPackage);
        }

        currPackage = Instantiate(availablePackages[currPackageIndex], spawnLocation.transform.position, spawnLocation.transform.rotation);
 
    }

}
