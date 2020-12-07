using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckController : MonoBehaviour
{

    private float m_horizontalInput;
    private float m_verticalInput;
    private float m_steeringAngle;

    public WheelCollider frontRightW, frontLeftW, trailer1_leftW, trailer1_rightW, trailer2_leftW, trailer2_rightW, trailer3_leftW, trailer3_rightW, trailer4_leftW, trailer4_rightW, trailer5_leftW, trailer5_rightW;
    public Transform frontRightT, frontLeftT, trailer1_leftT, trailer1_rightT, trailer2_leftT, trailer2_rightT, trailer3_leftT, trailer3_rightT, trailer4_leftT, trailer4_rightT, trailer5_leftT, trailer5_rightT;

    public float maxSteerAngle = 30; // in degrees
    public float motorForce = 1000;

    public Player player;

    public LevelController levelController;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
    }

    public void GetInput()
    {
        m_horizontalInput = Input.GetAxis("Horizontal");
        m_verticalInput = Input.GetAxis("Vertical");
    }

    private void Steer()
    {
        m_steeringAngle = maxSteerAngle * m_horizontalInput;
        frontLeftW.steerAngle = m_steeringAngle;
        frontRightW.steerAngle = m_steeringAngle;
    }

    private void Accelerate()
    {
        frontLeftW.motorTorque = m_verticalInput * motorForce;
        frontRightW.motorTorque = m_verticalInput * motorForce;
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontRightW, frontRightT);
        UpdateWheelPose(frontLeftW, frontLeftT);
        UpdateWheelPose(trailer1_leftW, trailer1_leftT);
        UpdateWheelPose(trailer1_rightW, trailer1_rightT);
        UpdateWheelPose(trailer2_leftW, trailer2_leftT);
        UpdateWheelPose(trailer2_rightW, trailer2_rightT);
        UpdateWheelPose(trailer3_leftW, trailer3_leftT);
        UpdateWheelPose(trailer3_rightW, trailer3_rightT);
        UpdateWheelPose(trailer4_leftW, trailer4_leftT);
        UpdateWheelPose(trailer4_rightW, trailer4_rightT);
        UpdateWheelPose(trailer5_leftW, trailer5_leftT);
        UpdateWheelPose(trailer5_rightW, trailer5_rightT);
    }

    private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;

        _collider.GetWorldPose(out _pos, out _quat);

        _transform.position = _pos;
        _transform.rotation = _quat;
        if (transform.up.y <= 0)
        {
            levelController.GameOver();
        }
    }

    private void FixedUpdate()
    {
        if (player.hasStarted)
        {
            GetInput();
            Steer();
            Accelerate();
            UpdateWheelPoses();
        }
    }
}
