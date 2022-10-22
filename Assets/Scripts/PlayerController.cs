using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject ball;
    [SerializeField] GameObject club;
    [SerializeField] TextMeshProUGUI currentClubText;
    [SerializeField] TextMeshProUGUI stanceAngleText;
    [SerializeField] GameObject stanceImage;
    [SerializeField] float power;

    private Club clubScript;
    private Ball ballScript;

    private float originalStanceImageAngle;
    private float stanceAngle;
    private Vector2 ballStrikeLocation;
    private bool isHit;
    
    void Start()
    {
        ballScript = ball.GetComponent<Ball>();
        SelectClub(club);
        originalStanceImageAngle = stanceImage.transform.eulerAngles.z;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StrikeBall();
        }
    }

    public void SelectClub(GameObject club)
    {
        this.club = club;
        clubScript = this.club.GetComponent<Club>();

        currentClubText.text = $"Current Club:\n{clubScript.nickname}";
    }

    public void SelectStanceAngle(float angleChange)
    {
        stanceAngleText.text = $"Stance Angle:\n{angleChange}";
        stanceAngle = angleChange;
        SetStanceImageRotation();
    }

    void SetStanceImageRotation()
    {
        Vector3 rotation = stanceImage.transform.eulerAngles;
        rotation.z = originalStanceImageAngle + stanceAngle;

        stanceImage.transform.eulerAngles = rotation;
    }

    public void SetHorizontalStrikePosition(float newPosition)
    {
        ballStrikeLocation.x = newPosition;
    }

    public void SetVerticalStrikePosition(float newPosition)
    {
        ballStrikeLocation.y = newPosition;
    }

    public void StrikeBall()
    {
        if (!isHit)
        {
            ballScript.AimAngle = stanceAngle;
            ballScript.LoftAngle = clubScript.loftAngle;
            ballScript.StrikeLocation = ballStrikeLocation;
            ballScript.Speed = CalculateBallSpeed();

            ballScript.StrikeBall();
            isHit = true;
        }
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    float CalculateBallSpeed()
    {
        return Mathf.Lerp(
            clubScript.minBallSpeed,
            clubScript.maxBallSpeed,
            power
        );
    }
}
