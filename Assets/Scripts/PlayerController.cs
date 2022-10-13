using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject club;
    [SerializeField] GameObject ball;
    [SerializeField] float stanceAngle;
    [SerializeField] float power;
    [SerializeField] Vector2 ballStrikeLocation;

    private Club clubScript;
    private Ball ballScript;
    
    void Start()
    {
        clubScript = club.GetComponent<Club>();
        ballScript = ball.GetComponent<Ball>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            StrikeBall();
        }
    }

    void StrikeBall()
    {
        ballScript.SetAim(stanceAngle);
        ballScript.SetLoft(clubScript.loft);
        ballScript.SetStrikeLocation(ballStrikeLocation);
        ballScript.SetForce(CalculateSwingForce());

        ballScript.StrikeBall();
    }

    float CalculateSwingForce()
    {
        return Mathf.Lerp(
            clubScript.minDistance,
            clubScript.maxDistance,
            power
        );
    }
}
