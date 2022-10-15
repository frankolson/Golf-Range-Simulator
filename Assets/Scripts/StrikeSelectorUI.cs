using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeSelectorUI : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject targetImage;

    private float positionChangeRange = 45f;
    private PlayerController playerController;
    
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
    }

    public void SelectStrikeHorizontalPosition(float newPosition)
    {
        UpdateTargetImagePosition(newXPosition: newPosition);
        playerController.SetHorizontalStrikePosition(newPosition);
    }

    public void SelectStrikeVerticalPosition(float newPosition)
    {
        UpdateTargetImagePosition(newYPosition: newPosition);
        playerController.SetVerticalStrikePosition(newPosition);
    }

    void UpdateTargetImagePosition(float? newXPosition = null, float? newYPosition = null)
    {
        newXPosition = (newXPosition != null)
            ? CalculateNewUIPosition((float) newXPosition)
            : targetImage.transform.localPosition.x;
        newYPosition = (newYPosition != null)
            ? CalculateNewUIPosition((float) newYPosition)
            : targetImage.transform.localPosition.y;

        targetImage.transform.localPosition = new Vector3(
            (float) newXPosition,
            (float) newYPosition,
            targetImage.transform.localPosition.z
        );
    }

    float CalculateNewUIPosition(float newPosition)
    {
        float newUIPosition = Mathf.Sign(newPosition) * Mathf.Lerp(0, positionChangeRange, Mathf.Abs(newPosition));
        return newUIPosition;
    }
}
