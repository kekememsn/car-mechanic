using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CarController : MonoBehaviour
{
    private Rigidbody rb;
    public float topspeed;
    public float speed;




    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle; //arabanin yon degistirme acisi icin degisken
    private float currentbreakForce;
    private bool isBreaking;
    int vites = 1;
    int vitese_gore_hp;


    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteerAngle;


    [SerializeField] private WheelCollider FrontLeftWheelCollider;
    [SerializeField] private WheelCollider FronRightWheelCollider;
    [SerializeField] private WheelCollider RearleftWheelCollider;
    [SerializeField] private WheelCollider RearrightWheelCollider;

    [SerializeField] private Transform FrontLeftWheelTransform;
    [SerializeField] private Transform FrontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearrightWheelTransform;

    bool _anahtar = false;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

   

    // yorm
    // test


    private void Update()
    {
        speed = CarSpeed();
        GetInput();
        HandleSteering();
        HandleMotor();
        UptadeWheels();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBreaking = Input.GetKey(KeyCode.Space);
        if (Input.GetKeyDown(KeyCode.R))
        {
            resetCarRotation();
        }
    }

    private void resetCarRotation()
    {
        Quaternion rotation = transform.rotation;
        rotation.z = 0f;
        rotation.x = 0f;
        transform.rotation = rotation;
    }


    private void HandleMotor()
    {

        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("motor");
            _anahtar = !_anahtar;
        }



        if (_anahtar)
        {
            
            FrontLeftWheelCollider.motorTorque = verticalInput * motorForce * sanziman(vites) ;
            FronRightWheelCollider.motorTorque = verticalInput * motorForce * vitese_gore_hp;
            RearleftWheelCollider.motorTorque = verticalInput * motorForce * vitese_gore_hp;
            RearrightWheelCollider.motorTorque = verticalInput * motorForce * vitese_gore_hp;
            currentbreakForce = isBreaking ? breakForce : 0f;
            ApplyBreaking();

        }




    }

    private void ApplyBreaking()
    {
        FronRightWheelCollider.brakeTorque = currentbreakForce * 10000000000;
        FrontLeftWheelCollider.brakeTorque = currentbreakForce * 10000000000;
        RearleftWheelCollider.brakeTorque = currentbreakForce * 10000000000;
        RearrightWheelCollider.brakeTorque = currentbreakForce * 10000000000;

        vites_degisikligi();
    }


    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        FrontLeftWheelCollider.steerAngle = currentSteerAngle;
        FronRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UptadeWheels()
    {
        UpdateSingleWheel(FrontLeftWheelCollider, FrontLeftWheelTransform);
        UpdateSingleWheel(FronRightWheelCollider, FrontRightWheelTransform);
        UpdateSingleWheel(RearrightWheelCollider, rearrightWheelTransform);
        UpdateSingleWheel(RearleftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;

    }

    public int sanziman(int _vites)
    {
        if (vites == 1)
        {
            vitese_gore_hp = 100;
            topspeed = 20;
            Debug.Log("vites 1");
        }
        else if (vites == 2)
        {
            vitese_gore_hp = 1000;
            topspeed = 50;
            Debug.Log("vites 2");
        }
        else if (vites == 3)
        {
            vitese_gore_hp = 10000;
            topspeed = 80;
            Debug.Log("vites 3");
        }
        else if (vites == 4)
        {
            vitese_gore_hp = 100000;
            topspeed = 120;
            Debug.Log("vites 4");
        }
        else if (vites == 5)
        {
            vitese_gore_hp = 1000000;
            topspeed = 240;
            Debug.Log("vites 5");
        }
        else if (vites == 6)
        {
            vitese_gore_hp = 10000000;
            topspeed = 360;
            Debug.Log("vites 6");
        }
        return vitese_gore_hp;
       

    }
    public void vites_degisikligi()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("eski : " + vites);
            if (vites < 1)
            {
                vites = 1;
            }
            else if (vites >= 6)
            {
                vites = 6;

            }
            else
            {
                vites++;
            }
            Debug.Log("yeni vites: " + vites);

        }
        else if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Debug.Log("eski : " + vites);
            if (vites <= 1)
            {
                vites = 1;

            }
            else if (vites > 6)
            {
                vites = 6;

            }
            else
            {
                vites--;
            }
            Debug.Log("yeni vites: " + vites);
        }
    }
    public float CarSpeed()
    {
        float speed = rb.velocity.magnitude;
        speed *= 3.6f;
        if (speed > topspeed)
        {
            rb.velocity = (topspeed / 3.6f) * rb.velocity.normalized;

        }
        return speed;
    }
}

