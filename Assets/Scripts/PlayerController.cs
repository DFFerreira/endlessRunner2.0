using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float laneDistance;

    [Header("Jump")]
    [SerializeField] private float jumpDistanceZ;
    [SerializeField] private float jumpHighY;

    float jumpStartZ;
    public bool IsJumping { get; private set; }

    Vector3 startPosition;
    float targetPositionX;
    private float LaneRight => startPosition.x + laneDistance;
    private float LaneLeft => startPosition.x - laneDistance;

    void Awake()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        Vector3 playerPosition = transform.position;

        ProcessInput();
        
        playerPosition.y = ProcessJump();
        playerPosition.x = ProcessLaneMove();
        playerPosition.z = ProcessForwardMove();

        transform.position = playerPosition;
    }

    void ProcessInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            targetPositionX -= laneDistance;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            targetPositionX += laneDistance;
        }
        if(Input.GetKeyDown(KeyCode.W) && !IsJumping)
        {
            IsJumping = true;
            jumpStartZ = transform.position.z;
        }

        targetPositionX = Mathf.Clamp(targetPositionX, LaneLeft, LaneRight);
    }

    float ProcessLaneMove()
    {
        return Mathf.Lerp(transform.position.x, targetPositionX, horizontalSpeed * Time.deltaTime);
    }

    float ProcessForwardMove()
    {
        return transform.position.z + forwardSpeed * Time.deltaTime;
    }

    float ProcessJump()
    {
        float deltaY = 0;
        if(IsJumping)
        {
            float jumpCurrent = transform.position.z - jumpStartZ;
            float jumpPercent = jumpCurrent / jumpDistanceZ;
            if(jumpPercent >= 1)
            {
                IsJumping = false;
            }
            else
            {
                deltaY = Mathf.Sin(Mathf.PI * jumpPercent) * jumpHighY;
            }
        }

        return startPosition.y + deltaY;        
    }
}
