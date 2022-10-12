using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivingRangeController : MonoBehaviour
{
    public GameObject currentGolfBall;
    public GameObject currentClub;
    
    public float stanceAngle;
    public float power;
    public Vector2 golfBallStrikePosition;

    private GolfBall currentGolfBallScript;
    private Club currentClubScript;

    // Start is called before the first frame update
    void Start()
    {
        currentGolfBallScript = currentGolfBall.GetComponent<GolfBall>();
        currentClubScript = currentClub.GetComponent<Club>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            StrikeGolfBall();
        }
    }

    void StrikeGolfBall()
    {
        // Set the horizontal launch angle from the player's stance
        currentGolfBallScript.SetAim(stanceAngle);

        // Set the vertical launch angle from the club choice
        currentGolfBallScript.SetLoft(currentClubScript.loft);

        // Set the spin from the strike position of the ball
        float backspin = golfBallStrikePosition.y;
        float sidespin = golfBallStrikePosition.x;
        currentGolfBallScript.SetSpin(golfBallStrikePosition.y, golfBallStrikePosition.x);

        // Set the force of the swing
        float force = Mathf.Lerp(currentClubScript.minDistance, currentClubScript.maxDistance, power);
        currentGolfBallScript.SetForce(force);

        currentGolfBallScript.StrikeBall();
        // Aim: 355, Loft: 340, Force: 139.8, Spin: (0.00, 0.00, 0.00)
    }
}
