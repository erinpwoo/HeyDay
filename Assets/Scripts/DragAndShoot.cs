using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class DragAndShoot : MonoBehaviour
{
    private Vector3 mousePressDownPos;
    private Vector3 mouseReleasePos;
    public GameObject launchPos;
    private Rigidbody rb;

    private bool isShoot;
    private bool isMouseDown;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isShoot = false;
        launchPos = GameObject.FindGameObjectWithTag("Package spawn");
        isMouseDown = false;
    }

    private void Update()
    {
        if (!isShoot)
        {
            transform.position = launchPos.transform.position;
            transform.rotation = launchPos.transform.rotation;
        }
        
    }

    private void OnMouseDown()
    {
        mousePressDownPos = Input.mousePosition;
        isMouseDown = true;
    }

    private void OnMouseUp()
    {
        mouseReleasePos = Input.mousePosition;
        isMouseDown = false;
        Shoot(mouseReleasePos - mousePressDownPos);
    }

    private float forceMultiplier = 10;
    void Shoot(Vector3 Force)
    {
        if (isShoot)
            return;
        isShoot = true;
        rb.velocity = (mouseReleasePos-mousePressDownPos).magnitude * .1f * launchPos.transform.forward;
    }

}