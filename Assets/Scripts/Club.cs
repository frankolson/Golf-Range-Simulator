using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Club : MonoBehaviour
{
    [Header("Measured in meters per second (m/s)")]
    public float maxBallSpeed;
    public float minBallSpeed;
    
    [Space]
    public float loftAngle; // degrees
    public string nickname;
}
