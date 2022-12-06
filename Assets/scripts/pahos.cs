using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using bikeSetting;

public class pahos: MonoBehaviour
{
    private Quaternion deltaRotation1, deltaRotation2;
    public float accel = 0.0f;
    private Rigidbody bikeRigidBody;
    public bool activeCtrl = false;
    public float speed = 0.0f;
    public float steer = 0;

    private bool Backward;
    [HideInInspector]
    public bool crash;
    private float flipRotate = 0.0f;

    private float Wheelie;
    public BikeSetting bikeSetting;

    [System.Serializable]
    public class BikeSetting 
{
    public Vector3 shiftCentre = new Vector3(0.0f, -0.6f, 0.0f);
    public Transform mainBody;
    public Transform bikeSteer;

    public float maxWheelie = 42.0f;
    
    public float bikePower          = 120;
    public bool autoGear            = true;
    public float[] gears            = {-10f, 9f, 6f, 4.5f, 3f, 2.5f };
    public float LimitBackwardSpeed = 60.0f;
    public float LimitForwardSpeed  = 220f;

}
    void Awake(){
        bikeRigidBody = transform.GetComponent<Rigidbody>();
    }

    void Update(){
        if(activeCtrl){
            if(!crash){
                flipRotate = (transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270) ? 180.0f : 0.0f;
                Wheelie = Mathf.Clamp(Wheelie, 0, bikeSetting.maxWheelie);
            }
            deltaRotation1 = Quaternion.Euler(-Wheelie, 0, flipRotate - transform.localEulerAngles.z);
            deltaRotation2 = Quaternion.Euler(0,0, flipRotate - transform.localEulerAngles.z);

            bikeRigidBody.MoveRotation(bikeRigidBody.rotation * deltaRotation2);
            bikeSetting.mainBody.localRotation = deltaRotation1;

        }
    }


    void FixedUpdate(){
        speed = bikeRigidBody.velocity.magnitude * 2.7f;

        if(crash){
            bikeRigidBody.constraints = RigidbodyConstraints.None;
            bikeRigidBody.centerOfMass = Vector3.zero;
        }else{
            bikeRigidBody.constraints = RigidbodyConstraints.FreezeRotationZ;
            bikeRigidBody.centerOfMass = bikeSetting.shiftCentre;
            
        }

        if(activeCtrl){
            accel = 0.0f;

            if(!crash){
                steer = Mathf.MoveTowards(steer, Input.GetAxis("Horizontal"), 0.1f);
                accel = Input.GetAxis("Vertical");
            }    else{
                steer = 0;
            }
        }else{
            accel = 0.0f;
            steer = 0.0f;
        }

        if(speed < 1.0f){
            Backward = true;
        }else{
            Backward = false;
        }

    }

}
