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
    [SerializeField] private float jumpLerpSpeed;

    [Header("Roll")]
    [SerializeField] private float rollDistanceZ;

    float rollStartZ;
    public bool IsRolling { get; private set; }

    float jumpStartZ;
    public bool IsJumping { get; private set; }



    Vector3 startPosition;
    float targetPositionX;
    public float RollDuration => rollDistanceZ / forwardSpeed;
    public float JumpDuration => jumpDistanceZ / forwardSpeed;
    private float LaneRight => startPosition.x + laneDistance;
    private float LaneLeft => startPosition.x - laneDistance;

    void Awake()
    {
        startPosition = transform.position;
        enabled = false;

    }

    void Update()
    {
        Vector3 playerPosition = transform.position;

        ProcessInput();
        ProcessRoll();

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
            StartRoll();
        }
        if (Input.GetKeyDown(KeyCode.S) && !IsRolling)
        {
            StartJump();
        }

        targetPositionX = Mathf.Clamp(targetPositionX, LaneLeft, LaneRight);
    }

    void StartRoll()
    {
        IsRolling = false;
        IsJumping = true;
        jumpStartZ = transform.position.z;
    }
    void StartJump()
    {
        IsJumping = false;
        IsRolling = true;
        rollStartZ = transform.position.z;
    }

    void ProcessRoll()
    {
        if(IsRolling)
        {
            float rollCurrent = transform.position.z - rollStartZ;
            float rollPercent = rollCurrent / rollDistanceZ;
            if(rollPercent >= 1)
            {
                IsRolling = false;                
            }
        }
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

        float targetPositionY = startPosition.y + deltaY;
        return Mathf.Lerp(transform.position.y, targetPositionY, jumpLerpSpeed * Time.deltaTime);
    }

    public void Death()
    {
        forwardSpeed = 0;
        horizontalSpeed = 0;
        IsJumping = false;
        IsRolling = false;
    }
}
