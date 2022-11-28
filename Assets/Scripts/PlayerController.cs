using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float forwardSpeed;

    void Update()
    {
        Vector3 targetPosition = transform.position;

        if (Input.GetKey(KeyCode.A))
        {
            targetPosition.x -= horizontalSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            targetPosition.x += horizontalSpeed * Time.deltaTime;
        }

        targetPosition.z += forwardSpeed * Time.deltaTime;
        transform.position = targetPosition;
    }
}
