using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class DragAndShoot : MonoBehaviour
{
    private Vector3 mousePressDownPos;
    private Vector3 mouseReleasePos;
    public GameObject launchPos;
    private Rigidbody rb;
    public Player player;
    private bool isShoot;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isShoot = false;
        launchPos = GameObject.FindGameObjectWithTag("Package spawn");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
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
    }

    private void OnMouseUp()
    {
        mouseReleasePos = Input.mousePosition;
        Shoot(mouseReleasePos - mousePressDownPos);
        if (player.currPackage == gameObject)
        {
            player.currPackage = null;
        }
        
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