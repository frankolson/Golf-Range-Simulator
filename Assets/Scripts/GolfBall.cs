using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBall : MonoBehaviour
{
    private Rigidbody golfBallRigidbody;
    private float force;
    private Vector3 spin = Vector3.zero;

	public float magnusConstant = 0.03f;

    // Start is called before the first frame update
    void Start()
    {
        golfBallRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (magnusConstant > 0)
        {
            Vector3 magnusForce = Vector3.Cross(golfBallRigidbody.angularVelocity, golfBallRigidbody.velocity);
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

    public void SetSpin(float backspin, float sidespin)
    {
        // x range: -1 to 1
        // y range: -1 to 1
        spin = new Vector3(backspin, sidespin, 0);
    }

    public void StrikeBall()
    {
        Debug.Log($"Aim: {transform.eulerAngles.y}, Loft: {transform.eulerAngles.x}, Force: {force}, Spin: {spin}");

        // Hit the golf ball
        golfBallRigidbody.AddForce(transform.forward * this.force, ForceMode.Impulse);
        
        // Add spin
        golfBallRigidbody.AddRelativeTorque(spin, ForceMode.Impulse);
    }
}
