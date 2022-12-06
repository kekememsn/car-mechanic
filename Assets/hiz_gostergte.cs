using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hiz_gostergte : MonoBehaviour
{
    public CarController cr;
    
    //0 = -194 derce 360 ise -453 derece

    Rigidbody rb;
    private const float MIN_SPEED_ANG = -170;
    private const float MAX_SPEED_ANG = -400.0f;

    private Transform _ibre_transfomr;

    
    public float speed;
    public float topspeed;

    public GameObject ibre;

   

    private void Awake()
    {
        _ibre_transfomr = ibre.transform;

        speed = 0f;
        
    }

    private void FixedUpdate()
    {
        speed = cr.speed;
        topspeed = cr.topspeed;

        if (speed > topspeed) speed = topspeed;

        _ibre_transfomr.eulerAngles = new Vector3(0, 0, GetSpeedRotation());
    }
    private float GetSpeedRotation()
    {
        float toplamDonusAcisi = MIN_SPEED_ANG - MAX_SPEED_ANG;
        float speedNormalized = speed / toplamDonusAcisi;
        return MIN_SPEED_ANG - speedNormalized * toplamDonusAcisi;
    }


}
