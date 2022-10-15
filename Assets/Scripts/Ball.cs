using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
	[SerializeField] float magnusConstant = 0.03f;

    private Rigidbody golfBallRigidbody;
    private Vector3 spin;
    private float force;
    
    void Start()
    {
        golfBallRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        ApplyMagnusForce();
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

    public void SetLoft(float loftAngle)
    {
        Vector3 rotation = transform.eulerAngles;
        rotation.x = -loftAngle;
        transform.eulerAngles = rotation;
    }

    public void SetAim(float aimAngle)
    {
        Vector3 rotation = transform.eulerAngles;
        rotation.y = -aimAngle;
        transform.eulerAngles = rotation;
    }

    public void SetForce(float force)
    {
        this.force = force;
    }

    public void SetStrikeLocation(Vector2 location)
    {
        // x range: -1 to 1
        float sidespin = location.x;
        // y range: -1 to 1
        float backspin = location.y;

        spin = new Vector3(backspin, sidespin, 0);
    }

    public void StrikeBall()
    {
        Debug.Log($"Aim: {transform.eulerAngles.y}, Loft: {transform.eulerAngles.x}, Force: {force}, Spin: {spin}");

        golfBallRigidbody.AddForce(transform.forward * this.force, ForceMode.Impulse);
        golfBallRigidbody.AddRelativeTorque(spin, ForceMode.Impulse);
    }
}
