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
    private bool isMouseDown;
    private Vector3 mOffset;
    private float mZCoord;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isShoot = false;
        launchPos = GameObject.FindGameObjectWithTag("Package spawn");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        isMouseDown = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isMouseDown) {
            MouseDown();
        }
        if (Input.GetKeyUp(KeyCode.Mouse0) && isMouseDown)
        {
            MouseUp();
        }
        if (!isShoot)
        {
            transform.rotation = launchPos.transform.rotation;
            if (!isMouseDown)
            {
                transform.position = launchPos.transform.position;
            }
            else
            {
                transform.position = GetMouseWorldPos() + mOffset;
            }
        }
        
    }

    private void MouseDown()
    {
        mousePressDownPos = Input.mousePosition;
        isMouseDown = true;
        mZCoord = Camera.main.WorldToScreenPoint(launchPos.transform.position).z;
        mOffset = transform.position - GetMouseWorldPos();
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void MouseUp()
    {
        mouseReleasePos = Input.mousePosition;
        Shoot(mouseReleasePos - mousePressDownPos);
        isMouseDown = false;
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
        rb.AddRelativeForce(new Vector3(Force.x, Force.y/2, Force.y) * forceMultiplier);
    }

}