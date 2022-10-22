using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
	const float magnusConstant = 0.03f;
    [SerializeField] TextMeshProUGUI distanceText;
    [SerializeField] TextMeshProUGUI heightText;

    private Rigidbody golfBallRigidbody;
    private int topDistance;
    private int topHeight;

    public Vector2 StrikeLocation { get; set; }
    public float AimAngle { get; set; }
    public float LoftAngle { get; set; }
    public float Force { get; set; }
    
    void Start()
    {
        golfBallRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        ApplyMagnusForce();
        UpdateDistanceText();
        UpdateHeightText();
    }

    public void StrikeBall()
    {
        Vector3 spin = CalculateSpin();
        
        ApplyAim();
        ApplyLoft();
        golfBallRigidbody.AddForce(transform.forward * Force, ForceMode.Impulse);
        golfBallRigidbody.AddRelativeTorque(spin, ForceMode.Impulse);

        Debug.Log($"Aim: {transform.eulerAngles.y}, Loft: {transform.eulerAngles.x}, Force: {Force}, Spin: {spin}");
    }

    void ApplyMagnusForce()
    {
        if (magnusConstant > 0)
        {
            Vector3 magnusForce = Vector3.Cross(
                golfBallRigidbody.angularVelocity,
                golfBallRigidbody.velocity
            );
            
            golfBallRigidbody.AddForce(magnusForce * magnusConstant);
        }
    }

    void ApplyLoft()
    {
        Vector3 rotation = transform.eulerAngles;
        rotation.x = -LoftAngle;
        transform.eulerAngles = rotation;
    }

    void ApplyAim()
    {
        Vector3 rotation = transform.eulerAngles;
        rotation.y = -AimAngle;
        transform.eulerAngles = rotation;
    }

    Vector3 CalculateSpin()
    {
        // x range: -1 to 1
        float sidespin = StrikeLocation.x;
        // y range: -1 to 1
        float backspin = StrikeLocation.y;

        return new Vector3(backspin, sidespin, 0);
    }
    
    void UpdateDistanceText()
    {
        if ((int) transform.position.z > topDistance)
        {
            topDistance = (int) transform.position.z;
            distanceText.text = $"Distance: {topDistance} yards";
        }
    }

    void UpdateHeightText()
    {
        if ((int) transform.position.y > topHeight)
        {
            topHeight = (int) transform.position.y;
            heightText.text = $"Height: {topHeight * 3} feet";
        }
    }
}
