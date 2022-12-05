using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerAnimConstans
{
    public const string jumpMultiplier = "jumpMultiplier";
    public const string isJumping = "isJumping";
    public const string start = "start";
}

[RequireComponent(typeof(PlayerController))]
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
        animator.SetBool(PlayerAnimConstans.isJumping , player.IsJumping);
    }

}
