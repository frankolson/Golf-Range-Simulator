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
    public float Distance { get; set; }
    
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
        ApplyAim();
        ApplyLoft();
        ApplyStrikeForce();
        ApplySpin();
        LogSwing();
    }

    void ApplyMagnusForce()
    {
        Vector3 magnusForce = Vector3.Cross(
            golfBallRigidbody.angularVelocity,
            golfBallRigidbody.velocity
        );
        
        golfBallRigidbody.AddForce(magnusForce * magnusConstant);
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

    void ApplyStrikeForce()
    {
        golfBallRigidbody.AddForce(transform.forward * CalculateForce(), ForceMode.Impulse);
    }

    void ApplySpin()
    {
        Vector3 spin = CalculateSpin();
        golfBallRigidbody.AddRelativeTorque(spin, ForceMode.Impulse);
    }

    float CalculateForce()
    {
        return Distance;
    }

    Vector3 CalculateSpin()
    {
        float sidespin = StrikeLocation.x; // x range: -1 to 1
        float backspin = StrikeLocation.y; // y range: -1 to 1

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

    void LogSwing()
    {
        string aim = $"Aim: {transform.eulerAngles.y}";
        string loft = $"Loft: {transform.eulerAngles.x}";
        string force = $"Force: {CalculateForce()}";
        string spin = $"Spin: {CalculateSpin()}";

        Debug.Log($"{aim}\n{loft}\n{force}\n{spin}");
    }
}
