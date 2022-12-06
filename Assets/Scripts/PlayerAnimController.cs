using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAnimController : MonoBehaviour
{
    PlayerController player;
    [SerializeField] Animator animator;

    void Awake()
    {
        player = GetComponent<PlayerController>();
    }
    void Update()
    {
        animator.SetBool(PlayerAnimConstants.isJumping, player.IsJumping);
        animator.SetBool(PlayerAnimConstants.isRolling, player.IsRolling);
    }

    public void DeathAnim()
    {
        animator.SetTrigger(PlayerAnimConstants.deathTrigger);
    }
}
