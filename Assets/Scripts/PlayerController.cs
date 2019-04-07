using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody car;
    public WheelCollider frontDriverW, frontPassengerW;
    public WheelCollider rearDriverW, rearPassengerW;
    public Transform frontDriverT, frontPassengerT;
    public Transform rearDriverT, rearPassengerT;

    public float maxSteerAngle = 30;
    public float motorForce = 50;

    private float m_horizontalInput;
    private float m_verticalInput;
    private float m_steeringAngle;
    public int Brakes = 0;

    private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;

        _collider.GetWorldPose(out _pos, out _quat);

        _transform.position = _pos;
        _transform.rotation = _quat;
    }

    public void GetInput()
    {
        Brakes = 0;
        m_verticalInput = 0;
        //Debug.Log("Getting Input");
        m_horizontalInput = Input.GetAxis("Horizontal");
        
        if (Input.GetKey("joystick 1 button 4")) {
                m_verticalInput = -10;
        }
        //Brake
        else if (Input.GetAxis("Mouse ScrollWheel") != 0 && Input.GetAxis("Mouse ScrollWheel") != 0.1f)
        {
            Debug.Log("Brakes Applied!");
            Brakes = 3000000;
        }
        else if (Input.GetAxis("Vertical") != 0 && Input.GetAxis("Vertical") != -1 && m_verticalInput < 10.19f)
        {
            m_verticalInput = 10;
            //m_verticalInput = Input.GetAxisRaw("Vertical")+10;
        }        
    }

    private void Steer()
    {
        //Debug.Log("Steering");
        m_steeringAngle = maxSteerAngle * m_horizontalInput;
        frontDriverW.steerAngle = m_steeringAngle;
        frontPassengerW.steerAngle = m_steeringAngle;
    }

    private void Accelerate()
    {
        if (Brakes == 0)
        {
            frontDriverW.motorTorque = m_verticalInput * motorForce;
            frontPassengerW.motorTorque = m_verticalInput * motorForce;
        }
        else
        {
            Debug.Log("Braking!");
            frontDriverW.motorTorque = 0;
            frontPassengerW.motorTorque = 0;
        }
    }

    private void Braking()
    {
        frontDriverW.brakeTorque = Brakes;
        rearDriverW.brakeTorque = Brakes;
        frontPassengerW.brakeTorque = Brakes;
        rearPassengerW.brakeTorque = Brakes;
    }

    private void UpdateWheelPoses()
    {
        //Debug.Log("Update Wheel Poses");
        UpdateWheelPose(frontDriverW, frontDriverT);
        UpdateWheelPose(frontPassengerW, frontPassengerT);
        UpdateWheelPose(rearDriverW, rearDriverT);
        UpdateWheelPose(rearPassengerW, rearPassengerT);
    }

    private void DetectInput()
    {
        for (int i = 0; i < 20; i++)
        {
            if (Input.GetKeyDown("joystick 1 button " + i))
            {
                print("joystick 1 button " + i);
            }
        }

    }

    private void FixedUpdate()
    {        
        GetInput();
        Steer();
        Accelerate();
        Braking();
        UpdateWheelPoses();
    }
}
/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int movementSpeed;
    public int rotateSpeed;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.down, Input.GetAxis("Horizontal") * -1 * rotateSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, 0, Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, 0, -0.07f);
        }
    }
}*/