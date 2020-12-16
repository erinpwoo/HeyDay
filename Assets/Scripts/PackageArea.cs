using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageArea : MonoBehaviour
{

    public Building parent;

    // Start is called before the first frame update
    void Start()
    {
        parent = transform.GetComponentInParent<Building>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (parent.requestedPackageType == null)
            return;

        if ((other.gameObject.CompareTag("No-rush") && (parent.requestedPackageType == "No rush")) ||
            (other.gameObject.CompareTag("Standard") && (parent.requestedPackageType == "Standard")))
        {
            parent.CheckPackageCollision(other.gameObject.tag);
        }
    }
}
