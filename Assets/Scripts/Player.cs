using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int points;
    public int currPackageIndex;
    private Package currPackage;
    public Package[] availablePackages;
    public GameObject spawnLocation;
    //public Image currPackageUI;
    //public RawImage[] packageUIs;
    
    // Start is called before the first frame update
    void Start()
    {
        currPackageIndex = 0;
        spawnLocation = GameObject.FindGameObjectWithTag("Package spawn");
        currPackage = availablePackages[currPackageIndex];
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

}
