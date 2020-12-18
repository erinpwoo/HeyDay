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

    private void OnTriggerEnter(Collider other)
    {
        if (parent.requestedPackageType == null) return;
        if ((other.gameObject.CompareTag("No-rush") && (parent.requestedPackageType == "No-rush")) ||
            (other.gameObject.CompareTag("Standard") && (parent.requestedPackageType == "Standard")) ||
            (other.gameObject.CompareTag("2-day") && (parent.requestedPackageType == "2-day")))
        {
            if (other.gameObject.GetComponent<DragAndShoot>().hasDelivered == true)
            {
                return;
            }
            other.gameObject.GetComponent<DragAndShoot>().hasDelivered = true;
            parent.CheckPackageCollision(other.gameObject.tag);
        }
    }
        
}
