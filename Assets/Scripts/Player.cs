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
    public GameObject minimapCam;
    
    // Start is called before the first frame update
    void Start()
    {
        currPackageIndex = 0;
        spawnLocation = GameObject.FindGameObjectWithTag("Package spawn");
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
        minimapCam.transform.position = new Vector3(transform.position.x, minimapCam.transform.position.y, transform.position.z);
        minimapCam.transform.rotation = Quaternion.Euler(minimapCam.transform.eulerAngles.x, gameObject.transform.eulerAngles.y, minimapCam.transform.eulerAngles.z);
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Small prop")
        {
            if (points >= 5)
            {
                points -= 5;
                MakePointsRed();
                UpdatePointsUI();
            }
            
        } else if (collision.transform.tag == "Medium prop")
        {
            if (points >= 10)
            {
                points -= 10;
                MakePointsRed();
                UpdatePointsUI();
            }
        } else if (collision.transform.tag == "Large prop")
        {
            if (points >= 15)
            {
                points -= 15;
                MakePointsRed();
                UpdatePointsUI();
            }
        }
    }

    public IEnumerator MakePointsRed()
    {
        pointsUI.color = Color.red;
        yield return new WaitForSeconds(1);
        pointsUI.color = Color.black;
    }

}
