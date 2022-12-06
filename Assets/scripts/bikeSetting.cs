using System;
using UnityEngine;

namespace bikeSetting{
public class bikeSetting 
{
    public Transform mainBody;
    public Transform bikeSteer;

    public float maxWheelie = 42.0f;
    
    public float bikePower = 120;
    public bool autoGear = true;
    public float[] gears = {-10f, 9f, 6f, 4.5f, 3f, 2.5f };
    public float LimitBackwardSpeed = 60.0f;
    public float LimitForwardSpeed = 220f;
}

}
