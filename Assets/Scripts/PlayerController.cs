using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject ball;
    [SerializeField] GameObject club;
    [SerializeField] TextMeshProUGUI currentClubText;
    [SerializeField] TextMeshProUGUI stanceAngleText;
    [SerializeField] GameObject stanceImage;
    [SerializeField] float stanceAngle;
    [SerializeField] float power;
    [SerializeField] Vector2 ballStrikeLocation;

    private Club clubScript;
    private Ball ballScript;

    private float originalStanceImageAngle;
    
    void Start()
    {
        ballScript = ball.GetComponent<Ball>();
        SelectClub(club);
        originalStanceImageAngle = stanceImage.transform.eulerAngles.z;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
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
