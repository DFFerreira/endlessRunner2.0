using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] Obstacle[] obstacle;

    void Start()
    {
        Instantiate(obstacle[Random.Range(0, obstacle.Length)], transform);
    }

}
