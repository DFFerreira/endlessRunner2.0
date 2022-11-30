using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float forwardSpeed;

    [SerializeField] private float laneDistance;

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
}
