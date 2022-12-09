using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    PlayerController playerController;
    PlayerAnimController playerAnimController;
    [SerializeField] GameMode gameMode;
    [SerializeField] Collider regularCollider;
    [SerializeField] Collider rollCollider;

    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerAnimController = GetComponent<PlayerAnimController>();
    }

    void Update()
    {
        if(playerController.IsRolling)
        {
            regularCollider.enabled = false;
        }
        else
        {
            regularCollider.enabled = true;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        Obstacle obstacle = other.GetComponent<Obstacle>();
        if(obstacle != null)
        {
            playerAnimController.DeathAnim();
            playerController.Death();
            gameMode.OnGameOver();
        }
    }
}
